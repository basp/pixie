// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;
    using Linie;

    public class CheckersPattern : Pattern
    {
        public CheckersPattern(Color a, Color b)
        {
            this.A = a;
            this.B = b;
        }

        public Color A { get; set; }

        public Color B { get; set; }

        public override Color GetColor(Vector4 point)
        {
            var ix = Math.Floor(point.X);
            var iy = Math.Floor(point.Y);
            var iz = Math.Floor(point.Z);
            if((ix + iy + iz) % 2 == 0)
            {
                return this.A;
            }

            return this.B;
        }
    }
}
