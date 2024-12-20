using System;
using System.Diagnostics;
using System.Threading;
using NAudio.Wave;
using ConsoleAppVisuals;
using ConsoleAppVisuals.PassiveElements;
using ConsoleAppVisuals.InteractiveElements;
using ConsoleAppVisuals.AnimatedElements;
using ConsoleAppVisuals.Enums;

namespace Jeu 
{
    public class Jeu

    {
        private static WaveOutEvent _dispositifSortie;
        private static AudioFileReader _fichierAudio;
        private static bool _isPlaying;

        static async Task Main(string[] args)
        {

            Window.Open();
            Title titre = new Title("BOOGLE");
            Window.AddElement(titre);
            Window.Render(titre);

            Text  texteSuivant = new Text(new List<string> { "Appuyez sur 'ENTRER' pour continuer..." }, TextAlignment.Left, Placement.BottomCenterFullWidth);
            Window.AddElement(texteSuivant);
            Window.ActivateElement(texteSuivant);
            Window.Freeze();
            Window.DeactivateElement(texteSuivant, true);


            /// Chemins des différentes musiques ou effets sonores du jeu
            string goofyMusic = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "goofy.mp3"));
            string cancanMusic = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "cancan.mp3"));
            string marioMusic = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "mario.mp3"));
            string candyMusic = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "candy.mp3"));
            string megaMusic = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "mega.mp3"));
            string starMusic = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "star.mp3"));
            string zeldaMusic = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "zelda.mp3"));
            string dreamMusic = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "dream.mp3"));

            /// Chemins des différents effets sonores du jeu
            string correctSFX = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "correct.mp3"));
            string wrongSFX = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "wrong.mp3"));
            string tambourSFX = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "tambour.mp3"));
            string victorySFX = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "victory.mp3"));


            string backgroundMusic = "";


            string[] options = new string[] { "Goofy", "Cancan", "Mario Kart", "Candy Crush", "Megalovania", "STAR !", "Zelda" };
            ScrollingMenu menu = new ScrollingMenu(
                "Veuillez sélectionner votre musique",
                0,
                Placement.TopCenter,
                options
            );

            Window.AddElement(menu);
            Window.ActivateElement(menu);

            var responseMenu = menu.GetResponse();

            Dialog embedResponse = new Dialog(
                new List<string>()
                {
                    $"Vous avez sélectionné: {options[responseMenu!.Value]}",
                       
                }
            );

            Window.ActivateElement(texteSuivant);


            Window.AddElement(embedResponse);
            Window.ActivateElement(embedResponse);

            /// On récupère le choix de l'utilisateur
            string choixMusique = (responseMenu!.Value + 1).ToString();

            /// Switch pour sélectionner la musique à jouer
            switch (choixMusique)
            {
                case "1":
                    backgroundMusic = goofyMusic;
                    break;
                case "2":
                    backgroundMusic = cancanMusic;
                    break;
                case "3":
                    backgroundMusic = marioMusic;
                    break;
                case "4":
                    backgroundMusic = candyMusic;
                    break;
                case "5":
                    backgroundMusic = megaMusic;
                    break;
                case "6":
                    backgroundMusic = starMusic;
                    break;
                case "7":
                    backgroundMusic = zeldaMusic;
                    break;
            }


            Window.DeactivateAllElements();


            /// Lancer la lecture audio dans un tâche séparée
            Task.Run(() => PlayAudioLoop(backgroundMusic));



            ///Sélection de la langue
            options = new string[] { "Français", "Anglais"};
            menu = new ScrollingMenu(
                "Veuillez choisir la langue du jeu : ",
                0,
                Placement.TopCenter,
                options
            );

            Window.AddElement(menu);
            Window.ActivateElement(menu);

            responseMenu = menu.GetResponse();

            embedResponse = new Dialog(
                new List<string>()
                {
                    $"Vous avez sélectionné: {options[responseMenu!.Value]}",

                }
            );

            Window.ActivateElement(texteSuivant);


            Window.AddElement(embedResponse);
            Window.ActivateElement(embedResponse);

            string langue = null;

            if (responseMenu!.Value == 0)
            {
                langue = "fr";
            }
            else
            {
                langue = "en";
            }

            Window.DeactivateAllElements();

            int nbJoueurs = 0; ///On initialise à 0 ici car on en a besoin si les utilisateurs sélectionnent l'IA car elle comptera comme joueur.

            /// On choisi si on joue avec une IA ou non
            options = new string[] { "OUI", "NON"};
            menu = new ScrollingMenu(
                "Voulez-vous jouer avec une IA : ",
                0,
                Placement.TopCenter,
                options
            );

            Window.AddElement(menu);
            Window.ActivateElement(menu);

            responseMenu = menu.GetResponse();


            Window.DeactivateAllElements();

            string YN = null;
            int indexCorrection = 0; /// Si on choisi l'IA on en aura besoin pour le choix des pseudos, car l'IA à déjà un nom sélectionné par défaut donc pas besoin de l'entre manuellement


            if (responseMenu!.Value == 0)
            {
                YN = "Y";
                nbJoueurs++; ///L'IA compte comme joueur
                indexCorrection = -1;
            }
            else
            {
                YN = "N";
            }
            

            /// Creation du plateau

            int taillePlateau = 0;

            options = new string[]
            {
                "4x4", "5x5",
                "6x6", "7x7", "8x8", "9x9", "10x10"
            };
            menu = new ScrollingMenu(
                "Veuillez choisir la taille du plateau : ",
                0,
                Placement.TopCenter,
                options
            );

            Window.AddElement(menu);
            Window.ActivateElement(menu);

            responseMenu = menu.GetResponse();

            embedResponse = new Dialog(
                new List<string>()
                {
                    $"Vous avez sélectionné un plateau de taille: {options[responseMenu!.Value]}",

                }
            );

            Window.ActivateElement(texteSuivant);


            Window.AddElement(embedResponse);
            Window.ActivateElement(embedResponse);

            taillePlateau = responseMenu!.Value + 4;


            Window.DeactivateAllElements();

            Plateau plateau = new Plateau(langue, taillePlateau);


            /// Création des joueurs en fonction de si on choisi de jouer avec l'IA ou non
            
            if(YN == "Y") 
            {
                options = new string[]
                {
                    "1", "2", "3", "4", "5", "6", "7", "8"
                };
            }
            else
            {
                options = new string[]
                {
                    "2", "3", "4", "5", "6", "7", "8"
                };
            }
            
            menu = new ScrollingMenu(
                "Veuillez choisir le nombre de joueur : ",
                0,
                Placement.TopCenter,
                options
            );

            Window.AddElement(menu);
            Window.ActivateElement(menu);

            responseMenu = menu.GetResponse();

            embedResponse = new Dialog(
                new List<string>()
                {
                        $"Le nombre de joueurs durant la partie est de : {options[responseMenu!.Value]}",

                }
            );

            Window.ActivateElement(texteSuivant);


            Window.AddElement(embedResponse);
            Window.ActivateElement(embedResponse);

            if(YN == "Y")
            {
                nbJoueurs += responseMenu!.Value + 1;
            }
            else
            {
                nbJoueurs = responseMenu!.Value + 2;
            }
            

            Window.DeactivateAllElements();
            

            Joueur[] joueurs = new Joueur[nbJoueurs];



            HashSet<string> nomsUtilises = new HashSet<string>(); /// Pour vérifier si les noms sont uniques

            /// Choix des pseudo pour chaque joueur (indiceCorrection sert uniquement si on a choisi de joueur avec l'IA)
            for (int i = 0; i < nbJoueurs + indexCorrection; i++)
            {
                string nom;
                do
                {

                    Prompt prompt = new Prompt($"Joueur {i+1} entrez votre pseudo (IA est un pseudo interdit) :");
                    Window.AddElement(prompt);
                    Window.ActivateElement(prompt);

                    var response = prompt.GetResponse();

                    nom = response!.Value;

                    if (nomsUtilises.Contains(nom.ToUpper()))
                    {
                        Dialog text = new Dialog(
                        new List<string>()
                        {
                            response!.Value + " a déjà été choisi !",
                            "Veuillez choisir un nouveau pseudo "
                        },
                        null,
                        "OK"
                        );
                        Window.AddElement(text);
                        Window.ActivateElement(text);
                    }
                    if (nom.ToUpper() == "IA")
                    {
                        Dialog text = new Dialog(
                        new List<string>()
                        {
                            response!.Value + " est un pseudo interdit !",
                            "Veuillez choisir un nouveau pseudo "
                        },
                        null,
                        "OK"
                        );
                        Window.AddElement(text);
                        Window.ActivateElement(text);
                    }
                } while (nomsUtilises.Contains(nom.ToUpper()) || nom == "IA");

                joueurs[i] = new Joueur(nom, langue);
                nomsUtilises.Add(nom.ToUpper()); /// Ajoute le nom à la liste des noms utilisés
            }

            Console.Clear();

            /// Instantiation de l'IA. Cela est effectué dans tous les cas pour garantir l'absence d'erreurs dans Visual Studio.
            IA monIA = new IA(plateau, langue);
            if (YN == "Y")
            {
                joueurs[joueurs.Length - 1] = monIA;
            }

            /// On affiche tous les joueurs
            List<string> titreJoueur = new List<string>() {"Liste des joueurs : "};
            EmbedText affichage = new EmbedText(titreJoueur, TextAlignment.Left, Placement.TopCenter, BordersType.SingleStraight);
            

            List<string> jListe = new List<string>();


            foreach (Joueur joueur in joueurs)
            {
                jListe.Add(joueur.Pseudo);
            }

            Text jAffichage = new Text(jListe, TextAlignment.Left, Placement.TopCenter);
            
            Window.ActivateElement(texteSuivant);

            Window.AddElement(affichage);
            Window.ActivateElement(affichage);
            Window.AddElement(jAffichage);
            Window.ActivateElement(jAffichage);
            
            Window.Freeze();

            Window.DeactivateAllElements();




            /// Sélection du nombre de tour
            int nbTours = 0;
            
            options = new string[]
              {
                    "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
              };

            menu = new ScrollingMenu(
                "Veuillez choisir le nombre de tours : ",
                0,
                Placement.TopCenter,
                options
            );

            Window.AddElement(menu);
            Window.ActivateElement(menu);

            responseMenu = menu.GetResponse();

            embedResponse = new Dialog(
                new List<string>()
                {
                        $"Le nombre de tours durant la partie est de : {options[responseMenu!.Value]}",

                }
            );

            Window.ActivateElement(texteSuivant);


            Window.AddElement(embedResponse);
            Window.ActivateElement(embedResponse);

            nbTours += responseMenu!.Value + 1;

            Window.DeactivateAllElements();



            /// Sélection du temps par tour 
            int tempsLimite = 60;  /// En secondes

            do
            {
                Prompt prompt = new Prompt("Veuillez entrer le temps par tour de 10s à 300s : ");
                Window.AddElement(prompt);
                Window.ActivateElement(prompt);

                var response = prompt.GetResponse();

                string input = response!.Value;

                if (!int.TryParse(input, out tempsLimite) || tempsLimite < 10)
                {
                    Dialog text = new Dialog(
                       new List<string>()
                       {
                            response!.Value + " est une entrée invalide. Veuillez entrer un nombre entier supérieur ou égal à 10.",
                       },
                       null,
                       "OK"
                       );
                    Window.AddElement(text);
                    Window.ActivateElement(text);
                }
                if (!int.TryParse(input, out tempsLimite) || tempsLimite > 300)
                {
                    Dialog text = new Dialog(
                      new List<string>()
                      {
                            response!.Value + " est une entrée invalide. Veuillez entrer un nombre entier inféirieur ou égal à 300.",
                      },
                      null,
                      "OK"
                      );
                    Window.AddElement(text);
                    Window.ActivateElement(text);
                }

            } while (tempsLimite < 10 || tempsLimite > 301);

            Window.DeactivateAllElements();

            Console.Clear();


            ///regler chrono si on depasse temps, le mot pas valide

            /// Création du chrono
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            /// Ici se trouve toute la méchanique du jeu
            /// Chaque joueur aura un temps limité pour trouver un maximum de mots puis on passe au joueur suivant et ainsi de suite
            /// Le plateau est randomisé à chaque changement de joueurs pour un maximum d'équité
            for (int j = 0; j < nbTours; j++)
            {
                foreach (Joueur joueur in joueurs)
                {
                    Console.Clear();
                    Console.WriteLine("Tour " + (j + 1));

                    Console.WriteLine($"C'est au tour de {joueur.Pseudo}");


                    stopwatch.Restart();

                    List<string> mots = null;
                    while (stopwatch.Elapsed.TotalSeconds < tempsLimite)
                    {
                        plateau.AfficherPlateau(); ///On réaffiche le tableau car il ne sera plus visible si beaucoup de mots sont entrés
                        string mot;

                        if (joueur.Pseudo != "IA")
                        {

                            Console.WriteLine();
                            Console.WriteLine("Saisissez un nouveau mot");
                            mot = Console.ReadLine().ToUpper();

                            if (stopwatch.Elapsed.TotalSeconds > tempsLimite)
                            {
                                Console.WriteLine($"Temps écoulé {mot} ne sera pas validé.");
                                Thread.Sleep(3000);
                                break;
                            }

                            Console.Clear();
                            bool dansPlateau = plateau.Test_Plateau(mot);
                            bool dejaVu = joueur.Contain(mot);
                            if (!dejaVu && dansPlateau)
                            {
                                PlayAudioAsync(correctSFX);
                                joueur.Add_Mot(mot);
                                Console.Clear();
                                Console.WriteLine($"Le mot {mot} a rapporté : {joueur.GetScore(mot)}");
                                Console.WriteLine();

                            }
                            else
                            {
                                PlayAudioAsync(wrongSFX);
                            }


                            if (joueur.Mots.Length > 0)
                            {
                                Console.WriteLine("Les mots valides saisis sont :");
                                foreach (string m in joueur.Mots)
                                {
                                    Console.Write(m + " ");
                                }
                                Console.WriteLine();
                                Console.WriteLine();

                            }
                        }

                        else
                        {
                            if (mots != null)
                            {
                                foreach (string m in mots)
                                {
                                    if (stopwatch.Elapsed.TotalSeconds > tempsLimite)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Les mots valides saisis par l'IA sont :");
                                        foreach (string mt in joueur.Mots)
                                        {
                                            Console.Write(mt + " ");

                                        }
                                        Thread.Sleep(3000);
                                        break;
                                    }
                                    joueur.Add_Mot(m);

                                }
                            }
                            else
                            {
                                mots = monIA.MotsIA();
                            }
                        }

                    }

                    Console.WriteLine();
                    plateau.UpdatePlateau();
                    Thread.Sleep(3000); /// Petite pause pour voir le score du joueur avant de Clear() la console
                }
            }

            Console.Clear();

            Console.WriteLine("La partie est finie");

            /// Comparer les scores des joueurs 
            int max = joueurs[0].Score;
            string gagnant = joueurs[0].Pseudo;
            HashSet<string> gagnants = new HashSet<string>(); /// Pour éviter les doublons
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

            /// On stoppe la musique de fin pour lancer le roulement de tambour
            StopAudio();



            /// Gestion de plusieurs gagnants ou pas
            if (gagnants.Count >= 2)
            {
                Console.WriteLine("Le score des vainqueurs est : " + max);
                PlayAudioAsync(tambourSFX);
                Thread.Sleep(5000);

                Console.WriteLine("Les gagnant sont : ");
                foreach (string n in gagnants)
                {
                    Console.Write($"{n}, ");
                }
                PlayAudioAsync(victorySFX);
                Thread.Sleep(10000);

            }
            else
            {
                Console.WriteLine("Le score du vainqueur est : " + max);
                PlayAudioAsync(tambourSFX);
                Thread.Sleep(5000);

                Console.WriteLine("La vainqueur est : " + gagnant);
                PlayAudioAsync(victorySFX);
                Thread.Sleep(10000);


            }

            Console.WriteLine();

            foreach (var joueur in joueurs)
            {
                Console.WriteLine($"{joueur.Pseudo} a marqué {joueur.Score} points durant la partie");
            }

            Console.WriteLine();

            /// Création nuage
            Nuage nuage = new Nuage(joueurs);
            nuage.Creation();

            Task.Run(() => PlayAudioLoop(dreamMusic));

            Console.WriteLine();

            Console.WriteLine("Appuyez sur 'Q' pour quitter.");

            /// Boucle infinie tant que 'Q' n'est pas pressé
            while (true)
            {
                if (Console.KeyAvailable) /// Vérifie si une touche a été pressée
                {
                    var key = Console.ReadKey(intercept: true).Key; // Récupère la touche sans l'afficher
                    if (key == ConsoleKey.Q) /// Vérifie si c'est 'Q'
                    {
                        Console.WriteLine("Vous avez appuyé sur 'Q'. Fin du programme.");
                        break; /// Sort de la boucle
                    }
                }
            }



        }


        /// Fonction asynchrone pour jouer de la musique en même temps que de jouer au jeu
        static async Task PlayAudioAsync(string chemin)
        {
            Task.Run(() =>
            {
                using (var fichierAudio = new AudioFileReader(chemin))
                using (var dispositifSortie = new WaveOutEvent())
                {
                    dispositifSortie.Init(fichierAudio);
                    dispositifSortie.Play();

                    /// On attend que la musique finisse
                    while (dispositifSortie.PlaybackState == PlaybackState.Playing)
                    {
                        Task.Delay(100).Wait(); /// On évite de bloquer le thread principal
                    }
                }
            });
        }

        /// Fonction asynchrone pour jouer de la musique en boucle en même temps que de jouer au jeu

        static void PlayAudioLoop(string chemin)
        {
            _fichierAudio = new AudioFileReader(chemin);
            _dispositifSortie = new WaveOutEvent();
            _dispositifSortie.Init(_fichierAudio);
            _dispositifSortie.Play();
            _isPlaying = true; /// Indique que la musique est en lecture

            /// Événement pour jouer la musique en boucle
            _dispositifSortie.PlaybackStopped += (sender, args) =>
            {
                if (_isPlaying)
                {
                    _fichierAudio.Position = 0; /// Remet le fichier audio au début
                    _dispositifSortie.Play(); /// Rejoue la musique
                }
            };

            /// Attendre que la musique finisse ou qu'elle soit arrêtée
            while (_isPlaying)
            {
                /// Vérifie si la musique est toujours en lecture
                if (_dispositifSortie.PlaybackState != PlaybackState.Playing)
                {
                    break; /// Sort de la boucle si la musique s'arrête
                }
                Task.Delay(100).Wait(); /// Petite pause pour éviter de bloquer le thread principal
            }
        }

        static void StopAudio()
        {
            /// Vérifie si le dispositif de sortie est initialisé avant d'arrêter
            if (_dispositifSortie != null)
            {
                _isPlaying = false; /// Indique que la musique ne doit plus jouer
                _dispositifSortie.Stop();
                _dispositifSortie.Dispose(); /// Libère les ressources
                _fichierAudio.Dispose(); /// Libère les ressources
                _dispositifSortie = null; /// Réinitialise la référence
                _fichierAudio = null; /// Réinitialise la référence
            }
        }


        /// Test des 3 méthodes de recherche dans le dictionnaire avec un chronomètre pour regarder la méthode la plus efficace.
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
