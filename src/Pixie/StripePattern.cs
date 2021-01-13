namespace Pixie
{
    using System;
    using Linsi;

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
            Math.Floor(point.X) % 2 == 0 ? A : B;
    }
}