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
        /// En fonction de la langue choisi on charge le dictionnaire français ou anglais
        if (this.langue == "fr")
        {
            /// Trouver le fichier dans le dossier de travail
            string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "MotsPossiblesFR.txt");
            chemin = Path.GetFullPath(chemin);

            /// On sépare chaque mot par un espace ou un saut de ligne et on le met dans string mots[]
            this.texte = File.ReadAllText(chemin);
            char[] separateurs = { ' ', '\n'};
            this.mots = this.texte.Split(separateurs, StringSplitOptions.RemoveEmptyEntries);
        }

        /// Même chose que pour langue == 'fr'
        else if (this.langue == "en")
        {
            string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "MotsPossiblesEN.txt");
            chemin = Path.GetFullPath(chemin);

            this.texte = File.ReadAllText(chemin);
            char[] separateurs = { ' ', '\n' };
            this.mots = this.texte.Split(separateurs, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    /// Tri du dictionnaire avec fonction intégrée
    public void OrderBy()
    {
        this.mots = this.mots.OrderBy(f => f).ToArray();
    }

    /// Propriété de lecture
    public string[] Mots { get { return this.mots;} }

    
        

}
