namespace Pixie.Cmd
{
    using System;
    using Pixie.Core;

    public class Scenes
    {
        public static World Example1()
        {
            var world = new World();

            var floor = new Plane()
            {
                Material = new Material
                {
                    Color = new Pixie.Core.Color(0.2, 0.5, 0.6),
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
                        Color = new Pixie.Core.Color(0.1, g, 0.4),
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
                    Color = new Pixie.Core.Color(0.2, 0.5, 0.6),
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
                    Pattern = new StripePattern(Color.White, Color.Black)
                    {
                        Transform = 
                            Transform.Scale(0.33, 1, 1),
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
    }
}