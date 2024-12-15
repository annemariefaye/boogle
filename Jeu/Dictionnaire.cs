using System;
using System.Collections.Generic;

public class Dictionnaire
{
    ///Attributs
    TriDico dico;
    HashSet<string> mots;
    string langue;


    ///Constructeur
    public Dictionnaire(string langue)
    {
        this.langue = langue;
        this.dico = new TriDico(this.langue);
        this.mots = new HashSet<string>(this.dico.Mots); ///On crée un HashSet() des mots du dictionnaire : Hashset (table de hachage) permet de chercher sans même trier le dictionnaire et ne prend pas en compte les duplicatas -> économie de temps
        this.dico.OrderBy();
    }


    ///Méthodes obligatoires


    /// Cette méthode va reprendre le principe de RechercheBinaire mais en récursif donc un complexité de O(log(n))
    public bool RechDichoRecursif(string mot, int debut = 0, int fin = -2)
    {

        /// this.dico.Mots.Length - 1 ne peut pas être en paramètre donc on initialise fin ici pour simplifer le code dans Jeu.cs
        if (fin == -2)
        {
            fin = this.dico.Mots.Length - 1;
        }


        if (debut > fin)
        {
            return false; /// On arrête l'algorithme car l'intervalle est nul voire inversé
        }


        int millieu = (debut + fin) / 2;
        int reponse = string.Compare(this.dico.Mots[millieu], mot.ToUpper(), StringComparison.Ordinal);

        if (reponse == 0)
        {
            return true;
        }

        else if (reponse < 0)
        {
            return RechDichoRecursif(mot.ToUpper(), millieu + 1, fin); /// +1 car on sait qu'à l'index millieu c'est pas égal donc on enlève une possibilité en plus 
        }

        else
        {
            return RechDichoRecursif(mot.ToUpper(), debut, millieu - 1); /// -1 car on sait qu'à l'index millieu c'est pas égal donc on enlève une possibilité en plus
        }
    }

    
    /// Nous allons créer 2 dictionnaires (commen en python). Le premier sera <char, int> soit le nombre de mots commençant par une lettre. Le deuxième sera <int, int> soit le nombre de mots qui ont un certain nombre de lettres
    public string toString()
    {
        Dictionary<int, int> dictNbLettres = new Dictionary<int, int>();
        Dictionary<char, int> dictInitiales = new Dictionary<char, int>();

        string initiales = "";
        string nbLettres = "";

        foreach(string mot in this.dico.Mots)
        {
            int longueur = mot.Length;
            char initiale = mot[0];

            


            /// Ici on regarde si il y a déjà des mots dans le dico de la longueur de mot, si c'est le cas on ajoute 1 à nombre de mots ayant la longueur de mots. Autrement on créer une nouvelle clé de la longueur du mot et on met sa valeur à 1
            if (dictNbLettres.ContainsKey(longueur))
            {
                dictNbLettres[longueur]++;
            }
            else
            {
                dictNbLettres[longueur] = 1;
            }


            /// On fait pareil mais au lieu de regarder la longueur on fait en fonction de l'initiale.
            if(dictInitiales.ContainsKey(initiale))
            {
                dictInitiales[initiale]++;
            }
            else 
            { 
                dictInitiales[initiale] = 1; 
            }
        }


        var dictNbLettresTrie = dictNbLettres.OrderBy(entry => entry.Key); /// Permet de trier par ordre croissant de longueur
        /// On combine tout dans 2 strings
        foreach (var mot in dictNbLettresTrie)
        {
            initiales += $"Longueur {mot.Key} : {mot.Value} mots\n";
        }

        foreach (var mot in dictInitiales)
        {
            nbLettres += $"Initiale {mot.Key} : {mot.Value} mots\n";
        }


        return $"La langue est : {this.langue}\n\n" + initiales + "\n" + nbLettres;
    }


    /// La meilleur méthode de Recherche est Recherche Hashset car elle a une complexité de O(1) donc elle n'est pas adapté pour un appel récusif et aussi grâce à StopWatch c'est la méthode la plus rapide

    
    /// On va au milieu de la liste de mots et on compare avec le mot recherché et si il est avant, on va dans au milieu de la partie droite de la liste de mots sinon on va à gauche. Et ainsi de suite jusqu'à trouver le bon mot. On divise à chaque fois le nombre de mots à comparé par 2. Soit une compléxité de O(log(n)
    public bool RechercheBinaire(string mot)
    {
        int debut = 0;
        int fin = this.dico.Mots.Length - 1;

        while (debut <= fin) 
        {
            int millieu = (debut + fin) / 2;
            int reponse = string.Compare(this.dico.Mots[millieu], mot.ToUpper() , StringComparison.Ordinal);

            if (reponse == 0)
            {
                return true;
            }

            else if (reponse < 0)
            {
                debut = millieu + 1; /// +1 car on sait qu'à l'index millieu c'est pas égal donc on enlève une possibilité en plus 
            }

            else
            {
                fin = millieu - 1 ; /// -1 car on sait qu'à l'index millieu c'est pas égal donc on enlève une possibilité en plus
            }
        }

        return false;
    }


    /// Vérifie en une ligne si le mot appartient au dictionnaire avec une complexité de O(1)
    public bool RechercheHashSet(string mot)
    {
        return this.mots.Contains(mot.ToUpper());  
    }


    /// Vérifie un à un chaque mot du dictionnaire et a une compléxité de O(n) dans le pire des cas mais si le mot est au début du dictionnaire alors c'est très rapide
    public bool RechercheLineaire(string mot)
    {
        foreach(string m in this.dico.Mots)
        {
            if (string.Compare(m, mot.ToUpper(), StringComparison.Ordinal) == 0)
            {
                return true; /// On a trouvé le mot
            }

            else if (string.Compare(m, mot.ToUpper(), StringComparison.Ordinal) > 0)
            {
                return false; /// Le mot se trouve avant si on suit l'ordre alphabétique donc il ne se trouve pas dans le dico
            }
        }
        return false; /// Le mot n 'est pas dans le dictionnaire
    }
}

