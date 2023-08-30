// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;

    public static class Transform
    {
#pragma warning disable SA1117 // ParametersMustBeOnSameLineOrSeparateLines
        public static Matrix4x4 Translate(double x, double y, double z) =>
            new Matrix4x4(
                1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1);

        public static Matrix4x4 Scale(double x, double y, double z) =>
            new Matrix4x4(
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1);

        public static Matrix4x4 RotateX(double r) =>
            new Matrix4x4(
                1, 0, 0, 0,
                0, (double)Math.Cos(r), -(double)Math.Sin(r), 0,
                0, (double)Math.Sin(r), (double)Math.Cos(r), 0,
                0, 0, 0, 1);

        public static Matrix4x4 RotateY(double r) =>
            new Matrix4x4(
                (double)Math.Cos(r), 0, (double)Math.Sin(r), 0,
                0, 1, 0, 0,
                -(double)Math.Sin(r), 0, (double)Math.Cos(r), 0,
                0, 0, 0, 1);

        public static Matrix4x4 RotateZ(double r) =>
            new Matrix4x4(
                (double)Math.Cos(r), -(double)Math.Sin(r), 0, 0,
                (double)Math.Sin(r), (double)Math.Cos(r), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        public static Matrix4x4 Shear(double xy, double xz, double yx, double yz, double zx, double zy) =>
            new Matrix4x4(
                1, xy, xz, 0,
                yx, 1, yz, 0,
                zx, zy, 1, 0,
                0, 0, 0, 1);

        public static Matrix4x4 View(Vector4 from, Vector4 to, Vector4 up)
        {
            var fwd = (to - from).Normalize();
            var left = Vector4.Cross3(fwd, up.Normalize());
            var trueUp = Vector4.Cross3(left, fwd);
            var orientation =
                new Matrix4x4(
                    left.X, left.Y, left.Z, 0,
                    trueUp.X, trueUp.Y, trueUp.Z, 0,
                    -fwd.X, -fwd.Y, -fwd.Z, 0,
                    0, 0, 0, 1);

            return orientation * Translate(-from.X, -from.Y, -from.Z);
        }
#pragma warning restore SA1117 // ParametersMustBeOnSameLineOrSeparateLines

        public static Matrix4x4 Translate(
            this Matrix4x4 m,
            double x,
            double y,
            double z) => Transform.Translate(x, y, z) * m;

        public static Matrix4x4 Scale(
            this Matrix4x4 m,
            double x,
            double y,
            double z) => Transform.Scale(x, y, z) * m;

        public static Matrix4x4 RotateX(this Matrix4x4 m, double r) =>
            Transform.RotateX(r) * m;

        public static Matrix4x4 RotateY(this Matrix4x4 m, double r) =>
            Transform.RotateY(r) * m;

        public static Matrix4x4 RotateZ(this Matrix4x4 m, double r) =>
            Transform.RotateZ(r) * m;

        public static Matrix4x4 Shear(
            this Matrix4x4 m,
            double xy,
            double xz,
            double yx,
            double yz,
            double zx,
            double zy) => Transform.Shear(xy, xz, yx, yz, zx, zy) * m;
    }
}
