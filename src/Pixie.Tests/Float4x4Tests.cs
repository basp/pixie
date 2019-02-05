namespace Pixie.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Pixie.Core;

    public class Float4x4Tests
    {
        const float epsilon = 0.000000001f;

        [Fact]
        public void TestFloat4x4Construction()
        {
            var m = new Float4x4(
                1.00f, 2.00f, 3.00f, 4.00f,
                5.50f, 6.50f, 7.50f, 8.50f,
                9.00f, 10.0f, 11.0f, 12.0f,
                13.5f, 14.5f, 15.5f, 16.5f);

            Assert.Equal(1.00f, m[0, 0]);
            Assert.Equal(4.00f, m[0, 3]);
            Assert.Equal(5.50f, m[1, 0]);
            Assert.Equal(7.50f, m[1, 2]);
            Assert.Equal(11.0f, m[2, 2]);
            Assert.Equal(13.5f, m[3, 0]);
            Assert.Equal(15.5f, m[3, 2]);
        }

        [Fact]
        public void TestMatrixEqualityWithIdenticalMatrices()
        {
            var a = new Float4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 8, 7, 6,
                5, 4, 3, 2);

            var b = new Float4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 8, 7, 6,
                5, 4, 3, 2);

            var comparer = Float4x4.GetEqualityComparer(epsilon);
            Assert.Equal(b, a, comparer);
        }

        [Fact]
        public void TestMatrixEqualityWithDifferentMatrices()
        {
            var a = new Float4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 8, 7, 6,
                5, 4, 3, 2);

            var b = new Float4x4(
                2, 3, 4, 5,
                6, 7, 8, 9,
                8, 7, 6, 5,
                4, 3, 2, 1);

            var comparer = Float4x4.GetEqualityComparer(epsilon);
            Assert.NotEqual(b, a, comparer);
        }

        [Fact]
        public void TestMultiplyTwoMatrices()
        {
            var a = new Float4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 8, 7, 6,
                5, 4, 3, 2);

            var b = new Float4x4(
                -2, 1, 2, 3,
                3, 2, 1, -1,
                4, 3, 6, 5,
                1, 2, 7, 8);

            var expected = new Float4x4(
                20, 22, 50, 48,
                44, 54, 114, 108,
                40, 58, 110, 102,
                16, 26, 46, 42);

            var comparer = Float4x4.GetEqualityComparer(epsilon);
            Assert.Equal(expected, a * b, comparer);
        }

        [Fact]
        public void TestMultiplyMatrixByTuple()
        {
            var a = new Float4x4(
                1, 2, 3, 4,
                2, 4, 4, 2,
                8, 6, 4, 1,
                0, 0, 0, 1);

            var t = new Float4(1, 2, 3, 1);

            var expected = new Float4(18, 24, 33, 1);
            var comparer = Float4.GetEqualityComparer(epsilon);
            Assert.Equal(expected, a * t, comparer);
        }

        [Fact]
        public void TestMultiplyMatrixByIdentityMatrix()
        {
            var a = new Float4x4(
                0, 1, 2, 4,
                1, 2, 4, 8,
                2, 4, 8, 16,
                4, 8, 16, 32);

            var comparer = Float4x4.GetEqualityComparer(epsilon);
            Assert.Equal(a, a * Float4x4.Identity, comparer);
        }

        [Fact]
        public void TestMultiplyIdentityMatrixByTuple()
        {
            var t = new Float4(1, 2, 3, 4);
            var comparer = Float4.GetEqualityComparer(epsilon);
            Assert.Equal(t, Float4x4.Identity * t, comparer);
        }

        [Fact]
        public void TestTransposeMatrix()
        {
            var m = new Float4x4(
                0, 9, 3, 0,
                9, 8, 0, 8,
                1, 8, 5, 3,
                0, 0, 5, 8);

            var expected = new Float4x4(
                0, 9, 1, 0,
                9, 8, 8, 0,
                3, 0, 5, 5,
                0, 8, 3, 8);

            var comparer = Float4x4.GetEqualityComparer(epsilon);
            Assert.Equal(expected, m.Transpose(), comparer);
        }

        [Fact]
        public void TestSubmatrix()
        {
            var m = new Float4x4(
                -6, 1, 1, 6,
                -8, 5, 8, 6,
                -1, 0, 8, 2,
                -7, 1, -1, 1);

            var expected = new Float3x3(
                -6, 1, 6,
                -8, 8, 6,
                -7, -1, 1);

            var comparer = Float3x3.GetEqualityComparer(epsilon);
            Assert.Equal(expected, m.Submatrix(2, 1), comparer);
        }


        [Fact]
        public void TestCalculateDeterminantOf4x4Matrix()
        {
            var a = new Float4x4(
                -2, -8, 3, 5,
                -3, 1, 7, 3,
                1, 2, -9, 6,
                -6, 7, 7, -9);

            Assert.Equal(690, a.Cofactor(0, 0));
            Assert.Equal(447, a.Cofactor(0, 1));
            Assert.Equal(210, a.Cofactor(0, 2));
            Assert.Equal(51, a.Cofactor(0, 3));
            Assert.Equal(-4071, a.Determinant());
        }

        [Fact]
        public void TestInvertibleMatrixForInvertibility()
        {
            var a = new Float4x4(
                6, 4, 4, 4,
                5, 5, 7, 6,
                4, -9, 3, -7,
                9, 1, 7, -6);

            Assert.True(a.IsInvertible());
        }

        [Fact]
        public void TestNonInvertibleMatrixForInvertibility()
        {
            var a = new Float4x4(
                -4, 2, -2, -3,
                9, 6, 2, 6,
                0, -5, 1, -5,
                0, 0, 0, 0);

            Assert.False(a.IsInvertible());
        }

        [Fact]
        public void TestCalculatingTheInverseOfAMatrix()
        {
            var a = new Float4x4(
                -5, 2, 6, -8,
                1, -5, 1, 8,
                7, 7, -6, -7,
                1, -3, 7, 4);

            var b = a.Inverse();

            var expected = new Float4x4(
                0.21805f, 0.45113f, 0.24060f, -0.04511f,
                -0.80827f, -1.45677f, -0.44361f, 0.52068f,
                -0.07895f, -0.22368f, -0.05263f, 0.19737f,
                -0.52256f, -0.81391f, -0.30075f, 0.30639f);

            Assert.Equal(532, a.Determinant());
            Assert.Equal(-160, a.Cofactor(2, 3));
            Assert.Equal(-160f / 532f, b[3, 2]);
            Assert.Equal(105, a.Cofactor(3, 2));
            Assert.Equal(105f / 532f, b[2, 3]);

            var comparer = Float4x4.GetEqualityComparer(0.00001f);
            Assert.Equal(expected, b, comparer);
        }

        [Fact]
        public void TestCalculatingTheInverseOfAnotherMatrix()
        {
            var a = new Float4x4(
                8, -5, 9, 2,
                7, 5, 6, 1,
                -6, 0, 9, 6,
                -3, 0, -9, -4);

            var b = a.Inverse();

            var expected = new Float4x4(
                -0.15385f, -0.15385f, -0.28205f, -0.53846f,
                -0.07692f, 0.12308f, 0.02564f, 0.03077f,
                0.35897f, 0.35897f, 0.43590f, 0.92308f,
                -0.69231f, -0.69231f, -0.76923f, -1.92308f);

            var comparer = Float4x4.GetEqualityComparer(0.00001f);
            Assert.Equal(expected, b, comparer);
        }

        [Fact]
        public void TestCalculatingTheInverseOfAThirdMatrix()
        {
            var a = new Float4x4(
                9, 3, 0, 9,
                -5, -2, -6, -3,
                -4, 9, 6, 4,
                -7, 6, 6, 2);

            var b = a.Inverse();

            var expected = new Float4x4(
                -0.04074f, -0.07778f, 0.14444f, -0.22222f,
                -0.07778f, 0.03333f, 0.36667f, -0.33333f,
                -0.02901f, -0.14630f, -0.10926f, 0.12963f,
                0.17778f, 0.06667f, -0.26667f, 0.33333f);

            var comparer = Float4x4.GetEqualityComparer(0.00001f);
            Assert.Equal(expected, b, comparer);
        }

        [Fact]
        public void TestMultiplyingAMatrixByItsInverse()
        {
            var a = new Float4x4(
                3, -9, 7, 3,
                3, -8, 2, -9,
                -4, 4, 4, 1,
                -6, 5, -1, 1);

            var b = new Float4x4(
                8, 2, 2, 2,
                3, -1, 7, 0,
                7, 0, 5, 4,
                6, -2, 0, 5);

            var c = a * b;

            var comparer = Float4x4.GetEqualityComparer(0.00001f);
            Assert.Equal(a, c * b.Inverse(), comparer);
        }
    }
}