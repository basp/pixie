namespace Pixie
{
    using System;
    using System.Linq;

    public struct Matrix : IEquatable<Matrix>
    {
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

        public Vector4[] Rows => new[]
        {
            new Vector4(this.M11, this.M12, this.M13, this.M14),
            new Vector4(this.M21, this.M22, this.M23, this.M24),
            new Vector4(this.M31, this.M32, this.M33, this.M34),
            new Vector4(this.M41, this.M42, this.M43, this.M44),
        };

        public Vector4[] Columns => new[]
        {
            new Vector4(this.M11, this.M21, this.M31, this.M41),
            new Vector4(this.M12, this.M22, this.M32, this.M42),
            new Vector4(this.M13, this.M23, this.M33 ,this.M43),
            new Vector4(this.M14, this.M24, this.M34 ,this.M44),
        };

        public static Matrix Add(Matrix a, Matrix b)
        {
            throw new NotImplementedException();
        }

        public static void Add(Matrix a, Matrix b, out Matrix result)
        {
            result = Add(a, b);
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

        public static void Multiply(Matrix a, Matrix b, out Matrix result)
        {
            result = Multiply(a, b);
        }

        public static Matrix Subtract(Matrix a, Matrix b)
        {
            throw new NotImplementedException();
        }

        public static void Subtract(Matrix a, Matrix b, out Matrix result)
        {
            result = Subtract(a, b);
        }

        public override bool Equals(object obj)
        {
            if(obj.GetType() != typeof(Matrix))
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
    }
}