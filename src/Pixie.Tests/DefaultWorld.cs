namespace Pixie.Tests
{
    using Linsi;

    public class DefaultWorld : World
    {
        public DefaultWorld()
        {
            var s1 = new Sphere
            {
                Material = new Material
                {
                    Color = new Color(0.8, 1.0, 0.6),
                    Diffuse = 0.7,
                    Specular = 0.2,
                },
            };

            var s2 = new Sphere
            {
                Transform = Transform.Scale(0.5, 0.5, 0.5),
            };

            var light = new PointLight(
                Vector4.CreatePosition(-10, 10, -10),
                Color.White);

            this.Objects.Add(s1);
            this.Objects.Add(s2);
            this.Lights.Add(light);
        }
    }
}