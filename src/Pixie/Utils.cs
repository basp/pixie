using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Pixie;

public static class Utils
{
    private static readonly Matrix4x4 nans4x4 = new(
        float.NaN, float.NaN, float.NaN, float.NaN,
        float.NaN, float.NaN, float.NaN, float.NaN,
        float.NaN, float.NaN, float.NaN, float.NaN,
        float.NaN, float.NaN, float.NaN, float.NaN);

    public static float Sqrt2Over2 = MathF.Sqrt(2) / 2;
    public static float Sqrt3Over3 = MathF.Sqrt(3) / 3;
    public static float PiOver2 = MathF.PI / 2;
    public static float PiOver4 = MathF.PI / 4;
    
    public static Matrix4x4 InvertOrNan(Matrix4x4 m) =>
        Utils.InvertOptional(m)
            .ValueOr(Utils.nans4x4);

    private static Option<Matrix4x4> InvertOptional(Matrix4x4 m) =>
        Matrix4x4.Invert(m, out var inv)
            ? Option.Some(inv)
            : Option.None<Matrix4x4>();
}