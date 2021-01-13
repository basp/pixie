namespace Linsi
{
    using System;
    using System.Collections.Generic;

    public struct Matrix2x2
    {
        private readonly double[] data;

        public Matrix2x2(double v)
        {
            this.data = new[]
            {
                v, v,
                v, v,
            };
        }

        public Matrix2x2(double m00, double m01,
                        double m10, double m11)
        {
            this.data = new[]
            {
                m00, m01,
                m10, m11,
            };
        }

        public double this[int row, int col]
        {
            get => this.data[row * 2 + col];
            set => this.data[row * 2 + col] = value;
        }

        public static IEqualityComparer<Matrix2x2> GetEqualityComparer(double epsilon = 0.0) =>
            new ApproxMatrix2x2EqualityComparer(epsilon);

        public override string ToString() =>
            $"({string.Join(", ", data)})";
    }

    public static class Matrix2x2Extensions
    {
        public static double Determinant(this Matrix2x2 m) =>
            m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
    }

    internal class ApproxMatrix2x2EqualityComparer : ApproxEqualityComparer<Matrix2x2>
    {
        public ApproxMatrix2x2EqualityComparer(double epsilon = 0.0)
            : base(epsilon)
        {
        }

        public override bool Equals(Matrix2x2 x, Matrix2x2 y)
        {
            for (var j = 0; j < 2; j++)
            {
                for (var i = 0; i < 2; i++)
                {
                    if (!ApproxEqual(x[i, j], y[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode(Matrix2x2 obj) =>
            HashCode.Combine(
                obj[0, 0], obj[0, 1],
                obj[1, 0], obj[1, 1]);
    }
}
