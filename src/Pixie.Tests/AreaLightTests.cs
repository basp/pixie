namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class AreaLightTests
    {
        [Theory]
        [InlineData(-1, -1, 0)]
        // [InlineData(-1, 1, 0)]
        // [InlineData(1, -1, 0)]
        // [InlineData(1, 1, 0)]
        public void Sandbox(
            double x,
            double y,
            double z)
        {
            var l = new AreaLight(
                Double4.Point(0, 0, 0),
                Color.White,
                Double4.Vector(2, 0, 0),
                Double4.Vector(0, 2, 0),
                2,
                2);

            var ls = l.GetLights();
            var expected = new PointLight(
                Double4.Point(x, y, z), Color.White);

            Assert.Contains(expected, ls);
        }       
    }
}