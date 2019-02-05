namespace Pixie.Cmd
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Pixie.Core;

    class Program
    {
        static void Main(string[] args)
        {
            var floor = new Sphere
            {
                Transform = Transform.Scale(10, 0.01f, 10),
                Material = new Material
                {
                    Color = new Color(1, 0.9f, 0.9f),
                    Specular = 0,
                },
            };

            var leftWall = new Sphere
            {
                Transform =
                    Transform.Translate(0, 0, 5) *
                    Transform.RotateY(-(float)Math.PI / 4) *
                    Transform.RotateX((float)Math.PI / 2) *
                    Transform.Scale(10, 0.01f, 10),
                Material = floor.Material,
            };

            var rightWall = new Sphere
            {
                Transform =
                    Transform.Translate(0, 0, 5) *
                    Transform.RotateY((float)Math.PI / 4) *
                    Transform.RotateX((float)Math.PI / 2) *
                    Transform.Scale(10, 0.01f, 10),
                Material = floor.Material,
            };

            var middle = new Sphere
            {
                Transform = Transform.Translate(-0.5f, 1, 0.5f),
                Material = new Material
                {
                    Color = new Color(0.1f, 1, 0.5f),
                    Diffuse = 0.7f,
                    Specular = 0.3f,
                },
            };

            var right = new Sphere
            {
                Transform =
                    Transform.Translate(1.5f, 0.5f, -0.5f) *
                    Transform.Scale(0.5f, 0.5f, 0.5f),
                Material = new Material
                {
                    Color = new Color(0.5f, 1, 0.1f),
                    Diffuse = 0.7f,
                    Specular = 0.3f,
                },
            };

            var left = new Sphere
            {
                Transform =
                    Transform.Translate(-1.5f, 0.33f, -0.75f) *
                    Transform.Scale(0.33f, 0.33f, 0.33f),
                Material = new Material
                {
                    Color = new Color(1, 0.8f, 0.1f),
                    Diffuse = 0.7f,
                    Specular = 0.3f,
                },
            };

            var light = new PointLight(
                Float4.Point(-10, 10, -10),
                Color.White);

            var camera = new Camera(200, 100, (float)Math.PI / 3)
            {
                Transform = Transform.View(
                    Float4.Point(0, 1.5f, -5),
                    Float4.Point(0, 1, 0),
                    Float4.Vector(0, 1, 0)),
            };

            var objects = new List<IShape>
            {
                floor, 
                leftWall, 
                rightWall, 
                left, 
                middle, 
                right,
            };

            var world = new World();
            world.Lights.Add(light);
            world.Objects = objects;

            var sw = new Stopwatch();
            sw.Start();
            var img = camera.Render(world);
            img.SavePpm(@"D:\temp\test.ppm");
            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
        }
    }
}
