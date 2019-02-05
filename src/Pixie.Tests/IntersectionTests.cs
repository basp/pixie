namespace Pixie.Tests
{
    using System;
    using Optional;
    using Xunit;
    using Pixie.Core;

    public class IntersectionTests
    {
        [Fact]
        public void TestIntersectionConstructor()
        {
            var s = new Sphere();
            var i = new Intersection(3.5f, s);
            Assert.Equal(s, i.Object);
            Assert.Equal(3.5f, i.T);
        }

        [Fact]
        public void TestIntersectionList()
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
        public void TestHitWhenAllIntersectionsHavePositiveT()
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
        public void TestHitWhenSomeIntersectionsHaveNegativeT()
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
        public void TestHitWhenAllIntersectionsHaveNegativeT()
        {
            var s = new Sphere();
            var i1 = new Intersection(-2, s);
            var i2 = new Intersection(-1, s);
            var xs = IntersectionList.Create(i1, i2);
            var hit = xs.TryGetHit(out var i);
            Assert.False(hit);
        }

        [Fact]
        public void TestHitIsAlwaysLowestNonNegativeIntersection()
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
        public void TestPrecomputingIntersectionState()
        {
            var r = new Ray(Float4.Point(0, 0, -5), Float4.Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(4, shape);
            var comps = i.PrepareComputations(r);
            Assert.Equal(i.T, comps.T);
            Assert.Equal(Float4.Point(0, 0, -1), comps.Point);
            Assert.Equal(Float4.Vector(0, 0, -1), comps.Eyev);
            Assert.Equal(Float4.Vector(0, 0, -1), comps.Normalv);
        }

        [Fact]
        public void TestHitWhenIntersectionOnOutside()
        {
            var r = new Ray(Float4.Point(0, 0, -5), Float4.Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(4, shape);
            var comps = i.PrepareComputations(r);
            Assert.False(comps.Inside);
        }

        [Fact]
        public void TestHitWhenIntersectionOnInside()
        {
            var r = new Ray(Float4.Point(0, 0, 0), Float4.Vector(0, 0, 1));
            var shape = new Sphere();
            var i = new Intersection(1, shape);
            var comps = i.PrepareComputations(r);
            Assert.Equal(Float4.Point(0, 0, 1), comps.Point);
            Assert.Equal(Float4.Vector(0, 0, -1), comps.Eyev);
            Assert.Equal(Float4.Vector(0, 0, -1), comps.Normalv);
            Assert.True(comps.Inside);
        }
    }
}