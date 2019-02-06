namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public struct Color
    {
        public static Color White =>
            new Color(1, 1, 1);

        public static Color Black =>
            new Color(0, 0, 0);

        public readonly double R;
        public readonly double G;
        public readonly double B;

        public Color(double r, double g, double b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public static Color operator +(Color a, Color b) =>
            new Color(
                a.R + b.R,
                a.G + b.G,
                a.B + b.B);

        public static Color operator -(Color a, Color b) =>
            new Color(
                a.R - b.R,
                a.G - b.G,
                a.B - b.B);

        public static Color operator *(Color a, double s) =>
            new Color(
                a.R * s,
                a.G * s,
                a.B * s);

        public static Color operator *(double s, Color a) => a * s;

        public static Color operator *(Color c1, Color c2) =>
            new Color(
                c1.R * c2.R,
                c1.G * c2.G,
                c1.B * c2.B);

        public static IEqualityComparer<Color> GetEqualityComparer(double epsilon = 0.0) =>
            new ApproxColorEqualityComparer(epsilon);

        public override string ToString() =>
            $"({this.R}, {this.G}, {this.B})";
    }

    internal class ApproxColorEqualityComparer : ApproxEqualityComparer<Color>
    {
        public ApproxColorEqualityComparer(double epsilon = 0.0)
            : base(epsilon)
        {
        }

        public override bool Equals(Color x, Color y) =>
            this.ApproxEqual(x.R, y.R) &&
            this.ApproxEqual(x.G, y.G) &&
            this.ApproxEqual(x.B, y.B);

        public override int GetHashCode(Color obj) =>
            HashCode.Combine(obj.R, obj.G, obj.B);
    }    
}