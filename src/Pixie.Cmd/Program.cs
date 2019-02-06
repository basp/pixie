namespace Pixie.Cmd
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using Humanizer;
    using Pixie.Core;

    class Program
    {
        static void Main(string[] args)
        {
            var world = new World();

            var s1 = new Sphere
            {
                Material = new Material
                {
                    Color = new Pixie.Core.Color(0.1, 0.5, 0.7),
                },
            };

            world.Objects.Add(s1);

            var piOver4 = Math.PI / 4;            
            for(var i = 0; i < 8; i++)
            {
                var s = new Sphere()
                {
                    Transform = 
                        Transform.RotateY(i * piOver4) *
                        Transform.Translate(0, 0, 1.35) *
                        Transform.Scale(0.3, 0.3, 0.3),

                    Material = new Material
                    {
                        Color = new Pixie.Core.Color(0.3, 0.7, 0.4),
                    },
                };

                world.Objects.Add(s);
            }

            var l1 = new PointLight(
                Double4.Point(0, 2, -10),
                Pixie.Core.Color.White);

            world.Lights.Add(l1);

            const int width = 1280;
            const int height = 1024;

            var camera = new Camera(width, height, Math.PI / 3)
            {
                Transform = Transform.View(
                    Double4.Point(3, 3, -3),
                    Double4.Point(0, 0, 0),
                    Double4.Vector(0, 1, 0)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            var sw = new Stopwatch();
            sw.Start();

            var img = camera.Render(world);
            // img.SavePpm(@"D:\temp\test.ppm");
            using (var bmp = new Bitmap(img.Width, img.Height))
            {
                for (var y = 0; y < img.Height; y++)
                {
                    for (var x = 0; x < img.Width; x++)
                    {
                        var t = Canvas.GetColorBytes(img[x, y]);
                        var c = System.Drawing.Color.FromArgb(t.Item1, t.Item2, t.Item3);
                        bmp.SetPixel(x, y, c);
                    }
                }

                bmp.Save(@"D:\temp\test.png");
            }

            sw.Stop();

            Console.WriteLine($"Total: {sw.Elapsed.Humanize()}");
        }
    }
}
