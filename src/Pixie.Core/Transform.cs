namespace Pixie.Core
{
    using System;

    public static class Transform
    {
        public static Float4x4 Translate(float x, float y, float z) =>
            new Float4x4(
                1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1);

        public static Float4x4 Scale(float x, float y, float z) =>
            new Float4x4(
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1);

        public static Float4x4 RotateX(float r) =>
            new Float4x4(
                1, 0, 0, 0,
                0, (float)Math.Cos(r), -(float)Math.Sin(r), 0,
                0, (float)Math.Sin(r), (float)Math.Cos(r), 0,
                0, 0, 0, 1);

        public static Float4x4 RotateY(float r) =>
            new Float4x4(
                (float)Math.Cos(r), 0, (float)Math.Sin(r), 0,
                0, 1, 0, 0,
                -(float)Math.Sin(r), 0, (float)Math.Cos(r), 0,
                0, 0, 0, 1);

        public static Float4x4 RotateZ(float r) =>
            new Float4x4(
                (float)Math.Cos(r), -(float)Math.Sin(r), 0, 0,
                (float)Math.Sin(r), (float)Math.Cos(r), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

        public static Float4x4 Shear(float xy, float xz, float yx, float yz, float zx, float zy) =>
            new Float4x4(
                1, xy, xz, 0,
                yx, 1, yz, 0,
                zx, zy, 1, 0,
                0, 0, 0, 1);
    }
}