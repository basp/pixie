namespace Pixie;

public static class Utils
{
    public static Matrix4x4 UnsafeInvert(Matrix4x4 m)
    {
        if (Matrix4x4.Invert(m, out var inv))
        {
            return inv;
        }

        return new Matrix4x4(
            float.NaN, float.NaN, float.NaN, float.NaN,
            float.NaN, float.NaN, float.NaN, float.NaN,
            float.NaN, float.NaN, float.NaN, float.NaN,
            float.NaN, float.NaN, float.NaN, float.NaN);
    }
}