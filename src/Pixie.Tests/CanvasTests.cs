namespace Pixie.Tests
{
    using Xunit;

    public class CanvasTests
    {
        [Fact]
        public void TestCanvasConstructor()
        {
            var c = new Canvas(10, 20);
            Assert.Equal(10, c.Width);
            Assert.Equal(20, c.Height);
            for (var j = 0; j < 20; j++)
            {
                for (var i = 0; i < 10; i++)
                {
                    Assert.Equal(new Color(0, 0, 0), c[i, j]);
                }
            }
        }

        [Fact]
        public void TestWritingPixels()
        {
            var c = new Canvas(10, 20);
            var red = new Color(1, 0, 0);
            c[2, 3] = red;
            Assert.Equal(red, c[2, 3]);
        }
    }
}
