using System.Drawing;

namespace Pixie.Tests;

public class VectorMathTests
{
    [Fact]
    public void MatrixMatrixMultiplication()
    {
        var A = new Matrix4x4(
            1, 2, 3, 4,
            5, 6, 7, 8,
            9, 8, 7, 6,
            5, 4, 3, 2);
        var B = new Matrix4x4(
            -2, 1, 2, 3,
            3, 2, 1, -1,
            4, 3, 6, 5,
            1, 2, 7, 8);
        var want = new Matrix4x4(
            20, 22, 50, 48,
            44, 54, 114, 108,
            40, 58, 110, 102,
            16, 26, 46, 42);
        var ans = A * B;
        Assert.Equal(want, ans);
    }

    [Fact]
    public void TupleMatrixMultiplication()
    {
        var A = new Matrix4x4(
            1, 2, 3, 4,
            2, 4, 4, 2,
            8, 6, 4, 1,
            0, 0, 0, 1);
        var b = new Vector4(1, 2, 3, 1);
        var want = new Vector4(18, 24, 33, 1);
        // In System.Numerics, there is no way to perform a <i>raw</i> vector
        // matrix multiplication. For example, you cannot just say <c>u * v</c>
        // or <c>v * u</c>. This has to be done via the <c>Transform</c> method.
        // If we want to compare results with reference material in the book we
        // can somewhat "cheat" by transposing the matrix before calling the
        // <c>Transform</c> method. Remember that in System.Numerics, matrices
        // are row-oriented and vector transformations are in the form
        // <c>b*A</c>.
        var ans = Vector4.Transform(
            b,
            Matrix4x4.Transpose(A));
        Assert.Equal(want, ans);
    }

    [Fact]
    public void MatrixTranspose()
    {
        var A = new Matrix4x4(
            0, 9, 3, 0,
            9, 8, 0, 8,
            1, 8, 5, 3,
            0, 0, 5, 8);
        var want = new Matrix4x4(
            0, 9, 1, 0,
            9, 8, 8, 0,
            3, 0, 5, 5,
            0, 8, 3, 8);
        var ans = Matrix4x4.Transpose(A);
        Assert.Equal(want, ans);
    }

    [Fact]
    public void MatrixInverse()
    {
        var A = new Matrix4x4(
            -5, 2, 6, -8,
            1, -5, 1, 8,
            7, 7, -6, -7,
            1, -3, 7, 4);
        var want = new Matrix4x4(
            0.21805f, 0.45113f, 0.24060f, -0.04511f,
            -0.80827f, -1.45677f, -0.44361f, 0.52068f,
            -0.07895f, -0.22368f, -0.05263f, 0.19737f,
            -0.52256f, -0.81391f, -0.30075f, 0.30639f);
        // Turns out that <c>1e-5f</c> is about the minimum
        // tolerance we can expect for the <c>Invert</c> method.
        var cmp = new Matrix4x4Comparer(1e-5f);
        Assert.True(Matrix4x4.Invert(A, out var ans));
        Assert.Equal(want, ans, cmp);
    }

    [Fact]
    public void AnotherMatrixInverse()
    {
        var A = new Matrix4x4(
            8, -5, 9, 2,
            7, 5, 6, 1,
            -6, 0, 9, 6,
            -3, 0, -9, -4);
        var want = new Matrix4x4(
            -0.15385f, -0.15385f, -0.28205f, -0.53846f,
            -0.07692f, 0.12308f, 0.02564f, 0.03077f,
            0.35897f, 0.35897f, 0.43590f, 0.92308f,
            -0.69231f, -0.69231f, -0.76923f, -1.92308f);
        var cmp = new Matrix4x4Comparer(1e-5f);
        Assert.True(Matrix4x4.Invert(A, out var ans));
        Assert.Equal(want, ans, cmp);
    }

