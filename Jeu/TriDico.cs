using System;
using System.IO;

class TriDico
{
    ///Attributs
    string langue;


    ///Constructeur
    public TriDico(string langue) 
    { 
        this.langue = langue;

        if (langue == "fr")
        {
            string cheminFichier = "MotsPossiblesFR.txt";
            string[] lignes = File.ReadAllLines("C:\\Users\\annem\\OneDrive\\ESILV\\A2\\S1\\Info\\Algorithmique et programmation orientée objet\\Projet\\Jeu\\Jeu\\MotsPossiblesFR.txt");

            foreach (string ligne in lignes)
            {
                Console.WriteLine(ligne);
            }
        }

        else if (langue == "en")
        {
            string cheminFichier = "MotsPossiblesEN.txt";
            string[] lignes = File.ReadAllLines("C:\\Users\\annem\\OneDrive\\ESILV\\A2\\S1\\Info\\Algorithmique et programmation orientée objet\\Projet\\Jeu\\Jeu\\MotsPossiblesEN.txt");

            foreach (string ligne in lignes)
            {
                Console.WriteLine(ligne);
            }
        }
    }
}
