namespace Pixie.Tests
{
    using Xunit;
    using Linsi;

    public class GradientTests
    {
        [Fact]
        public void GradientInterpolatesBetweenColors()
        {
            var g = new Gradient(Color.Black, Color.White);
            var expected = new Color(0.25, 0.25, 0.25);
            Assert.Equal(expected, g[0.25]);
        }
    }
}