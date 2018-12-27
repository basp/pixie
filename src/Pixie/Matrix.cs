namespace Pixie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public struct Matrix : IEquatable<Matrix>
    {
        public static Matrix Identity =>
            new Matrix(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        public readonly float M11;
        public readonly float M12; // first row, second column
        public readonly float M13;
        public readonly float M14;

        public readonly float M21;
        public readonly float M22;
        public readonly float M23;
        public readonly float M24;

        public readonly float M31;
        public readonly float M32;
        public readonly float M33;
        public readonly float M34;

        public readonly float M41;
        public readonly float M42;
        public readonly float M43;
        public readonly float M44;

        public Matrix(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;

            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;

            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;

            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        public Matrix(
            Vector4 row1,
            Vector4 row2,
            Vector4 row3,
            Vector4 row4)
        {
            M11 = row1.X;
            M12 = row1.Y;
            M13 = row1.Z;
            M14 = row1.W;

            M21 = row2.X;
            M22 = row2.Y;
            M23 = row2.Z;
            M24 = row2.W;

            M31 = row3.X;
            M32 = row3.Y;
            M33 = row3.Z;
            M34 = row3.W;

            M41 = row4.X;
            M42 = row4.Y;
            M43 = row4.Z;
            M44 = row4.W;
        }

        public static void CreateRotationX(float t, out Matrix result)
        {
            result = CreateRotationX(t);
        }

        public static Matrix CreateRotationX(float t) =>
            new Matrix(
                1, 0, 0, 0,
                0, (float)Math.Cos(t), -(float)Math.Sin(t), 0,
                0, (float)Math.Sin(t), (float)Math.Cos(t), 0,
                0, 0, 0, 1);

        public static void CreateRotationY(float t, out Matrix result)
        {
            result = CreateRotationY(t);
        }

        public static Matrix CreateRotationY(float t) =>
            new Matrix(
                (float)Math.Cos(t), 0, (float)Math.Sin(t), 0,
                0, 1, 0, 0,
                -(float)Math.Sin(t), 0, (float)Math.Cos(t), 0,
                0, 0, 0, 1);

        public static void CreateRotationZ(float t, out Matrix result)
        {
            result = CreateRotationZ(t);
        }

        public static Matrix CreateRotationZ(float t) =>
            new Matrix(
                (float)Math.Cos(t), -(float)Math.Sin(t), 0, 0,
                (float)Math.Sin(t), (float)Math.Cos(t), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        public static void CreateScale(float s, out Matrix result)
        {
            result = CreateScale(s);
        }

        public static Matrix CreateScale(float s) =>
            new Matrix(
                s, 0, 0, 0,
                0, s, 0, 0,
                0, 0, s, 0,
                0, 0, 0, 1);

        public static void CreateScale(float sx, float sy, float sz, out Matrix result)
        {
            result = CreateScale(sx, sy, sz);
        }

        public static Matrix CreateScale(float sx, float sy, float sz) =>
            new Matrix(
                sx, 0, 0, 0,
                0, sy, 0, 0,
                0, 0, sz, 0,
                0, 0, 0, 1);

        public static void CreateScale(Vector3 sv, out Matrix result)
        {
            result = CreateScale(sv);
        }

        public static Matrix CreateScale(Vector3 sv) =>
            CreateScale(sv.X, sv.Y, sv.Z);

        public static void CreateTranslation(float dx, float dy, float dz, out Matrix result)
        {
            result = CreateTranslation(dx, dy, dz);
        }

        public static Matrix CreateTranslation(float dx, float dy, float dz) =>
            new Matrix(
                1, 0, 0, dx,
                0, 1, 0, dy,
                0, 0, 1, dz,
                0, 0, 0, 1);

        public static void CreateTranslation(Vector3 tv, out Matrix result)
        {
            result = CreateTranslation(tv);
        }

        public static Matrix CreateTranslation(Vector3 tv) =>
            CreateTranslation(tv.X, tv.Y, tv.Z);

        public static void Add(Matrix a, Matrix b, out Matrix result)
        {
            result = Add(a, b);
        }

        public static Matrix Add(Matrix a, Matrix b)
        {
            var m11 = a.M11 + b.M11;
            var m12 = a.M12 + b.M12;
            var m13 = a.M13 + b.M13;
            var m14 = a.M14 + b.M14;

            var m21 = a.M21 + b.M21;
            var m22 = a.M22 + b.M22;
            var m23 = a.M23 + b.M23;
            var m24 = a.M24 + b.M24;

            var m31 = a.M31 + b.M31;
            var m32 = a.M32 + b.M32;
            var m33 = a.M33 + b.M33;
            var m34 = a.M34 + b.M34;

            var m41 = a.M41 + b.M41;
            var m42 = a.M42 + b.M42;
            var m43 = a.M43 + b.M43;
            var m44 = a.M44 + b.M44;

            return new Matrix(
                m11, m12, m13, m14,
                m21, m22, m23, m24,
                m31, m32, m33, m34,
                m41, m42, m43, m44);
        }

        public static void Divide(Matrix a, Matrix b, out Matrix result)
        {
            result = Divide(a, b);
        }

        public static Matrix Divide(Matrix a, Matrix b)
        {
            var row1 = new Vector4(a.M11 / b.M11, a.M12 / b.M12, a.M13 / b.M13, a.M14 / b.M14);
            var row2 = new Vector4(a.M21 / b.M21, a.M22 / b.M22, a.M23 / b.M23, a.M24 / b.M24);
            var row3 = new Vector4(a.M31 / b.M31, a.M32 / b.M32, a.M33 / b.M33, a.M34 / b.M34);
            var row4 = new Vector4(a.M41 / b.M41, a.M42 / b.M42, a.M43 / b.M43, a.M44 / b.M44);
            return new Matrix(row1, row2, row3, row4);
        }

        public static void Divide(Matrix a, float s, out Matrix result)
        {
            result = Divide(a, s);
        }

        public static Matrix Divide(Matrix a, float s)
        {
            var row1 = new Vector4(a.M11 / s, a.M12 / s, a.M13 / s, a.M14 / s);
            var row2 = new Vector4(a.M21 / s, a.M22 / s, a.M23 / s, a.M24 / s);
            var row3 = new Vector4(a.M31 / s, a.M32 / s, a.M33 / s, a.M34 / s);
            var row4 = new Vector4(a.M41 / s, a.M42 / s, a.M43 / s, a.M44 / s);
            return new Matrix(row1, row2, row3, row4);
        }

        public static void Invert(Matrix a, out Matrix result)
        {
            result = Invert(a);
        }

        public static Matrix Invert(Matrix a)
        {
            throw new NotImplementedException();
        }

        public static void Lerp(Matrix a, Matrix b, float t, out Matrix result)
        {
            result = Lerp(a, b, t);
        }

        public static Matrix Lerp(Matrix a, Matrix b, float t)
        {
            var row1 = new Vector4(
                MathHelpers.Lerp(a.M11, b.M11, t),
                MathHelpers.Lerp(a.M12, b.M12, t),
                MathHelpers.Lerp(a.M13, b.M13, t),
                MathHelpers.Lerp(a.M14, b.M14, t));

            var row2 = new Vector4(
                MathHelpers.Lerp(a.M21, b.M21, t),
                MathHelpers.Lerp(a.M22, b.M22, t),
                MathHelpers.Lerp(a.M23, b.M23, t),
                MathHelpers.Lerp(a.M24, b.M24, t));

            var row3 = new Vector4(
                MathHelpers.Lerp(a.M31, b.M31, t),
                MathHelpers.Lerp(a.M32, b.M32, t),
                MathHelpers.Lerp(a.M33, b.M33, t),
                MathHelpers.Lerp(a.M34, b.M34, t));

            var row4 = new Vector4(
                MathHelpers.Lerp(a.M41, b.M41, t),
                MathHelpers.Lerp(a.M42, b.M42, t),
                MathHelpers.Lerp(a.M43, b.M43, t),
                MathHelpers.Lerp(a.M44, b.M44, t));

            return new Matrix(row1, row2, row3, row4);
        }

        public static void Multiply(Matrix a, Matrix b, out Matrix result)
        {
            result = Multiply(a, b);
        }

        public static Matrix Multiply(Matrix a, Matrix b)
        {
            var m11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41;
            var m12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42;
            var m13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43;
            var m14 = a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44;

            var m21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41;
            var m22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42;
            var m23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43;
            var m24 = a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44;

            var m31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41;
            var m32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42;
            var m33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43;
            var m34 = a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44;

            var m41 = a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41;
            var m42 = a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42;
            var m43 = a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43;
            var m44 = a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44;

            return new Matrix(
                m11, m12, m13, m14,
                m21, m22, m23, m24,
                m31, m32, m33, m34,
                m41, m42, m43, m44);
        }

        public static void Multiply(Matrix a, float s, out Matrix result)
        {
            result = Multiply(a, s);
        }

        public static Matrix Multiply(Matrix a, float s)
        {
            var row1 = new Vector4(a.M11 * s, a.M12 * s, a.M13 * s, a.M14 * s);
            var row2 = new Vector4(a.M21 * s, a.M22 * s, a.M23 * s, a.M24 * s);
            var row3 = new Vector4(a.M31 * s, a.M32 * s, a.M33 * s, a.M34 * s);
            var row4 = new Vector4(a.M41 * s, a.M42 * s, a.M43 * s, a.M44 * s);
            return new Matrix(row1, row2, row3, row4);
        }

        public static void Negate(Matrix a, out Matrix result)
        {
            result = Negate(a);
        }

        public static Matrix Negate(Matrix a)
        {
            var row1 = new Vector4(-a.M11, -a.M12, -a.M13, -a.M14);
            var row2 = new Vector4(-a.M21, -a.M22, -a.M23, -a.M24);
            var row3 = new Vector4(-a.M31, -a.M32, -a.M33, -a.M34);
            var row4 = new Vector4(-a.M41, -a.M42, -a.M43, -a.M44);
            return new Matrix(row1, row2, row3, row4);
        }

        public static void Subtract(Matrix a, Matrix b, out Matrix result)
        {
            result = Subtract(a, b);
        }

        public static Matrix Subtract(Matrix a, Matrix b)
        {
            var m11 = a.M11 - b.M11;
            var m12 = a.M12 - b.M12;
            var m13 = a.M13 - b.M13;
            var m14 = a.M14 - b.M14;

            var m21 = a.M21 - b.M21;
            var m22 = a.M22 - b.M22;
            var m23 = a.M23 - b.M23;
            var m24 = a.M24 - b.M24;

            var m31 = a.M31 - b.M31;
            var m32 = a.M32 - b.M32;
            var m33 = a.M33 - b.M33;
            var m34 = a.M34 - b.M34;

            var m41 = a.M41 - b.M41;
            var m42 = a.M42 - b.M42;
            var m43 = a.M43 - b.M43;
            var m44 = a.M44 - b.M44;

            return new Matrix(
                m11, m12, m13, m14,
                m21, m22, m23, m24,
                m31, m32, m33, m34,
                m41, m42, m43, m44);
        }

        public static void Transpose(Matrix a, out Matrix result)
        {
            result = Transpose(a);
        }

        public static Matrix Transpose(Matrix a)
        {
            throw new NotImplementedException();
        }

        public static Matrix operator -(Matrix a) =>
            Matrix.Negate(a);

        public static Matrix operator +(Matrix a, Matrix b) =>
            Matrix.Add(a, b);

        public static Matrix operator -(Matrix a, Matrix b) =>
            Matrix.Subtract(a, b);

        public static Matrix operator *(Matrix a, Matrix b) =>
            Matrix.Multiply(a, b);

        public static Matrix operator *(Matrix a, float s) =>
            Matrix.Multiply(a, s);

        public static Matrix operator /(Matrix a, Matrix b) =>
            Matrix.Divide(a, b);

        public static Matrix operator /(Matrix a, float s) =>
            Matrix.Divide(a, s);

        public static bool operator ==(Matrix a, Matrix b) =>
            a.Equals(b);

        public static bool operator !=(Matrix a, Matrix b) =>
            !a.Equals(b);

        public float Determinant() => throw new NotImplementedException();

        public bool Equals(Matrix other) =>
            this.M11 == other.M11 &&
            this.M12 == other.M12 &&
            this.M13 == other.M13 &&
            this.M14 == other.M14 &&
            this.M21 == other.M21 &&
            this.M22 == other.M22 &&
            this.M23 == other.M23 &&
            this.M24 == other.M24 &&
            this.M31 == other.M31 &&
            this.M32 == other.M32 &&
            this.M33 == other.M33 &&
            this.M34 == other.M34 &&
            this.M41 == other.M41 &&
            this.M42 == other.M42 &&
            this.M43 == other.M43 &&
            this.M44 == other.M44;

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Matrix))
            {
                return false;
            }

            var other = (Matrix)obj;
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            var h = this.M11.GetHashCode();
            h ^= this.M12.GetHashCode();
            h ^= this.M13.GetHashCode();
            h ^= this.M14.GetHashCode();
            h ^= this.M21.GetHashCode();
            h ^= this.M22.GetHashCode();
            h ^= this.M23.GetHashCode();
            h ^= this.M24.GetHashCode();
            h ^= this.M31.GetHashCode();
            h ^= this.M32.GetHashCode();
            h ^= this.M33.GetHashCode();
            h ^= this.M34.GetHashCode();
            h ^= this.M41.GetHashCode();
            h ^= this.M42.GetHashCode();
            h ^= this.M43.GetHashCode();
            h ^= this.M44.GetHashCode();
            return h;
        }
    }
}