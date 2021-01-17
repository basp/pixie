namespace Linsi.Tests
{
    using Xunit;

    public class Point2Tests
    {
        [Fact]
        public void TestCtor()
        {
            var a = new Point2(1, 2);
            var b = new Point2(3, 5);
            Assert.Equal(1, a.X);
            Assert.Equal(2, a.Y);
            Assert.Equal(3, b.X);
            Assert.Equal(5, b.Y);
        }

        [Fact]
        public void TestPointVectorAddition()
        {
            var a = new Point2(1, 2);
            var u = new Vector2(2, 3);
            var b = a + u;
            Assert.IsType<Point2>(b);
            Assert.Equal(3, b.X);
            Assert.Equal(5, b.Y);
        }

        [Fact]
        public void TestPointVectorSubtraction()
        {
            var a = new Point2(1, 2);
            var u = new Vector2(2, 3);
            var b = a - u;
            Assert.IsType<Point2>(b);
            Assert.Equal(-1, b.X);
            Assert.Equal(-1, b.Y);
        }

        [Fact]
        public void TestPointPointSubtraction()
        {
            var a = new Point2(1, 2);
            var b = new Point2(3, 5);
            var u = a - b;
            Assert.IsType<Vector2>(u);
            Assert.Equal(-2, u.X);
            Assert.Equal(-3, u.Y);
        }

        [Fact]
        public void TestPointScalarMultiply()
        {
            var a = new Point2(1, 2);
            var b = a * 2;
            Assert.IsType<Point2>(b);
            Assert.Equal(2, b.X);
            Assert.Equal(4, b.Y);
        }

        [Fact]
        public void TestScalarPointMultiply()
        {
            var a = new Point2(1, 2);
            var b = 2 * a;
            Assert.IsType<Point2>(b);
            Assert.Equal(2, b.X);
            Assert.Equal(4, b.Y);
        }
    }
}