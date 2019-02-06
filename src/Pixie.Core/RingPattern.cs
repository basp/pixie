namespace Pixie.Core
{
    using System;

    public class RingPattern : Pattern
    {
        public RingPattern(Color a, Color b)
        {
            this.A = a;
            this.B = b;
        }

        public Color A { get; set; }
        public Color B { get; set; }

        public override Color PatternAt(Double4 point)
        {
            var r = Math.Sqrt(point.X * point.X + point.Z * point.Z);
            if (Math.Floor(r) % 2 == 0)
            {
                return this.A;
            }

            return this.B;
        }
    }
}