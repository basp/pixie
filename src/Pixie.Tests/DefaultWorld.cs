namespace Pixie.Tests
{
    using Pixie.Core;

    public class DefaultWorld : World
    {
        public DefaultWorld()
        {
            var s1 = new Sphere
            {
                Material = new Material
                {
                    Color = new Color(0.8f, 1.0f, 0.6f),
                    Diffuse = 0.7f,
                    Specular = 0.2f,
                },
            };

            var s2 = new Sphere
            {
                Transform = Transform.Scale(0.5f, 0.5f, 0.5f),
            };

            var light = new PointLight(
                Float4.Point(-10, 10, -10),
                Color.White);

            this.Objects.Add(s1);
            this.Objects.Add(s2);
            this.Lights.Add(light);
        }
    }
}