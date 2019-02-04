namespace Pixie.Core
{
    using System;
    using System.Linq;

    public struct Float3x3
    {
        private readonly float[] data;

        public Float3x3(float v)
        {
            this.data = new []
            {
                v, v, v,
                v, v, v,
                v, v, v,
            };
        }

        public Float3x3(float m00, float m01, float m02,
                        float m10, float m11, float m12,
                        float m20, float m21, float m22)
        {
            this.data = new [] 
            {
                m00, m01, m02,
                m10, m11, m12,
                m20, m21, m22,
            };
        }

        public float this[int row, int col]
        {
            get => this.data[row * 3 + col];
            set => this.data[row * 3 + col] = value;
        }
    }

    public static class Float3x3Extensions
    {
        public static Float2x2 Submatrix(this Float3x3 a, int dropRow, int dropCol)
        {
            var rows = Enumerable.Range(0, 3)
                .Where(x => x != dropRow)
                .ToArray();
            
            var cols = Enumerable.Range(0, 3)
                .Where(x => x != dropCol)
                .ToArray();

            var m = new Float2x2(0);
            for(var i = 0; i < 2; i++)
            {
                for(var j = 0; j < 2; j++)
                {
                    m[i, j] = a[rows[i], cols[j]];
                }
            }

            return m;
        }

        public static float Minor(this Float3x3 a, int row, int col) =>
            a.Submatrix(row, col).Determinant();

        public static float Cofactor(this Float3x3 a, int row, int col) =>
            (row + col) % 2 == 0 ? a.Minor(row, col) : -a.Minor(row, col);

        public static float Determinant(this Float3x3 a) =>
            a[0, 0] * a.Cofactor(0, 0) +
            a[0, 1] * a.Cofactor(0, 1) +
            a[0, 2] * a.Cofactor(0, 2);
    }
}