namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public class ApproxFloat4x4EqualityComparer : ApproxEqualityComparer<Float4x4>
    {
        protected readonly float epsilon;

        public ApproxFloat4x4EqualityComparer(float epsilon = 0.0f)
            : base(epsilon)
        {
        }

        public override bool Equals(Float4x4 x, Float4x4 y)
        {
            for (var j = 0; j < 4; j++)
            {
                for (var i = 0; i < 4; i++)
                {
                    if (!ApproxEqual(x[i, j], y[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode(Float4x4 obj) =>
            HashCode.Combine(
                HashCode.Combine(
                    obj[0, 0], obj[0, 1], obj[0, 2], obj[0, 3],
                    obj[1, 0], obj[1, 1], obj[1, 2], obj[1, 3]),
                HashCode.Combine(
                    obj[2, 0], obj[2, 1], obj[2, 2], obj[2, 3],
                    obj[3, 0], obj[3, 1], obj[3, 2], obj[3, 3]));
    }
}
