namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class Float4Tests
    {
        [Fact]
        public void TestPointWComponentIsOne()
        {
            var a = new Float4(4.3f, -4.2f, 3.1f, 1.0f);
            Assert.True(a.IsPoint);
            Assert.False(a.IsVector);
        }

        [Fact]
        public void TestVectorWComponentIsZero()
        {
            var a = new Float4(4.3f, -4.2f, 3.1f, 0.0f);
            Assert.False(a.IsPoint);
            Assert.True(a.IsVector);
        }

        [Fact]
        public void TestPointFactoryMethod()
        {
            var p = Float4.Point(4, -4, 3);
            var expected = new Float4(4, -4, 3, 1);
            Assert.Equal(expected, p);
        }

        [Fact]
        public void TestVectorFactoryMethod()
        {
            var v = Float4.Vector(4, -4, 3);
            var expected = new Float4(4, -4, 3, 0);
            Assert.Equal(expected, v);
        }

        [Fact]
        public void TestAddTwoFloat4s()
        {
            var a1 = new Float4(3, -2, 5, 1);
            var a2 = new Float4(-2, 3, 1, 0);
            var expected = new Float4(1, 1, 6, 1);
            Assert.Equal(expected, a1 + a2);
        }

        [Fact]
        public void TestSubtractTwoPoints()
        {
            var p1 = Float4.Point(3, 2, 1);
            var p2 = Float4.Point(5, 6, 7);
            var expected = Float4.Vector(-2, -4, -6);
            Assert.Equal(expected, p1 - p2);
        }

        [Fact]
        public void TestSubtractVectorFromPoint()
        {
            var p = Float4.Point(3, 2, 1);
            var v = Float4.Vector(5, 6, 7);
            var expected = Float4.Point(-2, -4, -6);
            Assert.Equal(expected, p - v);
        }

        [Fact]
        public void TestSubtractTwoVectors()
        {
            var v1 = Float4.Vector(3, 2, 1);
            var v2 = Float4.Vector(5, 6, 7);
            var expected = Float4.Vector(-2, -4, -6);
            Assert.Equal(expected, v1 - v2);
        }

        [Fact]
        public void TestSubtractVectorFromZeroVector()
        {
            var zero = Float4.Vector(0, 0, 0);
            var v = Float4.Vector(1, -2, 3);
            var expected = Float4.Vector(-1, 2, -3);
            Assert.Equal(expected, zero - v);
        }

        [Fact]
        public void TestNegateFloat4()
        {
            var a = new Float4(1, -2, 3, -4);
            var expected = new Float4(-1, 2, -3, 4);
            Assert.Equal(expected, -a);
        }

        [Fact]
        public void TestMultiplyFloat4ByScalar()
        {
            var a = new Float4(1, -2, 3, -4);
            var expected = new Float4(3.5f, -7, 10.5f, -14);
            Assert.Equal(expected, a * 3.5f);
        }

        [Fact]
        public void TestMultiplyFloat4ByFraction()
        {
            var a = new Float4(1, -2, 3, -4);
            var expected = new Float4(0.5f, -1, 1.5f, -2);
            Assert.Equal(expected, a * 0.5f);
        }

        [Fact]
        public void TestDivideFloat4ByScalar()
        {
            var a = new Float4(1, -2, 3, -4);
            var expected = new Float4(0.5f, -1, 1.5f, -2);
            Assert.Equal(expected, a / 2.0f);
        }

        [Fact]
        public void TestComputeMagnitude()
        {
            var cases = new[]
            {
                new { v = Float4.Vector(1, 0, 0), expected = 1f },
                new { v = Float4.Vector(0, 1, 0), expected = 1f },
                new { v = Float4.Vector(0, 0, 1), expected = 1f },
                new { v = Float4.Vector(1, 2, 3), expected = (float)Math.Sqrt(14) },
                new { v = Float4.Vector(-1, -2, -3), expected = (float)Math.Sqrt(14) },
            };

            Array.ForEach(cases, c => Assert.Equal(c.expected, c.v.Magnitude()));
        }

        [Fact]
        public void TestMagnitudeOfNormalizedVector()
        {
            var v = Float4.Vector(1, 2, 3);
            var norm = v.Normalize();
            Assert.Equal(1, norm.Magnitude(), 6);
        }

        [Fact]
        public void TestDotProductOfTwoFloat4s()
        {
            var a = Float4.Vector(1, 2, 3);
            var b = Float4.Vector(2, 3, 4);
            Assert.Equal(20, a.Dot(b));
        }

        [Fact]
        public void TestCrossProductOfTwoVectors()
        {
            var a = Float4.Vector(1, 2, 3);
            var b = Float4.Vector(2, 3, 4);
            Assert.Equal(Float4.Vector(-1, 2, -1), a.Cross(b));
            Assert.Equal(Float4.Vector(1, -2, 1), b.Cross(a));
        }

        [Fact]
        public void TestReflectVectorApproachingAt45Degrees()
        {
            var v = Float4.Vector(1, -1, 0);
            var n = Float4.Vector(0, 1, 0);
            var r = v.Reflect(n);
            var expected = Float4.Vector(1, 1, 0);
            Assert.Equal(expected, r);
        }

        [Fact]
        public void TestReflectOfSlantedSurface()
        {
            var v = Float4.Vector(0, -1, 0);
            var n = Float4.Vector(
                (float)Math.Sqrt(2)/2,
                (float)Math.Sqrt(2)/2,
                0);
            var r = v.Reflect(n);
            var expected = Float4.Vector(1, 0, 0);
            const float eps = 0.0000001f;
            var comparer = new ApproxFloat4EqualityComparer(eps);
            Assert.Equal(expected, r, comparer);
        }
    }
}
