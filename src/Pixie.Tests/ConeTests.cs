namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Linie;

    public class ConeTests
    {
        [Theory]
        [InlineData(0, 0, -5, 0, 0, 1, 5, 5)]
        [InlineData(0, 0, -5, 1, 1, 1, 8.66025, 8.66025)]
        [InlineData(1, 1, -5, -0.5, -1, 1, 4.55006, 49.44994)]
        public void IntersectConeWithRay4(
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
            var origin = Vector4.CreatePosition(ox, oy, oz);
            var direction = Vector4.CreateDirection(dx, dy, dz).Normalize();
            var r = new Ray4(origin, direction);
            var xs = shape.LocalIntersect(r);
            const int prec = 5;
            Assert.Equal(2, xs.Count);
            Assert.Equal(t0, xs[0].T, prec);
            Assert.Equal(t1, xs[1].T, prec);
        }

        [Fact]
        public void IntersectConeWithRay4ParallelToOneOfItsHalves()
        {
            var shape = new Cone();
            var direction = Vector4.CreateDirection(0, 1, 1).Normalize();
            var r = new Ray4(Vector4.CreatePosition(0, 0, -1), direction);
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

            var origin = Vector4.CreatePosition(ox, oy, oz);
            var direction = Vector4.CreateDirection(dx, dy, dz).Normalize();
            var r = new Ray4(origin, direction);
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
                    point = Vector4.CreatePosition(0, 0, 0),
                    normal = Vector4.CreateDirection(0, 0, 0),
                },
                new
                {
                    point = Vector4.CreatePosition(1, 1, 1),
                    normal = Vector4.CreateDirection(1, -Math.Sqrt(2), 1),
                },
                new
                {
                    point = Vector4.CreatePosition(-1, -1, 0),
                    normal = Vector4.CreateDirection(-1, 1, 0),
                }
            };

            foreach (var c in cases)
            {
                var n = shape.GetLocalNormal(c.point);
                Assert.Equal(c.normal, n);
            }
        }

        [Fact]
        public void UnboundedConeHasBoundingBox()
        {
            var shape = new Cone();
            var box = shape.GetBounds();

            Assert.True(double.IsNegativeInfinity(box.Min.X));
            Assert.True(double.IsNegativeInfinity(box.Min.Y));
            Assert.True(double.IsNegativeInfinity(box.Min.Z));

            Assert.True(double.IsPositiveInfinity(box.Max.X));
            Assert.True(double.IsPositiveInfinity(box.Max.Y));
            Assert.True(double.IsPositiveInfinity(box.Max.Z));
        }

        [Fact]
        public void BoundedConeHasBoundingBox()
        {
            var shape = new Cone()
            {
                Minimum = -5,
                Maximum = 3,
            };

            var box = shape.GetBounds();
            Assert.Equal(
                Vector4.CreatePosition(-5, -5, -5),
                box.Min);
            Assert.Equal(
                Vector4.CreatePosition(5, 3, 5),
                box.Max);
        }
    }
}