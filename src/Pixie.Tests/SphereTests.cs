namespace Pixie.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Pixie.Core;

    public class SphereTests
    {
        [Fact]
        public void TestRayIntersectsSphereAtTwoPoints()
        {
            var r = new Ray(Float4.Point(0, 0, -5), Float4.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Length);
            Assert.Equal(4, xs[0].T);
            Assert.Equal(6, xs[1].T);   
        }

        [Fact]
        public void TestRayIntersectsSphereAtTangent()
        {
            var r = new Ray(Float4.Point(0, 1, -5), Float4.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Length);
            Assert.Equal(5.0, xs[0].T);
            Assert.Equal(5.0, xs[1].T);
        }

        [Fact]
        public void TestRayMissesSphere()
        {
            var r = new Ray(Float4.Point(0, 2, -5), Float4.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void TestRayOriginatesInsideSphere()
        {
            var r = new Ray(Float4.Point(0, 0, 0), Float4.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Length);
            Assert.Equal(-1.0, xs[0].T);
            Assert.Equal(1.0, xs[1].T);
        }

        [Fact]
        public void TestSphereIsBehindRay()
        {
            var r = new Ray(Float4.Point(0, 0, 5), Float4.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Length);
            Assert.Equal(-6.0, xs[0].T);
            Assert.Equal(-4.0, xs[1].T);
        }
    }
}