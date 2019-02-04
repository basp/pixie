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
            Assert.Equal(2, xs.Count);
            Assert.Equal(4, xs[0].T);
            Assert.Equal(6, xs[1].T);   
        }

        [Fact]
        public void TestRayIntersectsSphereAtTangent()
        {
            var r = new Ray(Float4.Point(0, 1, -5), Float4.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Count);
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
            Assert.Equal(2, xs.Count);
            Assert.Equal(-1.0, xs[0].T);
            Assert.Equal(1.0, xs[1].T);
        }

        [Fact]
        public void TestSphereIsBehindRay()
        {
            var r = new Ray(Float4.Point(0, 0, 5), Float4.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Count);
            Assert.Equal(-6.0, xs[0].T);
            Assert.Equal(-4.0, xs[1].T);
        }

        [Fact]
        public void TestSphereDefaultTransformation()
        {
            const float eps = 0.000000000001f;
            var s = new Sphere();
            var comparer = new ApproxFloat4x4EqualityComparer(eps);
            Assert.Equal(Float4x4.Identity, s.Transform, comparer);
        }

        [Fact]
        public void TestChangeSphereTransformation()
        {
            var s = new Sphere();
            var t = Transform.Translate(2, 3, 4);
            s.Transform = t;
            Assert.Equal(t, s.Transform);
        }

        [Fact]
        public void TestIntersectScaledSphere()
        {
            var r = new Ray(Float4.Point(0, 0, -5), Float4.Vector(0, 0, 1));
            var s = new Sphere()
            {
                Transform = Transform.Scale(2, 2, 2),
            };

            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Count);
            Assert.Equal(3, xs[0].T);
            Assert.Equal(7, xs[1].T);
        }

        [Fact]
        public void TestIntersectTranslatedSphere()
        {
            var r = new Ray(Float4.Point(0, 0, -5), Float4.Vector(0, 0, 1));
            var s = new Sphere()
            {
                Transform = Transform.Translate(5, 0, 0),
            };

            var xs = s.Intersect(r);
            Assert.Empty(xs);
        }
    }
}