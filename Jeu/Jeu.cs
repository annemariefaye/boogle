using System;
using System.Diagnostics;

class Jeu
{
    static void Main(string[] args)
    {
        ///Sélection de la langue
        ///
        Console.WriteLine("Langue (fr)/Language (en): ");
        string langue = null;

        while (langue != "fr" && langue != "en" )
        {
            langue = Console.ReadLine().ToLower();

        }
        Console.Clear();

        /// Création des joueurs

        

        int nbJoueurs = 0;

        while (nbJoueurs < 2)
        {
            Console.WriteLine("Veuillez entrer le nombre de joueurs (2 minimum) : ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out nbJoueurs))
            {
                if (nbJoueurs < 3)
                {
                    Console.WriteLine("Le nombre de joueurs doit être au moins 2.");
                }
            }
            else
            {
                Console.WriteLine("Veuillez entrer un nombre valide.");
            }
        }
        Console.Clear();
        Console.WriteLine($"Nombre de joueurs accepté : {nbJoueurs}");
    
        

        Joueur[] joueurs = new Joueur[nbJoueurs];

        HashSet<string> nomsUtilises = new HashSet<string>(); // Pour vérifier les noms uniques

        for (int i = 0; i < nbJoueurs; i++)
        {
            string nom;
            do
            {
                Console.WriteLine($"Joueur {i + 1}, entrez votre nom :");
                nom = Console.ReadLine();

                if (nomsUtilises.Contains(nom))
                {
                    Console.WriteLine("Ce nom est déjà utilisé. Veuillez en choisir un autre.");
                }
            } while (nomsUtilises.Contains(nom));

            joueurs[i] = new Joueur(nom);
            nomsUtilises.Add(nom); // Ajoute le nom à la liste des noms utilisés
        }

        Console.Clear();
        Console.WriteLine("Liste des joueurs :");
        foreach (var joueur in joueurs)
        {
            Console.WriteLine(joueur.Pseudo);
        }

        Console.WriteLine();

        /// Creation du plateau

        int taillePlateau = 0;

        do
        {
            Console.WriteLine("Veuillez entrer la taille du plateau (min 4) : ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out taillePlateau) || taillePlateau < 4)
            {
                Console.WriteLine("Entrée invalide. Veuillez entrer un nombre entier supérieur ou égal à 4.");
            }

        } while (taillePlateau < 4);

        Console.Clear();

        Console.WriteLine($"Taille du plateau acceptée : {taillePlateau}");

        Plateau plateau = new Plateau(langue, taillePlateau);


        int nbTours;

        do
        {
            Console.WriteLine("Veuillez entrer le nombre de tours (min 1) : ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out nbTours) || nbTours < 1)
            {
                Console.WriteLine("Entrée invalide. Veuillez entrer un nombre entier supérieur ou égal à 1.");
            }

        } while (nbTours < 1);

        Console.WriteLine($"Nombre de tours accepté : {nbTours}");
        



        /// Création du chrono
        int tempsLimite = 1;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for(int j=0; j< nbTours; j++) 
        { 
            for (int i = 0; i< nbJoueurs; i++)
            {
                Console.Clear();
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
                        joueurs[i].UpdateScore(mot);
                        Console.WriteLine($"Le mot {mot} a rapporté : {joueurs[i].Score}");

                    }
                    /// Conditon pour voir si dansPlateau == true et dejaVu == false
                    /// update le score et afficher le score a chaque tour
                    /// dire il reste combien de temps
                    /// Console.WriteLine(tempsLimite - stopwatch.Elapsed.TotalSeconds);c
                }

                plateau.UpdatePlateau();
                Console.WriteLine($"Le score de {joueurs[i].Pseudo} à la fin du tour est : {joueurs[i].Score}");
                Thread.Sleep(000);
            }
        }
        
        Console.Clear();

        Console.WriteLine("La partie est finie");

        /// Comparer les scores des joueurs 
        int max = joueurs[0].Score;
        string gagnant = joueurs[0].Pseudo;
        HashSet<string> gagnants = new HashSet<string>(); ///Pour éviter les doublons
        for (int i = 0; i < nbJoueurs; i++)
        {
            if (max < joueurs[i].Score)
            {
                max = joueurs[i].Score;
                gagnant = joueurs[i].Pseudo;
                gagnants = new HashSet<string>();
            }

            /// Si égalité des scores maximum
            if(max == joueurs[i].Score)
            {
                gagnants.Add(joueurs[i].Pseudo);
                gagnants.Add(gagnant);
            }
        }
        
        /// Gestion de plusieurs gagnants ou pas
        if(gagnants.Count>=2)
        {
            Console.WriteLine("Le score des vainqueurs est : " + max);
            Thread.Sleep(5000);

            Console.WriteLine("Les gagnant sont : ");
            foreach (string n in gagnants)
            {
                Console.Write($"{n}, ");
            }
        }
        else
        {
            Console.WriteLine("Le score du vainqueur est : " + max);
            Thread.Sleep(5000);

            Console.WriteLine("La vainqueur est : " + gagnant);
        }

        /// Création nuage
        Nuage nuage = new Nuage(joueurs);
        nuage.Creation();
        
       
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