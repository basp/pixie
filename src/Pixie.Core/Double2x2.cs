using System;
using System.Collections.Generic;

namespace Pixie.Core
{
    public struct Double2x2
    {
        private readonly double[] data;

        public Double2x2(double v)
        {
            this.data = new[]
            {
                v, v,
                v, v,
            };
        }

        public Double2x2(double m00, double m01,
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

        public static IEqualityComparer<Double2x2> GetEqualityComparer(double epsilon = 0.0) =>
            new ApproxDouble2x2EqualityComparer(epsilon);

        public override string ToString() =>
            $"({string.Join(", ", data)})";
    }

    public static class Double2x2Extensions
    {
        public static double Determinant(this Double2x2 m) =>
            m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
    }

    internal class ApproxDouble2x2EqualityComparer : ApproxEqualityComparer<Double2x2>
    {
        public ApproxDouble2x2EqualityComparer(double epsilon = 0.0)
            : base(epsilon)
        {
        }

        public override bool Equals(Double2x2 x, Double2x2 y)
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

        public override int GetHashCode(Double2x2 obj) =>
            HashCode.Combine(
                obj[0, 0], obj[0, 1],
                obj[1, 0], obj[1, 1]);
    }
}