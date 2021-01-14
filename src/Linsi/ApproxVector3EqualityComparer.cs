// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Linsi
{
    using System;

    internal class ApproxVector3EqualityComparer
        : ApproxEqualityComparer<Vector3>
    {
        public ApproxVector3EqualityComparer(double epsilon = 0.0)
            : base(epsilon)
        {
        }

        public override bool Equals(Vector3 a, Vector3 b) =>
            this.ApproxEqual(a.X, b.X) &&
            this.ApproxEqual(a.Y, b.Y) &&
            this.ApproxEqual(a.Z, b.Z);

        public override int GetHashCode(Vector3 obj) =>
            HashCode.Combine(obj.X, obj.Y, obj.Z);
    }
}
