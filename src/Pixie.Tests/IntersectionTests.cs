namespace Pixie.Tests
{
    using System;
    using Optional;
    using Xunit;
    using Pixie.Core;

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
            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(4, shape);
            var comps = i.PrepareComputations(r);
            Assert.Equal(i.T, comps.T);
            Assert.Equal(Double4.Point(0, 0, -1), comps.Point);
            Assert.Equal(Double4.Vector(0, 0, -1), comps.Eyev);
            Assert.Equal(Double4.Vector(0, 0, -1), comps.Normalv);
        }

        [Fact]
        public void HitWhenIntersectionOnOutside()
        {
            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(4, shape);
            var comps = i.PrepareComputations(r);
            Assert.False(comps.Inside);
        }

        [Fact]
        public void HitWhenIntersectionOnInside()
        {
            var r = new Ray(Double4.Point(0, 0, 0), Double4.Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(1, shape);
            var comps = i.PrepareComputations(r);
            Assert.Equal(Double4.Point(0, 0, 1), comps.Point);
            Assert.Equal(Double4.Vector(0, 0, -1), comps.Eyev);
            Assert.Equal(Double4.Vector(0, 0, -1), comps.Normalv);
            Assert.True(comps.Inside);
        }

        [Fact]
        public void HitShouldOffsetThePoint()
        {
            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 0, 1));
            var shape = new Sphere
            {
                Transform = Transform.Translate(0, 0, 1),
            };

            var i = new Intersection(5, shape);
            var comps = i.PrepareComputations(r);
            Assert.True(comps.OverPoint.Z < (-Intersection.Epsilon / 2));
            Assert.True(comps.Point.Z > comps.OverPoint.Z);
        }
    }
}