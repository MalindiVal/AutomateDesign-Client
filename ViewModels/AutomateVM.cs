using LogicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class AutomateVM : BaseViewModel
    {
        private Automate metier;

        private readonly ObservableCollection<EtatVM> etats = new();
        private readonly ObservableCollection<TransitionVM> transitions = new();
        private double etatRadius = 75;
        private string title;


        /// <summary>
        /// Liste des etats contenus dans le canvas
        /// </summary>
        public ObservableCollection<EtatVM> Etats => etats;

        /// <summary>
        /// Liste des transitions
        /// </summary>
        public ObservableCollection<TransitionVM> Transitions => transitions;

        public Automate Metier
        {
            get => metier;
            set
            {
                metier = value;
                OnPropertyChanged();
            }
        }

        public double EtatRadius
        {
            get
            {
                return etatRadius;
            }
            set
            {
                etatRadius = value;
                OnPropertyChanged();
            }
        }

        public string Title { 
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public AutomateVM(Automate automate)
        {
            Metier = automate;
            foreach (var etat in automate.Etats)
            {
                Etats.Add(new EtatVM(etat));
            }
            foreach (var transition in automate.Transitions)
            {
                var etatDebutVM = Etats.First(e => e.Metier == transition.EtatDebut);
                var etatFinalVM = Etats.First(e => e.Metier == transition.EtatFinal);
                Transitions.Add(new TransitionVM(etatDebutVM, etatFinalVM, transition));
            }

        }

        #region Gestion Etats

        /// <summary>
        /// Ajoute un état à l’automate.
        /// </summary>
        /// <param name="x">Coordonnée X du centre de l’état</param>
        /// <param name="y">Coordonnée Y du centre de l’état</param>
        public void AjouterEtatNormal(double x, double y)
        {
            HashSet<int> indicesUtilises = new HashSet<int>();
            foreach (EtatVM e in Etats)
            {
                if (e.Nom.StartsWith("Etat "))
                {
                    string nombreTexte = e.Nom.Substring("Etat ".Length);
                    if (int.TryParse(nombreTexte, out int n))
                        indicesUtilises.Add(n);
                }
            }
            int indexLibre = 0;
            while (indicesUtilises.Contains(indexLibre))
                indexLibre++;
            Etat etat = new Etat
            {
                Nom = $"Etat {indexLibre}",
                Position = new Position(x, y),
                EstInitial = false,
                EstFinal = false
            };
            Etats.Add(new EtatVM(etat) { EtatRadius = EtatRadius });
        }

        public void AjouterEtatInitial(double x, double y)
        {
            EtatVM? ancienInitial = Etats.FirstOrDefault(e => e.EstInitial);
            if (ancienInitial != null)
                ancienInitial.EstInitial = false;
            HashSet<int> indicesUtilises = new HashSet<int>();
            foreach (EtatVM e in Etats)
            {
                if (e.Nom.StartsWith("Etat "))
                {
                    string nombreTexte = e.Nom.Substring("Etat ".Length);
                    if (int.TryParse(nombreTexte, out int n))
                        indicesUtilises.Add(n);
                }
            }
            int indexLibre = 0;
            while (indicesUtilises.Contains(indexLibre))
                indexLibre++;
            Etat etat = new Etat
            {
                Nom = $"Etat {indexLibre}",
                Position = new Position(x, y),
                EstInitial = true,
                EstFinal = false
            };
            Etats.Add(new EtatVM(etat) { EtatRadius = EtatRadius });

        }

        public void AjouterEtatFinal(double x, double y)
        {
            HashSet<int> indicesUtilises = new HashSet<int>();
            foreach (EtatVM e in Etats)
            {
                if (e.Nom.StartsWith("Etat "))
                {
                    string nombreTexte = e.Nom.Substring("Etat ".Length);
                    if (int.TryParse(nombreTexte, out int n))
                        indicesUtilises.Add(n);
                }
            }
            int indexLibre = 0;
            while (indicesUtilises.Contains(indexLibre))
                indexLibre++;
            Etat etat = new Etat
            {
                Nom = $"Etat {indexLibre}",
                Position = new Position(x, y),
                EstInitial = false,
                EstFinal = true
            };
            Etats.Add(new EtatVM(etat) { EtatRadius = EtatRadius });

        }

        /// <summary>
        /// Vérifie si une position chevauche un autre état ou sort du canvas.
        /// </summary>
        /// <param name="x">Coordonnée X</param>
        /// <param name="y">Coordonnée Y</param>
        /// <returns>True si chevauchement ou hors limites, false sinon</returns>
        public bool CheckOverlap(double x, double y)
        {
            bool res = false;
            foreach (EtatVM evm in Etats)
            {
                res = evm.CheckOverlap(x, y);
                if (res)
                {
                    break;
                }
            }

            return res;
        }

        /// <summary>
        /// Supprime un état et toutes ses transitions associées.
        /// </summary>
        /// <param name="evm">Etat à supprimer</param>
        public void SupprimerEtat(EtatVM evm)
        {
            List<TransitionVM> transitionsASupprimer = new List<TransitionVM>();
            transitionsASupprimer = Transitions.Where(t => t.EtatDepart == evm || t.EtatArrivee == evm).ToList();


            foreach (TransitionVM t in transitionsASupprimer)
                Transitions.Remove(t);

            Etats.Remove(evm);
        }


        #endregion

        #region Gestion des Transitions

        /// <summary>
        /// Ajoute une transition entre deux états.
        /// </summary>
        /// <param name="start">Etat de départ</param>
        /// <param name="end">Etat d’arrivée</param>
        public void AjouterTransition(EtatVM start, EtatVM end)
        {
            TransitionVM nouvelleTransition = new TransitionVM(start, end);

            // Calculer index et total AVANT d'ajouter
            // Ajout aux listes
            start.TransitionsOut.Add(nouvelleTransition);
            end.TransitionsIn.Add(nouvelleTransition);

            // previousTransition
            TransitionVM? previous = start.TransitionsOut
                .Where(t => t.EtatArrivee == end && t != nouvelleTransition)
                .LastOrDefault();
            nouvelleTransition.PreviousTransition = previous;

            // Génération condition
            HashSet<int> indicesUtilises = new HashSet<int>();
            foreach (TransitionVM t in Transitions)
            {
                if (t.Condition.Replace(" ","_").StartsWith("Condition_"))
                {
                    string nombreTexte = t.Condition.Substring("Condition ".Length);
                    if (int.TryParse(nombreTexte, out int n))
                        indicesUtilises.Add(n);
                }
            }

            int indexLibre = 0;
            while (indicesUtilises.Contains(indexLibre)) indexLibre++;
            nouvelleTransition.Condition = "Condition " + indexLibre;

            // Ajout final
            Transitions.Add(nouvelleTransition);
            nouvelleTransition.RefreshGeometry();
        }

        /// <summary>
        /// Supprime une transition.
        /// </summary>
        /// <param name="tvm">Transition à supprimer</param>
        public void SupprimerTransition(TransitionVM tvm)
        {
            tvm.EtatDepart.TransitionsOut.Remove(tvm);
            tvm.EtatArrivee.TransitionsIn.Remove(tvm);
            Transitions.Remove(tvm);
        }

        #endregion

        public void ConstruireAutomateDepuisVM()
        {
            this.metier.Etats.Clear();
            foreach (EtatVM etat in this.Etats)
            {
                this.metier.Etats.Add(etat.Metier);
            }
            this.metier.Transitions.Clear();
            foreach (TransitionVM transition in this.Transitions)
            {
                this.metier.Transitions.Add(transition.Metier);
            }
        }

        public void RecupAutomate()
        {
            Title = this.metier.Nom;
            // D'abord créer tous les EtatVM
            Dictionary<Etat, EtatVM> mapEtats = new Dictionary<Etat, EtatVM>();
            Etats.Clear();

            foreach (Etat etat in this.metier.Etats)
            {
                EtatVM etatVM = new EtatVM(etat) { EtatRadius = EtatRadius };
                Etats.Add(etatVM);
                mapEtats[etat] = etatVM;
            }

            Transitions.Clear();
            // Ensuite créer les TransitionVM avec les bonnes références
            foreach (Transition t in this.metier.Transitions)
            {
                if (mapEtats.TryGetValue(t.EtatDebut, out EtatVM debut) &&
                    mapEtats.TryGetValue(t.EtatFinal, out EtatVM fin))
                {
                    TransitionVM transitionVM = new TransitionVM(debut, fin, t);
                    transitionVM.Condition = t.Condition;
                    Transitions.Add(transitionVM);
                    debut.TransitionsOut.Add(transitionVM);
                    fin.TransitionsIn.Add(transitionVM);
                }
            }
        }

    }
}
