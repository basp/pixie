namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Linie;

    public class MaterialTests
    {
        private Material m = new Material();
        private Vector4 position = Vector4.CreatePosition(0, 0, 0);
        private Shape sphere = new Sphere();


        [Fact]
        public void TestDefaultMaterial()
        {
            const int prec = 15;
            var m = new Material();
            Assert.Equal(0.1, m.Ambient, prec);
            Assert.Equal(0.9, m.Diffuse, prec);
            Assert.Equal(0.9, m.Specular, prec);
            Assert.Equal(200, m.Shininess, prec);
        }

        [Fact]
        public void TestLightingWitheyeBetweenLightAndSurface()
        {
            var eyev = Vector4.CreateDirection(0, 0, -1);
            var normalv = Vector4.CreateDirection(0, 0, -1);
            var light = new PointLight(
                Vector4.CreatePosition(0, 0, -10),
                Color.White);
            var result = m.Li(sphere, light, position, eyev, normalv);
            var expected = new Color(1.9, 1.9, 1.9);
            var cmp = Color.GetEqualityComparer(0.00001);
            Assert.Equal(expected, result, cmp);
        }

        [Fact]
        public void TestLightingwithEyeBetweenLightAndSurfaceAmdEyeOffset45Deg()
        {
            var eyev = Vector4.CreateDirection(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var normalv = Vector4.CreateDirection(0, 0, -1);
            var light = new PointLight(
                Vector4.CreatePosition(0, 0, -10),
                Color.White);
            var result = m.Li(sphere, light, position, eyev, normalv);
            var expected = Color.White;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestLightingWithEyeOppositeSurfaceLightOffset45Deg()
        {
            var eyev = Vector4.CreateDirection(0, 0, -1);
            var normalv = Vector4.CreateDirection(0, 0, -1);
            var light = new PointLight(
                Vector4.CreatePosition(0, 10, -10),
                Color.White);
            var result = m.Li(sphere, light, position, eyev, normalv);
            var expected = new Color(0.7364, 0.7364, 0.7364);
            const double eps = 0.00001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, result, comparer);
        }

        [Fact]
        public void TestLightingWithEyeInPathOfReflectionVector()
        {
            var eyev = Vector4.CreateDirection(0, -Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var normalv = Vector4.CreateDirection(0, 0, -1);
            var light = new PointLight(
                Vector4.CreatePosition(0, 10, -10),
                Color.White);
            var result = m.Li(sphere, light, position, eyev, normalv);
            var expected = new Color(1.6364, 1.6364, 1.6364);
            // Reallyl low precision on this one.
            const double eps = 0.0001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, result, comparer);
        }

        [Fact]
        public void TestLightingWithLightBehindSurface()
        {
            var eyev = Vector4.CreateDirection(0, 0, -1);
            var normalv = Vector4.CreateDirection(0, 0, -1);
            var light = new PointLight(
                Vector4.CreatePosition(0, 0, 10),
                Color.White);
            var result = m.Li(sphere, light, position, eyev, normalv);
            var expected = new Color(0.1, 0.1, 0.1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LightingWithSurfaceInShadow()
        {
            var eyev = Vector4.CreateDirection(0, 0, -1);
            var normalv = Vector4.CreateDirection(0, 0, -1);
            var light = new PointLight(Vector4.CreatePosition(0, 0, -10), Color.White);
            var c = m.Li(sphere, light, position, eyev, normalv, intensity: 0.0);
            var expected = new Color(0.1, 0.1, 0.1);
            Assert.Equal(expected, c);
        }

        [Fact]
        public void LightingWithAPatternApplied()
        {
            m.Pattern = new StripePattern(Color.White, Color.Black);
            m.Ambient = 1;
            m.Diffuse = 0;
            m.Specular = 0;
            var eyev = Vector4.CreateDirection(0, 0, -1);
            var normalv = Vector4.CreateDirection(0, 0, -1);
            var light = new PointLight(Vector4.CreatePosition(0, 0, -10), Color.White);
            var c1 = m.Li(sphere, light, Vector4.CreatePosition(0.9, 0, 0), eyev, normalv);
            var c2 = m.Li(sphere, light, Vector4.CreatePosition(1.1, 0, 0), eyev, normalv);
            Assert.Equal(Color.White, c1);
            Assert.Equal(Color.Black, c2);
        }

        [Fact]
        public void ReflectivityForTheDefaultMaterial()
        {
            var m = new Material();
            Assert.Equal(0.0, m.Reflective);
        }

        [Fact]
        public void TransparencyAndRefractiveIndexForDefaultMaterial()
        {
            var m = new Material();
            Assert.Equal(0, m.Transparency);
            Assert.Equal(1.0, m.RefractiveIndex);
        }

        [Theory]
        [InlineData(1.0, 1, 1, 1)]
        [InlineData(0.5, 0.55, 0.55, 0.55)]
        [InlineData(0.0, 0.1, 0.1, 0.1)]
        public void LightingUsesLightIntensityToAttenuateColor(
            double intensity,
            double r,
            double g,
            double b)
        {
            var w = new DefaultWorld();
            w.Lights = new ILight[]
            {
                new PointLight(Vector4.CreatePosition(0, 0, -10), Color.White),
            };

            var shape = w.Objects[0];
            shape.Material.Ambient = 0.1;
            shape.Material.Diffuse = 0.9;
            shape.Material.Specular = 0;
            shape.Material.Color = new Color(1, 1, 1);
            var pt = Vector4.CreatePosition(0, 0, -1);
            var eyev = Vector4.CreateDirection(0, 0, -1);
            var normalv = Vector4.CreateDirection(0, 0, -1);
            var result = shape.Material.Li(
                shape, 
                w.Lights[0], 
                pt, 
                eyev, 
                normalv,
                intensity);
            
            var expected = new Color(r, g, b);
            Assert.Equal(expected, result);
        }
    }
}