namespace Pixie
{
    using System;

    public struct Matrix : IEquatable<Matrix>
    {
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
            M11 = row1.W;
            M12 = row1.X;
            M13 = row1.Y;
            M14 = row1.Z;

            M21 = row2.W;
            M22 = row2.X;
            M23 = row2.Y;
            M24 = row2.Z;

            M31 = row3.W;
            M32 = row3.X;
            M33 = row3.Y;
            M34 = row3.Z;

            M41 = row4.W;
            M42 = row4.X;
            M43 = row4.Y;
            M44 = row4.Z;
        }

        public float M11;
        public float M12; // first row, second column
        public float M13;
        public float M14;

        public float M21;
        public float M22;
        public float M23;
        public float M24;

        public float M31;
        public float M32;
        public float M33;
        public float M34;

        public float M41;
        public float M42;
        public float M43;
        public float M44;

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