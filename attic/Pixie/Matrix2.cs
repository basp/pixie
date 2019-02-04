namespace Pixie
{
    using System;

    public struct Matrix2
    {
        public readonly float M11;
        public readonly float M12;
        public readonly float M21;
        public readonly float M22;

        public Matrix2(float m11, float m12, float m21, float m22)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M21 = m21;
            this.M22 = m22;
        }

        public static Matrix2 CreateRotation(float theta)
        {
            var m11 = (float)Math.Cos(theta);
            var m12 = -(float)Math.Sin(theta);
            var m21 = (float)Math.Sin(theta);
            var m22 = (float)Math.Cos(theta);
            return new Matrix2(m11, m12, m21, m22);
        }
    }
}