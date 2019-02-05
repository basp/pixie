namespace Pixie.Cmd
{
    using System;
    using Pixie.Core;

    class Program
    {
        const string Banner = @"Pixie 0.0.1";

        static void Main(string[] args)
        {
            Console.WriteLine(Banner);

            var sphere = new Sphere();

            sphere.Material = 
                new Material
                {
                    Color = new Color(1, 0.2f, 1),
                };

            var lightPosition = Float4.Point(-10, 10, -10);
            var lightColor = new Color(1, 1, 1);
            var light = new PointLight(lightPosition, lightColor);

            var rayOrigin = Float4.Point(0, 0, -5);
            var wallZ = 10f;
            var wallSize = 7.0f;
            var nPixels = 100;
            var pixelSize = wallSize / nPixels;
            var half = wallSize / 2;
            var canvas = new Canvas(nPixels, nPixels);
            var s = new Sphere();
            for (var y = 0; y < nPixels; y++)
            {
                var worldY = half - pixelSize * y;
                for (var x = 0; x < nPixels; x++)
                {
                    var worldX = -half + pixelSize * x;
                    var pos = Float4.Point(
                        (float)worldX,
                        (float)worldY,
                        wallZ);

                    var rayDirection = (pos - rayOrigin).Normalize();
                    var r = new Ray(rayOrigin, rayDirection);
                    var xs = s.Intersect(r);
                    if (xs.TryGetHit(out var i))
                    {
                        var point = r.Position(i.T);
                        var normal = i.Object.NormalAt(point);
                        var eye = -r.Direction;
                        var color = i.Object.Material.Li(light, point, eye, normal);
                        canvas[x, y] = color;
                    }
                }
            }

            // canvas.SavePpm(@"D:\temp\test.ppm");
       }
    }
}
