namespace Pixie.Tests;

public class RayTests
{
    [Fact]
    public void TestConstructor()
    {
        var origin = new Vector4(1, 2, 3, 1);
        var direction = new Vector4(4, 5, 6, 0);
        var r = new Ray(origin, direction);
        Assert.Equal(origin, r.Origin);
        Assert.Equal(direction, r.Direction);
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

        var o = new Vector4(2, 3, 4, 1);
        var d = new Vector4(1, 0, 0, 0);
        var r = new Ray(o, d);
        foreach (var (t, want) in tests)
        {
            var p = r.GetPointAt(t);
            Assert.Equal(want, p);
        }
    }
}