// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;
    using Linsi;

    public class GradientPattern : Pattern
    {
        public GradientPattern(Color a, Color b)
        {
            this.A = a;
            this.B = b;
        }

        public Color A { get; set; }

        public Color B { get; set; }

        public override Color GetColor(Vector4 point)
        {
            var distance = this.B - this.A;
            var fraction = point.X - Math.Floor(point.X);
            return this.A + distance * fraction;
        }
    }
}
