class Plateau
{
    ///Attributs
    ///
    Dictionnaire dico;
    string langue;
    int taille;
    De[] des;



    ///Constructeur
    public Plateau(string langue, int taille) 
    { 
        this.langue = langue;
        this.dico = new Dictionnaire(this.langue);
        this.taille = taille;   
        this.des = new De[taille*taille];

        for (int i = 0; i < taille*taille; i++)
        {
            this.des[i] = new De(new char[] { 'E', 'L', 'I', 'L', 'A', 'T' });
        }

        /// Generer les des et faire un dico qui compte le nombre de lettre et ban une lettre si on a atteint le cota dans Lettres.txt
    }


    ///Méthodes obligatoires

    
    /// On regarde pour chaque char si il existe dans au moins une fois dans toutes les faces de tous les dés
    public bool Test_Plateau(string mot)
    {
        bool existe = true;
        mot = mot.ToUpper();

        ///On compte combien de fois on a une lettre dans le mot et on utilise un dictionnaire pour stocker l'info <char, int>
        var repetitionMot = new Dictionary<char, int>();
        foreach (char c in mot)
        {
            if (repetitionMot.ContainsKey(c))
            {
                repetitionMot[c]++;
            }
            else
            {
                repetitionMot[c] = 1;
            }
        }

        ///On compte combien de fois on a une lettre dans les faces et on utilise un dictionnaire pour stocker l'info <char, int>
        var repetitionFaces = new Dictionary<char, int>();
        foreach (var de in this.des)
        {
            foreach (char c in de.Faces)
            { 
                if (repetitionFaces.ContainsKey(c))
                {
                    repetitionFaces[c]++;
                }
                else
                {
                    repetitionFaces[c] = 1;
                }
            }
        }


        foreach (var x in repetitionMot)
        {
            /// On regarde si il y a un char qui n'est pas dans le dictionnaire des faces ou si il y a un char qui se répète  plus qu'il n'est dispo sur la plateau
            if (!repetitionFaces.ContainsKey(x.Key) || repetitionFaces[x.Key] < x.Value)
            {
                existe = false;
                break;
            }
        }


        return mot.Length >= 2 && this.dico.RechercheHashSet(mot) && existe;
    }

    public string toString(De[] des)
    {
        string res = "";

        foreach(De de in des)
        {
            res += de.toString() + "\n";
        }

        return res;
    }

    ///A modifier
    public void AfficherPlateau()
    {

        int z = 0;
        for (int i = 0; i < this.taille; i++)
        {
            for (int j = 0; j < this.taille; j++)
            {
                Console.Write(this.des[z].Faces[0]);
                z++;
            }
            Console.WriteLine();
        }

    }
}
