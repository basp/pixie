namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class CylinderTests
    {
        [Theory]
        [InlineData(1, 0, 0, 0, 1, 0)]
        [InlineData(0, 0, 0, 0, 1, 0)]
        [InlineData(0, 0, -5, 1, 1, 1)]
        public void RayMissesCylinder(
            double ox,
            double oy,
            double oz,
            double dx,
            double dy,
            double dz)
        {
            var cyl = new Cylinder();
            var origin = Double4.Point(ox, oy, oz);
            var direction = Double4.Vector(dx, dy, dz).Normalize();
            var r = new Ray(origin, direction);
            var xs = cyl.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Theory]
        [InlineData(1, 0, -5, 0, 0, 1, 5, 5)]
        [InlineData(0, 0, -5, 0, 0, 1, 4, 6)]
        [InlineData(0.5, 0, -5, 0.1, 1, 1, 6.80798, 7.08872)]
        public void RayStrikesCylinder(
            double ox,
            double oy,
            double oz,
            double dx,
            double dy,
            double dz,
            double t0,
            double t1)
        {
            var origin = Double4.Point(ox, oy, oz);
            var direction = Double4.Vector(dx, dy, dz).Normalize();
            var cyl = new Cylinder();
            var r = new Ray(origin, direction);
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
            var p = Double4.Point(px, py, pz);
            var expected = Double4.Vector(nx, ny, nz);
            var n = cyl.LocalNormalAt(p);
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

            var point = Double4.Point(px, py, pz);
            var direciton = Double4.Vector(dx, dy, dz).Normalize();
            var r = new Ray(point, direciton);
            var xs = cyl.LocalIntersect(r);
            Assert.Equal(count, xs.Count);
        }

        [Fact]
        public void DefaultClosedValueForCylinder()
        {
            var cyl = new Cylinder();
            Assert.False(cyl.IsClosed);
        }
    }
}