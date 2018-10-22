namespace Pixie
{
    using System;

    public class Turtle : IDisposable
    {
        private Vector2 heading = Vector2.UnitX;

        private Vector2 location;

        public Vector2 Heading => this.heading;

        public Vector2 Location => this.location;

        public Turtle Turn(float degrees)
        {
            var rad = ToRadians(degrees);
            var rotm = Matrix2.CreateRotation(rad);
            this.heading = heading.Multiply(rotm);
            return this;
        }

        public Turtle Forward(float distance)
        {
            var dx = this.heading.X * distance;
            var dy = this.heading.Y * distance;
            this.location = location.Translate(dx, dy);
            return this;
        }

        public void Dispose()
        {
        }

        static float ToRadians(float deg) => (float)(deg * 2 * Math.PI / 360);
    }

    internal struct Matrix2
    {
        public float M11;
        public float M12;
        public float M21;
        public float M22;

        public static Matrix2 CreateRotation(float theta) =>
            new Matrix2
            {
                M11 = (float)Math.Round(Math.Cos(theta), 0),
                M12 = -(float)Math.Round(Math.Sin(theta), 0),
                M21 = (float)Math.Round(Math.Sin(theta), 0),
                M22 = (float)Math.Round(Math.Cos(theta), 0),
            };
    }

    internal static class Extensions
    {
        public static Vector2 Rotate(this Vector2 a, double theta) =>
            new Vector2
            {
                X = (float)Math.Round(a.X * Math.Cos(theta) - a.Y * Math.Sin(theta), 2),
                Y = (float)Math.Round(a.X * Math.Sin(theta) + a.Y * Math.Cos(theta), 2),
            };

        public static Vector2 Multiply(this Vector2 a, Matrix2 b)
        {
            var x = b.M11 * a.X + b.M12 * a.Y;
            var y = b.M21 * a.X + b.M22 * a.Y;
            return new Vector2(x, y);
        }

        public static Vector2 Translate(this Vector2 a, float dx, float dy) =>
            new Vector2(a.X + dx, a.Y + dy);
    }
}