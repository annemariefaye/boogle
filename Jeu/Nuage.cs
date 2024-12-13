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

        /// Pour cette partie nous avons suivie la documentation de WordCloud
        const int k = 4; 
        var wordCloud = new WordCloudInput(
            frequences.Select(p => new WordCloudEntry(p.Key, p.Value)))
        {
            Width = 1024 * k,
            Height = 1024 * k,
            MinFontSize = 8 * k,
            MaxFontSize = 32 * k
        };
        var sizer = new LogSizer(wordCloud);
        using var engine = new SkGraphicEngine(sizer, wordCloud);
        var layout = new SpiralLayout(wordCloud);
        var colorizer = new RandomColorizer();
        var wcg = new WordCloudGenerator<SKBitmap>(wordCloud, engine, layout, colorizer);

        /// Dessing de l'arrière plan en blanc
        using var final = new SKBitmap(wordCloud.Width, wordCloud.Height);
        using var canvas = new SKCanvas(final);
        canvas.Clear(SKColors.White);

        using var bitmap = wcg.Draw();
        canvas.DrawBitmap(bitmap, 0, 0);

        /// Sauvegarder dans le dossier de travail
        var sourceFileDirectory = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
        var fileName = "Cloud.png";
        var filePath = Path.Combine(sourceFileDirectory, fileName);

        /// Sauvegarder l'image
        using var data = final.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = File.OpenWrite(filePath);
        data.SaveTo(stream);

        
        Console.WriteLine("Nuage sauvegardé");
    }
}
    
