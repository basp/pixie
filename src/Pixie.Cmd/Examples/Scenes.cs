namespace Pixie.Cmd.Examples
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

        public static Tuple<World, Camera> Example9(int width, int height)
        {
            var world = new World();

            var solidBlack = new SolidPattern(Color.Black);

            var stripes1 = new StripePattern(
                new Color(0.0, 0.0, 0.0),
                new Color(0.0, 0.0, 0.5))
            {
            };

            var stripes2 = new StripePattern(
                new Color(0.0, 0.0, 0.0),
                new Color(0.5, 0.0, 0.0))
            {
                Transform = Transform.RotateY(Math.PI / 2),
            };

            var stripes3 = new StripePattern(
                new Color(0.6, 0.6, 0.6),
                new Color(0.3, 0.3, 0.3))
            {
                Transform = Transform.RotateY(Math.PI / 4),
            };

            var stripes4 = new StripePattern(
                new Color(0.6, 0.6, 0.6),
                new Color(0.3, 0.3, 0.3))
            {
                Transform = Transform.RotateY(-Math.PI / 4),
            };

            var blended1 = new BlendedPattern(stripes1, stripes2)
            {
                Transform =
                    Transform.Scale(0.5, 0.5, 0.5),
            };

            var blended2 = new BlendedPattern(stripes3, stripes4)
            {
                Transform =
                    Transform.Scale(0.5, 0.5, 0.5),
            };

            var blended = new BlendedPattern(blended1, blended2);

            var floor = new Plane
            {
                Material = new Material
                {
                    Ambient = 0.6,
                    Diffuse = 0.2,
                    Specular = 0,
                    Pattern = blended,
                },
            };

            world.Objects.Add(floor);

            var ceil = new Plane
            {
                Transform =
                    Transform.Translate(0, 10, 0) *
                    Transform.RotateX(Math.PI),

                Material = new Material
                {
                    Ambient = 0.3,
                    Diffuse = 0.6,
                    Specular = 0,
                    Pattern = blended,
                },
            };

            world.Objects.Add(ceil);

            const int n = 48;

            for (var i = 0; i < n; i++)
            {
                var s0 = new Sphere
                {
                    Material = new Material
                    {
                        Reflective = 1.0,
                        Transparency = 1.0,
                        RefractiveIndex = 1.52,
                        Diffuse = 0.1,
                        Ambient = 0,
                        Pattern = new GradientPattern(
                            new Color(0.6, 0.1, 0.1),
                            new Color(0.1, 0.1, 0.6))
                        {
                            Transform =
                                Transform.Scale(2, 1, 1),
                        }
                    },

                    Transform =
                        Transform.Translate(-8.0 + i * (64.0 / n), 0.5, 0) *
                        Transform.Scale(0.3, 0.3, 0.3),
                };

                world.Objects.Add(s0);
            }

            var cyl = new Cylinder()
            {
                Transform =
                    Transform.Scale(20, 1, 20),
            };

            var light = new PointLight(Double4.Point(0, 5, 0), Color.White);
            world.Lights.Add(light);

            var camera = new Camera(width, height, Math.PI / 4.2)
            {
                Transform = Transform.View(
                    Double4.Point(-5, 5, -5),
                    Double4.Point(0, 0, 0),
                    Double4.Vector(0, 0, 1)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            return Tuple.Create(world, camera);
        }

        public static Tuple<World, Camera> Example10(int width, int height)
        {
            var world = new World();

            var floor = new Plane
            {
                Material = new Material
                {
                    Specular = 0,
                    Ambient = 0.1,
                    Color = new Color(0.05, 0.4, 0.2),
                },
            };

            world.Objects.Add(floor);

            var cone = new Cone()
            {
                Material = new Material()
                {
                    Color = new Color(0.8, 0.2, 0.2),
                    Specular = 0,
                },
                Minimum = -1,
                Maximum = 0,
                IsClosed = true,
                Transform =
                    Transform.RotateX(Math.PI / 2) *
                    Transform.Translate(0, 1, 0),
            };

            var cyl = new Cylinder
            {
                Material = new Material
                {
                    Color = new Color(0.8, 0.2, 0.2),
                    Specular = 0,
                },
                Minimum = 0,
                Maximum = 1,
                IsClosed = true,
            };

            world.Objects.Add(cone);

            var light = new PointLight(Double4.Point(-2, 2, -5), Color.White);
            world.Lights.Add(light);

            var camera = new Camera(width, height, Math.PI / 3)
            {
                Transform = Transform.View(
                    Double4.Point(0, 3, -3),
                    Double4.Point(0, 0.5, 0),
                    Double4.Vector(0, 1, 0)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            return Tuple.Create(world, camera);
        }

        public static Tuple<World, Camera> Example11(int width, int height)
        {
            var world = new World();

            var hex = Hexagon.Create();
            world.Objects.Add(hex);

            var light = new PointLight(
                Double4.Point(-10, 10, -10),
                Color.White);

            world.Lights.Add(light);

            var camera = new Camera(width, height, Math.PI / 4.2)
            {
                Transform = Transform.View(
                    Double4.Point(-4, 5, -3),
                    Double4.Point(0, 0.5, 0),
                    Double4.Vector(0, 1, 0)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            return Tuple.Create(world, camera);
        }

        public static Tuple<World, Camera> Example12(int width, int height)
        {
            var world = new World();

            var gray1 = new Color(0.12, 0.12, 0.12);
            var gray2 = new Color(0.3, 0.3, 0.3);

            var stripes1 = new StripePattern(gray1, gray2)
            {
                Transform = Transform.RotateY(-Math.PI / 4),
            };

            var stripes2 = new StripePattern(gray1, gray2)
            {
                Transform = Transform.RotateY(Math.PI / 4),
            };

            var blended = new BlendedPattern(stripes1, stripes2)
            {
                Transform = Transform.Scale(0.5, 0.5, 0.5),
            };

            var plane = new Plane()
            {
                Material = new Material()
                {
                    Reflective = 0.1,
                    Specular = 0,
                    Ambient = 0.17,
                    Diffuse = 0.8,
                    Pattern = blended,
                },
            };

            world.Objects.Add(plane);

            var gradTransform =
                Transform.Translate(0, 1, 0) *
                Transform.RotateZ(Math.PI / 2) *
                Transform.Scale(2, 1, 1);

            var grad1 = new Material
            {
                Reflective = 0.2,
                Ambient = 0.21,
                Diffuse = 0.81,
                Pattern = new GradientPattern(
                    new Color(0.13, 0.58, 0.69),
                    new Color(0.43, 0.84, 0.93))
                {
                    Transform = gradTransform,
                }
            };

            var grad2 = new Material
            {
                Reflective = 0.2,
                Ambient = 0.21,
                Diffuse = 0.81,
                Pattern = new GradientPattern(
                    new Color(0.74, 0.76, 0.78),
                    new Color(0.17, 0.24, 0.31))
                {
                    Transform = gradTransform,
                }
            };

            var grad3 = new Material
            {
                Reflective = 0.2,
                Ambient = 0.21,
                Diffuse = 0.81,
                Pattern = new GradientPattern(
                    new Color(0.80, 0.17, 0.37),
                    new Color(0.46, 0.23, 0.53))
                {
                    Transform = gradTransform,
                }
            };

            var grad4 = new Material
            {
                Reflective = 0.2,
                Ambient = 0.21,
                Diffuse = 0.81,
                Pattern = new GradientPattern(
                    new Color(0.0, 0.2, 0.16),
                    new Color(0.0, 0.31, 0.57))
                {
                    Transform = gradTransform,
                }
            };

            var grads = new[]
            {
                grad2,
                grad1,
                grad3,
                grad2,
                grad4,
                grad2,
                grad3,
                grad1,
                grad2,
                grad4,
                grad3,
                grad4,
                grad3,
                grad1,
                grad2,
                grad1,
            };

            const int nx = 32;
            // const int nz = 8;

            var stridex = 20.0 / nx;
            // var stridez = 20.0 / nz;

            for (var i = 0; i < nx; i++)
            {
                var sx = 0.1 + Rng.NextDouble() * 0.1;
                var sy = (0.2 + 1.5 * Rng.NextDouble()) * (1 + sx);
                var sz = sx;
                var dx = -10.0 + i * stridex;
                var grad = grads[i % grads.Length];
                var c = new Cube()
                {
                    Material = grad,
                    Transform =
                        Transform.Translate(dx, sy, 0) *
                        Transform.Scale(sx, sy, sz),

                };

                world.Objects.Add(c);
            }

            for (var i = 0; i < nx; i++)
            {
                var sx = 0.2 + Rng.NextDouble() * 0.1;
                var sy = (0.2 + 1.0 * Rng.NextDouble()) * (1 + sx);
                var sz = sx;
                var dx = -9.3 + i * stridex;
                var dz = 1.7;
                var grad = grads[(i + 3) % grads.Length];
                var c = new Cube()
                {
                    Material = grad,
                    Transform =
                        Transform.Translate(dx, sy, dz) *
                        Transform.Scale(sx, sy, sz),

                };

                world.Objects.Add(c);
            }

            for (var i = 0; i < nx; i++)
            {
                var sx = 0.15 + Rng.NextDouble() * 0.1;
                var sy = (0.2 + 0.5 * Rng.NextDouble()) * (1 + sx);
                var sz = sx;
                var dx = -9.3 + i * stridex;
                var dz = 2.5;
                var grad = grads[(i + 3) % grads.Length];
                var c = new Cube()
                {
                    Material = grad,
                    Transform =
                        Transform.Translate(dx, sy, dz) *
                        Transform.Scale(sx, sy, sz),

                };

                world.Objects.Add(c);
            }

            var l1 = new PointLight(
                Double4.Point(-10, 10, -10),
                new Color(0.6, 0.6, 0.6));

            world.Lights.Add(l1);

            var l2 = new PointLight(
                Double4.Point(10, 10, -10),
                new Color(0.6, 0.6, 0.6));

            world.Lights.Add(l2);

            var camera = new Camera(width, height, Math.PI / 2)
            {
                Transform = Transform.View(
                    Double4.Point(0.0, 3.2, -4),
                    Double4.Point(0, 0.5, 0),
                    Double4.Vector(0, 1, 0)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            return Tuple.Create(world, camera);
        }

        public static Tuple<World, Camera> Example13(int width, int height)
        {
            var world = new World();

            var yellow = new Material
            {
                Color = new Color(0.8, 0.8, 0.05),
            };

            var col1 = new Material
            {
                Color = new Color(0.2, 0.4, 0.65),
            };

            var col2 = new Material
            {
                Color = new Color(0.1, 0.45, 0.55),
            };

            var cyl1 = new Cylinder()
            {
                Minimum = -1,
                Maximum = 1,
                IsClosed = true,
                Transform =
                    Transform.Scale(0.5, 1, 0.5),
                Material = yellow,
            };

            var cyl2 = new Cylinder()
            {
                Minimum = -1,
                Maximum = 1,
                IsClosed = true,
                Transform =
                    Transform.RotateZ(Math.PI / 2) *
                    Transform.Scale(0.5, 1, 0.5),
                Material = yellow,
            };

            var cyl3 = new Cylinder()
            {
                Minimum = -1,
                Maximum = 1,
                IsClosed = true,
                Transform =
                    Transform.RotateX(Math.PI / 2) *
                    Transform.Scale(0.5, 1, 0.5),
                Material = yellow,
            };

            var union1 = new Csg(Operation.Union, cyl1, cyl2);
            var union2 = new Csg(Operation.Union, union1, cyl3)
            {
                Transform =
                    Transform.Scale(1.2, 1.2, 1.2),
            };

            var sphere1 = new Sphere()
            {
                // Transform = Transform.Scale(Math.Sqrt(2), Math.Sqrt(2), Math.Sqrt(2)),
                Transform = Transform.Scale(1.4, 1.4, 1.4),
                Material = col1,
            };
            var cube1 = new Cube()
            {
                Material = col2,
            };

            var intersec1 = new Csg(Operation.Intersect, cube1, sphere1);

            var diff1 = new Csg(Operation.Difference, intersec1, union2);

            world.Objects.Add(diff1);

            var light = new PointLight(
                Double4.Point(10, 10, -10),
                Color.White);

            world.Lights.Add(light);

            var camera = new Camera(width, height, Math.PI / 4.8)
            {
                Transform = Transform.View(
                    Double4.Point(3.5, 3.5, -5),
                    Double4.Point(0, 0.0, 0),
                    Double4.Vector(0, 1, 0)),

                ProgressMonitor =
                    new ParallelConsoleProgressMonitor(height),
            };

            return Tuple.Create(world, camera);
        }

        // https://www.youtube.com/watch?v=AuS6HDHc7XE&feature=youtu.be&t=9

        private static Shape CreateArtifact(Color col)
        {
            var sphere1 = new Sphere()
            {
                Transform = Transform.Scale(1.3, 1.3, 1.3),
                Material = new Material
                {
                    Color = col,
                    Ambient = 0.12,
                    Specular = 0.5,
                },
            };

            var cube1 = new Cube()
            {
                Material = new Material
                {
                    Color = col,
                    Ambient = 0.12,
                    Specular = 0.5,
                },
            };

            return new Csg(Operation.Difference, cube1, sphere1)
            {
            };
        }

        public static Tuple<World, Camera> Example14(int width, int height)
        {
            var world = new World();

            var art1 = CreateArtifact(new Color(1.0, 0.0, 0.0));
            var art2 = CreateArtifact(new Color(0.0, 1.0, 0.0));
            art2.Transform =
                Transform.Scale(0.7, 0.7, 0.7) *
                Transform.RotateZ(Math.PI / 5) *
                Transform.RotateX(Math.PI / 6);

            var art3 = CreateArtifact(new Color(0.0, 0.0, 1.0));
            art3.Transform =
                Transform.Scale(0.5, 0.5, 0.5) *
                Transform.RotateZ(-Math.PI / 5) *
                Transform.RotateY(Math.PI / 6) *
                Transform.RotateX(Math.PI / 8);

            var art4 = CreateArtifact(new Color(1.0, 1.0, 0.0));
            art4.Transform =
                Transform.Scale(0.3, 0.3, 0.3) *
                Transform.RotateZ(Math.PI / 5) *
                Transform.RotateX(Math.PI / 6);

            var art5 = CreateArtifact(new Color(0.0, 1.0, 1.0));
            art5.Transform =
                Transform.Scale(0.2, 0.2, 0.2) *
                Transform.RotateZ(Math.PI / 3) *
                Transform.RotateY(Math.PI / 5);

            var art6 = CreateArtifact(new Color(1.0, 0.0, 1.0));
            art6.Transform =
                Transform.Scale(0.14, 0.14, 0.14) *
                Transform.RotateZ(Math.PI / 5) *
                Transform.RotateX(Math.PI / 3);

            world.Objects.Add(art1);
            world.Objects.Add(art2);
            world.Objects.Add(art3);
            world.Objects.Add(art4);
            world.Objects.Add(art5);
            world.Objects.Add(art6);

            var l1 = new PointLight(
                Double4.Point(5, 10, 10),
                new Color(0.7, 0.7, 0.7));

            var l2 = new PointLight(
                Double4.Point(-3, 10, 10),
                new Color(0.7, 0.7, 0.7));

            world.Lights.Add(l1);
            world.Lights.Add(l2);

            var camera = new Camera(width, height, Math.PI / 4.5)
            {
                Transform = Transform.View(
                    Double4.Point(1, 2.5, -5),
                    Double4.Point(0, 0.0, 0),
                    Double4.Vector(0, 1, 0)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            return Tuple.Create(world, camera);
        }

        public static Tuple<World, Camera> Example15(int width, int height)
        {
            var world = new World();

            var sky = new Plane()
            {
                Transform = 
                    Transform.Translate(0, 30, 0) *
                    Transform.RotateX(Math.PI),

                Material = new Material()
                {
                    Color = Color.White,
                },
            };

            var floor = new Plane()
            {
                Material = new Material
                {
                    Specular = 0,
                    // Color = new Color(1, 1, 1),
                    Diffuse = 0.7,
                    Ambient = 0.3,                    
                    Pattern = new CheckersPattern(
                        new Color(0.6, 0.6, 0.6),
                        new Color(0.45, 0.45, 0.45))
                    {
                        // Transform = Transform.Scale(2, 2, 2),
                    },
                },
            };

            var sphere = new Sphere()
            {
                Transform =
                    Transform.Translate(0, 1, 0),

                Material = new Material
                {
                    Specular = 1.0,
                    Ambient = 0.0,
                    Diffuse = 0.001,
                    Shininess = 350,
                    Color = new Color(0.05, 0.05, 0.2),
                    Transparency = 0.90,
                    RefractiveIndex = 1.52,
                    Reflective = 0.98,
                },
            };

            var smallSphere1 = new Sphere()
            {
                Transform =
                    Transform.Translate(0, 0.5, -2) *
                    Transform.Scale(0.5, 0.5, 0.5),

                Material = new Material
                {
                    Specular = 0.2,
                    Ambient = 0.2,
                    Diffuse = 0.7,
                    Shininess = 20,
                    Color = new Color(0.8, 0.3, 0.4),
                },
            };

            var smallSphere2 = new Sphere()
            {
                Transform =
                    Transform.Translate(0, 0.75, 3.5) *
                    Transform.Scale(0.75, 0.75, 0.75),

                Material = new Material
                {
                    Specular = 0.2,
                    Ambient = 0.1,
                    Diffuse = 0.9,
                    Shininess = 50,
                    Color = new Color(0.2, 0.7, 0.6),
                },
            };

            // var light1 = new PointLight(
            //     Double4.Point(-10, 10, -10),
            //     Color.White);

            var areaLight = new AreaLight(
                Double4.Point(-8, 16, -2),
                Color.White,
                Double4.Vector(8, 0, 0),
                Double4.Vector(0, 0, 8),
                5,
                5);

            world.Objects.Add(sky);
            world.Objects.Add(floor);
            world.Objects.Add(sphere);
            world.Objects.Add(smallSphere1);
            world.Objects.Add(smallSphere2);
            world.Lights.Add(areaLight);

            var camera = new Camera(width, height, Math.PI / 4)
            {
                Transform = Transform.View(
                    Double4.Point(2.4, 1.08, -5.4),
                    Double4.Point(0, 0.7, 0),
                    Double4.Vector(0, 1, 0)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            return Tuple.Create(world, camera);
        }
    }
}