namespace Pixie
{
    using System;

    public struct Vector3 : IEquatable<Vector3>
    {
        public static Vector3 One = new Vector3(1, 1, 1);

        public static Vector3 UnitX = new Vector3(1, 0, 0);

        public static Vector3 UnitY = new Vector3(0, 1, 0);

        public static Vector3 UnitZ = new Vector3(0, 0, 1);

        public static Vector3 Zero = new Vector3(0, 0, 0);

        public readonly float X;

        public readonly float Y;

        public readonly float Z;

        public Vector3(float value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
        }

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3(Vector2 value, float z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }

        public static void Add(Vector3 v, Vector3 w, out Vector3 result)
        {
            result = Add(v, w);
        }

        public static Vector3 Add(Vector3 v, Vector3 w) =>
            new Vector3(
                v.X + w.X,
                v.Y + w.Y,
                v.Z + w.Z);

        public static void Ceiling(Vector3 v, out Vector3 result)
        {
            result = Ceiling(v);
        }

        public static Vector3 Ceiling(Vector3 v) =>
            new Vector3(
                (float)Math.Ceiling(v.X),
                (float)Math.Ceiling(v.Y),
                (float)Math.Ceiling(v.Z));

        public static void Clamp(Vector3 v, Vector3 min, Vector3 max, out Vector3 result)
        {
            result = Clamp(v, min, max);
        }

        public static Vector3 Clamp(Vector3 v, Vector3 min, Vector3 max) =>
            new Vector3(
                MathHelpers.Clamp(v.X, min.X, max.X),
                MathHelpers.Clamp(v.Y, min.Y, max.Y),
                MathHelpers.Clamp(v.Z, min.Z, max.Z));

        public static void Divide(Vector3 v, float divisor, out Vector3 result)
        {
            result = Divide(v, divisor);
        }

        public static void Divide(Vector3 v, Vector3 divisor, out Vector3 result)
        {
            result = Divide(v, divisor);
        }

        public static Vector3 Divide(Vector3 v, Vector3 divisor) =>
            new Vector3(
                v.X / divisor.X,
                v.Y / divisor.Y,
                v.Z / divisor.Z);

        public static Vector3 Divide(Vector3 v, float divisor) =>
            new Vector3(
                v.X / divisor,
                v.Y / divisor,
                v.Z / divisor);

        public static void Dot(Vector3 v, Vector3 w, out float result)
        {
            result = Dot(v, w);
        }

        public static float Dot(Vector3 v, Vector3 w) =>
            v.X * w.X + v.Y * w.Y + v.Z * w.Z;

        public static void Floor(Vector3 v, out Vector3 result)
        {
            result = Floor(v);
        }

        public static Vector3 Floor(Vector3 v) =>
            new Vector3(
                (float)Math.Floor(v.X),
                (float)Math.Floor(v.Y),
                (float)Math.Floor(v.Z));

        public static void Lerp(Vector3 v, Vector3 w, float a, out Vector3 result)
        {
            result = Lerp(v, w, a);
        }

        public static Vector3 Lerp(Vector3 v, Vector3 w, float a) =>
            new Vector3(
                MathHelpers.Lerp(v.X, w.X, a),
                MathHelpers.Lerp(v.Y, w.Y, a),
                MathHelpers.Lerp(v.Z, w.Z, a));

        public static void Max(Vector3 v, Vector3 w, out Vector3 result)
        {
            result = Max(v, w);
        }

        public static Vector3 Max(Vector3 v, Vector3 w) =>
            new Vector3(
                Math.Max(v.X, w.X),
                Math.Max(v.Y, w.Y),
                Math.Max(v.Z, w.Z));

        public static void Min(Vector3 v, Vector3 w, out Vector3 result)
        {
            result = Min(v, w);
        }

        public static Vector3 Min(Vector3 v, Vector3 w) =>
            new Vector3(
                Math.Min(v.X, w.X),
                Math.Min(v.Y, w.Y),
                Math.Min(v.Z, w.Z));

        public static void Multiply(Vector3 v, float s, out Vector3 result)
        {
            result = Multiply(v, s);
        }

