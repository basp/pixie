namespace Pixie.Tests;

public class RayTests
{
    [Fact]
    public void TestConstructor()
    {
        var origin = new Vector3(1, 2, 3);
        var direction = new Vector3(4, 5, 6);
        var r = new Ray(origin, direction);
        Assert.Equal(origin.AsPosition(), r.Origin);
        Assert.Equal(direction.AsDirection(), r.Direction);
    }

    [Fact]
    public void TestGetPointAt()
    {
        var tests = new[]
        {
            (0f, new Vector4(2, 3, 4, 1)),
            (1f, new Vector4(3, 3, 4, 1)),
            (-1f, new Vector4(1, 3, 4, 1)),
            (2.5f, new Vector4(4.5f, 3, 4, 1)),
        };

        var o = new Vector3(2, 3, 4);
        var d = new Vector3(1, 0, 0);
        var r = new Ray(o, d);
        foreach (var (t, want) in tests)
        {
            var p = r.GetPointAt(t);
            Assert.Equal(want, p);
        }
    }

    [Fact]
    public void TranslatingARay()
    {
        var r = new Ray(
            new Vector3(1, 2, 3),
            new Vector3(0, 1, 0));
        var m = Matrix4x4.CreateTranslation(3, 4, 5);
        var r2 = Ray.Transform(r, m);
        Assert.Equal(
            new Vector3(4, 6, 8).AsPosition(), 
            r2.Origin);
        Assert.Equal(
            new Vector3(0, 1, 0).AsDirection(),
            r2.Direction);
    }

    [Fact]
    public void ScalingARay()
    {
        var r = new Ray(
            new Vector3(1, 2, 3),
            new Vector3(0, 1, 0));
        var m = Matrix4x4.CreateScale(2, 3, 4);
        var r2 = Ray.Transform(r, m);
        Assert.Equal(
            new Vector3(2, 6, 12).AsPosition(),
            r2.Origin);
        Assert.Equal(
            new Vector3(0, 3, 0).AsDirection(),
            r2.Direction);

    }
}
