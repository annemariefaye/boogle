class IA : Joueur
{
    /// Attributs
    TriDico dico;
    Plateau plateau;
    string langue;


    /// Le constructeur hérite de la classe Joueur mais on ajoute le plateau et la langue en supplément.
    public IA(Plateau plateau, string langue, string pseudo = "IA") : base(pseudo)
    {
        this.langue = langue;
        this.dico = new TriDico(this.langue);
        this.langue = langue;
        this.plateau = plateau;
    }


    /// Ici on utilise simplement la méthode Test_Plateau pour chaque mot du dictionnaire et on retourne le mot rapportant le maximum de points
    public string MotIA()
    {

        int max = 0;
        string res = "";
        foreach (var mot in this.dico.Mots)
        {
            if (!this.Mots.Contains(mot)) /// Vérification que le mot n'a pas déjà été entré
            { 
                bool inside = plateau.Test_Plateau(mot); /// Appel de la fonction récursive qui cherchera le mot pour chaque case du plateau
                if (inside)
                {
                    int tempScore = this.GetScore(mot);
                    if (tempScore > max)
                    {
                        max = tempScore;
                        res = mot;
                    }
                } 
            }
        }

        return res;
    }
}