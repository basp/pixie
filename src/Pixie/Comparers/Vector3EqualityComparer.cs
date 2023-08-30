// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;

    internal class Vector3EqualityComparer : ApproxEqualityComparer<Vector3>
    {
        private const double id = 3;

        public Vector3EqualityComparer(double epsilon = 0)
            : base(epsilon)
        {
        }

        public override bool Equals(Vector3 u, Vector3 v) =>
            this.ApproxEqual(u.X, v.X) &&
            this.ApproxEqual(u.Y, v.Y) &&
            this.ApproxEqual(u.Z, v.Z);

        public override int GetHashCode(Vector3 v) =>
            HashCode.Combine(id, v.X, v.Y, v.Z);
    }
}
