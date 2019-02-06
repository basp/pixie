namespace Pixie.Core
{
    using System;

    public static class Transform
    {
        public static Double4x4 Translate(double x, double y, double z) =>
            new Double4x4(
                1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1);

        public static Double4x4 Scale(double x, double y, double z) =>
            new Double4x4(
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1);

        public static Double4x4 RotateX(double r) =>
            new Double4x4(
                1, 0, 0, 0,
                0, (double)Math.Cos(r), -(double)Math.Sin(r), 0,
                0, (double)Math.Sin(r), (double)Math.Cos(r), 0,
                0, 0, 0, 1);

        public static Double4x4 RotateY(double r) =>
            new Double4x4(
                (double)Math.Cos(r), 0, (double)Math.Sin(r), 0,
                0, 1, 0, 0,
                -(double)Math.Sin(r), 0, (double)Math.Cos(r), 0,
                0, 0, 0, 1);

        public static Double4x4 RotateZ(double r) =>
            new Double4x4(
                (double)Math.Cos(r), -(double)Math.Sin(r), 0, 0,
                (double)Math.Sin(r), (double)Math.Cos(r), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        public static Double4x4 Shear(double xy, double xz, double yx, double yz, double zx, double zy) =>
            new Double4x4(
                1, xy, xz, 0,
                yx, 1, yz, 0,
                zx, zy, 1, 0,
                0, 0, 0, 1);

        public static Double4x4 View(Double4 from, Double4 to, Double4 up)
        {
            var forward = (to - from).Normalize();
            var left = Double4.Cross(forward, up.Normalize());
            var trueUp = Double4.Cross(left, forward);   
            var orientation =         
                new Double4x4(
                    left.X, left.Y, left.Z, 0,
                    trueUp.X, trueUp.Y, trueUp.Z, 0,
                    -forward.X, -forward.Y, -forward.Z, 0,
                    0, 0, 0, 1);

            return orientation * Translate(-from.X, -from.Y, -from.Z);
        }
    }
}