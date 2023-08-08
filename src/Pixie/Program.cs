namespace Pixie;

/*
 We might want to include some benchmarks later. For now we don't have much to 
 compare against since we already optimized vs Linie/Pixie1 by using 
 System.Numerics.
 
public class PixieVsNumerics
{
    private Matrix4x4 numericsM;
    private Vector4 numericsP;

    [Benchmark]
    public Vector4 NumericsMatrix() =>
        Vector4.Transform(
            this.numericsP, 
            this.numericsM);
}
*/

public static class Program
{
    // ReSharper disable once UnusedMember.Local
    // This method is an example. It is fine if it is not called.
    private static void Example01()
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
    }
}