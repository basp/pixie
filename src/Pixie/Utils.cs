namespace Linie;

public static class Utils
{
    public static double InvPi = 1 / Math.PI;

    public static double Inv2Pi = 1 / (2 * Math.PI);

    public static double Inv4Pi = 1 / (4 * Math.PI);

    public static double PiOver2 = Math.PI / 2;

    public static double PiOver4 = Math.PI / 4;

    public static double Sqrt2 = Math.Sqrt(2);

    public static T Radians<T>(T deg)
        where T : IFloatingPointIeee754<T> =>
        (T.Pi / T.CreateChecked(180)) * deg;

    public static T Degrees<T>(T rad)
        where T : IFloatingPointIeee754<T> =>
        (T.CreateChecked(180) / T.Pi) * rad;

    public static T SmoothStep<T>(T x, T a, T b)
        where T : IFloatingPointIeee754<T>
    {
        if (a == b)
        {
            return (x < a) ? T.Zero : T.One;
        }

        var t = T.Clamp(
            (x - a) / (b - a),
            T.Zero,
            T.One);

        return t * t * (T.CreateChecked(3) - T.CreateChecked(2) * t);
    }

    public static double SafeAsin(double x)
    {
#if DEBUG
        if (x is < -1.0001 or > 1.0001)
        {
            throw new ArgumentOutOfRangeException(nameof(x));
        }
#endif
        return Math.Asin(Math.Clamp(x, -1, 1));
    }

    public static double SafeAcos(double x)
    {
#if DEBUG
        if (x is < -1.0001 or > 1.0001)
        {
            throw new ArgumentOutOfRangeException(nameof(x));
        }
#endif
        return Math.Acos(Math.Clamp(x, -1, 1));
    }

    public static double SafeSqrt(double x, double atol = 1e-3)
    {
#if DEBUG
        if (x < -atol)
        {
            throw new ArgumentOutOfRangeException(nameof(x));
        }
#endif
        return Math.Sqrt(Math.Max(x, 0));
    }
}