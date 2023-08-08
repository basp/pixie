namespace Pixie;

public class Program
{
    private void Example01()
    {
        var ppm = new Pixmap(256, 256);
        for (var j = ppm.Height - 1; j >= 0; j--)
        {
            Console.WriteLine($"Scanlines remaining: {j}");
            for (var i = 0; i < ppm.Width; i++)
            {
                var r = (double)i / (ppm.Width - 1);
                var g = (double)j / (ppm.Height - 1);
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