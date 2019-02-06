namespace Pixie.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Pixie.Core;

    public class RayTests
    {
        [Fact]
        public void TestRayConstructor()
        {
            var origin = Double4.Point(1, 2, 3);
            var direction = Double4.Vector(4, 5, 6);
            var r = new Ray(origin, direction);
            Assert.Equal(origin, r.Origin);
            Assert.Equal(direction, r.Direction);
        }

        [Fact]
        public void TestComputePointFromDistance()
        {
            var r = new Ray(Double4.Point(2, 3, 4), Double4.Vector(1, 0, 0));
            Assert.Equal(Double4.Point(2, 3, 4), r.Position(0));
            Assert.Equal(Double4.Point(3, 3, 4), r.Position(1));
            Assert.Equal(Double4.Point(1, 3, 4), r.Position(-1));
            Assert.Equal(Double4.Point(4.5, 3, 4), r.Position(2.5));
        }

        [Fact]
        public void TestTranslateRay()
        {
            var r = new Ray(Double4.Point(1, 2, 3), Double4.Vector(0, 1, 0));
            var t = Transform.Translate(3, 4, 5);
            var r2 = t * r;
            Assert.Equal(Double4.Point(4, 6, 8), r2.Origin);
            Assert.Equal(Double4.Vector(0, 1, 0), r2.Direction);
        }

        [Fact]
        public void TestScaleRay()
        {
            var r = new Ray(Double4.Point(1, 2, 3), Double4.Vector(0, 1, 0));
            var t = Transform.Scale(2, 3, 4);
            var r2 = t * r;
            Assert.Equal(Double4.Point(2, 6, 12), r2.Origin);
            Assert.Equal(Double4.Vector(0, 3, 0), r2.Direction);
        }
    }
}