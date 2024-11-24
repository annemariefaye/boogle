class Plateau
{
    ///Attributs
    ///
    Dictionnaire dico;
    string langue;
    De[] des;



    ///Constructeur
    public Plateau(string langue, De[] des) 
    { 
        this.langue = langue;
        this.dico = new Dictionnaire(this.langue);
        this.des = des;
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
}
