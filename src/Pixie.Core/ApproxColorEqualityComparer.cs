namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public class ApproxColorEqualityComparer : ApproxEqualityComparer<Color>
    {
        public ApproxColorEqualityComparer(float epsilon = 0.0f)
            : base(epsilon)
        {
        }

        public override bool Equals(Color x, Color y) =>
            this.ApproxEqual(x.R, y.R) &&
            this.ApproxEqual(x.G, y.G) &&
            this.ApproxEqual(x.B, y.B);

        public override int GetHashCode(Color obj) =>
            HashCode.Combine(obj.R, obj.G, obj.B);
    }
}