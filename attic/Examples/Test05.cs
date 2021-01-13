namespace Pixie.Cmd.Examples
{
    using System;

    public static class Test05
    {
        public static Tuple<World, Camera> Create(int width, int height)
        {
            var camera = new Camera(width, height, 0.7854)
            {
                Transform = Transform.View(
                    Vector4.CreatePosition(0, 10, 0),
                    Vector4.CreatePosition(0, 0, 0),
                    Vector4.CreateDirection(0, 0, 1)),

                ProgressMonitorFactory =
                    (_rows, _cols) => new DefaultProgressMonitor(),
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

            var @base = new Cube()
            {
                Transform =
                    Transform.Scale(3.0, 0.5, 3.0),
            };

            var light = new PointLight(
                Vector4.CreatePosition(-10, 10, -10),
                Color.White);

            var world = new World()
            {
                Objects = new Shape[] { @base },
                Lights = new ILight[] { light },
            };

            return Tuple.Create(world, camera);
        }
    }
}