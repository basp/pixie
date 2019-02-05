using System;
using System.Collections.Generic;

namespace Pixie.Core
{
    public struct Float2x2
    {
        private readonly float[] data;

        public Float2x2(float v)
        {
            this.data = new[]
            {
                v, v,
                v, v,
            };
        }

        public Float2x2(float m00, float m01,
                        float m10, float m11)
        {
            this.data = new[]
            {
                m00, m01,
                m10, m11,
            };
        }

        public float this[int row, int col]
        {
            get => this.data[row * 2 + col];
            set => this.data[row * 2 + col] = value;
        }

        public static IEqualityComparer<Float2x2> GetEqualityComparer(float epsilon = 0.0f) =>
            new ApproxFloat2x2EqualityComparer(epsilon);

        public override string ToString() =>
            $"({string.Join(", ", data)})";
    }

    public static class Float2x2Extensions
    {
        public static float Determinant(this Float2x2 m) =>
            m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
    }

    internal class ApproxFloat2x2EqualityComparer : ApproxEqualityComparer<Float2x2>
    {
        public ApproxFloat2x2EqualityComparer(float epsilon = 0.0f)
            : base(epsilon)
        {
        }

        public override bool Equals(Float2x2 x, Float2x2 y)
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

        public override int GetHashCode(Float2x2 obj) =>
            HashCode.Combine(
                obj[0, 0], obj[0, 1],
                obj[1, 0], obj[1, 1]);
    }
}