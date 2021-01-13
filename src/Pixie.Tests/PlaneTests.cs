namespace Pixie.Tests
{
    using Xunit;
    using Linsi;

    public class PlaneTests
    {
        [Fact]
        public void NormalOfPlaneIsConstantEverywhere()
        {
            var p = new Plane();
            var n1 = p.GetLocalNormal(Vector4.CreatePosition(0, 0, 0));
            var n2 = p.GetLocalNormal(Vector4.CreatePosition(10, 0, -10));
            var n3 = p.GetLocalNormal(Vector4.CreatePosition(-5, 0, 150));
            var expected = Vector4.CreateDirection(0, 1, 0);
            Assert.Equal(expected, n1);
            Assert.Equal(expected, n2);
            Assert.Equal(expected, n3);
        }

        [Fact]
        public void IntersectWithRayParallelToPlane()
        {
            var p = new Plane();
            var r = new Ray(Vector4.CreatePosition(0, 10, 0), Vector4.CreateDirection(0, 0, 1));
            var xs = p.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void IntersectWithCoplanarRay()
        {
            var p = new Plane();
            var r = new Ray(Vector4.CreatePosition(0, 0, 0), Vector4.CreateDirection(0, 0, 1));
            var xs = p.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void RayItersectingPlaneFromAbove()
        {
            var p = new Plane();
            var r = new Ray(Vector4.CreatePosition(0, 1, 0), Vector4.CreateDirection(0, -1, 0));
            var xs = p.LocalIntersect(r);
            Assert.Single(xs);
            Assert.Equal(1, xs[0].T);
            Assert.Equal(p, xs[0].Object);
        }

        [Fact]
        public void RayIntersectingPlaneFromBelow()
        {
            var p = new Plane();
            var r = new Ray(Vector4.CreatePosition(0, -1, 0), Vector4.CreateDirection(0, 1, 0));
            var xs = p.LocalIntersect(r);
            Assert.Single(xs);
            Assert.Equal(1, xs[0].T);
            Assert.Equal(p, xs[0].Object);
        }

        [Fact]
        public void PlaneHasBoundingBox()
        {
            var s = new Plane();
            var box = s.GetBounds();

            Assert.True(double.IsNegativeInfinity(box.Min.X));
            Assert.Equal(0, box.Min.Y);
            Assert.True(double.IsNegativeInfinity(box.Min.Z));

            Assert.True(double.IsPositiveInfinity(box.Max.X));
            Assert.Equal(0, box.Max.Y);
            Assert.True(double.IsPositiveInfinity(box.Max.Z));
        }
    }
}