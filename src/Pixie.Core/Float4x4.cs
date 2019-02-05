namespace Pixie.Core
{
    using System;
    using System.Linq;

    public struct Float4x4 : IEquatable<Float4x4>
    {
        public static Float4x4 Identity =>
            new Float4x4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        private readonly float[] data;

        public Float4x4(float v)
        {
            this.data = new []
            {
                v, v, v, v,
                v, v, v, v,
                v, v, v, v,
                v, v, v, v,
            };
        }

        public Float4x4(float m00, float m01, float m02, float m03,
                        float m10, float m11, float m12, float m13,
                        float m20, float m21, float m22, float m23,
                        float m30, float m31, float m32, float m33)
        {
            this.data = new[]
            {
                m00, m01, m02, m03,
                m10, m11, m12, m13,
                m20, m21, m22, m23,
                m30, m31, m32, m33
            };
        }

        public float this[int row, int col]
        {
            get => this.data[row * 4 + col];
            set => this.data[row * 4 + col] = value;
        }

        public static Float4x4 operator *(Float4x4 a, Float4x4 b)
        {
            var m = new Float4x4(0);
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

        public static Float4 operator *(Float4x4 a, Float4 b) =>
            new Float4(
                a[0, 0] * b.X + a[0, 1] * b.Y + a[0, 2] * b.Z + a[0, 3] * b.W,
                a[1, 0] * b.X + a[1, 1] * b.Y + a[1, 2] * b.Z + a[1, 3] * b.W,
                a[2, 0] * b.X + a[2, 1] * b.Y + a[2, 2] * b.Z + a[2, 3] * b.W,
                a[3, 0] * b.X + a[3, 1] * b.Y + a[3, 2] * b.Z + a[3, 3] * b.W);

        public static Ray operator*(Float4x4 a, Ray r) =>
            new Ray(a * r.Origin, a * r.Direction);

        public static Float4x4 Transpose(Float4x4 a)
        {
            var m = new Float4x4(0);
            for(var i = 0; i < 4; i++)
            {
                for(var j = 0; j < 4; j++)
                {
                    m[i, j] = a[j, i];
                }
            }

            return m;
        }

        public Float4x4 Transpose() => Float4x4.Transpose(this);

        public override string ToString() =>
            $"({string.Join(", ", this.data)})";

        public bool Equals(Float4x4 other)
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

    public static class Float4x4Extensions
    {
        public static Float3x3 Submatrix(this Float4x4 a, int dropRow, int dropCol)
        {
            var rows = Enumerable.Range(0, 4)
                .Where(x => x != dropRow)
                .ToArray();

            var cols = Enumerable.Range(0, 4)
                .Where(x => x != dropCol)
                .ToArray();

            var m = new Float3x3(0);
            for(var i = 0; i < 3; i++)
            {
                for(var j = 0; j < 3; j++)
                {
                    m[i, j] = a[rows[i], cols[j]];
                }
            }

            return m;
        }

        public static float Minor(this Float4x4 a, int row, int col) =>
            a.Submatrix(row, col).Determinant();

        public static float Cofactor(this Float4x4 a, int row, int col) =>
            (row + col) % 2 == 0 ? a.Minor(row, col) : -a.Minor(row, col);

        public static float Determinant(this Float4x4 a) =>
            a[0, 0] * a.Cofactor(0, 0) +
            a[0, 1] * a.Cofactor(0, 1) +
            a[0, 2] * a.Cofactor(0, 2) +
            a[0, 3] * a.Cofactor(0, 3);

        public static bool IsInvertible(this Float4x4 a) =>
            a.Determinant() != 0;

        public static Float4x4 Inverse(this Float4x4 a)
        {
            var m = new Float4x4(0);
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
}