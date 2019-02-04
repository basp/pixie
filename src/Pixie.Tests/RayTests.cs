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
            var origin = Float4.Point(1, 2, 3);
            var direction = Float4.Vector(4, 5, 6);
            var r = new Ray(origin, direction);
            Assert.Equal(origin, r.Origin);
            Assert.Equal(direction, r.Direction);
        }

        [Fact]
        public void TestComputePointFromDistance()
        {
            var r = new Ray(Float4.Point(2, 3, 4), Float4.Vector(1, 0, 0));
            Assert.Equal(Float4.Point(2, 3, 4), r.Position(0));
            Assert.Equal(Float4.Point(3, 3, 4), r.Position(1));
            Assert.Equal(Float4.Point(1, 3, 4), r.Position(-1));
            Assert.Equal(Float4.Point(4.5f, 3, 4), r.Position(2.5f));
        }
    }
}