using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Pixie;

public static class Utils
{
    private static readonly Matrix4x4 nans4x4 = new(
        float.NaN, float.NaN, float.NaN, float.NaN,
        float.NaN, float.NaN, float.NaN, float.NaN,
        float.NaN, float.NaN, float.NaN, float.NaN,
        float.NaN, float.NaN, float.NaN, float.NaN);

    public static Matrix4x4 InvertOrNan(Matrix4x4 m) =>
        Utils.InvertOptional(m)
            .ValueOr(Utils.nans4x4);

    private static Option<Matrix4x4> InvertOptional(Matrix4x4 m) =>
        Matrix4x4.Invert(m, out var inv)
            ? Option.Some(inv)
            : Option.None<Matrix4x4>();
}