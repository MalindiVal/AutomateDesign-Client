# AutomateDesign — Client

[![CI](https://github.com/MalindiVal/AutomateDesign-Client/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/MalindiVal/AutomateDesign-Client/actions/workflows/dotnet-desktop.yml)
[![Release](https://img.shields.io/github/v/release/MalindiVal/AutomateDesign-Client?include_prereleases)](https://github.com/MalindiVal/AutomateDesign-Client/releases/latest)

AutomateDesign est une application de bureau (WPF, .NET 8) pour concevoir des automates à états finis de façon visuelle : création des états et des transitions, mise en forme du schéma, puis sauvegarde, export ou partage via un serveur. L'objectif est de faciliter la conception et la compréhension des automates, à des fins pédagogiques ou de prototypage.

Ce projet est issu d'une SAE (Situation d'Apprentissage et d'Évaluation) réalisée à l'IUT de Dijon.

> 💡 Ce dépôt contient le **client**. La partie serveur (stockage des automates, comptes utilisateurs) se trouve dans le dépôt [AutomateDesign-API](https://github.com/MalindiVal/AutomateDesign-API).

## Table des matières

1. [Fonctionnalités](#fonctionnalités)
2. [Installation (utilisateurs)](#installation-utilisateurs)
3. [Guide d'utilisation](#guide-dutilisation)
   - [Ouvrir l'éditeur](#ouvrir-léditeur)
   - [Créer un état](#créer-un-état)
   - [Modifier un état](#modifier-un-état)
   - [Déplacer un état](#déplacer-un-état)
   - [Créer une transition](#créer-une-transition)
   - [Modifier une transition](#modifier-une-transition)
   - [Déplacer une transition](#déplacer-une-transition)
   - [Sauvegarder un automate](#sauvegarder-un-automate)
   - [Exporter un automate](#exporter-un-automate)
   - [Importer un automate](#importer-un-automate)
4. [Développement](#développement)
   - [Prérequis](#prérequis)
   - [Compiler et lancer](#compiler-et-lancer)
   - [Tests](#tests)
   - [Configuration de la connexion à l'API](#configuration-de-la-connexion-à-lapi)
5. [Intégration continue et releases](#intégration-continue-et-releases)
6. [Structure du projet](#structure-du-projet)

## Fonctionnalités

- Éditeur graphique interactif : états normaux, initial et finaux, transitions courbes
- Modification et suppression des états et transitions
- Sauvegarde et ouverture d'automates au format JSON
- Export en image (PNG, JPG, BMP) ou en classes C# (`.cs`)
- Envoi et récupération d'automates sur le serveur distant (avec authentification)

## Installation (utilisateurs)

1. Téléchargez l'archive `AutomateDesign-<version>-win-x64.zip` depuis la [dernière release](https://github.com/MalindiVal/AutomateDesign-Client/releases/latest).
2. Décompressez-la dans le dossier de votre choix.
3. Lancez `IHM.exe`.

Aucune installation de .NET n'est nécessaire : l'archive embarque le runtime. Elle est configurée pour utiliser l'API déployée (voir [Configuration](#configuration-de-la-connexion-à-lapi) pour la modifier).

> Windows peut afficher un avertissement SmartScreen au premier lancement, l'exécutable n'étant pas signé : cliquez sur « Informations complémentaires » puis « Exécuter quand même ».

## Guide d'utilisation

### Ouvrir l'éditeur

Au lancement, cliquez sur **« + Nouveau Automate »**.

<img width="1260" height="759" alt="Écran d'accueil" src="https://github.com/user-attachments/assets/ea29fc38-3608-4edb-8634-cbae6b7c6c90" />

### Créer un état

La barre d'outils propose trois types d'état :

- **État initial** (option 1, flèche pointant vers un cercle) — un seul par automate ;
- **État normal** (option 3, cercle simple) ;
- **État final** (option 4, double cercle).

<img width="221" height="71" alt="Barre d'outils" src="https://github.com/user-attachments/assets/c09101e1-ab0a-47da-bfc9-b453bd6c639b" />

Sélectionnez un type, puis cliquez dans la zone de dessin pour placer l'état.

![Création d'un état](https://github.com/user-attachments/assets/543d342e-ea71-4f21-9894-6db8268f19eb)

### Modifier un état

Faites un **clic droit** sur un état pour ouvrir sa fenêtre de modification. Cette fenêtre permet aussi de le supprimer.

<img width="336" height="190" alt="Modification d'un état" src="https://github.com/user-attachments/assets/d782f642-e282-4ebd-bb7d-fca89801e5a1" />

### Déplacer un état

Maintenez le clic sur un état, déplacez la souris, puis relâchez à l'endroit voulu.

![Déplacement d'un état](https://github.com/user-attachments/assets/07db7036-63ea-4cd0-9368-a4f21378a404)

> Si l'état est relâché sur un autre état, il revient à sa position de départ.

### Créer une transition

Sélectionnez l'outil flèche dans la barre d'outils, cliquez sur l'état de départ puis sur l'état d'arrivée. Appuyez sur **Échap** pour annuler la création en cours.

![Création d'une transition](https://github.com/user-attachments/assets/f40ce0a2-b3e6-422f-9ac9-62913dbf3634)

### Modifier une transition

Faites un **clic droit** sur une transition pour ouvrir sa fenêtre de modification. Cette fenêtre permet aussi de la supprimer.

<img width="329" height="188" alt="Modification d'une transition" src="https://github.com/user-attachments/assets/6d21ae18-0462-405c-854e-79921a12d3cf" />

### Déplacer une transition

Cliquez sur une transition : un point rouge apparaît. Faites-le glisser pour modifier la courbure de la transition.

https://github.com/user-attachments/assets/e905a87b-8ddd-4060-8d24-2d237047a13d

### Sauvegarder un automate

Cliquez sur **Enregistrer** pour sauvegarder l'automate au format JSON. Une notification verte confirme la réussite, une rouge signale un échec.

<img width="1001" height="696" alt="Bouton Enregistrer" src="https://github.com/user-attachments/assets/de5c7665-e8a9-4fc0-b0ff-a084c0acf6ea" />

### Exporter un automate

Cliquez sur **Exporter** pour ouvrir la fenêtre des options d'export.

<img width="1001" height="696" alt="Bouton Exporter" src="https://github.com/user-attachments/assets/4bdd1c22-29bf-45b9-9571-78ed16be5512" />

<img width="383" height="335" alt="Options d'export" src="https://github.com/user-attachments/assets/84aa2708-e855-473f-8354-5b13d84f09e3" />

Trois options sont disponibles :

- **Image** : enregistre le schéma en PNG, JPG ou BMP ;
- **Serveur distant** : envoie l'automate sur l'API (connexion requise) ;
- **Fichiers C#** : génère les classes représentant l'automate (`.cs`).

<img width="132" height="119" alt="Fichiers C# générés" src="https://github.com/user-attachments/assets/a45b67e7-a826-403a-ae29-6123be0298e2" />

### Importer un automate

Depuis l'accueil, cliquez sur **« Importer un automate »**.

<img width="1266" height="791" alt="Bouton Importer" src="https://github.com/user-attachments/assets/7058ce87-d390-4132-a2ce-39eed1cfec9e" />

<img width="377" height="280" alt="Options d'import" src="https://github.com/user-attachments/assets/b7c31bc2-6197-45a9-8cf8-06205ff4a6fe" />

**Depuis un fichier** — cliquez sur « Ouvrir un fichier » et choisissez un fichier `.json`.

**Depuis le serveur** — cliquez sur « Récupérer un Automate », puis connectez-vous :

<img width="1003" height="696" alt="Page de connexion" src="https://github.com/user-attachments/assets/1d12e83a-1768-4648-be5a-8ab1233561cf" />

Pour l'instant, un seul compte est disponible : identifiant `root`, mot de passe `root`.

<img width="540" height="313" alt="Connexion" src="https://github.com/user-attachments/assets/0b84a0e7-7225-4c97-a478-20b6c5f6b3e8" />

Choisissez un automate dans la liste puis cliquez sur **Importer**.

<img width="1004" height="701" alt="Liste des automates" src="https://github.com/user-attachments/assets/455044d7-116d-4937-a963-435318bcc6e8" />

> Le serveur est hébergé sur une offre gratuite : la première requête après une période d'inactivité peut prendre quelques dizaines de secondes.

## Développement

### Prérequis

- Windows (WPF)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) ou supérieur
- Visual Studio 2022 (facultatif, avec la charge de travail « Développement .NET Desktop »)

### Compiler et lancer

```bash
git clone https://github.com/MalindiVal/AutomateDesign-Client.git
```

```bash
cd AutomateDesign-Client
```

```bash
dotnet run --project IHM/IHM.csproj
```

Avec Visual Studio : ouvrez `AutomateDesign.sln`, définissez **IHM** comme projet de démarrage, puis lancez avec **F5**.

### Tests

```bash
dotnet test AutomateDesign.sln
```

### Configuration de la connexion à l'API

Les adresses de l'API sont définies dans [`Data/appsettings.json`](Data/appsettings.json), copié à côté de l'exécutable lors de la compilation (et présent dans l'archive des releases) :

```json
{
  "API": {
    "LocalMode": "true",
    "BaseUrl": "https://automatedesign-api.onrender.com/",
    "APICertificate": "50:51:2A:...:D9:31",
    "LocalUrl": "https://localhost:7294/"
  }
}
```

| Clé | Rôle |
| --- | --- |
| `LocalMode` | `"true"` : utilise `LocalUrl` (API lancée en local) ; `"false"` : utilise `BaseUrl` (API déployée). |
| `BaseUrl` | Adresse de l'API déployée. |
| `APICertificate` | Empreinte SHA-1 du certificat de l'API déployée, vérifiée à chaque connexion (non utilisée en mode local). |
| `LocalUrl` | Adresse de l'API lancée en local (voir [AutomateDesign-API](https://github.com/MalindiVal/AutomateDesign-API)). |

Le dépôt est configuré en mode local pour le développement ; les releases sont automatiquement basculées sur l'API déployée (`LocalMode` à `"false"`).

> Si le certificat de l'API déployée est renouvelé, mettez à jour `APICertificate`, sinon les connexions au serveur seront refusées.

## Intégration continue et releases

Deux workflows GitHub Actions sont définis dans [`.github/workflows`](.github/workflows) :

- **[`dotnet-desktop.yml`](.github/workflows/dotnet-desktop.yml)** — à chaque push ou pull request sur `main` : compilation (Debug et Release), tests unitaires, et mise à disposition de la build Release en artefact.
- **[`release.yml`](.github/workflows/release.yml)** — à chaque tag `v*` : tests, publication autonome `win-x64` (version de l'assembly tirée du tag), bascule de `appsettings.json` sur l'API déployée, puis création de la release GitHub avec l'archive `.zip` et des notes de version générées automatiquement.

Pour publier une nouvelle version :

```bash
git tag v1.2.0
```

```bash
git push origin v1.2.0
```

Un tag avec suffixe (par exemple `v1.2.0-beta.1`) est publié en pré-release. La release peut aussi être lancée manuellement depuis l'onglet **Actions** (workflow « Release », bouton « Run workflow »), en indiquant le tag à créer.

## Structure du projet

| Projet | Dossier | Rôle |
| --- | --- | --- |
| IHM | [`IHM/`](IHM) | Interface WPF (vues, convertisseurs, services d'export et de dialogue). Projet de démarrage. |
| ViewModels | [`ViewModels/`](ViewModels) | ViewModels (MVVM) faisant le lien entre l'IHM et la logique. |
| ClientService | [`Service/`](Service) | Services applicatifs : automates, utilisateurs, sérialisation JSON, génération C#. |
| ClientData | [`Data/`](Data) | Accès à l'API (DAO HTTP) et configuration (`appsettings.json`). |
| LogicLayer | [`LogicLayer/`](LogicLayer) | Modèle métier commun au client et à l'API : automates, états, transitions, géométrie. |
| TestLogic, TestVM, TestClientServices | `Test*/` | Tests unitaires de la logique, des ViewModels et des services. |
