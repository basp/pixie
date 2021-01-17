using Linsi;
using Pixie;

public class Test03 : ISceneBuilder
{
    public Scene Build(int width, int height)
    {
        var camera = new Camera(width, height, 0.7854)
        {
            Transform = Transform.View(
                Vector4.CreatePosition(-3, 1, 2.5),
                Vector4.CreatePosition(0, 0.5, 0),
                Vector4.CreateDirection(0, 1, 0)),
        };

        var light = new AreaLight(
            Vector4.CreatePosition(-1, 2, 4),
            Vector4.CreateDirection(2, 0, 0),
            10,
            Vector4.CreateDirection(0, 2, 0),
            10,
            new Color(1.5, 1.5, 1.5))
        {
            Jitter = new RandomSequence(),
        };

        var cube = new Cube()
        {
            Material = new Material()
            {
                Color = new Color(1.5, 1.5, 1.5),
                Ambient = 1,
                Diffuse = 0,
                Specular = 0,
            },

            Transform =
                Matrix4x4.Identity
                    .Scale(1, 1, 0.01)
                    .Translate(0, 3, 4),

            Shadow = false,
        };

        var plane = new Plane()
        {
            Material = new Material()
            {
                Color = new Color(1, 1, 1),
                Ambient = 0.025,
                Diffuse = 0.67,
                Specular = 0,
            },
        };

        var sphere1 = new Sphere()
        {
            Transform =
                Matrix4x4.Identity
                    .Scale(0.5, 0.5, 0.5)
                    .Translate(0.5, 0.5, 0),

            Material = new Material()
            {
                Color = new Color(1, 0, 0),
                Ambient = 0.1,
                Specular = 0,
                Diffuse = 0.6,
                Reflective = 0.3,
            },
        };

        var sphere2 = new Sphere()
        {
            Transform =
                Matrix4x4.Identity
                    .Scale(0.33, 0.33, 0.33)
                    .Translate(-0.25, 0.33, 0),

            Material = new Material()
            {
                Color = new Color(0.5, 0.5, 1),
                Ambient = 0.1,
                Specular = 0,
                Diffuse = 0.6,
                Reflective = 0.3,
            },
        };

        var world = new World()
        {
            Objects = new Shape[]
            {
                cube,
                plane,
                sphere1,
                sphere2
            },

            Lights = new ILight[]
            {
                light,
            },
        };

        return new Scene(world, camera);
    }
}