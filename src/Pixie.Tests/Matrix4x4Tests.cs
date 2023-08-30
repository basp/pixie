namespace Pixie.Tests
{
    using Xunit;

    public class Matrix4x4Tests
    {
        const double epsilon = 0.000000001;

        [Fact]
        public void TestMatrix4x4Construction()
        {
            var m = new Matrix4x4(
                1.00, 2.00, 3.00, 4.00,
                5.50, 6.50, 7.50, 8.50,
                9.00, 10.0, 11.0, 12.0,
                13.5, 14.5, 15.5, 16.5);

            Assert.Equal(1.00, m[0, 0]);
            Assert.Equal(4.00, m[0, 3]);
            Assert.Equal(5.50, m[1, 0]);
            Assert.Equal(7.50, m[1, 2]);
            Assert.Equal(11.0, m[2, 2]);
            Assert.Equal(13.5, m[3, 0]);
            Assert.Equal(15.5, m[3, 2]);
        }

        [Fact]
        public void TestMatrixEqualityWithIdenticalMatrices()
        {
            var a = new Matrix4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 8, 7, 6,
                5, 4, 3, 2);

            var b = new Matrix4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 8, 7, 6,
                5, 4, 3, 2);

            var comparer = Matrix4x4.GetEqualityComparer(epsilon);
            Assert.Equal(b, a, comparer);
        }

        [Fact]
        public void TestMatrixEqualityWithDifferentMatrices()
        {
            var a = new Matrix4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 8, 7, 6,
                5, 4, 3, 2);

            var b = new Matrix4x4(
                2, 3, 4, 5,
                6, 7, 8, 9,
                8, 7, 6, 5,
                4, 3, 2, 1);

            var comparer = Matrix4x4.GetEqualityComparer(epsilon);
            Assert.NotEqual(b, a, comparer);
        }

        [Fact]
        public void TestMultiplyTwoMatrices()
        {
            var a = new Matrix4x4(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 8, 7, 6,
                5, 4, 3, 2);

            var b = new Matrix4x4(
                -2, 1, 2, 3,
                3, 2, 1, -1,
                4, 3, 6, 5,
                1, 2, 7, 8);

            var expected = new Matrix4x4(
                20, 22, 50, 48,
                44, 54, 114, 108,
                40, 58, 110, 102,
                16, 26, 46, 42);

            var comparer = Matrix4x4.GetEqualityComparer(epsilon);
            Assert.Equal(expected, a * b, comparer);
        }

        [Fact]
        public void TestMultiplyMatrixByTuple()
        {
            var a = new Matrix4x4(
                1, 2, 3, 4,
                2, 4, 4, 2,
                8, 6, 4, 1,
                0, 0, 0, 1);

            var t = new Vector4(1, 2, 3, 1);

            var expected = new Vector4(18, 24, 33, 1);
            var comparer = Vector4.GetEqualityComparer(epsilon);
            Assert.Equal(expected, a * t, comparer);
        }

        [Fact]
        public void TestMultiplyMatrixByIdentityMatrix()
        {
            var a = new Matrix4x4(
                0, 1, 2, 4,
                1, 2, 4, 8,
                2, 4, 8, 16,
                4, 8, 16, 32);

            var comparer = Matrix4x4.GetEqualityComparer(epsilon);
            Assert.Equal(a, a * Matrix4x4.Identity, comparer);
        }

        [Fact]
        public void TestMultiplyIdentityMatrixByTuple()
        {
            var t = new Vector4(1, 2, 3, 4);
            var comparer = Vector4.GetEqualityComparer(epsilon);
            Assert.Equal(t, Matrix4x4.Identity * t, comparer);
        }

        [Fact]
        public void TestTransposeMatrix()
        {
            var m = new Matrix4x4(
                0, 9, 3, 0,
                9, 8, 0, 8,
                1, 8, 5, 3,
                0, 0, 5, 8);

            var expected = new Matrix4x4(
                0, 9, 1, 0,
                9, 8, 8, 0,
                3, 0, 5, 5,
                0, 8, 3, 8);

            var comparer = Matrix4x4.GetEqualityComparer(epsilon);
            Assert.Equal(expected, m.Transpose(), comparer);
        }

        // [Fact]
        // public void TestSubmatrix()
        // {
        //     var m = new Matrix4x4(
        //         -6, 1, 1, 6,
        //         -8, 5, 8, 6,
        //         -1, 0, 8, 2,
        //         -7, 1, -1, 1);

        //     var expected = new Matrix3x3(
        //         -6, 1, 6,
        //         -8, 8, 6,
        //         -7, -1, 1);

        //     var comparer = Matrix3x3.GetEqualityComparer(epsilon);
        //     Assert.Equal(expected, m.Submatrix(2, 1), comparer);
        // }


        // [Fact]
        // public void TestCalculateDeterminantOf4x4Matrix()
        // {
        //     var a = new Matrix4x4(
        //         -2, -8, 3, 5,
        //         -3, 1, 7, 3,
        //         1, 2, -9, 6,
        //         -6, 7, 7, -9);

        //     Assert.Equal(690, a.Cofactor(0, 0));
        //     Assert.Equal(447, a.Cofactor(0, 1));
        //     Assert.Equal(210, a.Cofactor(0, 2));
        //     Assert.Equal(51, a.Cofactor(0, 3));
        //     Assert.Equal(-4071, a.Determinant());
        // }

        [Fact]
        public void TestInvertibleMatrixForInvertibility()
        {
            var a = new Matrix4x4(
                6, 4, 4, 4,
                5, 5, 7, 6,
                4, -9, 3, -7,
                9, 1, 7, -6);

            Assert.True(a.IsInvertible());
        }

        [Fact]
        public void TestNonInvertibleMatrixForInvertibility()
        {
            var a = new Matrix4x4(
                -4, 2, -2, -3,
                9, 6, 2, 6,
                0, -5, 1, -5,
                0, 0, 0, 0);

            Assert.False(a.IsInvertible());
        }

        [Fact]
        public void TestCalculatingTheInverseOfAMatrix()
        {
            var a = new Matrix4x4(
                -5, 2, 6, -8,
                1, -5, 1, 8,
                7, 7, -6, -7,
                1, -3, 7, 4);

            var b = a.Inverse();

            var expected = new Matrix4x4(
                0.21805, 0.45113, 0.24060, -0.04511,
                -0.80827, -1.45677, -0.44361, 0.52068,
                -0.07895, -0.22368, -0.05263, 0.19737,
                -0.52256, -0.81391, -0.30075, 0.30639);

            Assert.Equal(532, a.Determinant());
            Assert.Equal(-160, a.Cofactor(2, 3));
            Assert.Equal(-160.0 / 532, b[3, 2]);
            Assert.Equal(105, a.Cofactor(3, 2));
            Assert.Equal(105.0 / 532, b[2, 3]);

            var comparer = Matrix4x4.GetEqualityComparer(0.00001);
            Assert.Equal(expected, b, comparer);
        }

        [Fact]
        public void TestCalculatingTheInverseOfAnotherMatrix()
        {
            var a = new Matrix4x4(
                8, -5, 9, 2,
                7, 5, 6, 1,
                -6, 0, 9, 6,
                -3, 0, -9, -4);

            var b = a.Inverse();

            var expected = new Matrix4x4(
                -0.15385, -0.15385, -0.28205, -0.53846,
                -0.07692, 0.12308, 0.02564, 0.03077,
                0.35897, 0.35897, 0.43590, 0.92308,
                -0.69231, -0.69231, -0.76923, -1.92308);

            var comparer = Matrix4x4.GetEqualityComparer(0.00001);
            Assert.Equal(expected, b, comparer);
        }

        [Fact]
        public void TestCalculatingTheInverseOfAThirdMatrix()
        {
            var a = new Matrix4x4(
                9, 3, 0, 9,
                -5, -2, -6, -3,
                -4, 9, 6, 4,
                -7, 6, 6, 2);

            var b = a.Inverse();

            var expected = new Matrix4x4(
                -0.04074, -0.07778, 0.14444, -0.22222,
                -0.07778, 0.03333, 0.36667, -0.33333,
                -0.02901, -0.14630, -0.10926, 0.12963,
                0.17778, 0.06667, -0.26667, 0.33333);

            var comparer = Matrix4x4.GetEqualityComparer(0.00001);
            Assert.Equal(expected, b, comparer);
        }

        [Fact]
        public void TestMultiplyingAMatrixByItsInverse()
        {
            var a = new Matrix4x4(
                3, -9, 7, 3,
                3, -8, 2, -9,
                -4, 4, 4, 1,
                -6, 5, -1, 1);

            var b = new Matrix4x4(
                8, 2, 2, 2,
                3, -1, 7, 0,
                7, 0, 5, 4,
                6, -2, 0, 5);

            var c = a * b;

            var comparer = Matrix4x4.GetEqualityComparer(0.00001);
            Assert.Equal(a, c * b.Inverse(), comparer);
        }
    }
}