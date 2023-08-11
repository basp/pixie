namespace Pixie;

public static class Extensions
{
    public static Option<Intersection> GetHit(
        this IEnumerable<Intersection> xs) => xs
            .Order()
            .FirstOrNone(x => x.T >= 0);
}