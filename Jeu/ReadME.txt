```markdown
# Boogle

## Description
Le projet **Boogle** est un jeu interactif qui permet aux joueurs de proposer des mots sur un plateau. Les joueurs peuvent jouer contre une intelligence artificielle (IA) ou en mode multijoueur. Le but du jeu est de collecter des points en proposant des mots valides sur le plateau, tout en respectant un temps limité pour chaque tour.

## Fonctionnalités
- Choix de la langue (français ou anglais).
- Possibilité de jouer avec une IA et/ou plusieurs joueurs humains.
- Validation des mots proposés par rapport à un plateau de jeu.
- Suivi des scores des joueurs.
- Affichage du gagnant à la fin de la partie.

## Installation
Pour exécuter le jeu, vous devez avoir installé .NET sur votre machine.

1. Clonez ce dépôt :
   ```bash
   git clone https://github.com/annemariefaye/boogle.git
   cd boogle
   ```

2. Ouvrez le projet dans Visual Studio.

3. Ouvrez la console du gestionnaire de package (Package Manager Console) dans Visual Studio :
   - Allez dans **Tools** > **NuGet Package Manager** > **Package Manager Console**.

4. Installez les dépendances nécessaires via le gestionnaire de package :
   ```powershell
   Install-Package KnowledgePicker.WordCloud
   Install-Package SkiaSharp
   ```

5. Compilez le projet :
   ```bash
   dotnet build
   ```

6. Exécutez le jeu :
   ```bash
   dotnet run
   ```

## Utilisation
Lorsque vous exécutez le programme, il vous demandera de :
1. Sélectionner une langue (français ou anglais).
2. Choisir si vous souhaitez jouer avec une IA.
3. Définir la taille du plateau de jeu (minimum 4).
4. Spécifier le nombre de joueurs (minimum 2 si aucune IA).
5. Entrer les noms des joueurs.

Ensuite, le jeu se déroule en tours, où chaque joueur propose des mots dans un temps limité. Les mots valides rapportent des points au joueur.

### Exemple de déroulement :
- "Langue (fr)/Language (en): "
- "Voulez-vous jouer avec une IA ? : (Y/N)"
- "Veuillez entrer la taille du plateau (min 4) : "
- "Veuillez entrer le nombre de joueurs : "
- "Saisissez un nouveau mot : "

## Code Principal
Voici un extrait du code principal du jeu :
```csharp

static void Main(string[] args)
{
    // Sélection de la langue
    Console.WriteLine("Langue (fr)/Language (en): ");
    string langue = null;

    while (langue != "fr" && langue != "en")
    {
        langue = Console.ReadLine().ToLower();
        if (langue != "fr" && langue != "en")
        {
            Console.WriteLine("Saisissez une langue valide");
        }
    }
    Console.Clear();

    // Récupération du nombre de joueurs
    int nbJoueurs = 0;
    // Reste du code pour initialiser le plateau et les joueurs...

    // Début du jeu avec des tours...
}
```

## Tests Unitaires
Le projet inclut des tests unitaires pour vérifier les fonctionnalités principales, comme la validation des mots et le calcul des scores. Tous les tests ont réussi, assurant la fiabilité des fonctionnalités du jeu.


## Acknowledgments
- Ce projet utilise les bibliothèques .NET pour la gestion des entrées/sorties et le chronométrage.

```

### Changements Apportés
- Les instructions d'installation des dépendances `KnowledgePicker.WordCloud` et `SkiaSharp` ont été modifiées pour utiliser le gestionnaire de package NuGet à travers la console de Visual Studio.

Si vous avez besoin de plus d'informations ou d'autres modifications, n'hésitez pas à le faire savoir !