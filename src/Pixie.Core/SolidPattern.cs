namespace Pixie.Core
{
    using System;

    public class SolidPattern : Pattern
    {
        public SolidPattern(Color color)
        {
            this.Color = color;
        }

        public Color Color { get; set; }

        public override Color PatternAt(Double4 point) => 
            this.Color;
    }
}