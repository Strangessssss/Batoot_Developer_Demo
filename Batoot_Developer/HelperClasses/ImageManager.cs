using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Windows;
using FontStyle = System.Drawing.FontStyle;

namespace Batoot_Developer.HelperClasses;

public static class ImageManager
{
    private static readonly List<Color> Colors = new()
    {
        Color.BlueViolet,
        Color.Aqua,
        Color.Blue,
        Color.Red,
        Color.Chartreuse,
        Color.Gold,
        Color.Fuchsia,
    };
    public static void Draw(string? projectName)
    {
        var bitmap = new Bitmap(100, 100);
        
        var color = Colors[new Random().Next(Colors.Count)];
        for (var x = 0; x < bitmap.Width; x++)
        {
            for (var y = 0; y < bitmap.Height; y++)
            {
                bitmap.SetPixel(x, y, color);
            }
        }
        
        var graphics = Graphics.FromImage(bitmap);
        var brush = new SolidBrush(Color.White);
        var arial = new Font("Impact", 40, FontStyle.Regular);
        var text = $"{projectName?.First()}{projectName?.Last()}";

        var rectangle = new Rectangle(5, 7, bitmap.Width, bitmap.Height);

        graphics.DrawString(text, arial, brush, rectangle);


        if (!Directory.Exists("Icons"))
            Directory.CreateDirectory("Icons");
        bitmap.Save($"Icons/{projectName}.png");
    }
}