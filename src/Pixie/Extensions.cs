namespace Pixie;

public static class Extensions
{
    public static bool IsApprox<T>(this T a, T b, T tol)
        where T : INumber<T> => T.Abs(a - b) < tol;
}