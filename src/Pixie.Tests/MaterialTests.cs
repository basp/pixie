namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class MaterialTests
    {
        private Material m;
        private Float4 position;

        public MaterialTests()
        {
            this.m = new Material();
            this.position = Float4.Point(0, 0, 0);
        }


        [Fact]
        public void TestDefaultMaterial()
        {
            const int prec = 15;
            var m = new Material();
            Assert.Equal(0.1f, m.Ambient, prec);
            Assert.Equal(0.9f, m.Diffuse, prec);
            Assert.Equal(0.9f, m.Specular, prec);
            Assert.Equal(200f, m.Shininess, prec);
        }

        [Fact]
        public void TestLightingWitheyeBetweenLightAndSurface()
        {
            var eyev = Float4.Vector(0, 0, -1);
            var normalv = Float4.Vector(0, 0, -1);
            var light = new PointLight(
                Float4.Point(0, 0, -10),
                Color.White);
            var result = m.Li(light, position, eyev, normalv);
            var expected = new Color(1.9f, 1.9f, 1.9f);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestLightingwithEyeBetrweenLightAndSurfaceAmdEyeOffset45Deg()
        {
            var eyev = Float4.Vector(0, (float)Math.Sqrt(2) / 2, -(float)Math.Sqrt(2) / 2);
            var normalv = Float4.Vector(0, 0, -1);
            var light = new PointLight(
                Float4.Point(0, 0, -10),
                Color.White);
            var result = m.Li(light, position, eyev, normalv);
            var expected = Color.White;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestLightingWithEyeOppositeSurfaceLightOffset45Deg()
        {
            var eyev = Float4.Vector(0, 0, -1);
            var normalv = Float4.Vector(0, 0, -1);
            var light = new PointLight(
                Float4.Point(0, 10, -10),
                Color.White);
            var result = m.Li(light, position, eyev, normalv);
            var expected = new Color(0.7364f, 0.7364f, 0.7364f);
            const float eps = 0.00001f;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, result, comparer);
        }

        [Fact]
        public void TestLightingWithEyeInPathOfReflectionVector()
        {
            var eyev = Float4.Vector(0, -(float)Math.Sqrt(2) / 2, -(float)Math.Sqrt(2) / 2);
            var normalv = Float4.Vector(0, 0, -1);
            var light = new PointLight(
                Float4.Point(0, 10, -10),
                Color.White);
            var result = m.Li(light, position, eyev, normalv);
            var expected = new Color(1.6364f, 1.6364f, 1.6364f);
            // Reallyl low precision on this one.
            const float eps = 0.0001f;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, result, comparer);
        }

        [Fact]
        public void TestLightingWithLightBehindSurface()
        {
            var eyev = Float4.Vector(0, 0, -1);
            var normalv = Float4.Vector(0, 0, -1);
            var light = new PointLight(
                Float4.Point(0, 0, 10),
                Color.White);
            var result = m.Li(light, position, eyev, normalv);
            var expected = new Color(0.1f, 0.1f, 0.1f);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LightingWithSurfaceInShadow()
        {
            var eyev = Float4.Vector(0, 0, -1);
            var normalv = Float4.Vector(0, 0, -1);
            var light = new PointLight(Float4.Point(0, 0, -10), Color.White);
            var c = m.Li(light, position, eyev, normalv, shadow: true);
            var expected = new Color(0.1f, 0.1f, 0.1f);
            Assert.Equal(expected, c);
        }

        [Fact]
        public void NoShadowWhenNothingCollinearWithPointAndLight()
        {
            var w = new DefaultWorld();
            var p = Float4.Point(0, 10, 0);
            Assert.False(w.IsShadowed(p, w.Lights[0]));
        }

        [Fact]
        public void ShadowWhenObjectBetweenPointAndLight()
        {
            var w = new DefaultWorld();
            var p = Float4.Point(10, -10, 10);
            Assert.True(w.IsShadowed(p, w.Lights[0]));
        }

        [Fact]
        public void NoShadowWhenObjectBehindTheLight()
        {
            var w = new DefaultWorld();
            var p = Float4.Point(-20, 20, -20);
            Assert.False(w.IsShadowed(p, w.Lights[0]));
        }

        [Fact]
        public void NoShadowWhenObjectBehindThePoint()
        {
            var w = new DefaultWorld();
            var p = Float4.Point(-2, 2, -2);
            Assert.False(w.IsShadowed(p, w.Lights[0]));
        }
    }
}