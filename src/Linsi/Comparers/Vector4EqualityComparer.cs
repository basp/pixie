// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Linsi
{
    using System;

    internal class Vector4EqualityComparer : ApproxEqualityComparer<Vector4>
    {
        private const double id = 2;

        public Vector4EqualityComparer(double epsilon = 0)
            : base(epsilon)
        {
        }

        public override bool Equals(Vector4 u, Vector4 v) =>
            this.ApproxEqual(u.X, v.X) &&
            this.ApproxEqual(u.Y, v.Y) &&
            this.ApproxEqual(u.Z, v.Z) &&
            this.ApproxEqual(u.W, v.W);

        public override int GetHashCode(Vector4 v) =>
            HashCode.Combine(id, v.X, v.Y, v.Z, v.W);
    }
}
