using System;
using System.Diagnostics;

class Jeu
{
    static void Main(string[] args)
    {
        ///Sélection de la langue
        ///
        Console.WriteLine("Langue/Language : ");
        string langue = Console.ReadLine();

        /// Création des joueurs

        Console.WriteLine("Veuillez entrer le nombre de joueurs : ");
        int nbJoueurs = int.Parse(Console.ReadLine());

        Joueur[] joueurs = new Joueur[nbJoueurs];

        for (int i = 0; i < nbJoueurs; i++)
        {
            Console.WriteLine("Entrer votre nom :");
            string n = Console.ReadLine();
            joueurs[i] = new Joueur(n); 
        }

        /// Creation du plateau

        Console.WriteLine("Veuillez entrer la taille du plateau (min 4) : ");
        int taillePlateau = int.Parse(Console.ReadLine());

        Plateau plateau = new Plateau(langue, taillePlateau);

        
        Console.WriteLine("Veuillez entrer le nombre de tours : ");
        int nbTours =int.Parse(Console.ReadLine());


        /// Création du chrono
        int tempsLimite = 10;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for(int j=0; j< nbTours; j++) 
        { 
            for (int i = 0; i< nbJoueurs; i++)
            {
                Console.WriteLine($"C'est au tour de {joueurs[i].Pseudo}"); ///remplacer ToString par pseudo

                plateau.AfficherPlateau();
                stopwatch.Restart();

                while (stopwatch.Elapsed.TotalSeconds < tempsLimite)
                { 
                    Console.WriteLine("Saisissez un nouveau mot");
                    string mot = Console.ReadLine().ToUpper();

                    bool dansPlateau = plateau.Test_Plateau(mot);
                    bool dejaVu = joueurs[i].Contain(mot);
                    if(!dejaVu && dansPlateau)
                    {
                        joueurs[i].Add_Mot(mot);
                    }
                    /// Conditon pour voir si dansPlateau == true et dejaVu == false
                    /// update le score et afficher le score a chaque tour
                    /// dire il reste combien de temps
                    Console.WriteLine(tempsLimite - stopwatch.Elapsed.TotalSeconds);
                }

                plateau.UpdatePlateau();
            }
        }

        Console.WriteLine("La partie est finie");

        /// Comparer les scores des joueurs 
        
        Console.WriteLine("La vainqueur est : ");







    }

    static void TestDico()
    {
        Dictionnaire dico = new Dictionnaire("fr");

        Console.WriteLine(dico.toString());

        string vraiMot = "bonjour";
        string fauxMot = "hjehrj";


        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        Console.WriteLine($"{vraiMot} exite dans RechercheLinéraire : " + dico.RechercheLineaire(vraiMot));
        stopwatch.Stop();
        Console.WriteLine("Temps écoulé : " + stopwatch.ElapsedTicks + " ticks");

        stopwatch.Restart();
        Console.WriteLine($"{vraiMot} exite dans RechercheBinaire : " + dico.RechercheBinaire(vraiMot));
        stopwatch.Stop();
        Console.WriteLine("Temps écoulé : " + stopwatch.ElapsedTicks + " ticks");

        stopwatch.Restart();
        Console.WriteLine($"{vraiMot} exite dans RechercheHashSet : " + dico.RechercheHashSet(vraiMot));
        stopwatch.Stop();
        Console.WriteLine("Temps écoulé : " + stopwatch.ElapsedTicks + " ticks");

        stopwatch.Restart();
        Console.WriteLine($"{vraiMot} exite dans RecherDicoRécursif : " + dico.RechDichoRecursif(vraiMot));
        stopwatch.Stop();
        Console.WriteLine("Temps écoulé : " + stopwatch.ElapsedTicks + " ticks");

        Console.WriteLine();

        Console.WriteLine($"{fauxMot} exite dans RechercheLinéraire : " + dico.RechercheLineaire(fauxMot));
        Console.WriteLine($"{fauxMot} exite dans RechercheBinaire : " + dico.RechercheBinaire(fauxMot));
        Console.WriteLine($"{fauxMot} exite dans RechercheHashSet : " + dico.RechercheHashSet(fauxMot));
        Console.WriteLine($"{fauxMot} exite dans RecherDicoRécursif : " + dico.RechDichoRecursif(fauxMot));


        De[] des = new De[6];

        des[0] = new De(new char[] { 'E', 'L', 'I', 'L', 'A', 'T' });
        des[1] = new De(new char[] { 'B', 'F', 'L', 'P', 'O', 'Z' });
        des[2] = new De(new char[] { 'C', 'M', 'R', 'S', 'K', 'J' });
        des[3] = new De(new char[] { 'E', 'H', 'N', 'Q', 'W', 'X' });
        des[4] = new De(new char[] { 'U', 'V', 'Y', 'T', 'F', 'Z' });
        des[5] = new De(new char[] { 'A', 'D', 'I', 'G', 'I', 'Y' });

        Plateau plateau = new Plateau("fr", 5);
        string motExiste = "elle";
        Console.WriteLine($"{motExiste} est dans le plateau : " + plateau.Test_Plateau(motExiste));
        motExiste = "le";
        Console.WriteLine($"{motExiste} est dans le plateau : " + plateau.Test_Plateau(motExiste));
    }


}