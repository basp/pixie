namespace Pixie;

public static class Utils
{
    private const double RadiansPerDegree = Math.PI / 180.0;
    private const double DegreesPerRadian = 180.0 / Math.PI;

    public static double Radians(double degrees) =>
        Utils.RadiansPerDegree * degrees;

    public static double Degrees(double radians) =>
        Utils.DegreesPerRadian * radians;

#if DEBUG
    public static double SafeSqrt(double x, double atol = 1e-3)
    {
        Debug.Assert(x >= -atol);
        return Math.Sqrt(Math.Max(0, x));
    }
#else
     public static double SafeSqrt(double x) => Math.Sqrt(Math.Max(0, x));
#endif

#if DEBUG
    public static double SafeAsin(double x)
    {
        Debug.Assert(x is >= -1.0001 and <= 1.0001);
        return Math.Asin(Math.Clamp(x, -1, 1));
    }
#else
     public static double SafeAsin(double x) =>
         Math.Asin(Math.Clamp(x, -1, 1));
#endif

#if DEBUG
    public static double SafeAcos(double x)
    {
        Debug.Assert(x is >= -1.0001 and <= 1.0001);
        return Math.Acos(Math.Clamp(x, -1, 1));
    }
#else
     public static double SafeAcos(double x) =>
         Math.Acos(Math.Clamp(x, -1, 1));
#endif

    public static T SinXOverX<T>(T x)
        where T : IFloatingPointIeee754<T>
    {
        if (T.One - x * x == T.One)
        {
            return T.One;
        }

        return T.Sin(x) / x;
    }

    public static double SmoothStep(double x, double a, double b)
    {
        if (a.IsApprox(b, 1e-6))
        {
            return (x < a) ? 0 : 1;
        }

        var t = Math.Clamp((x - a) / (b - a), 0, 1);
        return t * t * (3 - 2 * t);
    }

    public static T Sqr<T>(T x) where T : INumber<T> => x * x;

    public static double EvaluatePolynomial(
        IReadOnlyList<double> c,
        double x)
    {
        var n = c.Count;
        var y = c[n - 1];

        for (var k = n - 2; k >= 0; k--)
        {
            y = Math.FusedMultiplyAdd(x, y, c[k]);
        }

        return y;
    }

    public static T DifferenceOfProducts<T>(T a, T b, T c, T d)
        where T : IFloatingPointIeee754<T>
    {
        var cd = c * d;
        var differenceOfProducts = T.FusedMultiplyAdd(a, b, -cd);
        var error = T.FusedMultiplyAdd(-c, d, cd);
        return differenceOfProducts + error;
    }
}
