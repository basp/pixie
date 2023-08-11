namespace Pixie;

public static class Extensions
{
    public static Option<Intersection> GetHit(
        this IEnumerable<Intersection> xs) => xs
            .Order()
            .FirstOrNone(x => x.T >= 0);

    public static Vector4 Reflect(this Vector4 v, Vector4 n) =>
        v - n * 2 * Vector4.Dot(v, n);
}