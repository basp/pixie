using System;
using Linsi;
using Pixie;

public static class Test02
{
    public static Tuple<World, Camera> Create(int width, int height)
    {
        var camera = new Camera(width, height, 1.152)
        {
            Transform = Transform.View(
                Vector4.CreatePosition(-2.6, 1.5, -3.9),
                Vector4.CreatePosition(-0.6, 1, -0.8),
                Vector4.CreateDirection(0, 1, 0)),
        };

        var light = new PointLight(
            Vector4.CreatePosition(-4.9, 4.9, -1),
            Color.White);

        var wallMaterial = new Material()
        {
            Pattern = new StripePattern(
                new Color(0.45, 0.45, 0.45),
                new Color(0.55, 0.55, 0.55))
            {
                Transform =
                    Matrix4x4.Identity
                        .Scale(0.25, 0.25, 0.25)
                        .RotateY(1.5708),
            },
            Ambient = 0,
            Diffuse = 0.4,
            Specular = 0,
            Reflective = 0.3,
        };

        var floor = new Plane()
        {
            Transform =
                Matrix4x4.Identity
                    .RotateY(0.31415),

            Material = new Material()
            {
                Pattern = new CheckersPattern(
                    new Color(0.35, 0.35, 0.35),
                    new Color(0.65, 0.65, 0.65))
                {
                },
                Specular = 0,
                Reflective = 0.4,
            },
        };

        var ceiling = new Plane()
        {
            Transform =
                Transform.Translate(0, 5, 0),

            Material = new Material()
            {
                Color = new Color(0.8, 0.8, 0.8),
                Ambient = 0.3,
                Specular = 0,
            },
        };

        var westWall = new Plane()
        {
            Transform =
                Matrix4x4.Identity
                    .RotateY(1.5708)
                    .RotateZ(1.5708)
                    .Translate(-5, 0, 0),

            Material = wallMaterial,
        };

        var eastWall = new Plane()
        {
            Transform =
                Matrix4x4.Identity
                    .RotateY(1.5708)
                    .RotateZ(1.5708)
                    .Translate(5, 0, 0),

            Material = wallMaterial,
        };

        var northWall = new Plane()
        {
            Transform =
                Matrix4x4.Identity
                    .RotateX(1.5708)
                    .Translate(0, 0, 5),

            Material = wallMaterial,
        };

        var southWall = new Plane()
        {
            Transform =
                Matrix4x4.Identity
                    .RotateX(1.5708)
                    .Translate(0, 0, -5),

            Material = wallMaterial,
        };

        var s1 = new Sphere()
        {
            Transform =
                Matrix4x4.Identity
                    .Scale(0.4, 0.4, 0.4)
                    .Translate(4.6, 0.4, 1),

            Material = new Material()
            {
                Color = new Color(0.8, 0.5, 0.3),
                Shininess = 50,
            },
        };

        var s2 = new Sphere()
        {
            Transform =
                Matrix4x4.Identity
                    .Scale(0.3, 0.3, 0.3)
                    .Translate(4.7, 0.3, 0.4),

            Material = new Material()
            {
                Color = new Color(0.9, 0.4, 0.5),
                Shininess = 50,
            },
        };

        var s3 = new Sphere()
        {
            Transform =
                Matrix4x4.Identity
                    .Scale(0.5, 0.5, 0.5)
                    .Translate(-1, 0.5, 4.5),

            Material = new Material()
            {
                Color = new Color(0.4, 0.9, 0.6),
                Shininess = 50,
            },
        };

        var s4 = new Sphere()
        {
            Transform =
                Matrix4x4.Identity
                    .Scale(0.3, 0.3, 0.3)
                    .Translate(-1.7, 0.3, 4.7),

            Material = new Material()
            {
                Color = new Color(0.4, 0.6, 0.9),
                Shininess = 50,
            },
        };

        var redSphere = new Sphere()
        {
            Transform =
                Matrix4x4.Identity
                    .Translate(-0.6, 1, 0.6),

            Material = new Material()
            {
                Color = new Color(1, 0.3, 0.2),
                Specular = 0.4,
                Shininess = 5,
            },
        };

        var blueGlassSphere = new Sphere()
        {
            Transform =
                Matrix4x4.Identity
                    .Scale(0.7, 0.7, 0.7)
                    .Translate(0.6, 0.7, -0.6),

            Material = new Material()
            {
                Color = new Color(0, 0, 0.2),
                Ambient = 0,
                Diffuse = 0.4,
                Specular = 0.9,
                Shininess = 300,
                Reflective = 0.9,
                Transparency = 0.9,
                RefractiveIndex = 1.5,
            },
        };

        var greenGlassSphere = new Sphere()
        {
            Transform =
                Matrix4x4.Identity
                    .Scale(0.5, 0.5, 0.5)
                    .Translate(-0.7, 0.5, -0.8),

            Material = new Material()
            {
                Color = new Color(0, 0.2, 0),
                Ambient = 0,
                Diffuse = 0.4,
                Specular = 0.9,
                Shininess = 300,
                Reflective = 0.9,
                Transparency = 0.9,
                RefractiveIndex = 1.5,
            },
        };

        var world = new World();
        world.Objects = new Shape[]
        {
            floor,
            ceiling,
            westWall,
            eastWall,
            northWall,
            southWall,
            s1,
            s2,
            s3,
            s4,
            redSphere,
            blueGlassSphere,
            greenGlassSphere,
        };

        world.Lights = new [] { light };

        return Tuple.Create(world, camera);
    }
}
