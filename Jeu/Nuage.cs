using KnowledgePicker.WordCloud;
using KnowledgePicker.WordCloud.Coloring;
using KnowledgePicker.WordCloud.Drawing;
using KnowledgePicker.WordCloud.Layouts;
using KnowledgePicker.WordCloud.Primitives;
using KnowledgePicker.WordCloud.Sizers;
using SkiaSharp;
using System.IO;
using System.Collections.Generic;

class Nuage
{
    ///Attributs
    Joueur[] joueurs;
    List<string> mots = new List<string>();
    List<int> taille = new List<int>();



    ///Constructeur
    public Nuage(Joueur[] joueurs)
    {
        this.joueurs = joueurs;
       
    }

    public void Creation()
    {

        var frequences = new Dictionary<string, int>();

        foreach (var joueur in this.joueurs)
        {
            foreach (string mot in joueur.Mots)
            {
                frequences[mot] = joueur.GetScore(mot);
            }
        }

        /// Pour cette partie nous avons suivie la documentation de KnowledgePicker.WordCloud
        const int facteur = 4; 
        var wc = new WordCloudInput(
            frequences.Select(variable => new WordCloudEntry(variable.Key, variable.Value)))
        {
            Width = 1024 * facteur,
            Height = 1024 * facteur,
            MinFontSize = 8 * facteur,
            MaxFontSize = 32 * facteur
        };
        var calibreur = new LogSizer(wc);
        using var engin = new SkGraphicEngine(calibreur, wc);
        var plan = new SpiralLayout(wc);
        var colorisateur = new RandomColorizer();
        var generator = new WordCloudGenerator<SKBitmap>(wc, engin, plan, colorisateur);

        /// Dessing de l'arrière plan en blanc
        using var res = new SKBitmap(wc.Width, wc.Height);
        using var image = new SKCanvas(res);
        image.Clear(SKColors.White);

        using var bitmap = generator.Draw();
        image.DrawBitmap(bitmap, 0, 0);

        /// Sauvegarder dans le dossier de travail
        var dossierTravail = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
        var nom = "Cloud.png";
        var chemin = Path.Combine(dossierTravail, nom);

        /// Sauvegarder l'image
        using var donnees = res.Encode(SKEncodedImageFormat.Png, 100);
        using var flux = File.OpenWrite(chemin);
        donnees.SaveTo(flux);

        
        Console.WriteLine("Nuage sauvegardé");
    }
}
    
