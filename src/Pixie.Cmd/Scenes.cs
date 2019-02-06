namespace Pixie.Cmd
{
    using System;
    using SharpNoise;
    using Pixie.Core;
    public class Scenes
    {
        private static Random Rng = new Random();

        public static World Example1()
        {
            var world = new World();

            var floor = new Plane()
            {
                Material = new Material
                {
                    Color = new Color(0.2, 0.5, 0.6),
                    Specular = 0,
                },
            };

            world.Objects.Add(floor);

            const int n = 32;
            var arc = 2 * Math.PI / n;
            for (var i = 0; i < n; i++)
            {
                var rot = i * arc;
                var rad = 0.1 + (i + 1) * 0.1 / n;
                var g = 0.2 + (i + 1) * 0.5 / n;
                var s = new Sphere
                {
                    Transform =
                        Transform.RotateY(rot) *
                        Transform.Translate(0, rad, -2.0) *
                        Transform.Scale(rad, rad, rad),

                    Material = new Material
                    {
                        Color = new Color(0.1, g, 0.4),
                    },
                };

                world.Objects.Add(s);
            }

            var light = new PointLight(
                Double4.Point(-10, 10, -10),
                Pixie.Core.Color.White);

            world.Lights.Add(light);

            return world;
        }

        public static World Example2()
        {
            var world = new World();

            var floor = new Plane()
            {
                Material = new Material
                {
                    Color = new Color(0.2, 0.5, 0.6),
                    Specular = 0,
                },
            };

            world.Objects.Add(floor);

            var sphere = new Sphere
            {
                Transform =
                    Transform.Translate(0, 1, 0) *
                    Transform.RotateY(Math.PI / 5),

                Material = new Material
                {
                    Pattern = new StripePattern(
                        new Color(0.05, 0.3, 0.4),
                        new Color(0.05, 0.33, 0.6))
                    {
                        Transform =
                            Transform.Scale(0.23, 1, 1),
                    },
                },
            };

            world.Objects.Add(sphere);

            var light = new PointLight(
                Double4.Point(-10, 10, -10),
                Pixie.Core.Color.White);

            world.Lights.Add(light);

            return world;
        }

        public static World Example3()
        {
            var world = new World();

            var ringPattern1 = new RingPattern(
                new Color(0.4, 0.4, 0.4),
                new Color(0.7, 0.7, 0.7))
            {
                Transform =
                    Transform.Scale(4, 4, 4),
            };

            var stripePattern1 = new StripePattern(
                new Color(0.2, 0.2, 0.2),
                new Color(0.5, 0.5, 1.0))
            {
                Transform =
                    Transform.Scale(0.1, 0.1, 0.1),
            };

            var checkers = new CheckersPattern(
                Color.White, 
                Color.Black)
            {
                Transform =
                    Transform.Scale(1, 1, 1),
            };

            var nestedPattern = new NestedPattern(
                checkers,
                ringPattern1,
                stripePattern1);

            var pertubedPattern = new PertubedPattern(
                new SolidPattern(Color.Black),
                new SolidPattern(new Color(0.1, 0.2, 0.3)))
            {
                Transform = Transform.Scale(5.5, 5.5, 5.5),
            };

            var floor = new Plane
            {
                Material = new Material
                {
                    Pattern = pertubedPattern,
                    Specular = 0,
                },
            };

            world.Objects.Add(floor);

            var gradientTransform =
                Transform.Translate(1, 0, 0) *
                Transform.Scale(2, 1, 1);

            var sphere = new Sphere
            {
                Transform =
                    Transform.Translate(0, 1, 0),

                Material = new Material
                {
                    Pattern = new GradientPattern(
                        new Color(1.0, 0.0, 0.0),
                        new Color(0.0, 0.0, 1.0))
                    {
                        Transform = gradientTransform,
                    },
                },
            };

            world.Objects.Add(sphere);

            var gradients = new[]
            {
                new GradientPattern(
                    new Color(1.0, 0.0, 0.0),
                    new Color(0.0, 0.0, 1.0))
                {
                    Transform = gradientTransform,
                },
                new GradientPattern(
                    new Color(1.0, 0.0, 0.0),
                    new Color(0.0, 1.0, 0.0))
                {
                    Transform = gradientTransform,
                },
                new GradientPattern(
                    new Color(0.0, 1.0, 0.0),
                    new Color(0.0, 0.0, 1.0))
                {
                    Transform = gradientTransform,
                },
                new GradientPattern(
                    new Color(0.5, 0.5, 0.0),
                    new Color(0.0, 0.5, 0.5))
                {
                    Transform = gradientTransform,
                },
                new GradientPattern(
                    new Color(0.5, 0.0, 0.5),
                    new Color(0.0, 1.0, 0.0))
                {
                    Transform = gradientTransform,
                },
            };

            const int n = 1024 * 10;
            var arc = 2 * Math.PI / n;
            for (var i = 0; i < n; i++)
            {
                const double r = 0.15;
                var gi = i % gradients.Length;
                
                var ni = (double)i / n;
                const double scatter = 15;
                var q = scatter * Rng.NextDouble();     
                var s = new Sphere
                {
                    Transform =
                        Transform.RotateY(i * arc) *
                        Transform.Translate(0, r, 2 - r + q) *
                        Transform.Scale(r, r, r),

                    Material = new Material
                    {
                        Pattern = gradients[gi],
                    },
                };

                world.Objects.Add(s);
            }

            var light = new PointLight(
                Double4.Point(-10, 10, -10),
                Color.White);

            world.Lights.Add(light);

            return world;
        }
    }
}