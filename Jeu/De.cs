public class De
{
    ///Attributs
    char[] lettres;
    char face;
    Random random;

    ///Constructeur
    public De(char[] lettres)
    {
        this.lettres = lettres;
        this.random = new Random();
        int randomIndex = random.Next(this.lettres.Length);
        this.face = this.lettres[randomIndex];

    }
    ///propriétés de lecture
    public char[] Lettres { get { return this.lettres; } }
    public char Face { get { return this.face; } }

    ///Méthodes obligatoires

    public void Lance()
    {
        int randomIndex = this.random.Next() % 6; ///calcul manuel d'un index aléatoire
        this.face = this.lettres[randomIndex];


    }

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