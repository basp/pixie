namespace Pixie;

public static class Program
{
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

    private static void Example02()
    {
        var rayOrigin = new Vector3(0, 0, -5).AsPosition();
        var wallZ = 10;
        var wallSize = 7.0f;
        var canvasPixels = 100;
        var pixelSize = wallSize / canvasPixels;
        var half = wallSize / 2;
        var ppm = new Pixmap(canvasPixels, canvasPixels);
        var A = Matrix4x4.CreateScale(1, 0.5f, 1);
        var B = Matrix4x4.CreateRotationZ(MathF.PI / 4);
        var obj = new Primitive(new Sphere())
        {
            Transform = new Transform(A * B),
        };
        
        var integrator = new Example02Integrator(obj);
        for (var y = 0; y < canvasPixels - 1; y++)
        {
            var worldY = half - pixelSize * y;
            for (var x = 0; x < canvasPixels - 1; x++)
            {
                var worldX = -half + pixelSize * x;
                var position = new Vector3(worldX, worldY, wallZ)
                    .AsPosition();
                var r = new Ray(
                    rayOrigin,
                    Vector4.Normalize(position - rayOrigin));
                var L = integrator.Li(r);
                ppm[x, y] = L;
            }
        }

        using var s = File.OpenWrite(@"D:\temp\Example02.ppm");
        ppm.Write(s);
    }

    public static void Main(string[] _)
    {
        Program.Example02();
    }
}