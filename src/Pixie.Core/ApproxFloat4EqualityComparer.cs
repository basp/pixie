namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public class ApproxFloat4EqualityComparer : ApproxEqualityComparer<Float4>
    {
        protected readonly float epsilon;

        public ApproxFloat4EqualityComparer(float epsilon = 0.0f)
            : base(epsilon)
        {
        }

        public override bool Equals(Float4 a, Float4 b) =>
            ApproxEqual(a.X, b.X) &&
            ApproxEqual(a.Y, b.Y) &&
            ApproxEqual(a.Z, b.Z) &&
            ApproxEqual(a.W, b.W);

        public override int GetHashCode(Float4 obj) =>
            HashCode.Combine(obj.X, obj.Y, obj.Z, obj.W);
    }
}
