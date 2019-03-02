namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class TriangleTests
    {
        [Fact]
        public void CreateTriangle()
        {
            var p1 = Double4.Point(0, 1, 0);
            var p2 = Double4.Point(-1, 0, 0);
            var p3 = Double4.Point(1, 0, 0);
            var t = new Triangle(p1, p2, p3);
            Assert.Equal(p1, t.P1);
            Assert.Equal(p2, t.P2);
            Assert.Equal(p3, t.P3);
            Assert.Equal(Double4.Vector(-1, -1, 0), t.E1);
            Assert.Equal(Double4.Vector(1, -1, 0), t.E2);
            Assert.Equal(Double4.Vector(0, 0, -1), t.Normal);
        }

        [Fact]
        public void FindNormalOnTriangle()
        {
            var p1 = Double4.Point(0, 1, 0);
            var p2 = Double4.Point(-1, 0, 0);
            var p3 = Double4.Point(1, 0, 0);
            var t = new Triangle(p1, p2, p3);
            var n1 = t.LocalNormalAt(Double4.Point(0, 0.5, 0));
            var n2 = t.LocalNormalAt(Double4.Point(-0.5, 0.75, 0));
            var n3 = t.LocalNormalAt(Double4.Point(0.5, 0.25, 0));
            Assert.Equal(t.Normal, n1);
            Assert.Equal(t.Normal, n2);
            Assert.Equal(t.Normal, n3);
        }

        [Fact]
        public void IntersectRayParallelToTriangle()
        {
            var t = new Triangle(
                Double4.Point(0, 1, 0),
                Double4.Point(-1, 0, 0),
                Double4.Point(1, 0, 0));

            var r = new Ray(
                Double4.Point(0, -1, -2),
                Double4.Vector(0, 1, 0));

            var xs = t.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void RayMissesP1P3Edge()
        {
            var t = new Triangle(
                Double4.Point(0, 1, 0),
                Double4.Point(-1, 0, 0),
                Double4.Point(1, 0, 0));

            var r = new Ray(
                Double4.Point(1, 1, -2),
                Double4.Vector(0, 0, 1));

            var xs = t.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void RayMissedP1P2Edge()
        {
            var t = new Triangle(
                Double4.Point(0, 1, 0),
                Double4.Point(-1, 0, 0),
                Double4.Point(1, 0, 0));

            var r = new Ray(
                Double4.Point(-1, 1, -2),
                Double4.Vector(0, 0, 1));

            var xs = t.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void RayMissedP2P3Edge()
        {
            var t = new Triangle(
                Double4.Point(0, 1, 0),
                Double4.Point(-1, 0, 0),
                Double4.Point(1, 0, 0));

            var r = new Ray(
                Double4.Point(0, -1, -2),
                Double4.Vector(0, 0, 1));

            var xs = t.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void RayStrikesTriangle()
        {
            var t = new Triangle(
                Double4.Point(0, 1, 0),
                Double4.Point(-1, 0, 0),
                Double4.Point(1, 0, 0));

            var r = new Ray(
                Double4.Point(0, 0.5, -2),
                Double4.Vector(0, 0, 1));

            var xs = t.LocalIntersect(r);
            Assert.Single(xs);
            Assert.Equal(2, xs[0].T);
        }

        [Fact]
        public void TriangleHasBoundingBox()
        {
            var p1 = Double4.Point(-3, 7, 2);
            var p2 = Double4.Point(6, 2, -4);
            var p3 = Double4.Point(2, -1, -1);
            var shape = new Triangle(p1, p2, p3);
            var box = shape.Bounds();
            Assert.Equal(
                Double4.Point(-3, -1, -4),
                box.Min);
            Assert.Equal(
                Double4.Point(6, 7, 2),
                box.Max);
        }
    }
}