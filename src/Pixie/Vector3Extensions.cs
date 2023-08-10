namespace Pixie;

public static class Vector3Extensions
{
    public static Vector4 AsPosition(this Vector3 u) =>
        new(u.X, u.Y, u.Z, 1);

    public static Vector4 AsDirection(this Vector3 u) =>
        new(u.X, u.Y, u.Z, 0);
}