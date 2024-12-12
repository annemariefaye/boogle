using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

class TriDico
{
    ///Attributs
    string langue;
    string texte;
    string[] mots;


    ///Constructeur
    public TriDico(string langue)
    {
        this.langue = langue;

        if (langue == "fr")
        {
            string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "MotsPossiblesFR.txt");
            chemin = Path.GetFullPath(chemin);

            this.texte = File.ReadAllText(chemin);
            char[] separateurs = { ' ', '\n'};
            this.mots = this.texte.Split(separateurs, StringSplitOptions.RemoveEmptyEntries);
        }

        else if (langue == "en")
        {
            string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "MotsPossiblesEN.txt");
            chemin = Path.GetFullPath(chemin);

            this.texte = File.ReadAllText(chemin);
            char[] separateurs = { ' ', '\n' };
            this.mots = this.texte.Split(separateurs, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public void OrderBy()
    {
        this.mots = this.mots.OrderBy(f => f).ToArray();
    }

    public string[] Mots { get { return this.mots;} }

    
        

}
