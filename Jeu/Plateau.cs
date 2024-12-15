using System;

public class Plateau
{
    ///Attributs
    Dictionnaire dico;
    string langue;
    int taille;
    char[,] grille;
    De[] des;
    Dictionary<char, int> lettresDispo;



    ///Constructeur
    public Plateau(string langue, int taille)
    {
        this.langue = langue;
        this.dico = new Dictionnaire(this.langue);
        this.taille = taille;
        this.des = new De[taille * taille];
        this.lettresDispo = new Dictionary<char, int>();
        this.grille = new char[taille, taille];

        /// Generer les des et faire un dico qui compte le nombre de lettre et ban une lettre si on a atteint le cota dans Lettres.txt
        /// On lit le fichier Lettres.txt et on met la lettre et le nombre de lettres disponibles dans un dictionnaire
        /// string fileName = "fichier.txt";
        string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Lettres.txt");
        chemin = Path.GetFullPath(chemin);

        var lignes = File.ReadAllLines(chemin);
        foreach (var ligne in lignes)
        {
            var parties = ligne.Split(';');
            if (parties.Length == 3)
            {
                char lettre = parties[0][0];
                int nbLettres = int.Parse(parties[2]);
                lettresDispo[lettre] = nbLettres;
            }
        }

        Random rand = new Random();
        List<char> listeLettres = new List<char>();

        // On crée un liste qui ajoute les lettres en fonctions de combien sont autorisées la plateau (par exemple 5 "a" va donner : [a,a,a,a,a])
        foreach (var x in lettresDispo)
        {
            char lettre = x.Key;
            int quota = x.Value * this.taille;

            for (int i = 0; i < quota; i++)
            {
                listeLettres.Add(lettre);
            }
        }

        // On mélange la liste avec un tri à bulle mais avec un index aléatoire
        for (int i = listeLettres.Count - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);

            char temp = listeLettres[i];
            listeLettres[i] = listeLettres[j];
            listeLettres[j] = temp;
        }

        // On crée les dés en fonction des lettres dans la listeLettres on ajoute les lettres une à une
        int index = 0;
        for (int i = 0; i < this.des.Length; i++)
        {
            char[] faceLettres = new char[6];

            for (int j = 0; j < 6; j++) // Chaque dé a 6 faces
            {
                if (index < listeLettres.Count)
                {
                    faceLettres[j] = listeLettres[index];
                    index++;
                }
            }
            this.des[i] = new De(faceLettres);
        }

        index = 0;
        /// On rempli la grille des lettres affichées
        for (int i = 0; i < taille; i++)
        {
            for (int j = 0; j < taille; j++)
            {
                this.grille[i, j] = this.des[index].Face;
                index++;
            }
        }

    }

    /// Creation de Grille pour les TestUnitaires uniquement
    public char[,] Grille
    {
        get => this.grille;
        set
        {
            /// On checke que la matrice n'est pas null
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "La grille ne peut pas être null.");
            }

            /// On vérifie que la taille est au moins 4x4
            if (value.GetLength(0) < 4 || value.GetLength(1) < 4)
            {
                throw new ArgumentException("La grille doit être au moins 3x3.");
            }

            this.grille = value;
        }
    }


    ///Méthodes obligatoires

    public bool Test_Plateau(string mot)
    {
        int lignes = this.grille.GetLength(0);
        int cols = this.grille.GetLength(1);
        bool[,] visited = new bool[lignes, cols];

        /// On checke chaque case du plateau et on lance la fonction Recherche pour chaque lettre
        /// On checke si le mot existe dans la grille
        bool trouveDansGrille = false;

        for (int ligne = 0; ligne < lignes; ligne++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (Recherche(mot, 0, ligne, col, visited))
                {
                    trouveDansGrille = true;
                    break;
                }
            }
            if (trouveDansGrille)
            {
                break;
            }
        }

        if (!trouveDansGrille)
        {
            Console.WriteLine($"{mot} n'est pas dans la grille");
        }

        /// On checke si le mot est dans le dico
        bool dansDico = this.dico.RechercheHashSet(mot);

        if (!dansDico)
        {
            Console.WriteLine($"{mot} n'est pas dans le dictionnaire");
        }

        return trouveDansGrille && dansDico;
    }

    /// Fonction récursive qui cherche le lettre suivante du mots parmis les 8 cases autour la lettre actuelle
    /// On créer aussi un tableau qui checke si la case à déjà été visité pour éviter qu'on utilise 2 fois la même lettre au même index pour faire un mot
    bool Recherche(string mot, int index, int ligne, int col, bool[,] visite)
    {

        /// Si le mot est fini
        if (index + 1 >= mot.Length)
        {
            return true;
        }

        /// On évite le out of range ici
        if (ligne < 0 || col < 0 || ligne >= this.grille.GetLength(0) || col >= this.grille.GetLength(1))
        {
            return false;
        }

        /// Si on déjà visité la case ou que la case actuelle ne correspond pas à lettre qu'on cherche dans la grille
        if (visite[ligne, col] || this.grille[ligne, col] != mot[index])
        {
            return false;
        }

        visite[ligne, col] = true;

        /// On checke les 8 directions autour de la lettre actuelle
        int[] ligneDir = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] colDir = { -1, 0, 1, -1, 1, -1, 0, 1 };

        for (int d = 0; d < 8; d++)
        {
            if (Recherche(mot, index + 1, ligne + ligneDir[d], col + colDir[d], visite))
            {
                return true;
            }
        }

        visite[ligne, col] = false;
        return false;
    }

    public string toString(De[] des)
    {
        string res = "";

        foreach (De de in des)
        {
            res += de.toString() + "\n";
        }

        return res;
    }


    /// Mise à jour du plateau avec une randomisation des Faces
    public void UpdatePlateau()
    {
        foreach (De de in this.des)
        {
            de.Lance();

        }
        int index = 0;
        for (int i = 0; i < taille; i++)
        {
            for (int j = 0; j < taille; j++)
            {
                this.grille[i, j] = this.des[index].Face;
                index++;
            }
        }
    }

    ///A modifier
    public void AfficherPlateau()
    {

        int z = 0;
        for (int i = 0; i < this.taille; i++)
        {
            for (int j = 0; j < this.taille; j++)
            {
                Console.Write(this.des[z].Face);
                z++;
            }
            Console.WriteLine();
        }

    }
}
