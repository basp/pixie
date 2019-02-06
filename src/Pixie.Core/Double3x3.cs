namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public struct Double3x3
    {
        private readonly double[] data;

        public Double3x3(double v)
        {
            this.data = new[]
            {
                v, v, v,
                v, v, v,
                v, v, v,
            };
        }

        public Double3x3(double m00, double m01, double m02,
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

        public double this[int row, int col]
        {
            get => this.data[row * 3 + col];
            set => this.data[row * 3 + col] = value;
        }

        public static IEqualityComparer<Double3x3> GetEqualityComparer(double epsilon = 0.0) =>
            new ApproxDouble3x3EqualityComparer(epsilon);
    }

    public static class Double3x3Extensions
    {
        public static Double2x2 Submatrix(this Double3x3 a, int dropRow, int dropCol)
        {
            var rows = Enumerable.Range(0, 3)
                .Where(x => x != dropRow)
                .ToArray();

            var cols = Enumerable.Range(0, 3)
                .Where(x => x != dropCol)
                .ToArray();

            var m = new Double2x2(0);
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    m[i, j] = a[rows[i], cols[j]];
                }
            }

            return m;
        }

        public static double Minor(this Double3x3 a, int row, int col) =>
            a.Submatrix(row, col).Determinant();

        public static double Cofactor(this Double3x3 a, int row, int col) =>
            (row + col) % 2 == 0 ? a.Minor(row, col) : -a.Minor(row, col);

        public static double Determinant(this Double3x3 a) =>
            a[0, 0] * a.Cofactor(0, 0) +
            a[0, 1] * a.Cofactor(0, 1) +
            a[0, 2] * a.Cofactor(0, 2);
    }

    internal class ApproxDouble3x3EqualityComparer : ApproxEqualityComparer<Double3x3>
    {
        public ApproxDouble3x3EqualityComparer(double epsilon = 0.0)
            : base(epsilon)
        {
        }

        public override bool Equals(Double3x3 x, Double3x3 y)
        {
            for (var j = 0; j < 3; j++)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (!ApproxEqual(x[i, j], y[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode(Double3x3 obj) =>
            HashCode.Combine(
                HashCode.Combine(
                    obj[0, 0], obj[0, 1], obj[0, 2],
                    obj[1, 0], obj[1, 1], obj[1, 2]),
                HashCode.Combine(
                    obj[2, 0], obj[2, 1], obj[2, 2]));
    }
}