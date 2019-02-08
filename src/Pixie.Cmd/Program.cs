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
            const int width = 800;
            const int height = 600;

            var t = Scenes.Example7(width, height);

            // var camera = new Camera(width, height, Math.PI / 3)
            // {
            //     Transform = Transform.View(
            //         Double4.Point(-3, 1.5, 1),
            //         Double4.Point(0, 0.5, 0),
            //         Double4.Vector(0, 1, 0)),

            //     ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            // };

            var sw = new Stopwatch();
            sw.Start();
            var img = t.Item2.Render(t.Item1);
            img.SavePpm(@"D:\temp\test.ppm");
            sw.Stop();

            const int nPixels = width * height;
            Console.WriteLine($"Total: {sw.Elapsed})");
            Console.WriteLine($"{nPixels / sw.ElapsedMilliseconds}px/ms");
        }
    }
}
