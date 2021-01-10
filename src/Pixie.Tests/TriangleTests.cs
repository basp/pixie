namespace Pixie.Tests
{
    using Xunit;

    public class TriangleTests
    {
        [Fact]
        public void CreateTriangle()
        {
            var p1 = Vector4.CreatePosition(0, 1, 0);
            var p2 = Vector4.CreatePosition(-1, 0, 0);
            var p3 = Vector4.CreatePosition(1, 0, 0);
            var t = new Triangle(p1, p2, p3);
            Assert.Equal(p1, t.P1);
            Assert.Equal(p2, t.P2);
            Assert.Equal(p3, t.P3);
            Assert.Equal(Vector4.CreateDirection(-1, -1, 0), t.E1);
            Assert.Equal(Vector4.CreateDirection(1, -1, 0), t.E2);
            Assert.Equal(Vector4.CreateDirection(0, 0, -1), t.Normal);
        }

        [Fact]
        public void FindNormalOnTriangle()
        {
            var p1 = Vector4.CreatePosition(0, 1, 0);
            var p2 = Vector4.CreatePosition(-1, 0, 0);
            var p3 = Vector4.CreatePosition(1, 0, 0);
            var t = new Triangle(p1, p2, p3);
            var n1 = t.LocalNormalAt(Vector4.CreatePosition(0, 0.5, 0));
            var n2 = t.LocalNormalAt(Vector4.CreatePosition(-0.5, 0.75, 0));
            var n3 = t.LocalNormalAt(Vector4.CreatePosition(0.5, 0.25, 0));
            Assert.Equal(t.Normal, n1);
            Assert.Equal(t.Normal, n2);
            Assert.Equal(t.Normal, n3);
        }

        [Fact]
        public void IntersectRayParallelToTriangle()
        {
            var t = new Triangle(
                Vector4.CreatePosition(0, 1, 0),
                Vector4.CreatePosition(-1, 0, 0),
                Vector4.CreatePosition(1, 0, 0));

            var r = new Ray(
                Vector4.CreatePosition(0, -1, -2),
                Vector4.CreateDirection(0, 1, 0));

            var xs = t.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void RayMissesP1P3Edge()
        {
            var t = new Triangle(
                Vector4.CreatePosition(0, 1, 0),
                Vector4.CreatePosition(-1, 0, 0),
                Vector4.CreatePosition(1, 0, 0));

            var r = new Ray(
                Vector4.CreatePosition(1, 1, -2),
                Vector4.CreateDirection(0, 0, 1));

            var xs = t.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void RayMissedP1P2Edge()
        {
            var t = new Triangle(
                Vector4.CreatePosition(0, 1, 0),
                Vector4.CreatePosition(-1, 0, 0),
                Vector4.CreatePosition(1, 0, 0));

            var r = new Ray(
                Vector4.CreatePosition(-1, 1, -2),
                Vector4.CreateDirection(0, 0, 1));

            var xs = t.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void RayMissedP2P3Edge()
        {
            var t = new Triangle(
                Vector4.CreatePosition(0, 1, 0),
                Vector4.CreatePosition(-1, 0, 0),
                Vector4.CreatePosition(1, 0, 0));

            var r = new Ray(
                Vector4.CreatePosition(0, -1, -2),
                Vector4.CreateDirection(0, 0, 1));

            var xs = t.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void RayStrikesTriangle()
        {
            var t = new Triangle(
                Vector4.CreatePosition(0, 1, 0),
                Vector4.CreatePosition(-1, 0, 0),
                Vector4.CreatePosition(1, 0, 0));

            var r = new Ray(
                Vector4.CreatePosition(0, 0.5, -2),
                Vector4.CreateDirection(0, 0, 1));

            var xs = t.LocalIntersect(r);
            Assert.Single(xs);
            Assert.Equal(2, xs[0].T);
        }

        [Fact]
        public void TriangleHasBoundingBox()
        {
            var p1 = Vector4.CreatePosition(-3, 7, 2);
            var p2 = Vector4.CreatePosition(6, 2, -4);
            var p3 = Vector4.CreatePosition(2, -1, -1);
            var shape = new Triangle(p1, p2, p3);
            var box = shape.Bounds();
            Assert.Equal(
                Vector4.CreatePosition(-3, -1, -4),
                box.Min);
            Assert.Equal(
                Vector4.CreatePosition(6, 7, 2),
                box.Max);
        }
    }
}