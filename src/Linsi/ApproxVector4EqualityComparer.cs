// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Linsi
{
    using System;

    internal class ApproxVector4EqualityComparer
        : ApproxEqualityComparer<Vector4>
    {
        public ApproxVector4EqualityComparer(double epsilon = 0.0)
            : base(epsilon)
        {
        }

        public override bool Equals(Vector4 a, Vector4 b) =>
            this.ApproxEqual(a.X, b.X) &&
            this.ApproxEqual(a.Y, b.Y) &&
            this.ApproxEqual(a.Z, b.Z) &&
            this.ApproxEqual(a.W, b.W);

        public override int GetHashCode(Vector4 obj) =>
            HashCode.Combine(obj.X, obj.Y, obj.Z, obj.W);
    }
}
