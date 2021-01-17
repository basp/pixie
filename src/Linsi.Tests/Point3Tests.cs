namespace Linsi.Tests
{
    using Xunit;

    public class Point3Tests
    {
        [Fact]
        public void TestCtor()
        {
            var a = new Point3(1, 2, 3);
            Assert.Equal(1, a.X);
            Assert.Equal(2, a.Y);
            Assert.Equal(3, a.Z);
        }

        [Fact]
        public void TestPointVectorAddition()
        {
            var a = new Point3(1, 2, 3);
            var u = new Vector3(2, 3, 4);
            var b = a + u;
            Assert.IsType<Point3>(b);
            Assert.Equal(3, b.X);
            Assert.Equal(5, b.Y);
            Assert.Equal(7, b.Z);
        }

        [Fact]
        public void TestPointVectorSubtraction()
        {
            var a = new Point3(1, 2, 3);
            var u = new Vector3(2, 3, 4);
            var b = a - u;
            Assert.IsType<Point3>(b);
            Assert.Equal(-1, b.X);
            Assert.Equal(-1, b.Y);
            Assert.Equal(-1, b.Z);
        }

        [Fact]
        public void TestPointPointSubtraction()
        {
            var a = new Point3(1, 2, 3);
            var b = new Point3(3, 5, 7);
            var u = a - b;
            Assert.IsType<Vector3>(u);
            Assert.Equal(-2, u.X);
            Assert.Equal(-3, u.Y);
            Assert.Equal(-4, u.Z);
        }

        [Fact]
        public void TestPointScalarMultiply()
        {
            var a = new Point3(1, 2, 3);
            var b = a * 2;
            Assert.IsType<Point3>(b);
            Assert.Equal(2, b.X);
            Assert.Equal(4, b.Y);
            Assert.Equal(6, b.Z);
        }

        [Fact]
        public void TestScalarPointMultiply()
        {
            var a = new Point3(1, 2, 3);
            var b = 2 * a;
            Assert.IsType<Point3>(b);
            Assert.Equal(2, b.X);
            Assert.Equal(4, b.Y);
            Assert.Equal(6, b.Z);
        }

        [Fact]
        public void TestComparer()
        {
            var a = new Point3(1, 2, 3);
            var b = new Point3(1.0101, 2.0101, 3.005);
            var cmp = Point3.GetEqualityComparer(epsilon: 0.011);
            Assert.True(cmp.Equals(a, b));
        }
    }
}