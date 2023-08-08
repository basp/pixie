namespace Pixie;

public static class Transform
{
    public static Matrix4x4 CreateShear(
        float xy, float xz,
        float yx, float yz,
        float zx, float zy)
    {
        Matrix4x4 m = new(
            1, xy, xz, 0,
            yx, 1, yz, 0,
            zx, zy, 1, 0,
            0f, 0f, 0, 1);
        return Matrix4x4.Transpose(m);
    }
}