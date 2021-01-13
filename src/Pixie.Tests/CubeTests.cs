namespace Pixie.Tests
{
    using Xunit;
    using Linsi;

    public class CubeTests
    {
        [Theory]
        [InlineData(5, 0.5, 0, -1, 0, 0, 4, 6)]
        [InlineData(-5, 0.5, 0, 1, 0, 0, 4, 6)]
        [InlineData(0.5, 5, 0, 0, -1, 0, 4, 6)]
        [InlineData(0.5, -5, 0, 0, 1, 0, 4, 6)]
        [InlineData(0.5, 0, 5, 0, 0, -1, 4, 6)]
        [InlineData(0.5, 0, -5, 0, 0, 1, 4, 6)]
        [InlineData(0, 0.5, 0, 0, 0, 1, -1, 1)]
        public void RayIntersectsCube(
            double ox,
            double oy,
            double oz,
            double dx,
            double dy,
            double dz,
            double t1,
            double t2)
        {
            var c = new Cube();
            var origin = Vector4.CreatePosition(ox, oy, oz);
            var direction = Vector4.CreateDirection(dx, dy, dz);
            var r = new Ray(origin, direction);
            var xs = c.LocalIntersect(r);
            Assert.Equal(2, xs.Count);
            Assert.Equal(t1, xs[0].T);
            Assert.Equal(t2, xs[1].T);
        }

        [Theory]
        [InlineData(-2, 0, 0, 0.2673, 0.5345, 0.8018)]
        [InlineData(0, -2, 0, 0.8018, 0.2673, 0.5345)]
        [InlineData(0, 0, -2, 0.5345, 0.8018, 0.2673)]
        [InlineData(2, 0, 2, 0, 0, -1)]
        [InlineData(0, 2, 2, 0, -1, 0)]
        [InlineData(2, 2, 0, -1, 0, 0)]
        public void RayMissesCube(
            double ox,
            double oy,
            double oz,
            double dx,
            double dy,
            double dz)
        {
            var c = new Cube();
            var origin = Vector4.CreatePosition(ox, oy, oz);
            var direction = Vector4.CreateDirection(dx, dy, dz);
            var r = new Ray(origin, direction);
            var xs = c.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Theory]
        [InlineData(1, 0.5, -0.8, 1, 0, 0)]
        [InlineData(-1, -0.2, 0.9, -1, 0, 0)]
        [InlineData(-0.4, 1, -0.1, 0, 1, 0)]
        [InlineData(0.3, -1, -0.7, 0, -1, 0)]
        [InlineData(-0.6, 0.3, 1, 0, 0, 1)]
        [InlineData(0.4, 0.4, -1, 0, 0, -1)]
        [InlineData(1, 1, 1, 1, 0, 0)]
        [InlineData(-1, -1, -1, -1, 0, 0)]
        public void NormalOnSurfaceOfCube(
            double ox,
            double oy,
            double oz,
            double dx,
            double dy,
            double dz)
        {
            var c = new Cube();
            var p = Vector4.CreatePosition(ox, oy, oz);
            var expected = Vector4.CreateDirection(dx, dy, dz);
            var n = c.GetLocalNormal(p);
            Assert.Equal(expected, n);
        }

        [Fact]
        public void CubeHasBoundingBox()
        {
            var shape = new Cube();
            var box = shape.GetBounds();

            Assert.Equal(
                Vector4.CreatePosition(-1, -1, -1),
                box.Min);

            Assert.Equal(
                Vector4.CreatePosition(1, 1, 1),
                box.Max);
        }
    }
}