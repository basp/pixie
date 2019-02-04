namespace Pixie
{
    using System;

    public struct Vector2 : IEquatable<Vector2>
    {
        public static Vector2 One = new Vector2(1, 1);

        public static Vector2 UnitX = new Vector2(1, 0);

        public static Vector2 UnitY = new Vector2(0, 1);

        public static Vector2 Zero = new Vector2(0, 0);

        public readonly float X;

        public readonly float Y;

        public Vector2(float value)
        {
            this.X = value;
            this.Y = value;
        }

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public static void Add(Vector2 v, Vector2 w, out Vector2 result)
        {
            result = Add(v, w);
        }

        public static Vector2 Add(Vector2 v, Vector2 w) =>
            new Vector2(v.X + w.X, v.Y + w.Y);

        public static void Ceiling(Vector2 v, out Vector2 result)
        {
            result = Ceiling(v);
        }

        public static Vector2 Ceiling(Vector2 v) =>
            new Vector2(
                (float)Math.Ceiling(v.X),
                (float)Math.Ceiling(v.Y));

        public static void Clamp(Vector2 v, Vector2 min, Vector2 max, out Vector2 result)
        {
            result = Clamp(v, min, max);
        }

        public static Vector2 Clamp(Vector2 v, Vector2 min, Vector2 max) =>
            new Vector2(
                MathHelpers.Clamp(v.X, min.X, max.X),
                MathHelpers.Clamp(v.Y, min.Y, max.Y));

        public static void Divide(Vector2 v, float divisor, out Vector2 result)
        {
            result = Divide(v, divisor);
        }

        public static Vector2 Divide(Vector2 v, float divisor) =>
            new Vector2(v.X / divisor, v.Y / divisor);

        public static void Divide(Vector2 v, Vector2 divisor, out Vector2 result)
        {
            result = Divide(v, divisor);
        }

        public static Vector2 Divide(Vector2 v, Vector2 divisor) =>
            new Vector2(v.X / divisor.X, v.Y / divisor.Y);

        public static void Dot(Vector2 v, Vector2 w, out float result)
        {
            result = Dot(v, w);
        }

        public static float Dot(Vector2 v, Vector2 w) =>
            v.X * w.X + v.Y * w.Y;

        public static void Floor(Vector2 v, out Vector2 result)
        {
            result = Floor(v);
        }            

        public static Vector2 Floor(Vector2 v) =>
            new Vector2(
                (float)Math.Floor(v.X),
                (float)Math.Floor(v.Y));

        public static void Lerp(Vector2 v, Vector2 w, float a, out Vector2 result)
        {
            result = Lerp(v, w, a);
        }

        public static Vector2 Lerp(Vector2 v, Vector2 w, float a) =>
            new Vector2(
                MathHelpers.Lerp(v.X, w.X, a),
                MathHelpers.Lerp(v.Y, w.Y, a));

        public static void Max(Vector2 v, Vector2 w, out Vector2 result)
        {
            result = Max(v, w);
        }

        public static Vector2 Max(Vector2 v, Vector2 w) =>
            new Vector2(
                Math.Max(v.X, w.X),
                Math.Max(v.Y, w.Y));

        public static void Min(Vector2 v, Vector2 w, out Vector2 result)
        {
            result = Min(v, w);
        }

        public static Vector2 Min(Vector2 v, Vector2 w) =>
            new Vector2(
                Math.Min(v.X, w.X),
                Math.Min(v.Y, w.Y));

        public static void Multiply(Vector2 v, float s, out Vector2 result)
        {
            result = Multiply(v, s);
        }

        public static Vector2 Multiply(Vector2 v, float s) =>
            new Vector2(v.X * s, v.Y * s);

        public static void Multiply(Vector2 v, Vector2 w, out Vector2 result)
        {
            result = Multiply(v, w);
        }

        public static Vector2 Multiply(Vector2 v, Vector2 w) =>
            new Vector2(v.X * w.X, v.Y * w.Y);

        public static void Negate(Vector2 v, out Vector2 result)
        {
            result = Negate(v);
        }

        public static Vector2 Negate(Vector2 v) =>
            new Vector2(-v.X , -v.Y);

        public static void Normalize(Vector2 v, out Vector2 result)
        {
            result = Normalize(v);
        }

        public static Vector2 Normalize(Vector2 v)
        {
            var len = v.Length();
            var x = v.X / len;
            var y = v.Y / len;
            return new Vector2(x, y);
        }

        public static void Round(Vector2 v, out Vector2 result)
        {
            result = Round(v);
        }

        public static Vector2 Round(Vector2 v) =>
            new Vector2(
                (float)Math.Round(v.X),
                (float)Math.Round(v.Y));

        public static void Subtract(Vector2 v, Vector2 w, out Vector2 result)
        {
            result = Subtract(v, w);
        }

        public static Vector2 Subtract(Vector2 v, Vector2 w) =>
            new Vector2(v.X - w.X, v.Y - w.Y);

        public static void Transform(Vector2 v, Matrix m, out Vector2 result)
        {
            result = Transform(v, m);
        }

        public static Vector2 Transform(Vector2 v, Matrix m)
        {
            var v4 = new Vector4(v, 0, 1);
            var result = Vector4.Transform(v4, m);
            return new Vector2(result.X, result.Y);
        }

        public static Vector2 operator -(Vector2 v) =>
            Vector2.Negate(v);

        public static Vector2 operator +(Vector2 v, Vector2 w) =>
            Vector2.Add(v, w);

        public static Vector2 operator -(Vector2 v, Vector2 w) =>
            Vector2.Subtract(v, w);

        public static Vector2 operator *(Vector2 v, Vector2 w) =>
            Vector2.Multiply(v, w);

        public static Vector2 operator *(Vector2 v, float s) =>
            Vector2.Multiply(v, s);

        public static Vector2 operator *(float s, Vector2 v) =>
            Vector2.Multiply(v, s);

        public static Vector2 operator /(Vector2 v, Vector2 w) =>
            Vector2.Divide(v, w);

        public static Vector2 operator /(Vector2 v, float s) =>
            Vector2.Divide(v, s);

        public static bool operator ==(Vector2 v, Vector2 w) =>
            v.Equals(w);

        public static bool operator !=(Vector2 v, Vector2 w) =>
            !v.Equals(w);

        public Vector2 Ceiling() => Ceiling(this);

        public bool Equals(Vector2 other) =>
            this.X == other.X && 
            this.Y == other.Y;

        public void Deconstruct(
            out float x,
            out float y)
        {
            x = this.X;
            y = this.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Vector2))
            {
                return false;
            }

            var other = (Vector2)obj;
            return this.Equals(other);
        }

        public Vector2 Floor() => Floor(this);

        public override int GetHashCode() =>
            this.X.GetHashCode() * 31 + this.Y.GetHashCode();

        public float Length() =>
            (float)Math.Sqrt(this.LengthSquared());

        public float LengthSquared() =>
            (float)(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2));

        public Vector2 Normalize() => 
            Normalize(this);

        public Vector2 Round() => Round(this);

        public override string ToString() =>
            $"{{X:{this.X}, Y:{this.Y}}}";
    }
}
