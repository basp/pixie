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

        [Fact]
        public void TestNormalOnSphereAtPointOnXAxis()
        {
            var s = new Sphere();
            var n = s.NormalAt(Float4.Point(1, 0, 0));
            var expected = Float4.Vector(1, 0, 0);
            Assert.Equal(expected, n);
        }

        [Fact]
        public void TestNormalOnSphereAtPointOnYAxis()
        {
            var s = new Sphere();
            var n = s.NormalAt(Float4.Point(0, 1, 0));
            var expected = Float4.Vector(0, 1, 0);
            Assert.Equal(expected, n);
        }

        [Fact]
        public void TestNormalOnSphereAtPointOnZAxis()
        {
            var s = new Sphere();
            var n = s.NormalAt(Float4.Point(0, 0, 1));
            var expected = Float4.Vector(0, 0, 1);
            Assert.Equal(expected, n);
        }

        [Fact]
        public void TestNormalOnSphereAtNonAxialPoint()
        {
            var sqrt3over3 = (float)Math.Sqrt(3)/3;
            var s = new Sphere();            
            var p = Float4.Point(sqrt3over3, sqrt3over3, sqrt3over3);
            var n = s.NormalAt(p);
            var expected = Float4.Vector(sqrt3over3, sqrt3over3, sqrt3over3);
            const float eps = 0.0000001f;
            var comparer = new ApproxFloat4EqualityComparer(eps);
            Assert.Equal(expected, n, comparer);
        }

        [Fact]
        public void TestNormalIsNormalized()
        {
            var s = new Sphere();
            var p = Float4.Point(
                (float)Math.Sqrt(3)/3,
                (float)Math.Sqrt(3)/3,
                (float)Math.Sqrt(3)/3);
            var n = s.NormalAt(p);
            const float eps = 0.0000001f;
            var comparer = new ApproxFloat4EqualityComparer(eps);
            Assert.Equal(n.Normalize(), n, comparer);
        }

        [Fact]
        public void TestNormalOnTranslatedSphere()
        {
            var s = new Sphere()
            {
                Transform = Transform.Translate(0, 1, 0),
            };

            var p = Float4.Point(0, 1.70711f, -0.70711f);
            var n = s.NormalAt(p);
            var expected = Float4.Vector(0, 0.70711f, -0.70711f);
            const float eps = 0.00001f;
            var comparer = new ApproxFloat4EqualityComparer(eps);
            Assert.Equal(expected, n, comparer);
        }

        [Fact]
        public void TestNormalOnTransformedSphere()
        {
            var s = new Sphere()
            {
                Transform = 
                    Transform.Scale(1, 0.5f, 1) * Transform.RotateZ((float)Math.PI/5),
            };

            var p = Float4.Point(0, (float)Math.Sqrt(2)/2, -(float)Math.Sqrt(2)/2);
            var n = s.NormalAt(p);
            var expected = Float4.Vector(0, 0.97014f, -0.24254f);
            const float eps = 0.00001f;
            var comparer = new ApproxFloat4EqualityComparer(eps);
            Assert.Equal(expected, n, comparer);
        }
    }
}