        public static Vector3 Multiply(Vector3 v, float s) =>
            new Vector3(
                v.X * s,
                v.Y * s,
                v.Z * s);

        public static void Multiply(Vector3 v, Vector3 w, out Vector3 result)
        {
            result = Multiply(v, w);
        }

        public static Vector3 Multiply(Vector3 v, Vector3 w) =>
            new Vector3(
                v.X * w.X,
                v.Y * w.Y,
                v.Z * w.Z);

        public static void Negate(Vector3 v, out Vector3 result)
        {
            result = Negate(v);
        }

        public static Vector3 Negate(Vector3 v) =>
            new Vector3(
                -v.X,
                -v.Y,
                -v.Z);

        public static void Normalize(Vector3 v, out Vector3 result)
        {
            result = Normalize(v);
        }

        public static Vector3 Normalize(Vector3 v)
        {
            var len = v.Length();
            var x = v.X / len;
            var y = v.Y / len;
            var z = v.Z / len;
            return new Vector3(x, y, z);
        }

        public static void Round(Vector3 u, out Vector3 result)
        {
            result = Round(u);
        }

        public static Vector3 Round(Vector3 u) =>
            new Vector3(
                (float)Math.Round(u.X),
                (float)Math.Round(u.Y),
                (float)Math.Round(u.Z));

        public static void Subtract(Vector3 v, Vector3 w, out Vector3 result)
        {
            result = Subtract(v, w);
        }

        public static Vector3 Subtract(Vector3 v, Vector3 w) =>
            new Vector3(
                v.X - w.X,
                v.Y - w.Y,
                v.Z - w.Z);

        public static void Transform(Vector3 v, Matrix m, out Vector3 result)
        {
            result = Transform(v, m);
        }

        public static Vector3 Transform(Vector3 v, Matrix m)
        {
            var v4 = new Vector4(v.X, v.Y, v.Z, 1);
            var result = Vector4.Transform(v4, m);
            return new Vector3(result.X, result.Y, result.Z);
        }

        public static Vector3 operator -(Vector3 v) =>
            Vector3.Negate(v);

        public static Vector3 operator +(Vector3 v, Vector3 w) =>
            Vector3.Add(v, w);

        public static Vector3 operator *(Vector3 v, Vector3 w) =>
            Vector3.Multiply(v, w);

        public static Vector3 operator *(Vector3 v, float s) =>
            Vector3.Multiply(v, s);

        public static Vector3 operator *(float s, Vector3 v) =>
            Vector3.Multiply(v, s);

        public static Vector3 operator /(Vector3 v, Vector3 w) =>
            Vector3.Divide(v, w);

        public static Vector3 operator /(Vector3 v, float s) =>
            Vector3.Divide(v, s);

        public static bool operator ==(Vector3 v, Vector3 w) =>
            v.Equals(w);

        public static bool operator !=(Vector3 v, Vector3 w) =>
            !v.Equals(w);

        public Vector3 Ceiling() => Ceiling(this);

        public bool Equals(Vector3 other) =>
            this.X == other.X &&
            this.Y == other.Y &&
            this.Z == other.Z;

        public void Deconstruct(
            out float x,
            out float y,
            out float z)
        {
            x = this.X;
            y = this.Y;
            z = this.Z;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Vector3))
            {
                return false;
            }

            var other = (Vector3)obj;
            return this.Equals(other);
        }

        public Vector3 Floor() => Floor(this);

        public override int GetHashCode()
        {
            var h = this.X.GetHashCode();
            h ^= this.Y.GetHashCode();
            h ^= this.Z.GetHashCode();
            return h;
        }

        public float Length() =>
            (float)Math.Sqrt(this.LengthSquared());

        public float LengthSquared() =>
            (float)(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2) + Math.Pow(this.Y, 2));

        public Vector3 Normalize() => Normalize(this);

        public Vector3 Round() => Round(this);

        public override string ToString() =>
            $"{{X:{this.X}, Y:{this.Y}, Z:{this.Z}}}";
    }
}
