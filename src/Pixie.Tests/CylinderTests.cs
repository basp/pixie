namespace Pixie.Tests
{
    using Xunit;
    using Linie;

    public class CylinderTests
    {
        [Theory]
        [InlineData(1, 0, 0, 0, 1, 0)]
        [InlineData(0, 0, 0, 0, 1, 0)]
        [InlineData(0, 0, -5, 1, 1, 1)]
        public void Ray4MissesCylinder(
            double ox,
            double oy,
            double oz,
            double dx,
            double dy,
            double dz)
        {
            var cyl = new Cylinder();
            var origin = Vector4.CreatePosition(ox, oy, oz);
            var direction = Vector4.CreateDirection(dx, dy, dz).Normalize();
            var r = new Ray4(origin, direction);
            var xs = cyl.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Theory]
        [InlineData(1, 0, -5, 0, 0, 1, 5, 5)]
        [InlineData(0, 0, -5, 0, 0, 1, 4, 6)]
        [InlineData(0.5, 0, -5, 0.1, 1, 1, 6.80798, 7.08872)]
        public void Ray4StrikesCylinder(
            double ox,
            double oy,
            double oz,
            double dx,
            double dy,
            double dz,
            double t0,
            double t1)
        {
            var origin = Vector4.CreatePosition(ox, oy, oz);
            var direction = Vector4.CreateDirection(dx, dy, dz).Normalize();
            var cyl = new Cylinder();
            var r = new Ray4(origin, direction);
            var xs = cyl.LocalIntersect(r);
            Assert.Equal(2, xs.Count);
            const int prec = 5;
            Assert.Equal(t0, xs[0].T, prec);
            Assert.Equal(t1, xs[1].T, prec);
        }

        [Theory]
        [InlineData(1, 0, 0, 1, 0, 0)]
        [InlineData(0, 5, -1, 0, 0, -1)]
        [InlineData(0, -2, 1, 0, 0, 1)]
        [InlineData(-1, 1, 0, -1, 0, 0)]
        public void NormalVectorOnCylinder(
            double px,
            double py,
            double pz,
            double nx,
            double ny,
            double nz)
        {
            var cyl = new Cylinder();
            var p = Vector4.CreatePosition(px, py, pz);
            var expected = Vector4.CreateDirection(nx, ny, nz);
            var n = cyl.GetLocalNormal(p);
            Assert.Equal(expected, n);
        }

        [Fact]
        public void DefaultMinAndMaxForCylinder()
        {
            var cyl = new Cylinder();
            Assert.Equal(double.NegativeInfinity, cyl.Minimum);
            Assert.Equal(double.PositiveInfinity, cyl.Maximum);
        }

        [Theory]
        [InlineData(0, 1.5, 0, 0.1, 1, 0, 0)]
        [InlineData(0, 3, -5, 0, 0, 1, 0)]
        [InlineData(0, 0, -5, 0, 0, 1, 0)]
        [InlineData(0, 2, -5, 0, 0, 1, 0)]
        [InlineData(0, 1, -5, 0, 0, 1, 0)]
        [InlineData(0, 1.5, -2, 0, 0, 1, 2)]
        public void IntersectConstrainedCylinder(
            double px,
            double py,
            double pz,
            double dx,
            double dy,
            double dz,
            int count)
        {
            var cyl = new Cylinder()
            {
                Minimum = 1,
                Maximum = 2,
            };

            var point = Vector4.CreatePosition(px, py, pz);
            var direciton = Vector4.CreateDirection(dx, dy, dz).Normalize();
            var r = new Ray4(point, direciton);
            var xs = cyl.LocalIntersect(r);
            Assert.Equal(count, xs.Count);
        }

        [Fact]
        public void DefaultClosedValueForCylinder()
        {
            var cyl = new Cylinder();
            Assert.False(cyl.IsClosed);
        }

        [Theory]
        [InlineData(0, 3, 0, 0, -1, 0, 2)]
        public void IntersectCapsOfClosedCylinder(
            double ox,
            double oy,
            double oz,
            double dx,
            double dy,
            double dz,
            int count)
        {
            var cyl = new Cylinder();
            cyl.Minimum = 1;
            cyl.Maximum = 2;
            cyl.IsClosed = true;
            var direction = Vector4.CreateDirection(dx, dy, dz).Normalize();
            var point = Vector4.CreatePosition(ox, oy, oz);
            var r = new Ray4(point, direction);
            var xs = cyl.LocalIntersect(r);
            Assert.Equal(count, xs.Count);
        }

        [Theory]
        [InlineData(0, 1, 0, 0, -1, 0)]
        [InlineData(0.5, 1, 0, 0, -1, 0)]
        [InlineData(0, 1, 0.5, 0, -1, 0)]
        [InlineData(0, 2, 0f, 0, 1, 0)]
        [InlineData(0.5, 2, 0, 0, 1, 0)]
        [InlineData(0, 2, 0.5, 0, 1, 0)]
        public void NormalVectorOnCylinderEndCaps(
            double px,
            double py,
            double pz,
            double nx,
            double ny,
            double nz)
        {
            var cyl = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                IsClosed = true,
            };

            var p = Vector4.CreatePosition(px, py, pz);
            var n = cyl.GetLocalNormal(p);
            var expected = Vector4.CreateDirection(nx, ny, nz);
            Assert.Equal(expected, n);
        }

        [Fact]
        public void UnboundedCylinderHasBoundingBox()
        {
            var shape = new Cylinder();
            var box = shape.GetBounds();

            Assert.Equal(-1, box.Min.X);
            Assert.True(double.IsNegativeInfinity(box.Min.Y));
            Assert.Equal(-1, box.Min.Z);

            Assert.Equal(1, box.Max.X);
            Assert.True(double.IsInfinity(box.Max.Y));
            Assert.Equal(1, box.Max.Z);
        }

        [Fact]
        public void BoundedCylinderHasBoundingBox()
        {
            var shape = new Cylinder();
            shape.Minimum = -5;
            shape.Maximum = 3;

            var box = shape.GetBounds();
            Assert.Equal(
                Vector4.CreatePosition(-1, -5, -1),
                box.Min);

            Assert.Equal(
                Vector4.CreatePosition(1, 3, 1),
                box.Max);
        }
    }
}