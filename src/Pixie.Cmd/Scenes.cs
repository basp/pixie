namespace Pixie.Cmd
{
    using System;
    using Pixie.Core;

    public class Scenes
    {
        public const double Vacuum = 1.0;
        public const double Air = 1.000293;
        public const double Water = 1.333;
        public const double Ice = 1.31;
        public const double Diamond = 2.42;
        public const double Glass = 1.52;

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

            const int n = 1024;
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

        public static World Example4()
        {
            var world = new World();

            var plane = new Plane
            {
                Material = new Material
                {
                    Specular = 0,
                    Pattern = new CheckersPattern(Color.White, Color.Black)
                    {
                        Transform = Transform.Scale(2, 2, 2),
                    },
                },
            };

            world.Objects.Add(plane);

            var sphere = new Sphere
            {
                Transform =
                    Transform.Translate(0, 1, 0),

                Material = new Material
                {
                    Color = new Color(0.1, 0.4, 0.5),
                    Reflective = 0.6,
                    Specular = 0.95,
                    Shininess = 300,
                },
            };

            world.Objects.Add(sphere);

            const int n = 16;
            var arc = 2 * Math.PI / n;
            for (var i = 0; i < n; i++)
            {
                var s = new Sphere
                {
                    Transform =
                        Transform.RotateY(i * arc) *
                        Transform.Translate(0, 0.2, 1.5) *
                        Transform.Scale(0.2, 0.2, 0.2),

                    Material = new Material
                    {
                        Color = new Color(0.57, 0.1, 0.2),
                        Reflective = 0.4,
                    },
                };

                world.Objects.Add(s);
            }


            var light = new PointLight(Double4.Point(-10, 10, -10), Color.White);
            world.Lights.Add(light);

            return world;
        }

        public static World Example5()
        {
            var world = new World();

            var w0 = new Plane
            {
                Material = new Material
                {
                    Specular = 0,
                    Pattern = new CheckersPattern(Color.White, Color.Black)
                    {
                        Transform = Transform.Scale(0.5, 0.5, 0.5),
                    },
                },
            };

            world.Objects.Add(w0);

            var w1 = new Plane
            {
                Material = w0.Material,
                Transform =
                    Transform.Translate(0, 0, 6) *
                    Transform.RotateX(-Math.PI / 2),
            };

            world.Objects.Add(w1);

            var w2 = new Plane
            {
                Material = w0.Material,
                Transform =
                    Transform.Translate(-6, 0, 0) *
                    Transform.RotateZ(-Math.PI / 2),
            };

            world.Objects.Add(w2);

            var w3 = new Plane
            {
                Material = w0.Material,
                Transform =
                    Transform.Translate(6, 0, 0) *
                    Transform.RotateZ(Math.PI / 2),
            };

            world.Objects.Add(w3);

            var w4 = new Plane
            {
                Material = w0.Material,
                Transform =
                    Transform.Translate(0, 0, -6) *
                    Transform.RotateX(Math.PI / 2),
            };

            world.Objects.Add(w4);

            var w5 = new Plane
            {
                Material = w0.Material,
                Transform =
                    Transform.Translate(0, 6, 0) *
                    Transform.RotateZ(-Math.PI),
            };

            world.Objects.Add(w5);

            var sphere = new Sphere
            {
                Transform =
                    Transform.Translate(0, 1, 0),

                Material = new Material
                {
                    // Color = new Color(0.1, 0.2, 0.3),
                    Ambient = 0.0,
                    Diffuse = 0.0,
                    Transparency = 0.3,
                    // RefractiveIndex = 1.000029, // air
                    // RefractiveIndex = 1.333, // water
                    RefractiveIndex = 1.52, // glass
                    Reflective = 0.9,
                    Specular = 0.9,
                    Shininess = 300,
                },
            };

            world.Objects.Add(sphere);

            const int n = 8;
            var arc = 2 * Math.PI / n;
            for (var i = 0; i < n; i++)
            {
                var s = new Sphere
                {
                    Transform =
                        Transform.RotateY(i * arc) *
                        Transform.Translate(0, 0.5, 1.75) *
                        Transform.Scale(0.4, 0.4, 0.4),

                    Material = new Material
                    {
                        Color = new Color(0.1, 0.6, 0.7),
                        Reflective = 0.1,
                        Ambient = 0.05,
                        Diffuse = 0.8,
                        Specular = 1.0,
                        Shininess = 300,
                    },
                };

                world.Objects.Add(s);
            }


            var l1 = new PointLight(Double4.Point(-1, 4, 1), Color.White);
            world.Lights.Add(l1);

            return world;
        }

        public static Tuple<World, Camera> Example6(int width, int height)
        {
            var world = new World();

            var floor = new Plane
            {
                Material = new Material
                {
                    // Color = new Color(0.05, 0.35, 0.1),
                    Pattern = new CheckersPattern(
                        new Color(0.1, 0.55, 0.2),
                        new Color(0.1, 0.65, 0.3))
                    {
                        Transform =
                            Transform.Scale(0.6, 0.6, 0.6),
                    },
                    Specular = 0,
                },
            };

            world.Objects.Add(floor);

            var c0 = new Cube
            {
                Transform =
                    Transform.Translate(0, 0.7, 0) *
                    Transform.Scale(0.7, 0.7001, 0.7),

                Material = new Material
                {
                    Color = new Color(0.2, 0.5, 0.85),
                    Specular = 1.0,
                    Shininess = 300,
                    Reflective = 1.0,
                    Ambient = 0.1,
                    Diffuse = 0.2,
                    Transparency = 0.0,
                    RefractiveIndex = 1.00052,
                },
            };

            world.Objects.Add(c0);

            var c1 = new Cube
            {
                Transform =
                    Transform.Translate(0.7, 0.2, -1.4) *
                    Transform.Scale(0.2, 0.2, 0.2) *
                    Transform.RotateY(Math.PI / 5),

                Material = new Material
                {
                    Pattern = new StripePattern(
                        new Color(0.1, 0.3, 0.4),
                        new Color(0.1, 0.6, 0.8))
                    {
                        Transform =
                            Transform.Scale(0.25, 1, 1) *
                            Transform.RotateZ(Math.PI / 3),
                    },
                },
            };

            world.Objects.Add(c1);

            var c2 = new Cube
            {
                Material = new Material
                {
                    Color = new Color(0.8, 0.2, 0.8),
                },
                Transform =
                    Transform.Translate(-0.1, 0.1, -1.5) *
                    Transform.Scale(0.1, 0.1, 0.1) *
                    Transform.RotateY(-Math.PI / 4.2),
            };

            world.Objects.Add(c2);

            var c3 = new Cube
            {
                Material = new Material
                {
                    Pattern = new StripePattern(
                        new Color(0.2, 0.9, 0.8),
                        new Color(0.2, 0.4, 0.4))
                    {
                        Transform =
                            Transform.Scale(0.23, 1, 1) *
                            Transform.RotateZ(-Math.PI * 0.7),
                    },
                },
                Transform =
                    Transform.Translate(-0.2, 0.4, 2.0) *
                    Transform.Scale(0.4, 0.4, 0.4) *
                    Transform.RotateY(Math.PI / 4.6),
            };

            world.Objects.Add(c3);

            var light = new PointLight(
                Double4.Point(-10, 8, 1),
                Color.White);

            world.Lights.Add(light);

            var camera = new Camera(width, height, Math.PI / 3.3)
            {
                Transform = Transform.View(
                    Double4.Point(-3.25, 2.2, -4.2),
                    Double4.Point(0, 0.3, 0),
                    Double4.Vector(0, 1, 0)),

                ProgressMonitor =
                    new ParallelConsoleProgressMonitor(height),
            };

            return Tuple.Create(world, camera);
        }

        public static Tuple<World, Camera> Example7(int width, int height)
        {
            var world = new World();

            var floor = new Plane()
            {
                Material = new Material()
                {
                    Pattern = new StripePattern(
                        new Color(0.1, 0.3, 0.5),
                        new Color(0.1, 0.5, 0.6))
                    {
                        Transform =
                            Transform.Scale(1, 1, 1) *
                            Transform.RotateY(Math.PI / 4),
                    },
                },
            };

            world.Objects.Add(floor);

            var wall = new Cube()
            {
                Transform =
                    Transform.Translate(0, 0.999, 0) *
                    Transform.Scale(1, 1, 0.1),

                Material = new Material()
                {
                    Reflective = 1,
                    Specular = 1.0,
                    Shininess = 300,
                    Diffuse = 0.0,
                    Ambient = 0.0,
                    // Color = new Color(0.01, 0.02, 0.2),
                    Transparency = 1.0,
                    RefractiveIndex = 1.52,
                },
            };

            world.Objects.Add(wall);

            var s1 = new Sphere()
            {
                Transform =
                    Transform.Translate(0, 0.25, -1.0) *
                    Transform.Scale(0.25, 0.25, 0.25),

                Material = new Material()
                {
                    Ambient = 0.2,
                    Diffuse = 0.95,
                    Shininess = 50,
                    Specular = 0.3,
                    Color = new Color(0.9, 0.5, 0.84),
                },
            };

            world.Objects.Add(s1);

            var s2 = new Sphere()
            {
                Transform =
                    Transform.Translate(-0.45, 0.25, 1.0) *
                    Transform.Scale(0.25, 0.25, 0.25),

                Material = new Material()
                {
                    Ambient = 0.2,
                    Diffuse = 0.95,
                    Shininess = 50,
                    Specular = 0.3,
                    Color = new Color(0.1, 0.7, 0.7),
                },
            };

            world.Objects.Add(s2);

            var light = new PointLight(
                Double4.Point(0, 10, -2),
                Color.White);

            world.Lights.Add(light);

            var camera = new Camera(width, height, Math.PI / 3)
            {
                Transform = Transform.View(
                    Double4.Point(0.6, 1.0, -2.3),
                    Double4.Point(0, 0.25, 0),
                    Double4.Vector(0, 1, 0)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            return Tuple.Create(world, camera);
        }

        public static Tuple<World, Camera> Example8(int width, int height)
        {
            var world = new World();

            var skyblue = new Color(181.0 / 255, 228.0 / 255, 231.0 / 255);

            var sky = new Material
            {
                Color = skyblue,
                Specular = 0,
                Ambient = 0.2,
                Diffuse = 0.3,
            };

            var floor = new Plane()
            {
                Material = new Material()
                {
                    Specular = 0,
                    Ambient = 0.4,
                    Diffuse = 0.5,
                    Pattern = new CheckersPattern(
                        new Color(0.925, 0.870, 0.670),
                        new Color(0.886, 0.804, 0.610))
                    {
                        Transform =
                            Transform.Scale(0.25, 0.25, 0.25),
                    },
                },
            };

            world.Objects.Add(floor);

            var w1 = new Plane()
            {
                Transform =
                    Transform.Translate(0, 0, 100) *
                    Transform.RotateX(-Math.PI / 2),

                Material = sky,
            };

            world.Objects.Add(w1);

            var w2 = new Plane()
            {
                Transform =
                    Transform.Translate(0, 0, -100) *
                    Transform.RotateX(Math.PI / 2),

                Material = sky,
            };

            world.Objects.Add(w2);

            var w3 = new Plane()
            {
                Transform =
                    Transform.Translate(100, 0, 0) *
                    Transform.RotateZ(Math.PI / 2),

                Material = sky,
            };

            world.Objects.Add(w3);

            var w4 = new Plane()
            {
                Transform =
                    Transform.Translate(-100, 0, 0) *
                    Transform.RotateZ(-Math.PI / 2),

                Material = sky,
            };

            world.Objects.Add(w4);

            var w5 = new Plane()
            {
                Transform =
                    Transform.Translate(0, 100, 0) *
                    Transform.RotateZ(-Math.PI),

                Material = sky,
            };

            world.Objects.Add(w5);

            var metallic1 = new Material()
            {
                Color = new Color(96.0 / 255, 93.0 / 255, 82.0 / 255),
                Reflective = 0.17,
                Ambient = 0.2,
                Diffuse = 0.54,
                Shininess = 10,
                Specular = 0.3,
            };

            var metallic2 = new Material()
            {
                Color = new Color(59.0 / 255, 47.0 / 255, 57.0 / 255),
                Reflective = 0.1,
                Diffuse = 0.8,
                Shininess = 5,
                Specular = 0.3,
            };

            var metallic3 = new Material()
            {
                Color = new Color(32.0 / 255, 132.0 / 255, 70.0 / 255),
                Reflective = 0.14,
                Diffuse = 0.7,
                Shininess = 20,
                Specular = 0.4,
            };

            var glass = new Material()
            {
                Color = new Color(0.1, 0.05, 0.2),
                Reflective = 0.72,
                Transparency = 0.72,
                RefractiveIndex = Water,
                Diffuse = 0.32,
                Ambient = 0.01,
                Specular = 1.0,
                Shininess = 350,
            };

            glass.Pattern = new GradientPattern(
                new Color(231.0 / 255, 128.0 / 255, 102.0 / 255),
                new Color(70.0 / 255, 143.0 / 255, 30.0 / 255))
            {
                Transform =
                    Transform.Translate(1.0, 0, 0) *
                    Transform.Scale(2.0, 1, 1),
            };

            var s1 = new Sphere()
            {
                Transform =
                    Transform.Translate(0, 1, 0),

                Material = metallic1,
            };

            world.Objects.Add(s1);

            var s2 = new Sphere()
            {
                Transform =
                    Transform.Translate(-1.8, 0.5, -0.7) *
                    Transform.Scale(0.5, 0.5, 0.5),

                Material = glass,
            };

            world.Objects.Add(s2);

            var s3 = new Sphere()
            {
                Transform =
                    Transform.Translate(-0.2, 0.3, -1.5) *
                    Transform.Scale(0.3, 0.3, 0.3),

                Material = glass,
            };

            world.Objects.Add(s3);

            var s4 = new Sphere()
            {
                Transform =
                    Transform.Translate(-3.0, 0.7, 1.0) *
                    Transform.Scale(0.7, 0.7, 0.7),

                Material = metallic3,
            };

            world.Objects.Add(s4);

            var s5 = new Sphere()
            {
                Transform =
                    Transform.Translate(1.2, 0.5, -0.85) *
                    Transform.Scale(0.5, 0.5, 0.5),

                Material = metallic2,
            };

            world.Objects.Add(s5);

            var c1 = new Cube()
            {
                Transform =
                    Transform.Translate(0, 0.999, -7) *
                    Transform.Scale(2, 2, 2) *
                    Transform.RotateY(Math.PI / 5),

                Material = new Material
                {
                    Color = new Color(0.8, 0.2, 0.6),
                    Reflective = 0.1,
                    Diffuse = 0.8,
                },
            };

            world.Objects.Add(c1);

            var c2 = new Cube()
            {
                Transform =
                    Transform.Translate(6, 0.999, -5) *
                    Transform.Scale(1.5, 1.5, 1.5) *
                    Transform.RotateY(-Math.PI / 7),

                Material = new Material
                {
                    Color = new Color(0.1, 0.5, 0.5),
                    Reflective = 0.1,
                    Diffuse = 0.8,
                },
            };

            world.Objects.Add(c2);

            var c3 = new Cube()
            {
                Transform =
                    Transform.Translate(-4, 0.999, -6) *
                    Transform.Scale(1.5, 1.5, 1.5) *
                    Transform.RotateY(-Math.PI / 5),

                Material = new Material
                {
                    Color = new Color(0.1, 0.5, 0.5),
                    Reflective = 0.1,
                    Diffuse = 0.8,
                },
            };

            world.Objects.Add(c3);

            var light = new PointLight(
                Double4.Point(-5, 10, -2),
                Color.White);

            world.Lights.Add(light);

            var camera = new Camera(width, height, Math.PI / 4)
            {
                Transform = Transform.View(
                    Double4.Point(2.5, 2.5, -5.5),
                    Double4.Point(0.1, 0.9, -1.0),
                    Double4.Vector(0, 1, 0)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            return Tuple.Create(world, camera);
        }
    }
}