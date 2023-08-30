// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System.Collections.Generic;

    internal class Matrix2x2
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

#pragma warning disable SA1117 // ParametersMustBeOnSameLineOrSeparateLines
        public Matrix2x2(
            double m00, double m01,
            double m10, double m11)
        {
            this.data = new[]
            {
                m00, m01,
                m10, m11,
            };
        }
#pragma warning restore SA1117 // ParametersMustBeOnSameLineOrSeparateLines

        public double this[int row, int col]
        {
            get => this.data[(row * 2) + col];
            set => this.data[(row * 2) + col] = value;
        }

        public static IEqualityComparer<Matrix2x2> GetEqualityComparer(double epsilon = 0.0) =>
            new Matrix2x2EqualityComparer(epsilon);

        public override string ToString() =>
            $"({string.Join(", ", this.data)})";
    }

    public static class Matrix2x2Extensions
    {
        internal static double Determinant(this Matrix2x2 m) =>
            (m[0, 0] * m[1, 1]) - (m[0, 1] * m[1, 0]);
    }
}
