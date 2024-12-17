class IA : Joueur
{
    /// Attributs
    TriDico dico;
    Plateau plateau;
    string langue;


    /// Le constructeur hérite de la classe Joueur mais on ajoute le plateau et la langue en supplément.
    public IA(Plateau plateau, string langue, string pseudo = "IA") : base(pseudo, langue)
    {
        this.langue = langue;
        this.dico = new TriDico(this.langue);
        this.langue = langue;
        this.plateau = plateau;
    }


    /// Ici on utilise simplement la méthode Test_Plateau pour chaque mot du dictionnaire et on retourne le mot rapportant le maximum de points
    public List<string> MotsIA()
    {

        var motsEtScores = new List<(string Mot, int Score)>();
        var res = new List<string>();

        foreach (var mot in this.dico.Mots)
        {
            if (!this.Mots.Contains(mot)) /// Vérification que le mot n'a pas déjà été entré
            {        
                if (plateau.Test_Plateau(mot)) /// Appel de la fonction récursive qui cherchera le mot pour chaque case du plateau
                {
                    /// Calculer le score du mot et l'ajouter à la liste
                    int score = this.GetScore(mot);
                    motsEtScores.Add((mot, score));
                }
            }
        }

        /// On crée une liste qui prend les mots avec les scores les plus élevés d'abord et on enlève le score de la liste on passe de : (mot, score) à mot
        res = motsEtScores.OrderByDescending(x => x.Score).Select(x => x.Mot).ToList();

        return res;
    }
}