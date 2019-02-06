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
    }
}