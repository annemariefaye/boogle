class Joueur
{
    ///1)Attributs
    string pseudo;
    int score;
    string[] mots;


    ///2)Constructeur
    public Joueur(string pseudo)
    {
        this.pseudo = pseudo;
        while (this.pseudo.Length >= 20)
        {
            Console.WriteLine("pseudo trop grand, veuillez réentrer le pseudo");///mettre un accent sur reentrer
            this.pseudo = Console.ReadLine();
        }
        this.score = 0;
        this.mots = new string[0];
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

    public void Add_Mot(string mot)
    {
        if (!Contain(mot))
        {
            string[] nouveauxMots = new string[this.mots.Length + 1];
            for (int i = 0; i < this.mots.Length; i++)
            {
                nouveauxMots[i] = this.mots[i];
            }
            nouveauxMots[this.mots.Length] = mot; // ajouter le nouveau mot
            this.mots = nouveauxMots; // mise à jour du tableau
            Console.WriteLine($"Le mot {mot} est valide");
            this.score++; // mise à jour du score
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

    public void UpdateScore(string mot)
    {
        mot = mot.ToUpper();
        int scoreTotal = 0;
        Dictionary<char, int> pointLettre = new Dictionary<char, int>();
        string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Lettres.txt");
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
                this.score += score;
            }
        }
    }


}
