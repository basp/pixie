// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Linsi.Tests")]
namespace Linsi
{
    using System.Collections.Generic;
    using System.Linq;

    internal struct Matrix3x3
    {
        private readonly double[] data;

        public Matrix3x3(double v)
        {
            this.data = new[]
            {
                v, v, v,
                v, v, v,
                v, v, v,
            };
        }

#pragma warning disable SA1117 // ParametersMustBeOnSameLineOrSeparateLines
        public Matrix3x3(
            double m00, double m01, double m02,
            double m10, double m11, double m12,
            double m20, double m21, double m22)
        {
            this.data = new[]
            {
                m00, m01, m02,
                m10, m11, m12,
                m20, m21, m22,
            };
        }
#pragma warning restore SA1117 // ParametersMustBeOnSameLineOrSeparateLines

        public double this[int row, int col]
        {
            get => this.data[(row * 3) + col];
            set => this.data[(row * 3) + col] = value;
        }

        public static IEqualityComparer<Matrix3x3> GetEqualityComparer(double epsilon = 0.0) =>
            new ApproxMatrix3x3EqualityComparer(epsilon);
    }

    public static class Matrix3x3Extensions
    {
        internal static Matrix2x2 Submatrix(this Matrix3x3 a, int dropRow, int dropCol)
        {
            var rows = Enumerable.Range(0, 3)
                .Where(x => x != dropRow)
                .ToArray();

            var cols = Enumerable.Range(0, 3)
                .Where(x => x != dropCol)
                .ToArray();

            var m = new Matrix2x2(0);
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    m[i, j] = a[rows[i], cols[j]];
                }
            }

            return m;
        }

        internal static double Minor(this Matrix3x3 a, int row, int col) =>
            a.Submatrix(row, col).Determinant();

        internal static double Cofactor(this Matrix3x3 a, int row, int col) =>
            (row + col) % 2 == 0 ? a.Minor(row, col) : -a.Minor(row, col);

        internal static double Determinant(this Matrix3x3 a) =>
            (a[0, 0] * a.Cofactor(0, 0)) +
            (a[0, 1] * a.Cofactor(0, 1)) +
            (a[0, 2] * a.Cofactor(0, 2));
    }
}
