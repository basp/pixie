namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public class ApproxFloat3x3EqualityComparer : ApproxEqualityComparer<Float3x3>
    {
        protected readonly float epsilon;

        public ApproxFloat3x3EqualityComparer(float epsilon = 0.0f)
            : base(epsilon)
        {
        }

        public override bool Equals(Float3x3 x, Float3x3 y)
        {
            for (var j = 0; j < 3; j++)
            {
                for (var i = 0; i < 3; i++)
                {
                    if (!ApproxEqual(x[i, j], y[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode(Float3x3 obj) =>
            HashCode.Combine(
                HashCode.Combine(
                    obj[0, 0], obj[0, 1], obj[0, 2],
                    obj[1, 0], obj[1, 1], obj[1, 2]),
                HashCode.Combine(
                    obj[2, 0], obj[2, 1], obj[2, 2]));
    }
}
