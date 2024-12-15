using System;
using System.Diagnostics;

namespace Jeu 
{
    public class Jeu
    {
        static void Main(string[] args)
        {

            ///Sélection de la langue
            ///
            Console.WriteLine("Langue (fr)/Language (en): ");
            string langue = null;

            while (langue != "fr" && langue != "en")
            {
                langue = Console.ReadLine().ToLower();
                if (langue != "fr" && langue != "en")
                {
                    Console.WriteLine("Saissisez une langue valide");
                }

            }
            Console.Clear();


            int nbJoueurs = 0;

            Console.WriteLine("Voulez-vous jouer avec une IA ? : (Y/N)");
            string YN = null;

            while (YN != "Y" && YN != "N")
            {
                YN = Console.ReadLine().ToUpper();
                if (YN != "Y" && YN != "N")
                {
                    Console.WriteLine("Saissisez 'Y' ou 'N'");
                }
            }

            if (YN == "Y")
            {
                nbJoueurs++;
            }
            Console.Clear();

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

            int indexCorrection = 0;

            /// Création des joueurs
            if (YN == "Y")
            {
                while (nbJoueurs - 1 < 1)
                {
                    Console.WriteLine("Veuillez entrer le nombre de joueurs : ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int joueursSup))
                    {
                        if (joueursSup > 0)
                        {
                            nbJoueurs += joueursSup; /// Ajoute les joueurs supplémentaires
                        }
                        else
                        {
                            Console.WriteLine("Veuillez entrer un nombre positif.");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Veuillez entrer un nombre valide.");
                    }
                }
                Console.Clear();
                Console.WriteLine($"Nombre de joueurs accepté : {nbJoueurs}");
                indexCorrection = -1;
            }

            else
            {
                while (nbJoueurs < 2)
                {
                    Console.WriteLine("Veuillez entrer le nombre de joueurs (2 minimum) : ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out nbJoueurs))
                    {
                        Console.WriteLine("Le nombre de joueurs doit être au moins 2.");
                    }
                    else
                    {
                        Console.WriteLine("Veuillez entrer un nombre valide.");
                    }
                }
                Console.Clear();
                Console.WriteLine($"Nombre de joueurs accepté : {nbJoueurs}");

            }

            Joueur[] joueurs = new Joueur[nbJoueurs];



            HashSet<string> nomsUtilises = new HashSet<string>(); /// Pour vérifier si les noms sont uniques

            for (int i = 0; i < nbJoueurs + indexCorrection; i++)
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
                nomsUtilises.Add(nom); /// Ajoute le nom à la liste des noms utilisés
            }

            Console.Clear();

            /// On crée l'IA
            IA monIA = new IA(plateau, langue);
            if (YN == "Y")
            {
                joueurs[joueurs.Length - 1] = monIA;
            }


            Console.WriteLine("Liste des joueurs :");
            foreach (var joueur in joueurs)
            {
                Console.WriteLine(joueur.Pseudo);
            }

            Console.WriteLine();



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
            int tempsLimite = 10;  /// En secondes
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int j = 0; j < nbTours; j++)
            {
                foreach (Joueur joueur in joueurs)
                {
                    Console.Clear();
                    Console.WriteLine("Tour " + j + 1);

                    Console.WriteLine($"C'est au tour de {joueur.Pseudo}");

                    plateau.AfficherPlateau();
                    stopwatch.Restart();


                    while (stopwatch.Elapsed.TotalSeconds < tempsLimite)
                    {
                        string mot;
                        if (joueur.Pseudo != "IA")
                        {
                            Console.WriteLine("Saisissez un nouveau mot");
                            mot = Console.ReadLine().ToUpper();
                        }

                        else
                        {
                            mot = monIA.MotIA();
                            Console.Clear();
                            Console.WriteLine(mot + " a été choisi par l'IA.");
                            Thread.Sleep(2000);
                        }


                        bool dansPlateau = plateau.Test_Plateau(mot);
                        bool dejaVu = joueur.Contain(mot);
                        if (!dejaVu && dansPlateau)
                        {
                            joueur.Add_Mot(mot);
                            joueur.UpdateScore(mot);
                            Console.WriteLine($"Le mot {mot} a rapporté : {joueur.GetScore(mot)}");

                        }

                    }

                    plateau.UpdatePlateau();
                    Console.WriteLine($"Le score de {joueur.Pseudo} à la fin du tour est : {joueur.Score}");
                    Thread.Sleep(3000);
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
                if (max == joueurs[i].Score)
                {
                    gagnants.Add(joueurs[i].Pseudo);
                    gagnants.Add(gagnant);
                }
            }

            /// Gestion de plusieurs gagnants ou pas
            if (gagnants.Count >= 2)
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
        }


    }
}
