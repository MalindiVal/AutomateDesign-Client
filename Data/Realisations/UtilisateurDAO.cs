using Client.Data;
using ClientData.Interfaces;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientData.Realisations
{
    /// <summary>
    /// Implémentation concrète de <see cref="IUtilisateurDAO"/> permettant la gestion des utilisateurs via l’API distante.
    /// </summary>
    public class UtilisateurDAO : DAO, IUtilisateurDAO
    {
        /// <summary>
        /// Stocke le token JWT après une authentification réussie.
        /// </summary>
        private static string _token;

        /// <summary>
        /// Expose le token en lecture seule pour que d'autres DAO puissent l'utiliser.
        /// </summary>
        public static string Token => _token;

        /// inheritdoc/>
        public async Task<Utilisateur> Login(Utilisateur user)
        {
            Utilisateur res = new Utilisateur();
            HttpResponseMessage reponseHttp = await this.PostAsync("Utilisateur/Login", user);
            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<LoginResponseDTO>(reponse, Options);

                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                {
                    _token = loginResponse.Token;
                    SetToken(_token);          // Injecte le Bearer token sur le HttpClient
                    res = loginResponse.Utilisateur;
                }
            }
            return res;
        }

        /// inheritdoc/>
        public async Task<Utilisateur> Register(Utilisateur user)
        {
            Utilisateur res = new Utilisateur();
            HttpResponseMessage reponseHttp = await this.PostAsync("Utilisateur/Register", user);
            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();

                var registerResponse = JsonSerializer.Deserialize<LoginResponseDTO>(reponse, Options);

                if (registerResponse != null && !string.IsNullOrEmpty(registerResponse.Token))
                {
                    _token = registerResponse.Token;
                    SetToken(_token);
                    res = registerResponse.Utilisateur;
                }
            }
            return res;
        }

        /// <summary>
        /// Déconnecte l'utilisateur : supprime le token stocké et celui du HttpClient.
        /// </summary>
        public void Logout()
        {
            _token = null;
            RemoveToken();
        }

        /// <summary>
        /// Vérifie si un token existe et n'est pas expiré.
        /// </summary>
        /// <summary>
        /// Vérifie si le token existant est encore valide (non expiré).
        /// Utile avant de faire une requête protégée.
        /// </summary>
        /// <returns>true si le token existe et n'est pas expiré.</returns>
        public static bool IsAuthenticated()
        {
            if (string.IsNullOrEmpty(_token))
                return false;

            try
            {
                // Le JWT est composé de 3 parties séparées par '.' : header.payload.signature
                string[] parts = _token.Split('.');
                if (parts.Length != 3)
                    return false;

                // Décode le payload (partie du milieu) depuis Base64Url
                string payload = Encoding.UTF8.GetString(
                    Convert.FromBase64String(PadBase64(parts[1]))
                );

                var claims = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(payload);

                // La claim "exp" contient l'expiration en secondes depuis epoch Unix
                if (claims != null && claims.ContainsKey("exp"))
                {
                    long exp = claims["exp"].GetInt64();
                    DateTimeOffset expirationDate = DateTimeOffset.FromUnixTimeSeconds(exp);
                    return DateTimeOffset.UtcNow < expirationDate;
                }

                // Pas de claim exp → on considère le token valide
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ─── Méthodes privées ─────────────────────────────────────────────


        /// <summary>
        /// Corrige le padding Base64 pour le décodage JWT.
        /// </summary>
        private static string PadBase64(string input)
        {
            return input.Length % 4 switch
            {
                2 => input + "==",
                3 => input + "=",
                _ => input
            };
        }
    }

    /// <summary>
    /// DTO attendu en réponse des endpoints Login et Register.
    /// L'API doit retourner ces deux champs dans le body JSON.
    /// </summary>
    public class LoginResponseDTO
    {
        public Utilisateur Utilisateur { get; set; }
        public string Token { get; set; }
    }
}
