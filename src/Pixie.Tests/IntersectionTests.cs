namespace Pixie.Tests
{
    using System;
    using Xunit;

    public class IntersectionTests
    {
        [Fact]
        public void CreateIntersection()
        {
            var s = new Sphere();
            var i = new Intersection(3.5, s);
            Assert.Equal(s, i.Object);
            Assert.Equal(3.5, i.T);
        }

        [Fact]
        public void CreateIntersectionList()
        {
            var s = new Sphere();
            var i1 = new Intersection(1, s);
            var i2 = new Intersection(2, s);
            var xs = IntersectionList.Create(i1, i2);
            Assert.Equal(2, xs.Count);
            Assert.Equal(1, xs[0].T);
            Assert.Equal(2, xs[1].T);
        }

        [Fact]
        public void HitWhenAllIntersectionsHavePositiveT()
        {
            var s = new Sphere();
            var i1 = new Intersection(1, s);
            var i2 = new Intersection(2, s);
            var xs = IntersectionList.Create(i1, i2);
            var hit = xs.TryGetHit(out var i);
            Assert.True(hit);
            Assert.Equal(i1, i);
        }

        [Fact]
        public void HitWhenSomeIntersectionsHaveNegativeT()
        {
            var s = new Sphere();
            var i1 = new Intersection(-1, s);
            var i2 = new Intersection(1, s);
            var xs = IntersectionList.Create(i1, i2);
            var hit = xs.TryGetHit(out var i);
            Assert.True(hit);
            Assert.Equal(i2, i);
        }

        [Fact]
        public void HitWhenAllIntersectionsHaveNegativeT()
        {
            var s = new Sphere();
            var i1 = new Intersection(-2, s);
            var i2 = new Intersection(-1, s);
            var xs = IntersectionList.Create(i1, i2);
            var hit = xs.TryGetHit(out var i);
            Assert.False(hit);
        }

        [Fact]
        public void HitIsAlwaysLowestNonNegativeIntersection()
        {
            var s = new Sphere();
            var i1 = new Intersection(5, s);
            var i2 = new Intersection(7, s);
            var i3 = new Intersection(-3, s);
            var i4 = new Intersection(2, s);
            var xs = IntersectionList.Create(i1, i2, i3, i4);
            var hit = xs.TryGetHit(out var i);
            Assert.True(hit);
            Assert.Equal(i4, i);
        }

        [Fact]
        public void PrecomputingIntersectionState()
        {
            var r = new Ray(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(4, shape);
            var comps = i.Precompute(r);
            Assert.Equal(i.T, comps.T);
            Assert.Equal(Vector4.CreatePosition(0, 0, -1), comps.Point);
            Assert.Equal(Vector4.CreateDirection(0, 0, -1), comps.Eyev);
            Assert.Equal(Vector4.CreateDirection(0, 0, -1), comps.Normalv);
        }

        [Fact]
        public void HitWhenIntersectionOnOutside()
        {
            var r = new Ray(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(4, shape);
            var comps = i.Precompute(r);
            Assert.False(comps.Inside);
        }

        [Fact]
        public void HitWhenIntersectionOnInside()
        {
            var r = new Ray(Vector4.CreatePosition(0, 0, 0), Vector4.CreateDirection(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(1, shape);
            var comps = i.Precompute(r);
            Assert.Equal(Vector4.CreatePosition(0, 0, 1), comps.Point);
            Assert.Equal(Vector4.CreateDirection(0, 0, -1), comps.Eyev);
            Assert.Equal(Vector4.CreateDirection(0, 0, -1), comps.Normalv);
            Assert.True(comps.Inside);
        }

        [Fact]
        public void HitShouldOffsetThePoint()
        {
            var r = new Ray(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
            var shape = new Sphere
            {
                Transform = Transform.Translate(0, 0, 1),
            };

            var i = new Intersection(5, shape);
            var comps = i.Precompute(r);
            Assert.True(comps.OverPoint.Z < (-Intersection.Epsilon / 2));
            Assert.True(comps.Point.Z > comps.OverPoint.Z);
        }

        [Fact]
        public void PrecomputingTheReflectionVector()
        {
            var shape = new Plane();
            var r = new Ray(Vector4.CreatePosition(0, 1, -1), Vector4.CreateDirection(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            var i = new Intersection(Math.Sqrt(2), shape);
            var comps = i.Precompute(r);
            var expected = Vector4.CreateDirection(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2);
            Assert.Equal(expected, comps.Reflectv);
        }

        [Theory]
        [InlineData(0, 1.0, 1.5)]
        [InlineData(1, 1.5, 2.0)]
        [InlineData(2, 2.0, 2.5)]
        [InlineData(3, 2.5, 2.5)]
        [InlineData(4, 2.5, 1.5)]
        [InlineData(5, 1.5, 1.0)]
        public void FindingN1AndN2AtVariousIntersections(int index, double n1, double n2)
        {
            var a = new GlassSphere()
            {
                Transform = Transform.Scale(2, 2, 2),
            };

            var b = new GlassSphere()
            {
                Transform = Transform.Translate(0, 0, -0.25),
            };

            var c = new GlassSphere()
            {
                Transform = Transform.Translate(0, 0, 25),
            };

            a.Material.RefractiveIndex = 1.5;
            b.Material.RefractiveIndex = 2.0;
            c.Material.RefractiveIndex = 2.5;

            var r = new Ray(Vector4.CreatePosition(0, 0, -4), Vector4.CreateDirection(0, 0, 1));
            var xs = IntersectionList.Create(
                new Intersection(2, a),
                new Intersection(2.75, b),
                new Intersection(3.25, c),
                new Intersection(4.75, b),
                new Intersection(5.25, c),
                new Intersection(6, a));

            var comps = xs[index].Precompute(r, xs);
            Assert.Equal(n1, comps.N1);
            Assert.Equal(n2, comps.N2);
        }

        [Fact]
        public void UnderPointIsOffsetBelowTheSurface()
        {
            var r = new Ray(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
            var shape = new GlassSphere()
            {
                Transform = Transform.Translate(0, 0, 1),
            };

            var i = new Intersection(5, shape);
            var xs = IntersectionList.Create(i);
            var comps = i.Precompute(r, xs);
            Assert.True(comps.UnderPoint.Z > Intersection.Epsilon / 2);
            Assert.True(comps.Point.Z < comps.UnderPoint.Z);
        }

        [Fact]
        public void SchlickApproxUnderTotalInternalReflection()
        {
            var shape = new GlassSphere();
            var r = new Ray(
                Vector4.CreatePosition(0, 0, Math.Sqrt(2)/2), 
                Vector4.CreateDirection(0, 1, 0));
            
            var xs = IntersectionList.Create(
                new Intersection(-Math.Sqrt(2)/2, shape),
                new Intersection(Math.Sqrt(2)/2, shape));
            
            var comps = xs[1].Precompute(r, xs);
            var reflectance = comps.SchlicksApproximation();
            Assert.Equal(1.0, reflectance);
        }

        [Fact]
        public void SchlickApproxWithPerpendicularViewingAngle()
        {
            var shape = new GlassSphere();
            var r = new Ray(Vector4.CreatePosition(0, 0, 0), Vector4.CreateDirection(0, 1, 0));
            var xs = IntersectionList.Create(
                new Intersection(-1, shape),
                new Intersection(1, shape));

            var comps = xs[1].Precompute(r, xs);
            var reflectance = comps.SchlicksApproximation();
            const int prec = 8;
            Assert.Equal(0.04, reflectance, prec);
        }

        [Fact]
        public void SchlickApproxWithSmallAngleAndN2GreaterThanN1()
        {
            var shape = new GlassSphere();
            var r = new Ray(Vector4.CreatePosition(0, 0.99, -2), Vector4.CreateDirection(0, 0, 1));
            var xs = IntersectionList.Create(
                new Intersection(1.8589, shape));
            var comps = xs[0].Precompute(r, xs);
            var reflectance = comps.SchlicksApproximation();
            const int prec = 5;
            Assert.Equal(0.48873, reflectance, prec);
        }
    }
}