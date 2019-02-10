namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class MaterialTests
    {
        private Material m = new Material();
        private Double4 position = Double4.Point(0, 0, 0);
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
            var eyev = Double4.Vector(0, 0, -1);
            var normalv = Double4.Vector(0, 0, -1);
            var light = new PointLight(
                Double4.Point(0, 0, -10),
                Color.White);
            var result = m.Li(sphere, light, position, eyev, normalv);
            var expected = new Color(1.9, 1.9, 1.9);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestLightingwithEyeBetrweenLightAndSurfaceAmdEyeOffset45Deg()
        {
            var eyev = Double4.Vector(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var normalv = Double4.Vector(0, 0, -1);
            var light = new PointLight(
                Double4.Point(0, 0, -10),
                Color.White);
            var result = m.Li(sphere, light, position, eyev, normalv);
            var expected = Color.White;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestLightingWithEyeOppositeSurfaceLightOffset45Deg()
        {
            var eyev = Double4.Vector(0, 0, -1);
            var normalv = Double4.Vector(0, 0, -1);
            var light = new PointLight(
                Double4.Point(0, 10, -10),
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
            var eyev = Double4.Vector(0, -Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var normalv = Double4.Vector(0, 0, -1);
            var light = new PointLight(
                Double4.Point(0, 10, -10),
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
            var eyev = Double4.Vector(0, 0, -1);
            var normalv = Double4.Vector(0, 0, -1);
            var light = new PointLight(
                Double4.Point(0, 0, 10),
                Color.White);
            var result = m.Li(sphere, light, position, eyev, normalv);
            var expected = new Color(0.1, 0.1, 0.1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LightingWithSurfaceInShadow()
        {
            var eyev = Double4.Vector(0, 0, -1);
            var normalv = Double4.Vector(0, 0, -1);
            var light = new PointLight(Double4.Point(0, 0, -10), Color.White);
            var c = m.Li(sphere, light, position, eyev, normalv, shadow: true);
            var expected = new Color(0.1, 0.1, 0.1);
            Assert.Equal(expected, c);
        }

        [Fact]
        public void NoShadowWhenNothingCollinearWithPointAndLight()
        {
            var w = new DefaultWorld();
            var p = Double4.Point(0, 10, 0);
            Assert.False(w.IsShadowed(p, (ILight)w.Lights[0]));
        }

        [Fact]
        public void ShadowWhenObjectBetweenPointAndLight()
        {
            var w = new DefaultWorld();
            var p = Double4.Point(10, -10, 10);
            Assert.True(w.IsShadowed(p, (ILight)w.Lights[0]));
        }

        [Fact]
        public void NoShadowWhenObjectBehindTheLight()
        {
            var w = new DefaultWorld();
            var p = Double4.Point(-20, 20, -20);
            Assert.False(w.IsShadowed(p, (ILight)w.Lights[0]));
        }

        [Fact]
        public void NoShadowWhenObjectBehindThePoint()
        {
            var w = new DefaultWorld();
            var p = Double4.Point(-2, 2, -2);
            Assert.False(w.IsShadowed(p, (ILight)w.Lights[0]));
        }

        [Fact]
        public void LightingWithAPatternApplied()
        {
            m.Pattern = new StripePattern(Color.White, Color.Black);
            m.Ambient = 1;
            m.Diffuse = 0;
            m.Specular = 0;
            var eyev = Double4.Vector(0, 0, -1);
            var normalv = Double4.Vector(0, 0, -1);
            var light = new PointLight(Double4.Point(0, 0, -10), Color.White);
            var c1 = m.Li(sphere, light, Double4.Point(0.9, 0, 0), eyev, normalv);
            var c2 = m.Li(sphere, light, Double4.Point(1.1, 0, 0), eyev, normalv);
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
    }
}