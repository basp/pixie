namespace Pixie
{
    using System;

    public class SolidPattern : Pattern
    {
        public SolidPattern(Color color)
        {
            this.Color = color;
        }

        public Color Color { get; set; }

        public override Color GetColor(Vector4 point) => 
            this.Color;
    }
}