# AutomateDesign
AutomateDesign est un projet qui consiste en la création d'un schéma d'automate avec ses états et ses transitions

# Table des matières

1. [Objectif du projet](#objectif-du-projet)

2. [Fonctionnalités](#fonctionnalités)
   
3. [Guide d'utilisation](#guide-dutilisation)
   - 3.1. [Ouvrir l'éditeur](#ouvrir-léditeur)
   - 3.2. [Créer un État](#créer-un-etat)
   - 3.3. [Modifier un état](#modifier-un-état)
   - 3.4. [Déplacer un état](#déplacer-un-état)
   - 3.5. [Créer une Transition](#créer-une-transition)
   - 3.6. [Modifier une transition](#modifier-une-transition)
   - 3.7. [Déplacer une transition](#déplacer-une-transition)
   - 3.8. [Sauvegarder un Automate](#sauvegarde-un-automate)
   - 3.9. [Exporter son Automate](#exporter-son-automate)
     - 3.9.1. [En tant qu'image](#en-tant-quimage)
     - 3.9.2. [Vers le serveur distant](#vers-le-serveur-distant)
   - 3.10. [Importer un Automate](#importer-un-automate)
     - 3.10.1. [Depuis un fichier](#depuis-un-fichier)
     - 3.10.2. [Depuis le réseau](#depuis-le-réseau)

4. [Installation & exécution](#installation--exécution)
   - 4.1. [Prérequis](#prérequis)
   - 4.2. [Cloner le dépôt](#cloner-le-dépôt)
   - 4.4. [Lancer le client WPF](#lancer-le-client-wpf)
   - 4.6. [Configuration de la connexion Client-API](#configuration-de-la-connexion-client-api)
   
5. [Structure du Projet](#structure-du-projet)

# Objectif du projet
Le but d’AutomateDesign est de fournir un outil intuitif permettant de :
- Concevoir visuellement des automates à états finis
- Définir leurs transitions et propriétés
- Sauvegarder et charger des automates depuis une base de données
- Exporter les automates pour réutilisation dans d’autres applications.

Ce projet met en œuvre plusieurs technologies modernes de développement .NET (C#, WPF, ASP.NET, SQLite, Docker) afin de proposer une solution robuste et maintenable.

# Fonctionnalités
- Création et édition d’automates via une interface graphique interactive
- Ajout, modification et suppression d’états et de transitions
- Sauvegarde locale ou distante via l’API

# Guide d'utilisation
## Démarrez le logiciel
Reprenez le dossier compréssé dans les realease, décompresser le dans un dissier accessible, ouvrez le et cliquer sur l'executable nommé IHM.exe
## Ouvrir l'éditeur
Lorsque vous ouvrez l'executable cliquer sur "+ Nouveau Automate"

<img width="1260" height="759" alt="image" src="https://github.com/user-attachments/assets/ea29fc38-3608-4edb-8634-cbae6b7c6c90" />


## Créer un Etat
Dans l'éditeur, vous pouvez choisir de créer un etat normal (Option 3 avec un cercle simple) ,un etat initial(Option 1 avec une fleche pointant vers un cercle)(Il ne peut en y avoir qu'un) et un etat final(Option 4 avec le double cercle). 

<img width="221" height="71" alt="image" src="https://github.com/user-attachments/assets/c09101e1-ab0a-47da-bfc9-b453bd6c639b" />

Clique sur une de ces options puis cliquer sur la zone de dessin pour en créer un
![Enregistrement 2025-11-13 143807](https://github.com/user-attachments/assets/543d342e-ea71-4f21-9894-6db8268f19eb)

## Modifier Un état
Dans l'éditeur, vous pouvez modifier l'état en faisant un click droit pour ouvrir une fenetre de modification

<img width="336" height="190" alt="image" src="https://github.com/user-attachments/assets/d782f642-e282-4ebd-bb7d-fca89801e5a1" />

Il est également possible de supprimer un état depuis cette fenetre.

## Déplacer un état
Les états sont aussi déplacables. Maintenez le clic sur un état et déplacer la souris pour déplacer l'état, relachez à un endroit qui vous satisfaira

![Enregistrement 2025-11-13 144319](https://github.com/user-attachments/assets/07db7036-63ea-4cd0-9368-a4f21378a404)

Remarque : Si un état se superpose sur un autre, l'etat retournera à son état initial

## Créer une Transition
Dans la barre à outils, vous pouvez choisir la flèche corespondant à la création des transitions
Cliquez sur un etat qui sera l'etat de départ puis sur un autre état d'arivée.
![Enregistrement 2025-11-13 144724](https://github.com/user-attachments/assets/f40ce0a2-b3e6-422f-9ac9-62913dbf3634)
Vous pouvez annuler la création d'une transition en appuyant sur la touche echap.

## Modifier une transition
Dans l'éditeur, vous pouvez modifier la transition en faisant un click droit pour ouvrir une fenetre de modification

<img width="329" height="188" alt="image" src="https://github.com/user-attachments/assets/6d21ae18-0462-405c-854e-79921a12d3cf" />

Il est également possible de supprimer une transition depuis cette fenetre.

## Déplacer une transition

Lors que vous cliquer sur une transition, un point rouge s'affichera, maintenez le clic sur ce point vous permettra de modifier la trajectoire de la transition

https://github.com/user-attachments/assets/e905a87b-8ddd-4060-8d24-2d237047a13d

## Sauvegarde un Automate
Cliquez sur le bouton enregistrer pour sauvegarder votre automate sous format json.

<img width="1001" height="696" alt="image" src="https://github.com/user-attachments/assets/de5c7665-e8a9-4fc0-b0ff-a084c0acf6ea" />

Vous serez notifié par une notification vert en cas de réussite et rouge en cas d'échec

## Exporter son Automate
Cliquez sur le bouton Export ouvrira une fenetre d'option d'exportation.

<img width="1001" height="696" alt="image" src="https://github.com/user-attachments/assets/4bdd1c22-29bf-45b9-9571-78ed16be5512" />

Vous pouvez choisir entre trois options :

<img width="383" height="335" alt="image" src="https://github.com/user-attachments/assets/84aa2708-e855-473f-8354-5b13d84f09e3" />

### En tant qu'image
Vous pourrez enregitrez l'automate en format image (png, jpg et bmp).
### Vers le serveur distant
Vous pouvez également enregistrer l'Automate dans le réseau.
### En fichiers c#
Vous pouvez également enregistrer l'Automate sous forme de plusieurs fichiers de classe en format .cs
<img width="132" height="119" alt="image" src="https://github.com/user-attachments/assets/a45b67e7-a826-403a-ae29-6123be0298e2" />

## Importer un Automate
Vous pouvez récupérer un automate en cliquer sur le bouton "Importer un automate".

<img width="1266" height="791" alt="image" src="https://github.com/user-attachments/assets/7058ce87-d390-4132-a2ce-39eed1cfec9e" />


Cela ouvrir une nouvel page avec deux options : 

<img width="377" height="280" alt="image" src="https://github.com/user-attachments/assets/b7c31bc2-6197-45a9-8cf8-06205ff4a6fe" />

### Depuis un fichier
Cliquez sur le bouton "Ouvrir un fichier". Cela ouvrira une fenetre de dialoque qui demandera un fichier en .json

### Depuis le réseau
Cliquez sur "Récupérer un Automate" vous menera à une page de connexion :

<img width="1003" height="696" alt="image" src="https://github.com/user-attachments/assets/1d12e83a-1768-4648-be5a-8ab1233561cf" />

Insérez y vos identifiants et mot de passe.

Pour l'intant, il n'y a qu'un compte utilisateur utilisable :
- Identifiant : root
- Mot de passe : root

Cliquez sur "Connexion"

<img width="540" height="313" alt="image" src="https://github.com/user-attachments/assets/0b84a0e7-7225-4c97-a478-20b6c5f6b3e8" />


Choisissez un automate présent dans la liste et cliquer sur le bouton "importer"

<img width="1004" height="701" alt="image" src="https://github.com/user-attachments/assets/455044d7-116d-4937-a963-435318bcc6e8" />

# Installation & exécution
## Prérequis
- .NET 8 SDK ou supérieur
- Docker (optionnel pour déploiement)
- Visual Studio 2022.

## Cloner le dépôt
- Ouvrez Powershell sur le dossier de votre choix
- Cloner le projet
```
git clone https://github.com/dept-info-iut-dijon/S5_ModernTech_AutomateDesign.git
```

## Lancer le client WPF
- Cliquez sur le dossier Client
  
<img width="196" height="259" alt="image" src="https://github.com/user-attachments/assets/5d851ae1-3668-4075-97e8-f9fb870d020b" />

- Cliquez sur la solution AutomateDesign.sln

<img width="252" height="429" alt="image" src="https://github.com/user-attachments/assets/ce6f62aa-d0bb-426e-a946-7d1de3d5895c" />

- Choisisez Visual Studio 2022 pour ouvrir la solution
- Cliquez sur la flèche à coté de l'engrenage et sélectionner IHM

<img width="189" height="191" alt="image" src="https://github.com/user-attachments/assets/5d7a0368-212f-4b04-8eef-afafa28bcb7a" />

- Cliquez sur le bouton Démarrer

<img width="66" height="63" alt="image" src="https://github.com/user-attachments/assets/5d433954-0ea7-478d-a4d5-3a285cbd9310" />

### Configuration de la connexion Client-API
La connexion entre le client et l'API est gérée dans un projet nommé ClientData et les adresses sont stockées dans le fichier appsettings.json (il se trouve également dans le dossier contenant l'executable dans les releases)

<img width="669" height="148" alt="Capture d’écran 2025-11-13 142229" src="https://github.com/user-attachments/assets/53c19d25-5203-4c93-bae9-8f9fdb6aff8a" />

- LocalMode permet à la classe APIConfig de savoir si il doit utiliser l'adresse se trouvant dans LocalUrl ou BaseUrl (true (le client utilisera l'api en localhost) ou false (le client utilisera l'api qui est actuellement déployé))
- BaseUrl coresspond à l'adresse de l'API déployé
- APICertificate correspond aux certificat de sécurité de l'API (SHA-1)
- LocalUrl correspond à l'adresse utilisé pour l'hébergement local de l'API

# Structure du Projet
Le projet est composé de plusieurs projets :
- Un Client IHM en WPF
- Un LogicLayer commun au Client et à l'API
- Une bibliothèque dédiée à la gestion des données coté client
- Une bibliothèque dédié aux ViewModels qui font le lien entre le logiclayer et l'IHM
- Des tests unitaires pour les differents projets

