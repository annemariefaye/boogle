using System;
using System.Diagnostics;

class Jeu
{
    static void Main(string[] args)
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

        des[0] = new De(new char[] { 'A', 'D', 'I', 'G', 'Y', 'T' });
        des[1] = new De(new char[] { 'B', 'F', 'L', 'P', 'O', 'Z' });
        des[2] = new De(new char[] { 'C', 'M', 'R', 'S', 'K', 'J' });
        des[3] = new De(new char[] { 'E', 'H', 'N', 'Q', 'W', 'X' });
        des[4] = new De(new char[] { 'U', 'V', 'Y', 'T', 'F', 'Z' });
        des[5] = new De(new char[] { 'A', 'D', 'I', 'G', 'I', 'Y' });

        Plateau plateau = new Plateau("fr", des);
        string motExiste = "elle";
        Console.WriteLine($"{motExiste} est dans le plateau : " + plateau.Test_Plateau(motExiste));
        motExiste = "le";
        Console.WriteLine($"{motExiste} est dans le plateau : " + plateau.Test_Plateau(motExiste));
    }

}