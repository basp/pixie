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
            var world = Scenes.Example2();

            const int width = 640;
            const int height = 480;

            var camera = new Camera(width, height, Math.PI / 3)
            {
                Transform = Transform.View(
                    Double4.Point(0, 2, -3),
                    Double4.Point(0, 1, 0),
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
