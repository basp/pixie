namespace Pixie
{
    using System;

    public struct Color : IEquatable<Color>
    {
        public Color(float r, float g, float b)
            : this(r, g, b, 1.0f)
        {
        }

        public Color(float r, float g, float b, float alpha)
        {
            R = (byte)(r * 255.5);
            G = (byte)(r * 255.5);
            B = (byte)(r * 255.5);
            A = (byte)(alpha * 255.5);
        }

        public Color(byte r, byte g, byte b, byte alpha)
        {
            R = r;
            G = g;
            B = b;
            A = alpha;
        }
        
        public static Color White = new Color(255, 255, 255, 255);

        public byte R;

        public byte G;

        public byte B;

        public byte A;

        public bool Equals(Color other) =>
            this.R == other.R &&
            this.G == other.G &&
            this.B == other.B &&
            this.A == other.A;
    }

    public static class ColorExtensions
    {
        public static System.Drawing.Color ToSystemDrawingColor(this Color self) =>
            System.Drawing.Color.FromArgb(self.A, self.R, self.G, self.B);
    }
}
