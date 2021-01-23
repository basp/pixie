// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using Linie;

    public class Gradient
    {
        public Gradient(Color a, Color b)
        {
            this.A = a;
            this.B = b;
        }

        public Color A { get; }

        public Color B { get; }

        public Color this[double t]
        {
            get
            {
                if (t <= 0)
                {
                    return this.A;
                }

                if (t >= 1)
                {
                    return this.B;
                }

                return (1 - t) * this.A + t * this.B;
            }
        }
    }
}
