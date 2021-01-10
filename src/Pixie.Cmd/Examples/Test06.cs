namespace Pixie.Cmd.Examples
{
    using System;
    using Pixie.Core;

    public static class Test06
    {
        public static Tuple<World, Camera> Create(int width, int height)
        {
            var camera = new Camera(width, height, 0.7854)
            {
                Transform = Transform.View(
                    Vector4.CreatePosition(0, 10, 0),
                    Vector4.CreatePosition(0, 0, 0),
                    Vector4.CreateDirection(0, 0, 1)),

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

            var light = new PointLight(
                Vector4.CreatePosition(-10, 10, -10),
                Color.White);

            var world = new World()
            {
                Objects = new Shape[] { sky, p },
                Lights = new ILight[] { light },
            };

            return Tuple.Create(world, camera);
        }
    }
}