    [Fact]
    public void YetAnotherMatrixInverse()
    {
        var A = new Matrix4x4(
            9, 3, 0, 9,
            -5, -2, -6, -3,
            -4, 9, 6, 4,
            -7, 6, 6, 2);
        var want = new Matrix4x4(
            -0.04074f, -0.07778f, 0.14444f, -0.22222f,
            -0.07778f, 0.03333f, 0.36667f, -0.33333f,
            -0.02901f, -0.14630f, -0.10926f, 0.12963f,
            0.17778f, 0.06667f, -0.26667f, 0.33333f);
        var cmp = new Matrix4x4Comparer(1e-5f);
        Assert.True(Matrix4x4.Invert(A, out var ans));
        Assert.Equal(want, ans, cmp);
    }

    [Fact]
    public void MatrixProductMatrixInverse()
    {
        var A = new Matrix4x4(
            3, -9, 7, 3,
            3, -8, 2, -9,
            -4, 4, 4, 1,
            -6, 5, -1, 1);
        var B = new Matrix4x4(
            3, -9, 7, 3,
            3, -8, 2, -9,
            -4, 4, 4, 1,
            -6, 5, -1, 1);
        var cmp = new Matrix4x4Comparer(1e-5f);
        // C = A * B
        // A = C * inv(B)
        var C = A * B;
        Assert.True(Matrix4x4.Invert(B, out var invB));
        Assert.Equal(A, C * invB, cmp);
    }

    [Fact]
    public void TranslationTransform()
    {
        var T = Matrix4x4.CreateTranslation(5, -3, 2);
        var p = new Vector4(-3, 4, 5, 1);
        var want = new Vector4(2, 1, 7, 1);
        var ans = Vector4.Transform(p, T);
        Assert.Equal(want, ans);
    }

    [Fact]
    public void InverseTranslation()
    {
        var T = Matrix4x4.CreateTranslation(5, -3, 2);
        Assert.True(Matrix4x4.Invert(T, out var Tinv));
        var p = new Vector4(-3, 4, 5, 1);
        var want = new Vector4(-8, 7, 3, 1);
        var ans = Vector4.Transform(p, Tinv);
        Assert.Equal(want, ans);
    }

    [Fact]
    public void VectorTranslation()
    {
        var T = Matrix4x4.CreateTranslation(5, -3, 2);
        var v = new Vector4(-3, 4, 5, 0);
        var ans = Vector4.Transform(v, T);
        // Translation does not affect vectors.
        Assert.Equal(v, ans);
    }

    [Fact]
    public void PointScaling()
    {
        var T = Matrix4x4.CreateScale(2, 3, 4);
        var p = new Vector4(-4, 6, 8, 1);
        var want = new Vector4(-8, 18, 32, 1);
        var ans = Vector4.Transform(p, T);
        Assert.Equal(want, ans);
    }

    [Fact]
    public void VectorScaling()
    {
        var T = Matrix4x4.CreateScale(2, 3, 4);
        var v = new Vector4(-4, 6, 8, 0);
        var want = new Vector4(-8, 18, 32, 0);
        var ans = Vector4.Transform(v, T);
        Assert.Equal(want, ans);
    }

    [Fact]
    public void InverseVectorScaling()
    {
        var T = Matrix4x4.CreateScale(2, 3, 4);
        Assert.True(Matrix4x4.Invert(T, out var Tinv));
        var v = new Vector4(-4, 6, 8, 0);
        var want = new Vector4(-2, 2, 2, 0);
        var ans = Vector4.Transform(v, Tinv);
        Assert.Equal(want, ans);
    }

    [Fact]
    public void ReflectionScaling()
    {
        // Reflection is scaling by a negative value.
        var T = Matrix4x4.CreateScale(-1, 1, 1);
        var p = new Vector4(2, 3, 4, 1);
        var want = new Vector4(-2, 3, 4, 1);
        var ans = Vector4.Transform(p, T);
        Assert.Equal(want, ans);
    }


    private static readonly float Sqrt2 = MathF.Sqrt(2);
    private static readonly float Sqrt2Over2 = VectorMathTests.Sqrt2 / 2;

    private const float PiOver4 = MathF.PI / 4;
    private const float PiOver2 = MathF.PI / 2;

