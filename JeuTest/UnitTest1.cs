using Jeu;

namespace JeuTest

{
    /// Voir les détails de ce fichier dans le rapport
    [TestClass]
    public class UnitTest1
    {
        /// Méthode de la classe Joueur
        [TestMethod]
        public void TestGetScore()
        {
            Joueur joueur = new Joueur("j1", "fr");
            string motTest = "bonjour";
            float coeff = 1 + (motTest.Length / 10.0f) - 0.2f;
            int attendu = (int)Math.Round(16 * coeff);

            int obtenu = joueur.GetScore(motTest);

            Assert.AreEqual(attendu, obtenu);
        }

        /// Méthode de la classe Joueur
        [TestMethod]
        public void TestContain()
        {
            Joueur joueur = new Joueur("j1", "fr");
            string motTest = "bonjour";
            bool attendu = true;
            
            joueur.Add_Mot(motTest);

            bool obtenu = joueur.Contain(motTest);

            Assert.AreEqual(attendu, obtenu);
        }

        /// Méthode de la classe Plateau
        [TestMethod]
        public void TestTestPlateau()
        {
            Plateau plateau = new Plateau("fr", 4);
            plateau.Grille = new char[4, 4]
            {
                { 'A', 'B', 'C', 'P' },
                { 'J', 'E', 'T', 'E' },
                { 'O', 'H', 'U', 'N' },
                { 'G', 'X', 'I', 'E' },
            };

            string motTest = "JEUX";
            bool attendu = true;
            bool obtenu = plateau.Test_Plateau(motTest);

            Assert.AreEqual(attendu, obtenu);

        }

        /// Méthode de la classe Dictionnaire
        [TestMethod]
        public void TestRechDichoRecursif()
        {
            Dictionnaire dico = new Dictionnaire("fr");

            string motTest = "khusdfkhuzfe";
            bool attendu = false;

            bool obtenu = dico.RechDichoRecursif(motTest);

            Assert.AreEqual(attendu, obtenu);
        }

        /// Méthode de la classe Dictionnaire
        [TestMethod]
        public void TestRechercheHashset()
        {
            Dictionnaire dico = new Dictionnaire("fr");

            string motTest = "sorghos";
            bool attendu = true;

            bool obtenu = dico.RechercheHashSet(motTest);

            Assert.AreEqual(attendu, obtenu);
        }

    }
}