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
            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Count);
            Assert.Equal(4, xs[0].T);
            Assert.Equal(6, xs[1].T);
        }

        [Fact]
        public void TestRayIntersectsSphereAtTangent()
        {
            var r = new Ray(Double4.Point(0, 1, -5), Double4.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Count);
            Assert.Equal(5.0, xs[0].T);
            Assert.Equal(5.0, xs[1].T);
        }

        [Fact]
        public void TestRayMissesSphere()
        {
            var r = new Ray(Double4.Point(0, 2, -5), Double4.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void TestRayOriginatesInsideSphere()
        {
            var r = new Ray(Double4.Point(0, 0, 0), Double4.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Count);
            Assert.Equal(-1.0, xs[0].T);
            Assert.Equal(1.0, xs[1].T);
        }

        [Fact]
        public void TestSphereIsBehindRay()
        {
            var r = new Ray(Double4.Point(0, 0, 5), Double4.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Count);
            Assert.Equal(-6.0, xs[0].T);
            Assert.Equal(-4.0, xs[1].T);
        }

        [Fact]
        public void TestIntersectScaledSphere()
        {
            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 0, 1));
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
            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 0, 1));
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
            var n = s.NormalAt(Double4.Point(1, 0, 0));
            var expected = Double4.Vector(1, 0, 0);
            Assert.Equal(expected, n);
        }

        [Fact]
        public void TestNormalOnSphereAtPointOnYAxis()
        {
            var s = new Sphere();
            var n = s.NormalAt(Double4.Point(0, 1, 0));
            var expected = Double4.Vector(0, 1, 0);
            Assert.Equal(expected, n);
        }

        [Fact]
        public void TestNormalOnSphereAtPointOnZAxis()
        {
            var s = new Sphere();
            var n = s.NormalAt(Double4.Point(0, 0, 1));
            var expected = Double4.Vector(0, 0, 1);
            Assert.Equal(expected, n);
        }

        [Fact]
        public void TestNormalOnSphereAtNonAxialPoint()
        {
            var sqrt3over3 = Math.Sqrt(3) / 3;
            var s = new Sphere();
            var p = Double4.Point(sqrt3over3, sqrt3over3, sqrt3over3);
            var n = s.NormalAt(p);
            var expected = Double4.Vector(sqrt3over3, sqrt3over3, sqrt3over3);
            const double eps = 0.0000001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(expected, n, comparer);
        }

        [Fact]
        public void TestNormalIsNormalized()
        {
            var s = new Sphere();
            var p = Double4.Point(
                Math.Sqrt(3) / 3,
                Math.Sqrt(3) / 3,
                Math.Sqrt(3) / 3);
            var n = s.NormalAt(p);
            const double eps = 0.0000001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(n.Normalize(), n, comparer);
        }

        [Fact]
        public void TestNormalOnTranslatedSphere()
        {
            var s = new Sphere()
            {
                Transform = Transform.Translate(0, 1, 0),
            };

            var p = Double4.Point(0, 1.70711, -0.70711);
            var n = s.NormalAt(p);
            var expected = Double4.Vector(0, 0.70711, -0.70711);
            const double eps = 0.00001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(expected, n, comparer);
        }

        [Fact]
        public void TestNormalOnTransformedSphere()
        {
            var s = new Sphere()
            {
                Transform =
                    Transform.Scale(1, 0.5, 1) * Transform.RotateZ(Math.PI / 5),
            };

            var p = Double4.Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            var n = s.NormalAt(p);
            var expected = Double4.Vector(0, 0.97014, -0.24254);
            const double eps = 0.00001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(expected, n, comparer);
        }

        [Fact]
        public void TestSphereDefaultMaterial()
        {
            var s = new Sphere();
            var @default = new Material();
            const int prec = 15;
            Assert.Equal(@default.Ambient, s.Material.Ambient, prec);
            Assert.Equal(@default.Diffuse, s.Material.Diffuse, prec);
            Assert.Equal(@default.Specular, s.Material.Specular, prec);
            Assert.Equal(@default.Shininess, s.Material.Shininess, prec);
        }

        [Fact]
        public void TestSphereMaybeAssignedMaterial()
        {
            var s = new Sphere();
            var m = s.Material;
            m.Ambient = 1;
            s.Material = m;
            Assert.Equal(m, s.Material);
        }
    }
}