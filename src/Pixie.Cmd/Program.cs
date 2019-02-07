namespace Pixie.Cmd
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using Pixie.Core;

    class Program
    {
        static void Main(string[] args)
        {
            var world = Scenes.Example5();

            const int width = 400;
            const int height = 200;

            var camera = new Camera(width, height, Math.PI / 3)
            {
                Transform = Transform.View(
                    Double4.Point(-1.5, 0.7, -3),
                    Double4.Point(0, 0.3, 0),
                    Double4.Vector(0, 1, 0)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            var sw = new Stopwatch();
            sw.Start();
            var img = camera.Render(world);
            img.SavePpm(@"D:\temp\test.ppm");
            sw.Stop();

            const int nPixels = width * height;
            Console.WriteLine($"Total: {sw.Elapsed})");
            Console.WriteLine($"{nPixels / sw.ElapsedMilliseconds}px/ms");
        }
    }
}
