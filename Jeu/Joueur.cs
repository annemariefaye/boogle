public class Joueur
{
    ///1)Attributs
    string pseudo;
    int score;
    string langue;
    string[] mots;


    ///2)Constructeur
    public Joueur(string pseudo, string langue)
    {
        this.pseudo = pseudo;
        this.langue = langue;
        while (this.pseudo.Length >= 20)
        {
            Console.WriteLine("pseudo trop grand, veuillez réentrer le pseudo");
            this.pseudo = Console.ReadLine();
        }
        this.score = 0;
        this.mots = new string[0];
        this.langue = langue;
    }

    ///3)proprietes de lecture
    public string Pseudo { get { return this.pseudo; } }
    public int Score { get { return this.score; } }
    public string[] Mots { get { return this.mots; } }

    ///4)Méthodes obligatoires
    public bool Contain(string mot)
    {
        bool con = false;
        foreach (string m in this.mots)
        {
            if (m == mot)
            {
                con = true; /// Le mot existe déjà
                Console.WriteLine("Le mot a déjà été entré");
            }
        }

        return con;
    }

    /// On ajoute le mot à au tableau de mot si il n'existe pas déjà dans le tableau
    public void Add_Mot(string mot)
    {
        if (!Contain(mot))
        {
            string[] nouveauxMots = new string[this.mots.Length + 1];
            for (int i = 0; i < this.mots.Length; i++)
            {
                nouveauxMots[i] = this.mots[i];
            }
            nouveauxMots[this.mots.Length] = mot; /// Ajouter le nouveau mot
            this.mots = nouveauxMots; /// Mise à jour du tableau
            Console.WriteLine($"Le mot {mot} est valide");
            this.score++; /// Mise à jour du score

        }
        else
        {
            Console.WriteLine("le mot " + mot + " a deja etait trouve par le joueur " + this.pseudo);
        }
    }
    public string toString()
    {
        string chaine = "";
        for (int i = 0; i < this.mots.Length; i++)
        {
            chaine += this.mots[i];
            if (i < this.mots.Length - 1)
            {
                chaine += ", ";
            }
        }
        return "Joueur: " + this.pseudo + "Score: " + this.score + "Mots trouvés: " + chaine;
    }

    /// On calcule le nombre de points que rapporte un mot grâce à un dictionnaire Dictionary<char, int> associant chaque char à un nombre de points
    /// Ensuite on actualise le score du joueur avec this.score
    public void UpdateScore(string mot)
    {
        mot = mot.ToUpper();
        float coeff = 1 + (mot.Length / 10.0f) - 0.2f;
        Dictionary<char, int> pointLettre = new Dictionary<char, int>();
        string chemin;
        if (this.langue == "fr")
        {
            chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Lettres.txt");
        }
        else
        {
            chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Letters.txt");
        }
        chemin = Path.GetFullPath(chemin);
        var lignes = File.ReadAllLines(chemin);
        foreach (var ligne in lignes)
        {
            var parties = ligne.Split(';');
            if (parties.Length == 3)
            {
                char lettre = parties[0][0];
                int scorelettre = int.Parse(parties[1]);
                pointLettre[lettre] = scorelettre;
            }
        }
        foreach (char c in mot)
        {
            if (pointLettre.TryGetValue(c, out int score))
            {
                this.score += (int)Math.Round(score * coeff);
            }
        }
    }

    /// On calcule et on retourne le nombre de points que rapporte un mot grâce à un dictionnaire Dictionary<char, int> associant chaque char à un nombre de points
    /// C'est casiment la même fonction que UpdateScore sauf qu'on retourne un int
    public int GetScore(string mot)
    {
        mot = mot.ToUpper();
        float coeff = 1 + (mot.Length / 10.0f) - 0.2f; /// On ajoute un bonus en fonction de la longueur du mot que s'il fait plus de 2 lettres
        int total = 0;
        Dictionary<char, int> pointLettre = new Dictionary<char, int>();
        string chemin;
        if (this.langue == "fr")
        {
            chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Lettres.txt");
        }
        else
        {
            chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Letters.txt");
        }
        chemin = Path.GetFullPath(chemin);
        var lignes = File.ReadAllLines(chemin);
        foreach (var ligne in lignes)
        {
            var parties = ligne.Split(';');
            if (parties.Length == 3)
            {
                char lettre = parties[0][0];
                int scorelettre = int.Parse(parties[1]);
                pointLettre[lettre] = scorelettre;
            }
        }
        foreach (char c in mot)
        {
            if (pointLettre.TryGetValue(c, out int score))
            {
                total += score;
            }
        }

        return (int)Math.Round(total * coeff);
    }

}
