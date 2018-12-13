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

        public static Vector2 Add(Vector2 v, Vector2 w)
        {
            var x = v.X + w.X;
            var y = v.Y + w.Y;
            return new Vector2(x, y);
        }

        public static void Clamp(Vector2 v, Vector2 min, Vector2 max, out Vector2 result)
        {
            result = Clamp(v, min, max);
        }

        public static Vector2 Clamp(Vector2 v, Vector2 min, Vector2 max)
        {
            var x = Clamp(v.X, min.X, max.X);
            var y = Clamp(v.Y, min.Y, max.Y);
            return new Vector2(x, y);
        }

        public static void Divide(Vector2 v, float divisor, out Vector2 result)
        {
            result = Divide(v, divisor);
        }

        public static Vector2 Divide(Vector2 v, float divisor)
        {
            var x = v.X / divisor;
            var y = v.Y / divisor;
            return new Vector2(x, y);
        }

        public static void Divide(Vector2 v, Vector2 divisor, out Vector2 result)
        {
            result = Divide(v, divisor);
        }

        public static Vector2 Divide(Vector2 v, Vector2 divisor)
        {
            var x = v.X / divisor.X;
            var y = v.Y / divisor.Y;
            return new Vector2(x, y);
        }

        public static void Dot(Vector2 v, Vector2 w, out float result)
        {
            result = Dot(v, w);
        }

        public static float Dot(Vector2 v, Vector2 w) =>
            v.X * w.X + v.Y * w.Y;

        public static void Lerp(Vector2 v, Vector2 w, float a, out Vector2 result)
        {
            throw new NotImplementedException();
        }

        public static Vector2 Lerp(Vector2 v, Vector2 w, float a)
        {
            throw new NotImplementedException();
        }

        public static void Max(Vector2 v, Vector2 w, out Vector2 result)
        {
            result = Max(v, w);
        }

        public static Vector2 Max(Vector2 v, Vector2 w)
        {
            var x = v.X > w.X ? v.X : w.X;
            var y = v.Y > w.Y ? v.Y : w.Y;
            return new Vector2(x, y);
        }

        public static void Min(Vector2 v, Vector2 w, out Vector2 result)
        {
            result = Min(v, w);
        }

        public static Vector2 Min(Vector2 v, Vector2 w)
        {
            var x = v.X < w.X ? v.X : w.X;
            var y = v.Y < w.Y ? v.Y : w.Y;
            return new Vector2(x, y);
        }

        public static void Multiply(Vector2 v, float s, out Vector2 result)
        {
            result = Multiply(v, s);
        }

        public static Vector2 Multiply(Vector2 v, float s)
        {
            var x = v.X * s;
            var y = v.Y * s;
            return new Vector2(x, y);
        }

        public static void Multiply(Vector2 v, Vector2 w, out Vector2 result)
        {
            throw new NotImplementedException();
        }

        public static Vector2 Multiply(Vector2 v, Vector2 w)
        {
            throw new NotImplementedException();
        }

        public static void Negate(Vector2 v, out Vector2 result)
        {
            result = Negate(v);
        }

        public static Vector2 Negate(Vector2 v)
        {
            var x = -v.X;
            var y = -v.Y;
            return new Vector2(x, y);
        }

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

        public static void Subtract(Vector2 v, Vector2 w, out Vector2 result)
        {
            result = Subtract(v, w);
        }

        public static Vector2 Subtract(Vector2 v, Vector2 w)
        {
            var x = v.X - w.X;
            var y = v.Y - w.Y;
            return new Vector2(x, y);
        }

        public static void Transform(Vector2 v, Matrix m, out Vector2 result)
        {
            throw new NotImplementedException();
        }

        public static Vector2 Transform(Vector2 v, Matrix m)
        {
            throw new NotImplementedException();
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

        public bool Equals(Vector2 other) =>
            this.X == other.X && this.Y == other.Y;

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Vector2))
            {
                return false;
            }

            var other = (Vector2)obj;
            return this.Equals(other);
        }

        public override int GetHashCode() =>
            this.X.GetHashCode() * 31 + this.Y.GetHashCode();

        public float Length() =>
            (float)Math.Sqrt(LengthSquared());

        public float LengthSquared() =>
            (float)(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2));

        public Vector2 Normalize() => 
            Normalize(this);

        public override string ToString() =>
            $"{{X:{this.X}, Y:{this.Y}}}";

        private static float Clamp(float v, float min, float max)
        {
            if (v < min) return min;
            if (v > max) return max;
            return v;
        }
    }
}
