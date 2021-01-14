// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Linsi
{
    using System;

    internal class ApproxVector2EqualityComparer
        : ApproxEqualityComparer<Vector2>
    {
        public ApproxVector2EqualityComparer(double epsilon = 0.0)
            : base(epsilon)
        {
        }

        public override bool Equals(Vector2 a, Vector2 b) =>
            this.ApproxEqual(a.X, b.X) &&
            this.ApproxEqual(a.Y, b.Y);

        public override int GetHashCode(Vector2 obj) =>
            HashCode.Combine(obj.X, obj.Y);
    }
}
