namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class ConeTests
    {
        [Theory]
        [InlineData(0, 0, -5, 0, 0, 1, 5, 5)]
        [InlineData(0, 0, -5, 1, 1, 1, 8.66025, 8.66025)]
        [InlineData(1, 1, -5, -0.5, -1, 1, 4.55006, 49.44994)]
        public void IntersectConeWithRay(
            double ox,
            double oy,
            double oz,
            double dx,
            double dy,
            double dz,
            double t0,
            double t1)
        {
            var shape = new Cone();
            var origin = Double4.Point(ox, oy, oz);
            var direction = Double4.Vector(dx, dy, dz).Normalize();
            var r = new Ray(origin, direction);
            var xs = shape.LocalIntersect(r);
            const int prec = 5;
            Assert.Equal(2, xs.Count);
            Assert.Equal(t0, xs[0].T, prec);
            Assert.Equal(t1, xs[1].T, prec);
        }

        [Fact]
        public void IntersectConeWithRayParallelToOneOfItsHalves()
        {
            var shape = new Cone();
            var direction = Double4.Vector(0, 1, 1).Normalize();
            var r = new Ray(Double4.Point(0, 0, -1), direction);
            var xs = shape.LocalIntersect(r);
            const int prec = 5;
            Assert.Single(xs);
            Assert.Equal(0.35355, xs[0].T, prec);
        }

        [Theory]
        [InlineData(0, 0, -5, 0, 1, 0, 0)]
        [InlineData(0, 0, -0.25, 0, 1, 1, 2)]
        [InlineData(0, 0, -0.25, 0, 1, 0, 4)]
        public void IntersectingConeEndCaps(
            double ox,
            double oy,
            double oz,
            double dx,
            double dy,
            double dz,
            int count)
        {
            var shape = new Cone()
            {
                Minimum = -0.5,
                Maximum = 0.5,
                IsClosed = true,
            };

            var origin = Double4.Point(ox, oy, oz);
            var direction = Double4.Vector(dx, dy, dz).Normalize();
            var r = new Ray(origin, direction);
            var xs = shape.LocalIntersect(r);
            Assert.Equal(count, xs.Count);
        }

        [Fact]
        public void ComputingTheNormalVectorOnCone()
        {
            var shape = new Cone();
            var cases = new[]
            {
                new
                {
                    point = Double4.Point(0, 0, 0),
                    normal = Double4.Vector(0, 0, 0),
                },
                new
                {
                    point = Double4.Point(1, 1, 1),
                    normal = Double4.Vector(1, -Math.Sqrt(2), 1),
                },
                new
                {
                    point = Double4.Point(-1, -1, 0),
                    normal = Double4.Vector(-1, 1, 0),
                }
            };

            foreach (var c in cases)
            {
                var n = shape.LocalNormalAt(c.point);
                Assert.Equal(c.normal, n);
            }
        }
    }
}