namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class Double4Tests
    {
        [Fact]
        public void TestPointWComponentIsOne()
        {
            var a = new Double4(4.3, -4.2, 3.1, 1.0);
            Assert.True(a.IsPoint);
            Assert.False(a.IsVector);
        }

        [Fact]
        public void TestVectorWComponentIsZero()
        {
            var a = new Double4(4.3, -4.2, 3.1, 0.0);
            Assert.False(a.IsPoint);
            Assert.True(a.IsVector);
        }

        [Fact]
        public void TestPointFactoryMethod()
        {
            var p = Double4.Point(4, -4, 3);
            var expected = new Double4(4, -4, 3, 1);
            Assert.Equal(expected, p);
        }

        [Fact]
        public void TestVectorFactoryMethod()
        {
            var v = Double4.Vector(4, -4, 3);
            var expected = new Double4(4, -4, 3, 0);
            Assert.Equal(expected, v);
        }

        [Fact]
        public void TestAddTwoDouble4s()
        {
            var a1 = new Double4(3, -2, 5, 1);
            var a2 = new Double4(-2, 3, 1, 0);
            var expected = new Double4(1, 1, 6, 1);
            Assert.Equal(expected, a1 + a2);
        }

        [Fact]
        public void TestSubtractTwoPoints()
        {
            var p1 = Double4.Point(3, 2, 1);
            var p2 = Double4.Point(5, 6, 7);
            var expected = Double4.Vector(-2, -4, -6);
            Assert.Equal(expected, p1 - p2);
        }

        [Fact]
        public void TestSubtractVectorFromPoint()
        {
            var p = Double4.Point(3, 2, 1);
            var v = Double4.Vector(5, 6, 7);
            var expected = Double4.Point(-2, -4, -6);
            Assert.Equal(expected, p - v);
        }

        [Fact]
        public void TestSubtractTwoVectors()
        {
            var v1 = Double4.Vector(3, 2, 1);
            var v2 = Double4.Vector(5, 6, 7);
            var expected = Double4.Vector(-2, -4, -6);
            Assert.Equal(expected, v1 - v2);
        }

        [Fact]
        public void TestSubtractVectorFromZeroVector()
        {
            var zero = Double4.Vector(0, 0, 0);
            var v = Double4.Vector(1, -2, 3);
            var expected = Double4.Vector(-1, 2, -3);
            Assert.Equal(expected, zero - v);
        }

        [Fact]
        public void TestNegateDouble4()
        {
            var a = new Double4(1, -2, 3, -4);
            var expected = new Double4(-1, 2, -3, 4);
            Assert.Equal(expected, -a);
        }

        [Fact]
        public void TestMultiplyDouble4ByScalar()
        {
            var a = new Double4(1, -2, 3, -4);
            var expected = new Double4(3.5, -7, 10.5, -14);
            Assert.Equal(expected, a * 3.5);
        }

        [Fact]
        public void TestMultiplyDouble4ByFraction()
        {
            var a = new Double4(1, -2, 3, -4);
            var expected = new Double4(0.5, -1, 1.5, -2);
            Assert.Equal(expected, a * 0.5);
        }

        [Fact]
        public void TestDivideDouble4ByScalar()
        {
            var a = new Double4(1, -2, 3, -4);
            var expected = new Double4(0.5, -1, 1.5, -2);
            Assert.Equal(expected, a / 2.0);
        }

        [Fact]
        public void TestComputeMagnitude()
        {
            var cases = new[]
            {
                new { v = Double4.Vector(1, 0, 0), expected = 1.0 },
                new { v = Double4.Vector(0, 1, 0), expected = 1.0 },
                new { v = Double4.Vector(0, 0, 1), expected = 1.0 },
                new { v = Double4.Vector(1, 2, 3), expected = Math.Sqrt(14) },
                new { v = Double4.Vector(-1, -2, -3), expected = Math.Sqrt(14) },
            };

            Array.ForEach(cases, c => Assert.Equal(c.expected, c.v.Magnitude()));
        }

        [Fact]
        public void TestMagnitudeOfNormalizedVector()
        {
            var v = Double4.Vector(1, 2, 3);
            var norm = v.Normalize();
            Assert.Equal(1, norm.Magnitude(), 6);
        }

        [Fact]
        public void TestDotProductOfTwoDouble4s()
        {
            var a = Double4.Vector(1, 2, 3);
            var b = Double4.Vector(2, 3, 4);
            Assert.Equal(20, a.Dot(b));
        }

        [Fact]
        public void TestCrossProductOfTwoVectors()
        {
            var a = Double4.Vector(1, 2, 3);
            var b = Double4.Vector(2, 3, 4);
            Assert.Equal(Double4.Vector(-1, 2, -1), a.Cross(b));
            Assert.Equal(Double4.Vector(1, -2, 1), b.Cross(a));
        }

        [Fact]
        public void TestReflectVectorApproachingAt45Degrees()
        {
            var v = Double4.Vector(1, -1, 0);
            var n = Double4.Vector(0, 1, 0);
            var r = v.Reflect(n);
            var expected = Double4.Vector(1, 1, 0);
            Assert.Equal(expected, r);
        }

        [Fact]
        public void TestReflectOfSlantedSurface()
        {
            var v = Double4.Vector(0, -1, 0);
            var n = Double4.Vector(
                Math.Sqrt(2)/2,
                Math.Sqrt(2)/2,
                0);
            var r = v.Reflect(n);
            var expected = Double4.Vector(1, 0, 0);
            const double eps = 0.0000001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(expected, r, comparer);
        }
    }
}