    [Fact]
    public void PointXAxisRotation()
    {
        var p = new Vector4(0, 1, 0, 1);
        var halfQuarter = Matrix4x4.CreateRotationX(VectorMathTests.PiOver4);
        var fullQuarter = Matrix4x4.CreateRotationX(VectorMathTests.PiOver2);
        var cmp = new Vector4Comparer(1e-5f);
        Assert.Equal(
            new Vector4(
                0,
                VectorMathTests.Sqrt2Over2,
                VectorMathTests.Sqrt2Over2,
                1),
            Vector4.Transform(p, halfQuarter),
            cmp);
        Assert.Equal(
            // For a left-handed system, this rotates *into* the screen.
            new Vector4(0, 0, 1, 1),
            Vector4.Transform(p, fullQuarter),
            cmp);
    }

    [Fact]
    public void InversePointXAxisRotation()
    {
        var p = new Vector4(0, 1, 0, 1);
        var halfQuarter = Matrix4x4.CreateRotationX(VectorMathTests.PiOver4);
        Assert.True(Matrix4x4.Invert(halfQuarter, out var inv));
        var want = new Vector4(
            0,
            VectorMathTests.Sqrt2Over2,
            // For a left-handed system, this rotates *away* from the screen.
            -VectorMathTests.Sqrt2Over2,
            1);
        var ans = Vector4.Transform(p, inv);
        var cmp = new Vector4Comparer(1e-5f);
        Assert.Equal(want, ans, cmp);
    }

    [Fact]
    public void PointYAxisRotation()
    {
        var p = new Vector4(0, 0, 1, 1);
        var halfQuarter = Matrix4x4.CreateRotationY(VectorMathTests.PiOver4);
        var fullQuarter = Matrix4x4.CreateRotationY(VectorMathTests.PiOver2);
        var cmp = new Vector4Comparer(1e-5f);
        Assert.Equal(
            new Vector4(
                VectorMathTests.Sqrt2Over2,
                0,
                VectorMathTests.Sqrt2Over2,
                1),
            Vector4.Transform(p, halfQuarter),
            cmp);
        Assert.Equal(
            // Rotating around the y-axis translates the z-coordinate
            // towards the <c>+z</c> around a circle.
            new Vector4(1, 0, 0, 1),
            Vector4.Transform(p, fullQuarter),
            cmp);
    }

    [Fact]
    public void PointZAxisRotation()
    {
        var p = new Vector4(0, 1, 0, 1);
        var halfQuarter = Matrix4x4.CreateRotationZ(VectorMathTests.PiOver4);
        var fullQuarter = Matrix4x4.CreateRotationZ(VectorMathTests.PiOver2);
        var cmp = new Vector4Comparer(1e-5f);
        Assert.Equal(
            new Vector4(
                -VectorMathTests.Sqrt2Over2,
                VectorMathTests.Sqrt2Over2,
                0,
                1),
            Vector4.Transform(p, halfQuarter),
            cmp);
        Assert.Equal(
            new Vector4(-1, 0, 0, 1),
            Vector4.Transform(p, fullQuarter),
            cmp);
    }

    [Fact]
    public void PointShearing()
    {
        var tests = new[]
        {
            new
            {
                T = Transform.CreateShear(1, 0, 0, 0, 0, 0),
                want = new Vector4(5, 3, 4, 1),
            },
            new
            {
                T = Transform.CreateShear(0, 1, 0, 0, 0, 0),
                want = new Vector4(6, 3, 4, 1),
            },
            new
            {
                T = Transform.CreateShear(0, 0, 1, 0, 0, 0),
                want = new Vector4(2, 5, 4, 1),
            },
            new
            {
                T = Transform.CreateShear(0, 0, 0, 1, 0, 0),
                want = new Vector4(2, 7, 4, 1),
            },
            new
            {
                T = Transform.CreateShear(0, 0, 0, 0, 1, 0),
                want = new Vector4(2, 3, 6, 1),
            },
            new
            {
                T = Transform.CreateShear(0, 0, 0, 0, 0, 1),
                want = new Vector4(2, 3, 7, 1),
            },
        };

        var p = new Vector4(2, 3, 4, 1);
        foreach (var @case in tests)
        {
            var ans = Vector4.Transform(p, @case.T);
            Assert.Equal(@case.want, ans);
        }
    }
}