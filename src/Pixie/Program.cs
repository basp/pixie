namespace Pixie;

public class Program
{
    private void Example01()
    {
        const int ImageWidth = 256;
        const int ImageHeight = 256;

        var ppm = new Pixmap(ImageWidth, ImageHeight);
        for (var j = ImageHeight - 1; j >= 0; j--)
        {
            Console.WriteLine($"Scanlines remaining: {j}");
            for (var i = 0; i < ImageWidth; i++)
            {
                var r = (double)i / (ImageWidth - 1);
                var g = (double)j / (ImageHeight - 1);
                var b = 0.25;
                ppm[i, j] = new Color<double>(r, g, b);
            }
        }

        using var s = File.OpenWrite(@"D:\temp\Example01.ppm");
        ppm.Write(s);
    }
    
    public static void Main(string[] _)
    {
        var p = new Program();
        p.Example01();
    }
}