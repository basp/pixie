namespace Pixie
{
    using System;

    public struct Vector4 : IEquatable<Vector4>
    {
        public static Vector4 One = new Vector4(1, 1, 1, 1);

        public static Vector4 UnitW = new Vector4(0, 0, 0, 1);

        public static Vector4 UnitX = new Vector4(1, 0, 0, 0);

        public static Vector4 UnitY = new Vector4(0, 1, 0, 0);

        public static Vector4 UnitZ = new Vector4(0, 0, 1, 0);

        public static Vector4 Zero = new Vector4(0, 0, 0, 0);

        public float W;

        public float X;

        public float Y;

        public float Z;

        public Vector4(float value)
        {
            W = value;
            X = value;
            Y = value;
            Z = value;
        }

        public Vector4(float x, float y, float z, float w)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }

        public Vector4(Vector3 value, float w)
        {
            W = w;
            X = value.X;
            Y = value.Y;
            Z = value.Z;
        }

        public Vector4(Vector2 value, float z, float w)
        {
            W = w;
            X = value.X;
            Y = value.Y;
            Z = z;
        }

        public static void Add(Vector4 u, Vector4 v, out Vector4 result)
        {
            result = Add(u, v);
        }

        public static Vector4 Add(Vector4 u, Vector4 v) =>
            new Vector4(
                u.X + v.X,
                u.Y + v.Y,
                u.Z + v.Z,
                u.W + v.W);

        public static void Ceiling(Vector4 u, out Vector4 result)
        {
            result = Ceiling(u);
        }

        public static Vector4 Ceiling(Vector4 u) =>
            new Vector4(
                (float)Math.Ceiling(u.X),
                (float)Math.Ceiling(u.Y),
                (float)Math.Ceiling(u.Z),
                (float)Math.Ceiling(u.W));

        public static void Clamp(Vector4 u, Vector4 min, Vector4 max, out Vector4 result)
        {
            result = Clamp(u, min, max);
        }

        public static Vector4 Clamp(Vector4 u, Vector4 min, Vector4 max) =>
            new Vector4(
                MathHelpers.Clamp(u.X, min.X, max.X),
                MathHelpers.Clamp(u.Y, min.Y, max.Y),
                MathHelpers.Clamp(u.Z, min.Z, max.Z),
                MathHelpers.Clamp(u.W, min.W, max.W));

        public static void Divide(Vector4 u, float divisor, out Vector4 result)
        {
            result = Divide(u, divisor);
        }

        public static Vector4 Divide(Vector4 u, float divisor) =>
            new Vector4(
                u.X / divisor,
                u.Y / divisor,
                u.Z / divisor,
                u.W / divisor);

        public static void Divide(Vector4 u, Vector4 v, out Vector4 result)
        {
            result = Divide(u, v);
        }

        public static Vector4 Divide(Vector4 u, Vector4 v) =>
            new Vector4(
                u.X / v.X,
                u.Y / v.Y,
                u.Z / v.Z,
                u.W / v.W);

        public static void Dot(Vector4 u, Vector4 v, out float result)
        {
            result = Dot(u, v);
        }

        public static float Dot(Vector4 u, Vector4 v) =>
            u.X * v.X +
            u.Y * v.Y +
            u.Z * v.Z +
            u.W * v.W;

        public static void Floor(Vector4 u, out Vector4 result)
        {
            result = Floor(u);
        }

        public static Vector4 Floor(Vector4 u) =>
            new Vector4(
                (float)Math.Floor(u.X),
                (float)Math.Floor(u.Y),
                (float)Math.Floor(u.Z),
                (float)Math.Floor(u.W));

        public static void Lerp(Vector4 u, Vector4 v, float a, out Vector4 result)
        {
            result = Lerp(u, v, a);
        }

        public static Vector4 Lerp(Vector4 u, Vector4 v, float a) =>
            new Vector4(
                MathHelpers.Lerp(u.X, v.X, a),
                MathHelpers.Lerp(u.Y, v.Y, a),
                MathHelpers.Lerp(u.Z, v.Z, a),
                MathHelpers.Lerp(u.W, v.W, a));

        public static void Max(Vector4 u, Vector4 v, out Vector4 result)
        {
            result = Max(u, v);
        }

        public static Vector4 Max(Vector4 u, Vector4 v) =>
            new Vector4(
                Math.Max(u.X, v.X),
                Math.Max(u.Y, v.Y),
                Math.Max(u.Z, v.Z),
                Math.Max(u.W, v.W));

        public static void Min(Vector4 u, Vector4 v, out Vector4 result)
        {
            result = Min(u, v);
        }

        public static Vector4 Min(Vector4 u, Vector4 v) =>
            new Vector4(
                Math.Min(u.X, v.X),
                Math.Min(u.Y, v.Y),
                Math.Min(u.Z, v.Z),
                Math.Min(u.W, v.W));

        public static void Multiply(Vector4 u, Vector4 v, out Vector4 result)
        {
            result = Multiply(u, v);
        }

        public static Vector4 Multiply(Vector4 u, Vector4 v) =>
            new Vector4(
                u.X * v.X,
                u.Y * v.Y,
                u.Z * v.Z,
                u.W * v.W);

        public static void Multiply(Vector4 u, float s, out Vector4 result)
        {
            result = Multiply(u, s);
        }

        public static Vector4 Multiply(Vector4 u, float s) =>
            new Vector4(
                u.X * s,
                u.Y * s,
                u.Z * s,
                u.W * s);

        public static void Negate(Vector4 u, out Vector4 result)
        {
            result = Negate(u);
        }

        public static Vector4 Negate(Vector4 u) =>
            new Vector4(
                -u.X,
                -u.Y,
                -u.Z,
                -u.W);

        public static void Normalize(Vector4 u, out Vector4 result)
        {
            result = Normalize(u);
        }

        public static Vector4 Normalize(Vector4 u)
        {
            var len = u.Length();
            var x = u.X / len;
            var y = u.Y / len;
            var z = u.Z / len;
            var w = u.W / len;
            return new Vector4(x, y, z, w);
        }

        public static void Round(Vector4 u, out Vector4 result)
        {
            result = Round(u);
        }

        public static Vector4 Round(Vector4 u) =>
            new Vector4(
                (float)Math.Round(u.X),
                (float)Math.Round(u.Y),
                (float)Math.Round(u.Z),
                (float)Math.Round(u.W));

        public static void Subtract(Vector4 u, Vector4 v, out Vector4 result)
        {
            result = Subtract(u, v);
        }

        public static Vector4 Subtract(Vector4 u, Vector4 v) =>
            new Vector4(
                u.X - v.X,
                u.Y - v.Y,
                u.Z - v.Z,
                u.W - v.W);

        public static void Transform(Vector4 v, Matrix m, out Vector4 result)
        {
            result = Transform(v, m);
        }

        public static Vector4 Transform(Vector4 v, Matrix m)
        {
            var row1 = new Vector4(m.M11, m.M12, m.M13, m.M14);
            var row2 = new Vector4(m.M21, m.M22, m.M23, m.M24);
            var row3 = new Vector4(m.M31, m.M32, m.M33, m.M34);
            var row4 = new Vector4(m.M41, m.M42, m.M43, m.M44);

            return new Vector4(
                Vector4.Dot(v, row1),
                Vector4.Dot(v, row2),
                Vector4.Dot(v, row3),
                Vector4.Dot(v, row4));
        }

        public static Vector4 operator -(Vector4 u) =>
            Vector4.Negate(u);

        public static Vector4 operator +(Vector4 u, Vector4 v) =>
            Vector4.Add(u, v);

        public static Vector4 operator -(Vector4 u, Vector4 v) =>
            Vector4.Subtract(u, v);

        public static Vector4 operator *(Vector4 u, Vector4 v) =>
            Vector4.Multiply(u, v);

        public static Vector4 operator *(Vector4 u, float s) =>
            Vector4.Multiply(u, s);

        public static Vector4 operator *(float s, Vector4 u) =>
            Vector4.Multiply(u, s);

        public static Vector4 operator /(Vector4 u, Vector4 v) =>
            Vector4.Divide(u, v);

        public static Vector4 operator /(Vector4 u, float s) =>
            Vector4.Divide(u, s);

        public static bool operator ==(Vector4 u, Vector4 v) =>
            u.Equals(v);

        public static bool operator !=(Vector4 u, Vector4 v) =>
            !u.Equals(v);

        public Vector4 Ceiling() => Ceiling(this);

        public void Deconstruct(
            out float x,
            out float y,
            out float z,
            out float w)
        {
            x = this.X;
            y = this.Y;
            z = this.Z;
            w = this.W;
        }

        public bool Equals(Vector4 other) =>
            this.X == other.X &&
            this.Y == other.Y &&
            this.Z == other.Z &&
            this.W == other.W;

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Vector4))
            {
                return false;
            }

            var other = (Vector4)obj;
            return this.Equals(other);
        }

        public Vector4 Floor() => Floor(this);

        public override int GetHashCode()
        {
            var h = this.X.GetHashCode();
            h ^= this.Y.GetHashCode();
            h ^= this.Z.GetHashCode();
            h ^= this.W.GetHashCode();
            return h;
        }

        public float Length() =>
            (float)Math.Sqrt(this.LengthSquared());

        public float LengthSquared() =>
            (float)(
                Math.Pow(this.X, 2) +
                Math.Pow(this.Y, 2) +
                Math.Pow(this.Z, 2) +
                Math.Pow(this.W, 2));

        public Vector4 Normalize() =>
            Normalize(this);

        public Vector4 Round() => Round(this);

        public override string ToString() =>
            $"{{X:{this.X}, Y:{this.Y}, Z:{this.Z}, W:{this.W}}}";
    }
}