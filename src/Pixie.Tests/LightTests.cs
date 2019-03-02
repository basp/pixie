namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class LightTests
    {
        [Fact]
        public void TestPointLightHasPositionAndIntensity()
        {
            var intensity = new Color(1, 1, 1);
            var position = Double4.Point(0, 0, 0);
            var light = new PointLight(position, intensity);
            Assert.Equal(position, light.Position);
            Assert.Equal(intensity, light.Intensity);
        }

        [Theory]
        [InlineData(0, 1.0001, 0, 1.0)]
        [InlineData(-1.0001, 0, 0, 1.0)]
        [InlineData(0, 0, -1.0001, 1.0)]
        [InlineData(0, 0, 1.0001, 0.0)]
        [InlineData(1.0001, 0, 0, 0.0)]
        [InlineData(0, -1.0001, 0, 0.0)]
        [InlineData(0, 0, 0, 0.0)]
        public void PointLightsEvaluateLightIntensityAtGivenPoint(
            double px,
            double py,
            double pz,
            double result)
        {
            var w = new DefaultWorld();
            var light = w.Lights[0];
            var pt = Double4.Point(px, py, pz);
            var intensity = light.IntensityAt(pt, w);
            Assert.Equal(result, intensity);
        }

        [Fact]
        public void CreatingAnAreaLight()
        {
            var corner = Double4.Point(0, 0, 0);
            var v1 = Double4.Vector(2, 0, 0);
            var v2 = Double4.Vector(0, 0, 1);
            var light = new AreaLight(
                corner,
                v1,
                4,
                v2,
                2,
                Color.White);
            
            Assert.Equal(corner, light.Corner);
            Assert.Equal(Double4.Vector(0.5, 0, 0), light.Uvec);
            Assert.Equal(4, light.Usteps);
            Assert.Equal(Double4.Vector(0, 0, 0.5), light.Vvec);
            Assert.Equal(2, light.Vsteps);
            Assert.Equal(8, light.Samples);
            Assert.Equal(Double4.Point(1, 0, 0.5), light.Position);
        }
    }
}