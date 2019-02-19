namespace Pixie.Cmd
{
    using System;
    using Pixie.Core;

    public class Sandbox
    {
        public static Tuple<World, Camera> Create(int width, int height)
        {
            var world = new World();
            var cam = new Camera(width, height, Math.PI / 2);
            cam.Transform = Transform.View(
                Double4.Point(0, 5, 0),
                Double4.Point(0, 0, 0),
                Double4.Vector(0, 0, -1));
                
            var l1 = new PointLight(Double4.Point(0, 0.0, -14), Color.White);

            var g1 = new GradientPattern(
                new Color(0, 1, 0),
                new Color(0, 0, 1));

            g1.Transform = 
                Transform.RotateZ(-0.2) *
                Transform.RotateY(Math.PI / 5) *
                Transform.Translate(-1, 0, 0) *
                Transform.Scale(2, 1, 1);

            var s1 = new Sphere()
            {
                Transform = 
                    Transform.Scale(2, 2, 2),
                
                Material = new Material()
                {
                    // Color = new Color(0.24, 0.69, 0.92),
                    Pattern = g1,
                },
            };

            var s2 = new Sphere()
            {
                Transform = 
                    Transform.RotateY(0.05) *
                    Transform.Translate(0, 0.72, -7) *
                    Transform.Scale(0.1, 0.1, 0.1),
            };

            world.Objects.Add(s1);
            world.Objects.Add(s2);
            world.Lights.Add(l1);
            
            return Tuple.Create(world, cam);
        }
    }
}