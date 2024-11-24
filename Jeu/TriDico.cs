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
            this.texte = File.ReadAllText("C:\\Users\\annem\\OneDrive\\ESILV\\A2\\S1\\Info\\Algorithmique et programmation orientée objet\\Projet\\Jeu\\Jeu\\MotsPossiblesFR.txt");
            char[] separateurs = { ' ', '\n'};
            this.mots = this.texte.Split(separateurs, StringSplitOptions.RemoveEmptyEntries);
        }

        else if (langue == "en")
        {
            this.texte = File.ReadAllText("C:\\Users\\annem\\OneDrive\\ESILV\\A2\\S1\\Info\\Algorithmique et programmation orientée objet\\Projet\\Jeu\\Jeu\\MotsPossiblesEN.txt");
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
