namespace Pixie.Cmd.Examples
{
    using System;
    using Pixie.Core;

    public class Sandbox
    {
        static Random rng = new Random();

        public static Tuple<World, Camera> Create(int width, int height)
        {
            var world = new World();
         
            var cam = new Camera(width, height, Math.PI / 3);
            cam.Transform = Transform.View(
                Double4.Point(0, 10, 0),
                Double4.Point(0, 0.25, 0),
                Double4.Vector(0, 0, -1));

            cam.ProgressMonitor = new ParallelConsoleProgressMonitor(height);

            var l1 = new PointLight(Double4.Point(-20, 3.5, -50), Color.White);
            var l2 = new PointLight(Double4.Point(10, 100, 500), new Color(1, 1, 1));

            var g1 = new GradientPattern(
                new Color(0, 1, 0),
                new Color(0, 0, 1));

            g1.Transform =
                Transform.RotateZ(-0.2) *
                Transform.RotateY(Math.PI / 5) *
                Transform.Translate(-1, 0, 0) *
                Transform.Scale(2, 2, 2);

            var s0 = new Sphere()
            {
                Transform =
                    Transform.Scale(2, 2, 2),

                Material = new Material()
                {
                    Pattern = g1,
                    Reflective = 0.9,
                    Transparency = 0.4,
                    RefractiveIndex = 1.5,
                    Ambient = 0.01,
                    Diffuse = 0.2,
                    Specular = 1.0,
                    Shininess = 350,
                },
            };

            var s00 = new Sphere()
            {
                Transform = 
                    Transform.Scale(1.8, 1.8, 1.8),

                Material = new Material()
                {
                    Pattern = g1,
                    Reflective = 0.9,
                    Transparency = 0.8,
                    RefractiveIndex = 1.0022,
                    Ambient = 0.001,
                    Diffuse = 0.1,
                    Specular = 5.0,
                    Shininess = 350,
                },
            };

            var group1 = new Group();
            const int n = 16;
            for (var i = 0; i < n; i++)
            {
                var r = (2 * Math.PI / n) * i;
                var s = new Sphere();
                var g = 0.5 + (i * 0.5 / n);
                var col = new Color(0.605, g, 0.869);
                s.Material = new Material()
                {
                    // Color = new Color(0.605, 0.904, 0.869),
                    Color = col,
                    Diffuse = 0.9,
                    Ambient = 0.1,
                    Specular = 0.2,
                    Shininess = 20,
                };

                var offset = rng.NextDouble() * 2.0;

                s.Transform =
                    Transform.RotateY(r) *
                    Transform.Translate(0, 0, -5 + offset) *
                    Transform.Scale(0.5, 0.5, 0.5);
                
                group1.Add(s);
            }

            group1.Transform =
                Transform.RotateX(Math.PI / 3) *
                Transform.RotateY(Math.PI / 1);

            var floor = new Plane();
            floor.Material = new Material()
            {
                Color = new Color(1, 1, 1),
                Ambient = 1.0,
                Specular = 0,
                Diffuse = 0,
            };

            floor.Transform = 
                Transform.Translate(0, -100, 0);

            floor.Material = new Material()
            {
                Specular = 0,
                Pattern = new StripePattern(
                    new Color(0.22, 0.22, 0.22),
                    new Color(0.41, 0.41, 0.41))
                {
                    Transform = 
                        Transform.RotateY(Math.PI / 4) *
                        Transform.Scale(10, 1, 1),
                },
            };

            world.Objects.Add(s0);
            world.Objects.Add(s00);
            world.Objects.Add(group1);
            world.Objects.Add(floor);

            world.Lights.Add(l1);
            // world.Lights.Add(l2);

            return Tuple.Create(world, cam);
        }
    }
}