// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;
    using Linie;

    public class StripePattern : Pattern
    {
        public StripePattern(Color a, Color b)
        {
            this.A = a;
            this.B = b;
        }

        public Color A { get; set; }

        public Color B { get; set; }

        public override Color GetColor(Vector4 point) =>
            Math.Floor(point.X) % 2 == 0 ? this.A : this.B;
    }
}
