namespace Pixie.Cmd
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Humanizer;
    using Pixie.Core;

    class Program
    {
        static void Main(string[] args)
        {
            var s1 = new Sphere
            {
                Material = new Material
                {
                    Color = new Color(0.1f, 0.5f, 0.7f),
                    Ambient = 0,
                },
            };

            var s2 = new Sphere
            {
                Transform = 
                    Transform.Translate(0.065f, 0, -4.2f) *
                    Transform.Scale(0.5f, 0.5f, 0.5f),
            };

            var s3 = new Sphere
            {
                Transform =
                    Transform.Translate(-0.4f, 0, -5.2f) *
                    Transform.Scale(0.3f, 0.3f, 0.3f),
            };

            var light = new PointLight(
                Float4.Point(0, 0, -10),
                Color.White);

            const int width = 400;
            const int height = 400;

            var camera = new Camera(width, height, (float)Math.PI / 3)
            {
                Transform = Transform.View(
                    Float4.Point(0, 0, -3),
                    Float4.Point(0, 0, 0),
                    Float4.Vector(0, 1, 0)),

                ProgressMonitor = new ConsoleProgressMonitor(height),
            };

            var world = new World();
            world.Lights.Add(light);
            world.Objects = new List<IShape> { s1, s2, s3 };

            var sw = new Stopwatch();
            sw.Start();
            var img = camera.Render(world);
            img.SavePpm(@"D:\temp\test.ppm");
            sw.Stop();
            Console.WriteLine($"Total: {sw.Elapsed.Humanize()}");
        }
    }
}
