namespace Pixie.Cmd.Examples
{
    using System;

    public static class Test04
    {
        public static Tuple<World, Camera> Create(int width, int height)
        {
            var camera = new Camera(width, height, 0.7854)
            {
                Transform = Transform.View(
                    Vector4.CreatePosition(0, 5, -10),
                    Vector4.CreatePosition(0, 0.5, 0),
                    Vector4.CreateDirection(0, 1, 0)),

                ProgressMonitor = new ParallelConsoleProgressMonitor(height),
            };

            var sky = new Sphere()
            {
                Transform = Transform.Scale(100, 100, 100),
                Material = new Material()
                {
                    Ambient = 0.1,
                    Specular = 0,
                    Diffuse = 1.0,
                    Color = Color.FromByteValues(193, 207, 252),
                },
            };

            var metal1 = new Material()
            {
                Pattern = new GradientPattern(
                    Color.FromByteValues(56, 63, 73),
                    Color.FromByteValues(79, 77, 75))
                {
                    Transform =
                        Transform.Translate(-1, 0, 0) *
                        Transform.RotateY(Math.PI / 3) *
                        Transform.Scale(2, 1, 1),
                },
                Ambient = 0.3,
                Reflective = 0.12,
                Specular = 0.27,
                Diffuse = 1.0,
                Shininess = 3,
            };

            var metal2 = metal1.Extend(x =>
            {
                x.Pattern = new GradientPattern(
                    Color.FromByteValues(93, 89, 104),
                    Color.FromByteValues(65, 61, 76))
                {
                    Transform =
                        Transform.Translate(-1, 0, 0) *
                        Transform.RotateY(Math.PI / 2) *
                        Transform.Scale(2, 1, 1),
                };
            });

            var metal3 = metal1.Extend(x =>
            {
                x.Pattern = new GradientPattern(
                    Color.FromByteValues(87, 109, 132),
                    Color.FromByteValues(53, 67, 81))
                {
                    Transform =
                        Transform.Translate(-1, 0, 0) *
                        Transform.RotateY(-Math.PI / 2) *
                        Transform.Scale(2, 1, 1),
                };
            });

            var glass1 = new Material()
            {
                Transparency = 0.9,
                Reflective = 0.3,
                RefractiveIndex = 1.52,
                Specular = 0.9,
                Shininess = 300,
                Diffuse = 0.05,
                Ambient = 0,
            };

            var p = new Plane()
            {
                Material = new Material()
                {
                    Pattern = new CheckersPattern(
                        Color.FromByteValues(249, 243, 229),
                        Color.FromByteValues(226, 220, 204)),
                    Ambient = 0.5,
                    Specular = 0,
                    Diffuse = 0.4,
                },
            };

            var small =
                Transform.Translate(0, 0.25, 0) *
                Transform.Scale(0.25, 0.25, 0.25);

            var med =
                Transform.Translate(0, 0.4, 0) *
                Transform.Scale(0.4, 0.4, 0.4);

            var large =
                Transform.Translate(0, 0.6, 0) *
                Transform.Scale(0.6, 0.6, 0.6);

            var xlarge =
                Transform.Translate(0, 0.75, 0) *
                Transform.Scale(0.75, 0.75, 0.75);

            var sizes = new[]
            {
                small,
                med,
                large,
                xlarge,
            };

            var materials = new[]
            {
                metal1,
                metal2,
                metal3,
                glass1,
            };

            var rng = new Random();
            var g = new Group();
            for (var i = 0; i < 100; i++)
            {
                var size = sizes[i % sizes.Length];
                var mat = materials[i % materials.Length];
                var x = 20 - rng.NextDouble() * 40;
                var z = 20 - rng.NextDouble() * 40;
                var t = size * Transform.Translate(x, 0, z);
                var s = new Sphere()
                {
                    Transform = t,
                    Material = mat,
                };

                g.Add(s);
            }

            var light = new PointLight(
                Vector4.CreatePosition(-10, 10, -10),
                Color.White);

            var world = new World()
            {
                Objects = new Shape[] { sky, p, g },
                Lights = new ILight[] { light },
            };

            return Tuple.Create(world, camera);
        }
    }
}