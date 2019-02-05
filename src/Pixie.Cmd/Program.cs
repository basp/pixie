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
                    Transform.Translate(-0.55f, 0, -5.2f) *
                    Transform.Scale(0.3f, 0.3f, 0.3f),
            };

            var s4 = new Sphere
            {
                Transform =
                    Transform.Translate(-0.5f, 0, -0.5f) *
                    Transform.Scale(0.1f, 0.1f, 0.1f),

                Material = new Material
                {
                    Color = new Color(0.1f, 0.3f, 0.7f),
                },
            };

            var l1 = new PointLight(
                Float4.Point(0, 0, -10),
                Color.White);

            var l2 = new PointLight(
                Float4.Point(10f, 0.2f, -10),
                new Color(0.2f, 0.2f, 0.3f));


            const int width = 400;
            const int height = 200;

            var camera = new Camera(width, height, (float)Math.PI / 3)
            {
                Transform = Transform.View(
                    Float4.Point(0, 0, -3),
                    Float4.Point(0, 0, 0),
                    Float4.Vector(0, 1, 0)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            var world = new World();

            world.Lights = new List<PointLight> { l1, l2 };
            world.Objects = new List<IShape> { s1 };

            var sw = new Stopwatch();
            sw.Start();

            var img = camera.Render(world);
            img.SavePpm(@"D:\temp\test.ppm");
            
            sw.Stop();
            
            Console.WriteLine($"Total: {sw.Elapsed.Humanize()}");
        }
    }
}
