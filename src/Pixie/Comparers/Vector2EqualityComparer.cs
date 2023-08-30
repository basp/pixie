// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;

    internal class Vector2EqualityComparer : ApproxEqualityComparer<Vector2>
    {
        private const double id = 5;

        public Vector2EqualityComparer(double epsilon = 0)
            : base(epsilon)
        {
        }

        public override bool Equals(Vector2 u, Vector2 v) =>
            this.ApproxEqual(u.X, v.X) &&
            this.ApproxEqual(u.Y, v.Y);

        public override int GetHashCode(Vector2 v) =>
            HashCode.Combine(id, v.X, v.Y);
    }
}
