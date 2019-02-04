namespace Pixie.Core
{
    using System;
    using System.IO;

    public class Canvas
    {
        private int width;
        private int height;
        private Color[] data;

        public Canvas(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.data = new Color[width * height];
        }

        public Color this[int x, int y]
        {
            get => this.data[y * this.width + x];
            set => this.data[y * this.width + x] = value;
        }

        public int Width => this.width;

        public int Height => this.height;

        public void SavePpm(string filename)
        {
            using (var s = File.OpenWrite(filename))
            using (var w = new StreamWriter(s))
            {
                w.WriteLine($"P3");
                w.WriteLine($"{this.Width} {this.Height}");
                w.WriteLine($"255");

                for (var j = 0; j < this.height; j++)
                {
                    for (var i = 0; i < this.width; i++)
                    {
                        var rgb = GetColorBytes(this[i, j]);
                        w.WriteLine($"{rgb.Item1} {rgb.Item2} {rgb.Item3}");
                    }
                }
            }
        }

        private static int Clamp(int v, int min, int max)
        {
            if (v < min) return min;
            if (v > max) return max;
            return v;
        }

        private static Tuple<int, int, int> GetColorBytes(Color c) =>
            Tuple.Create(
                Clamp((int)(c.R * 255), 0, 255),
                Clamp((int)(c.G * 255), 0, 255),
                Clamp((int)(c.B * 255), 0, 255));
    }

    public static class CanvasExamples
    {
        public static Canvas Example1()
        {
            var eye = Float4.Point(0, 0, -5);
            var wallZ = 10f;
            var wallSize = 7.0f;
            var nPixels = 100;
            var pixelSize = wallSize / nPixels;
            var half = wallSize / 2;
            var canvas = new Canvas(nPixels, nPixels);
            var color = new Color(1, 0, 0);
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

                    var r = new Ray(eye, (pos - eye).Normalize());
                    var xs = s.Intersect(r);

                    if (xs.TryGetHit(out var i))
                    {
                        canvas[x, y] = color;
                    }
                }
            }

            return canvas;
        }
    }
}