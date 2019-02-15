namespace Pixie.Tests
{
    using System;
    using System.Linq;
    using Xunit;
    using Pixie.Core;

    public class AreaLightTests
    {
        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(0, 1, -1)]
        [InlineData(0, -1, 1)]
        [InlineData(0, -1, -1)]
        public void Sandbox(
            double x,
            double y,
            double z)
        {
            var l = new AreaLight(
                Double4.Point(0, 0, 0),
                Color.White,
                Double4.Vector(0, 0, 2),
                Double4.Vector(0, 2, 0),
                2,
                2);

            var ls = l.GetLights();
            var expected = new PointLight(
                Double4.Point(x, y, z), Color.White * (1.0 / (2 * 2)));

            Assert.Contains(expected, ls);
        }

        [Theory]
        [InlineData(-2, -1, 1)]
        [InlineData(-2, -1, 3)]
        [InlineData(-2, 1, 1)]
        [InlineData(-2, 1, 3)]
        public void TranslatedAreaLight(
            double x,
            double y,
            double z)
        {
            var l = new AreaLight(
                Double4.Point(-2, 0, 2),
                Color.White,
                Double4.Vector(0, 0, 2),
                Double4.Vector(0, 2, 0),
                2,
                2);
            
            var ls = l.GetLights();
            var expected = new PointLight(
                Double4.Point(x, y, z), Color.White * (1.0 / (2 * 2)));

            Assert.Contains(expected, ls);
        }       

        [Theory]
        [InlineData(2, -1, 3)]
        [InlineData(2, -1, 1)]
        [InlineData(2, 1, 3)]
        [InlineData(2, 1, 1)]

        public void TranslatedAreaLight2(
            double x,
            double y,
            double z)
        {
            var l = new AreaLight(
                Double4.Point(2, 0, 2),
                Color.White,
                Double4.Vector(0, 0, -2),
                Double4.Vector(0, 2, 0),
                2,
                2);
            
            var ls = l.GetLights().ToList();
            var expected = new PointLight(
                Double4.Point(x, y, z), Color.White * (1.0 / ls.Count));

            Assert.Contains(expected, ls);
        }     

        // [Fact]
        // public void SingleAreaLight()
        // {
        //     var l = new AreaLight(
        //         Double4.Point(0, 0, 0),
        //         Color.White,
        //         Double4.Vector(0, 1, 0),
        //         Double4.Vector(1, 0, 0),
        //         1,
        //         1);

        //     var ls = l.GetLights().ToList();
        //     var expected = new PointLight(
        //         Double4.Point(0, 0, 0),
        //         Color.White);

        //     Assert.Single(ls);
        //     Assert.Equal(expected, ls[0]);
        // }  
    }
}