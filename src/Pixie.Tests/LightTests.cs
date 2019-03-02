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
    }
}