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
            var world = Scenes.Example3();

            const int width = 640;
            const int height = 480;

            var camera = new Camera(width, height, Math.PI / 3)
            {
                Transform = Transform.View(
                    Double4.Point(1, 2.7, -3.7),
                    Double4.Point(0, 0.5, 0),
                    Double4.Vector(0, 1, 0)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            var sw = new Stopwatch();
            sw.Start();
            var img = camera.Render(world);
            img.SavePpm(@"D:\temp\test.ppm");
            sw.Stop();
            Console.WriteLine($"Total: {sw.Elapsed.Humanize()}");
        }
    }
}
