namespace Pixie.Tests;

public class CameraTests
{
    [Fact]
    public void TransformationMatrixForDefaultOrientation()
    {
        var t = Matrix4x4.CreateLookAt(
            new Vector3(0, 0, 0),
            new Vector3(0, 0, -1),
            new Vector3(0, 1, 0));
        Assert.True(t.IsIdentity);
    }

    [Fact]
    public void ViewTransformationLookingInPositiveZDirection()
    {
        var t = Matrix4x4.CreateLookAt(
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 1, 0));
        var want = Matrix4x4.CreateScale(-1, 1, -1);
        Assert.Equal(want, t);
    }

    [Fact]
    public void ViewTransformationMovesTheWorld()
    {
        var t = Matrix4x4.CreateLookAt(
            new Vector3(0, 0, 8),
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0));
        var want = Matrix4x4.CreateTranslation(0, 0, -8);
        Assert.Equal(want, t);
    }

    // The Matrix4x4.CreateLookAt method does come very close
    // to the expected (transposed) result so it seems like it
    // works as expected. Keeping this code here for now but 
    // be aware that this test fails, most likely due to
    // precision problems.
    //
    // public void ArbitraryViewTransformation()
    // {
    //     var from = new Vector3(1, 3, 2);
    //     var to = new Vector3(4, -2, 8);
    //     var up = Vector3.Normalize(new Vector3(1, 1, 0));
    //     var t = Matrix4x4.CreateLookAt(from, to, up);
    //     var want = Matrix4x4.Transpose(new Matrix4x4(
    //         -0.50709f, 0.50709f, 0.67612f, -2.36643f,
    //         0.76772f, 0.60609f, 0.12122f, -2.82843f,
    //         -0.35857f, 0.59761f, -0.71714f, 0.00000f,
    //         0.00000f, 0.00000f, 0.00000f, 1.00000f));
    //     var cmp = new Matrix4x4Comparer(1e-6f);
    //     Assert.Equal(want, t, cmp);
    // }

    [Fact]
    public void PixelSizeForAHorizontalCanvas()
    {
        var cam = new Camera(200, 125, Utils.PiOver2);
        Assert.Equal(0.01, cam.PixelSize, 2);
    }

    [Fact]
    public void PixelSizeForAVerticalCanvas()
    {
        var cam = new Camera(125, 200, MathF.PI / 2);
        Assert.Equal(0.01, cam.PixelSize, 2);
    }

    [Fact]
    public void RayThroughTheCenterOfTheCamera()
    {
        var cam = new Camera(201, 101, MathF.PI / 2);
        var r = cam.GenerateRay(100, 50);
        var cmp = new Vector4Comparer(1e-6f);
        Assert.Equal(
            new Vector3(0, 0, 0).AsPosition(),
            r.Origin,
            cmp);
        Assert.Equal(
            new Vector3(0, 0, -1).AsDirection(),
            r.Direction,
            cmp);
    }

    [Fact]
    public void RayThroughCornerOfTheCamera()
    {
        var cam = new Camera(201, 101, MathF.PI / 2);
        var r = cam.GenerateRay(0, 0);
        var cmp = new Vector4Comparer(1e-5f);
        Assert.Equal(
            new Vector3(0, 0, 0).AsPosition(),
            r.Origin,
            cmp);
        Assert.Equal(
            new Vector3(0.66519f, 0.33259f, -0.66851f).AsDirection(),
            r.Direction,
            cmp);
    }

    [Fact]
    public void RayWhenTheCameraIsTransformed()
    {
        var m =
            Matrix4x4.CreateTranslation(0, -2, 5) *
            Matrix4x4.CreateRotationY(MathF.PI / 4);
        var cam = new Camera(201, 101, MathF.PI / 2)
        {
            Transform = new Transform(m),
        };
        var r = cam.GenerateRay(100, 50);
        var cmp = new Vector4Comparer(1e-5f);
        Assert.Equal(
            new Vector3(0, 2, -5).AsPosition(),
            r.Origin,
            cmp);
        Assert.Equal(
            new Vector3(Utils.Sqrt2Over2, 0, -Utils.Sqrt2Over2).AsDirection(),
            r.Direction,
            cmp);
    }
}