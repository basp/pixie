namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public struct Double4x4 : IEquatable<Double4x4>
    {
        public static Double4x4 Identity =>
            new Double4x4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        private readonly double[] data;

        public Double4x4(double v)
        {
            this.data = new []
            {
                v, v, v, v,
                v, v, v, v,
                v, v, v, v,
                v, v, v, v,
            };
        }

        public Double4x4(double m00, double m01, double m02, double m03,
                         double m10, double m11, double m12, double m13,
                         double m20, double m21, double m22, double m23,
                         double m30, double m31, double m32, double m33)
        {
            this.data = new[]
            {
                m00, m01, m02, m03,
                m10, m11, m12, m13,
                m20, m21, m22, m23,
                m30, m31, m32, m33
            };
        }

        public double this[int row, int col]
        {
            get => this.data[row * 4 + col];
            set => this.data[row * 4 + col] = value;
        }

        public static Double4x4 operator *(Double4x4 a, Double4x4 b)
        {
            var m = new Double4x4(0);
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    m[i, j] =
                        a[i, 0] * b[0, j] +
                        a[i, 1] * b[1, j] +
                        a[i, 2] * b[2, j] +
                        a[i, 3] * b[3, j];
                }
            }

            return m;
        }

        public static Double4 operator *(Double4x4 a, Double4 b) =>
            new Double4(
                a[0, 0] * b.X + a[0, 1] * b.Y + a[0, 2] * b.Z + a[0, 3] * b.W,
                a[1, 0] * b.X + a[1, 1] * b.Y + a[1, 2] * b.Z + a[1, 3] * b.W,
                a[2, 0] * b.X + a[2, 1] * b.Y + a[2, 2] * b.Z + a[2, 3] * b.W,
                a[3, 0] * b.X + a[3, 1] * b.Y + a[3, 2] * b.Z + a[3, 3] * b.W);

        public static Ray operator*(Double4x4 a, Ray r) =>
            new Ray(a * r.Origin, a * r.Direction);

        public static Double4x4 Transpose(Double4x4 a)
        {
            var m = new Double4x4(0);
            for(var i = 0; i < 4; i++)
            {
                for(var j = 0; j < 4; j++)
                {
                    m[i, j] = a[j, i];
                }
            }

            return m;
        }

        public Double4x4 Transpose() => Double4x4.Transpose(this);

        public static IEqualityComparer<Double4x4> GetEqualityComparer(double epsilon = 0.0) =>
            new ApproxDouble4x4EqualityComparer(epsilon);

        public override string ToString() =>
            $"({string.Join(", ", this.data)})";

        public bool Equals(Double4x4 other)
        {
            for(var i = 0; i < 4; i++)
            {
                for(var j = 0; j < 4; j++)
                {
                    if(this[i, j] != other[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    public static class Double4x4Extensions
    {
        public static Double3x3 Submatrix(this Double4x4 a, int dropRow, int dropCol)
        {
            var rows = Enumerable.Range(0, 4)
                .Where(x => x != dropRow)
                .ToArray();

            var cols = Enumerable.Range(0, 4)
                .Where(x => x != dropCol)
                .ToArray();

            var m = new Double3x3(0);
            for(var i = 0; i < 3; i++)
            {
                for(var j = 0; j < 3; j++)
                {
                    m[i, j] = a[rows[i], cols[j]];
                }
            }

            return m;
        }

        public static double Minor(this Double4x4 a, int row, int col) =>
            a.Submatrix(row, col).Determinant();

        public static double Cofactor(this Double4x4 a, int row, int col) =>
            (row + col) % 2 == 0 ? a.Minor(row, col) : -a.Minor(row, col);

        public static double Determinant(this Double4x4 a) =>
            a[0, 0] * a.Cofactor(0, 0) +
            a[0, 1] * a.Cofactor(0, 1) +
            a[0, 2] * a.Cofactor(0, 2) +
            a[0, 3] * a.Cofactor(0, 3);

        public static bool IsInvertible(this Double4x4 a) =>
            a.Determinant() != 0;

        public static Double4x4 Inverse(this Double4x4 a)
        {
            var m = new Double4x4(0);
            var d = a.Determinant();
            for(var i = 0; i < 4; i++)
            {
                for(var j = 0; j < 4; j++)
                {
                    m[j, i] = a.Cofactor(i, j) / d;
                }
            }

            return m;
        }
    }

    internal class ApproxDouble4x4EqualityComparer : ApproxEqualityComparer<Double4x4>
    {
        public ApproxDouble4x4EqualityComparer(double epsilon = 0.0)
            : base(epsilon)
        {
        }

        public override bool Equals(Double4x4 x, Double4x4 y)
        {
            for (var j = 0; j < 4; j++)
            {
                for (var i = 0; i < 4; i++)
                {
                    if (!ApproxEqual(x[i, j], y[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode(Double4x4 obj) =>
            HashCode.Combine(
                HashCode.Combine(
                    obj[0, 0], obj[0, 1], obj[0, 2], obj[0, 3],
                    obj[1, 0], obj[1, 1], obj[1, 2], obj[1, 3]),
                HashCode.Combine(
                    obj[2, 0], obj[2, 1], obj[2, 2], obj[2, 3],
                    obj[3, 0], obj[3, 1], obj[3, 2], obj[3, 3]));
    }
}