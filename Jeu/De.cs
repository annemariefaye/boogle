public class De
{
    ///Attributs
    char[] lettres;
    char face;
    Random random = new Random();

    ///Constructeur
    public De(char[] lettres)
    {
        this.lettres = lettres;
        int randomIndex = random.Next(this.lettres.Length);
        this.face = this.lettres[randomIndex];

    }
    ///Propriétés de lecture
    public char[] Lettres { get { return this.lettres; } }
    public char Face { get { return this.face; } }

    ///Méthodes obligatoires

    public void Lance(Random r)
    {
        int randomIndex = r.Next() % 6; ///Calcul manuel d'un index aléatoire
        this.face = this.lettres[randomIndex];
    }

    /// On retourne les 6 lettres du dé ainsi qude sa face visible
    public string toString()
    {
        string facesString = "";
        for (int i = 0; i < this.lettres.Length; i++)
        {
            facesString += this.lettres[i];
            if (i < this.lettres.Length - 1)
            {
                facesString += ", ";
            }
        }
        return "Dé: Faces " + facesString + ", Face visible: " + this.face;

    }

}