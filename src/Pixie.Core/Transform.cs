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
            var fwd = (to - from).Normalize();
            var left = Double4.Cross(fwd, up.Normalize());
            var trueUp = Double4.Cross(left, fwd);   
            var orientation =         
                new Double4x4(
                    left.X, left.Y, left.Z, 0,
                    trueUp.X, trueUp.Y, trueUp.Z, 0,
                    -fwd.X, -fwd.Y, -fwd.Z, 0,
                    0, 0, 0, 1);

            return orientation * Translate(-from.X, -from.Y, -from.Z);
        }

        public static Double4x4 Translate(
            this Double4x4 m, 
            double x, 
            double y, 
            double z) => Transform.Translate(x, y, z) * m;

        public static Double4x4 Scale(
            this Double4x4 m, 
            double x, 
            double y, 
            double z) => Transform.Scale(x, y, z) * m;

        public static Double4x4 RotateX(this Double4x4 m, double r) =>
            Transform.RotateX(r) * m;

        public static Double4x4 RotateY(this Double4x4 m, double r) =>
            Transform.RotateY(r) * m;

        public static Double4x4 RotateZ(this Double4x4 m, double r) =>
            Transform.RotateZ(r) * m;

        public static Double4x4 Shear(
            this Double4x4 m,
            double xy,
            double xz,
            double yx,
            double yz,
            double zx,
            double zy) => Transform.Shear(xy, xz, yx, yz, zx, zy) * m;
    }
}