using Jeu;

namespace JeuTest

{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetScore()
        {
            Joueur joueur = new Joueur("j1");
            string motTest = "bonjour";
            int attendu = 16;

            int obtenu = joueur.GetScore(motTest);

            Assert.AreEqual(attendu, obtenu);
        }

        [TestMethod]
        public void TestContain()
        {
            Joueur joueur = new Joueur("j1");
            string motTest = "bonjour";
            bool attendu = true;
            
            joueur.Add_Mot(motTest);

            bool obtenu = joueur.Contain(motTest);

            Assert.AreEqual(attendu, obtenu);
        }


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


        [TestMethod]
        public void TestRechDichoRecursif()
        {
            Dictionnaire dico = new Dictionnaire("fr");

            string motTest = "khusdfkhuzfe";
            bool attendu = false;

            bool obtenu = dico.RechDichoRecursif(motTest);

            Assert.AreEqual(attendu, obtenu);
        }


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