namespace Pixie.Core
{
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
    }
}