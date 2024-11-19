Voici les parties dont tu devras t'occuper :
- Classe Joueur
- Classe De

Mets des commentaires un peu partout (pour faire un commentaire écrit "///") 

Classe Joueur:

1. Crée les attributs "string pseudo", "int score" et "string[] mots" pour le joueur


2. Crée un constructeur Joueur avec le "pseudo", le "score du joueur en paramètre (string). N'oublie pas de créer le tableau de "mots" dans le constructeur. Fait les this.xxx aussi.

Essaie de limiter le nombre de caractères maximal à 20
(Dans le futur il faudra vérifier si le pseudo n'est pas déjà pris mais je pense que ce sera à faire dans le Jeu.cs)


3. Crée les propriétés de lecture (avec get) pour "pseudo", "score" et "mots".


4. Pour la méthode Contain(string mot) il faudra utiliser "int[] mots" et comparer chaque mot du tableau au "string mot"


5. Pour la méthode Add_Mot(string mot), on ajoute le mot au tableau.

Méthode 1 : Il faudra donc remplacer "mots". Créer un nouveau tableau de taille "tab.Length + 1". Copie les valeurs de "mots" dans le nouveau et ajoute le nouveau mot à la fin.

Méthode 2 : Sinon tu peux utiliser :
Array.Resize(ref this.mots, this.mots.Length + 1); ///Agrandit le tableau de 1
this.mots[^1] = mot; ///Ajoute un mot à la dernière place du tableau qui est vide


6. Pour la méthode toString(), return le "pseudo" du joueur et peut être la liste des mots trouvés (à voir je suis pas sûre)



Classe De:

1. Crée les attributs "char[] lettres", "char face"


2. Crée un constructeur De sans paramètres. Fait le this.xxx aussi pour "lettres" et "face"


3. Crée les propriétés de lecture (avec get) pour "lettres" et "face"


4. Pour la méthode Lance(Random r). Utilise r.Next et modifie "this.face" (j'en dis pas plus pour l'instant débrouille toi)


5. Pour la méthode toString(), return les "lettres" du dé et sa "face" actuelle



