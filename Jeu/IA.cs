class IA : Joueur
{
    TriDico dico;
    Plateau plateau;
    string langue;


    public IA(Plateau plateau, string langue, string pseudo = "IA") : base(pseudo)
    {
        this.langue = langue;
        this.dico = new TriDico(this.langue);
        this.langue = langue;
        this.plateau = plateau;
    }

    public string MotIA()
    {

        int max = 0;
        string res = "";
        foreach (var mot in this.dico.Mots)
        {
            if (!this.Mots.Contains(mot))
            { 
                bool inside = plateau.Test_Plateau(mot);
                if (inside)
                {
                    int tempScore = this.GetScore(mot);
                    if (tempScore > max)
                    {
                        max = tempScore;
                        tempScore = 0;
                        res = mot;
                    }
                } 
            }
        }

        return res;
    }
}