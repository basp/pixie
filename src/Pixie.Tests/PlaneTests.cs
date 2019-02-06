namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class PlaneTests
    {
        [Fact]
        public void NormalOfPlaneIsConstantEverywhere()
        {
            var p = new Plane();
            var n1 = p.LocalNormalAt(Double4.Point(0, 0, 0));
            var n2 = p.LocalNormalAt(Double4.Point(10, 0, -10));
            var n3 = p.LocalNormalAt(Double4.Point(-5, 0, 150));
            var expected = Double4.Vector(0, 1, 0);
            Assert.Equal(expected, n1);
            Assert.Equal(expected, n2);
            Assert.Equal(expected, n3);
        }

        [Fact]
        public void IntersectWithRayParallelToPlane()
        {
            var p = new Plane();
            var r = new Ray(Double4.Point(0, 10, 0), Double4.Vector(0, 0, 1));
            var xs = p.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void IntersectWithCoplanarRay()
        {
            var p = new Plane();
            var r = new Ray(Double4.Point(0, 0, 0), Double4.Vector(0, 0, 1));
            var xs = p.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public  void RayItersectingPlaneFromAbove()
        {
            var p = new Plane();
            var r = new Ray(Double4.Point(0, 1, 0), Double4.Vector(0, -1, 0));
            var xs = p.LocalIntersect(r);
            Assert.Single(xs);
            Assert.Equal(1, xs[0].T);
            Assert.Equal(p, xs[0].Object);
        }

        [Fact]
        public void RayIntersectingPlaneFromBelow()
        {
            var p = new Plane();
            var r = new Ray(Double4.Point(0, -1, 0), Double4.Vector(0, 1, 0));
            var xs = p.LocalIntersect(r);
            Assert.Single(xs);
            Assert.Equal(1, xs[0].T);
            Assert.Equal(p, xs[0].Object);
        }
    }
}