// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Linsi
{
    using System;

    internal class Matrix4x4EqualityComparer : ApproxEqualityComparer<Matrix4x4>
    {
        public Matrix4x4EqualityComparer(double epsilon = 0)
            : base(epsilon)
        {
        }

        public override bool Equals(Matrix4x4 x, Matrix4x4 y)
        {
            for (var j = 0; j < 4; j++)
            {
                for (var i = 0; i < 4; i++)
                {
                    if (!this.ApproxEqual(x[i, j], y[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

#pragma warning disable SA1117 // ParametersMustBeOnSameLineOrSeparateLines
        public override int GetHashCode(Matrix4x4 obj) =>
            HashCode.Combine(
                HashCode.Combine(
                    obj[0, 0], obj[0, 1], obj[0, 2], obj[0, 3],
                    obj[1, 0], obj[1, 1], obj[1, 2], obj[1, 3]),
                HashCode.Combine(
                    obj[2, 0], obj[2, 1], obj[2, 2], obj[2, 3],
                    obj[3, 0], obj[3, 1], obj[3, 2], obj[3, 3]));
#pragma warning restore SA1117 // ParametersMustBeOnSameLineOrSeparateLines
    }
}
