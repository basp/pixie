namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Linie;

    public class SphereTests
    {
        [Fact]
        public void TestRay4IntersectsSphereAtTwoPoints()
        {
            var r = new Ray4(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Count);
            Assert.Equal(4, xs[0].T);
            Assert.Equal(6, xs[1].T);
        }

        [Fact]
        public void TestRay4IntersectsSphereAtTangent()
        {
            var r = new Ray4(Vector4.CreatePosition(0, 1, -5), Vector4.CreateDirection(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Count);
            Assert.Equal(5.0, xs[0].T);
            Assert.Equal(5.0, xs[1].T);
        }

        [Fact]
        public void TestRay4MissesSphere()
        {
            var r = new Ray4(Vector4.CreatePosition(0, 2, -5), Vector4.CreateDirection(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void TestRay4OriginatesInsideSphere()
        {
            var r = new Ray4(Vector4.CreatePosition(0, 0, 0), Vector4.CreateDirection(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Count);
            Assert.Equal(-1.0, xs[0].T);
            Assert.Equal(1.0, xs[1].T);
        }

        [Fact]
        public void TestSphereIsBehindRay4()
        {
            var r = new Ray4(Vector4.CreatePosition(0, 0, 5), Vector4.CreateDirection(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.Equal(2, xs.Count);
            Assert.Equal(-6.0, xs[0].T);
            Assert.Equal(-4.0, xs[1].T);
        }

        [Fact]
        public void TestIntersectScaledSphere()
        {
            var r = new Ray4(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
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
            var r = new Ray4(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
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
            var n = s.GetNormal(Vector4.CreatePosition(1, 0, 0));
            var expected = Vector4.CreateDirection(1, 0, 0);
            Assert.Equal(expected, n);
        }

        [Fact]
        public void TestNormalOnSphereAtPointOnYAxis()
        {
            var s = new Sphere();
            var n = s.GetNormal(Vector4.CreatePosition(0, 1, 0));
            var expected = Vector4.CreateDirection(0, 1, 0);
            Assert.Equal(expected, n);
        }

        [Fact]
        public void TestNormalOnSphereAtPointOnZAxis()
        {
            var s = new Sphere();
            var n = s.GetNormal(Vector4.CreatePosition(0, 0, 1));
            var expected = Vector4.CreateDirection(0, 0, 1);
            Assert.Equal(expected, n);
        }

        [Fact]
        public void TestNormalOnSphereAtNonAxialPoint()
        {
            var sqrt3over3 = Math.Sqrt(3) / 3;
            var s = new Sphere();
            var p = Vector4.CreatePosition(sqrt3over3, sqrt3over3, sqrt3over3);
            var n = s.GetNormal(p);
            var expected = Vector4.CreateDirection(sqrt3over3, sqrt3over3, sqrt3over3);
            const double eps = 0.0000001;
            var comparer = Vector4.GetEqualityComparer(eps);
            Assert.Equal(expected, n, comparer);
        }

        [Fact]
        public void TestNormalIsNormalized()
        {
            var s = new Sphere();
            var p = Vector4.CreatePosition(
                Math.Sqrt(3) / 3,
                Math.Sqrt(3) / 3,
                Math.Sqrt(3) / 3);
            var n = s.GetNormal(p);
            const double eps = 0.0000001;
            var comparer = Vector4.GetEqualityComparer(eps);
            Assert.Equal(n.Normalize(), n, comparer);
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

        [Fact]
        public void HelperForSphereWithGlassyMaterial()
        {
            var s = new GlassSphere();
            Assert.Equal(Matrix4x4.Identity, s.Transform);
            Assert.Equal(1.0, s.Material.Transparency);
            Assert.Equal(1.5, s.Material.RefractiveIndex);
        }

        [Fact]
        public void SphereHasBoundingBox()
        {
            var s = new Sphere();
            var box = s.GetBounds();
            Assert.Equal(Vector4.CreatePosition(-1, -1, -1), box.Min);
            Assert.Equal(Vector4.CreatePosition(1, 1, 1), box.Max);
        }
    }
}