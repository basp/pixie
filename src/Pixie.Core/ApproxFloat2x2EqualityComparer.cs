namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public class ApproxFloat2x2EqualityComparer : ApproxEqualityComparer<Float2x2>
    {
        protected readonly float epsilon;

        public ApproxFloat2x2EqualityComparer(float epsilon = 0.0f)
            : base(epsilon)
        {
        }

        public override bool Equals(Float2x2 x, Float2x2 y)
        {
            for (var j = 0; j < 2; j++)
            {
                for (var i = 0; i < 2; i++)
                {
                    if (!ApproxEqual(x[i, j], y[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode(Float2x2 obj) =>
            HashCode.Combine(
                obj[0, 0], obj[0, 1], 
                obj[1, 0], obj[1, 1]);
    }
}
