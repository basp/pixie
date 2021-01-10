namespace Pixie.Cmd.Examples
{
    using System;

    public class TestPattern1 : Pattern
    {
        const double eps = 0.0001;

        public override Color PatternAt(Vector4 point)
        {
            var ix = Math.Floor(point.X);
            var f = Math.Abs(point.X - ix);

            if((f - eps) < 0.2)
            {
                return new Color(0.22, 0.24, 0.24);                
            }

            if((f - eps) < 0.5)
            {
                return new Color(0, 0.67, 0.71);
            }

            if((f - eps) < 0.7)
            {
                return new Color(0.97, 0.31, 0);
            }

            if((f - eps) < 0.8)
            {
                return new Color(0.99, 0.50, 0.38);
            }
            
            return new Color(1, 0.99, 0.95);
        }
    }

    public static class Test01
    {
        public static Tuple<World, Camera> Create(int width, int height)
        {
            // var pat1 = new TestPattern1()
            // {
            //     Transform = Matrix4x4.Identity * 
            //         Transform.Scale(2.1, 1, 1.7) *
            //         Transform.RotateY(Math.PI / 4),
            // };

            var floor = new Plane()
            {
                Material = new Material()
                {
                    Color = new Color(0.5, 0.5, 0.5),
                    Specular = 0,
                    Diffuse = 0.8,
                    Ambient = 0.2,
                },
            };

            var s1 = new Sphere()
            {
                Material = new Material()
                {
                    Color = new Color(0.1, 0.4, 0.72),
                    Reflective = 0.6,
                    Diffuse = 0.3,
                    Specular = 0.95,
                    Shininess = 300,
                },

                Transform = Matrix4x4.Identity *
                    Transform.Scale(0.5, 0.5, 0.5) *
                    Transform.Translate(0, 1, 0),
            };

            var s2 = new Sphere()
            {
                Material = new Material()
                {
                    Color = new Color(0.9, 0.9, 0.1),
                    Reflective = 0.4,
                    Diffuse = 0.4,
                    Specular = 0.96,
                    Shininess = 300,
                },

                Transform = Matrix4x4.Identity *
                    Transform.Scale(0.3, 0.3, 0.3) *
                    Transform.Translate(0, 1, -2.5),
            };

            var s3 = new Sphere()
            {
                Material = new Material()
                {
                    Color = new Color(0.72, 0.11, 0.32),
                },

                Transform = Matrix4x4.Identity *
                    Transform.Scale(0.5, 0.5, 0.5) *
                    Transform.Translate(0, 1, -4),
            };

            var l1 = new PointLight(
                Vector4.CreatePosition(-100, 40, -20),
                new Color(1, 1, 1));

            var world = new World();
            world.Objects.Add(floor);
            world.Objects.Add(s1);
            world.Objects.Add(s2);
            world.Objects.Add(s3);
            world.Lights.Add(l1);

            var cam = new Camera(width, height, Math.PI / 4)
            {
                Transform = Transform.View(
                    Vector4.CreatePosition(0, 2, -3),
                    Vector4.CreatePosition(0, 0, 0),
                    Vector4.CreateDirection(0, 1, 0)),

                ProgressMonitorFactory =
                    (_rows, _cols) => new DefaultProgressMonitor(),
            };

            return Tuple.Create(world, cam);
        }
    }
}