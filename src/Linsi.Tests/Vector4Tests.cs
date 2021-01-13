namespace Linsi.Tests
{
    using System;
    using Xunit;

    public class Vector4Tests
    {
        [Fact]
        public void TestPointWComponentIsOne()
        {
            var a = new Vector4(4.3, -4.2, 3.1, 1.0);
            Assert.True(a.IsPosition);
            Assert.False(a.IsDirection);
        }

        [Fact]
        public void TestVectorWComponentIsZero()
        {
            var a = new Vector4(4.3, -4.2, 3.1, 0.0);
            Assert.False(a.IsPosition);
            Assert.True(a.IsDirection);
        }

        [Fact]
        public void TestPointFactoryMethod()
        {
            var p = Vector4.CreatePosition(4, -4, 3);
            var expected = new Vector4(4, -4, 3, 1);
            Assert.Equal(expected, p);
        }

        [Fact]
        public void TestVectorFactoryMethod()
        {
            var v = Vector4.CreateDirection(4, -4, 3);
            var expected = new Vector4(4, -4, 3, 0);
            Assert.Equal(expected, v);
        }

        [Fact]
        public void TestAddTwoVector4s()
        {
            var a1 = new Vector4(3, -2, 5, 1);
            var a2 = new Vector4(-2, 3, 1, 0);
            var expected = new Vector4(1, 1, 6, 1);
            Assert.Equal(expected, a1 + a2);
        }

        [Fact]
        public void TestSubtractTwoPoints()
        {
            var p1 = Vector4.CreatePosition(3, 2, 1);
            var p2 = Vector4.CreatePosition(5, 6, 7);
            var expected = Vector4.CreateDirection(-2, -4, -6);
            Assert.Equal(expected, p1 - p2);
        }

        [Fact]
        public void TestSubtractVectorFromPoint()
        {
            var p = Vector4.CreatePosition(3, 2, 1);
            var v = Vector4.CreateDirection(5, 6, 7);
            var expected = Vector4.CreatePosition(-2, -4, -6);
            Assert.Equal(expected, p - v);
        }

        [Fact]
        public void TestSubtractTwoVectors()
        {
            var v1 = Vector4.CreateDirection(3, 2, 1);
            var v2 = Vector4.CreateDirection(5, 6, 7);
            var expected = Vector4.CreateDirection(-2, -4, -6);
            Assert.Equal(expected, v1 - v2);
        }

        [Fact]
        public void TestSubtractVectorFromZeroVector()
        {
            var zero = Vector4.CreateDirection(0, 0, 0);
            var v = Vector4.CreateDirection(1, -2, 3);
            var expected = Vector4.CreateDirection(-1, 2, -3);
            Assert.Equal(expected, zero - v);
        }

        [Fact]
        public void TestNegateVector4()
        {
            var a = new Vector4(1, -2, 3, -4);
            var expected = new Vector4(-1, 2, -3, 4);
            Assert.Equal(expected, -a);
        }

        [Fact]
        public void TestMultiplyVector4ByScalar()
        {
            var a = new Vector4(1, -2, 3, -4);
            var expected = new Vector4(3.5, -7, 10.5, -14);
            Assert.Equal(expected, a * 3.5);
        }

        [Fact]
        public void TestMultiplyVector4ByFraction()
        {
            var a = new Vector4(1, -2, 3, -4);
            var expected = new Vector4(0.5, -1, 1.5, -2);
            Assert.Equal(expected, a * 0.5);
        }

        [Fact]
        public void TestDivideVector4ByScalar()
        {
            var a = new Vector4(1, -2, 3, -4);
            var expected = new Vector4(0.5, -1, 1.5, -2);
            Assert.Equal(expected, a / 2.0);
        }

        [Fact]
        public void TestComputeMagnitude()
        {
            var cases = new[]
            {
                new { v = Vector4.CreateDirection(1, 0, 0), expected = 1.0 },
                new { v = Vector4.CreateDirection(0, 1, 0), expected = 1.0 },
                new { v = Vector4.CreateDirection(0, 0, 1), expected = 1.0 },
                new { v = Vector4.CreateDirection(1, 2, 3), expected = Math.Sqrt(14) },
                new { v = Vector4.CreateDirection(-1, -2, -3), expected = Math.Sqrt(14) },
            };

            Array.ForEach(cases, c => Assert.Equal(c.expected, c.v.Magnitude()));
        }

        [Fact]
        public void TestMagnitudeOfNormalizedVector()
        {
            var v = Vector4.CreateDirection(1, 2, 3);
            var norm = v.Normalize();
            Assert.Equal(1, norm.Magnitude(), 6);
        }

        [Fact]
        public void TestDotProductOfTwoVector4s()
        {
            var a = Vector4.CreateDirection(1, 2, 3);
            var b = Vector4.CreateDirection(2, 3, 4);
            Assert.Equal(20, a.Dot(b));
        }

        // [Fact]
        // public void TestCrossProductOfTwoVectors()
        // {
        //     var a = Vector4.CreateDirection(1, 2, 3);
        //     var b = Vector4.CreateDirection(2, 3, 4);
        //     Assert.Equal(Vector4.CreateDirection(-1, 2, -1), a.Cross(b));
        //     Assert.Equal(Vector4.CreateDirection(1, -2, 1), b.Cross(a));
        // }

        [Fact]
        public void TestReflectVectorApproachingAt45Degrees()
        {
            var v = Vector4.CreateDirection(1, -1, 0);
            var n = Vector4.CreateDirection(0, 1, 0);
            var r = v.Reflect(n);
            var expected = Vector4.CreateDirection(1, 1, 0);
            Assert.Equal(expected, r);
        }

        [Fact]
        public void TestReflectOfSlantedSurface()
        {
            var v = Vector4.CreateDirection(0, -1, 0);
            var n = Vector4.CreateDirection(
                Math.Sqrt(2)/2,
                Math.Sqrt(2)/2,
                0);
            var r = v.Reflect(n);
            var expected = Vector4.CreateDirection(1, 0, 0);
            const double eps = 0.0000001;
            var comparer = Vector4.GetEqualityComparer(eps);
            Assert.Equal(expected, r, comparer);
        }
    }
}
