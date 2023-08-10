namespace Pixie.Tests;

public class ShapeTests
{
    [Fact]
    public void DefaultTransformation()
    {
        var obj = new Primitive(new Sphere());
        Assert.Equal(Matrix4x4.Identity, obj.Transform.Matrix);
        Assert.Equal(Matrix4x4.Identity, obj.Transform.Inverse);
    }

    [Fact]
    public void SettingTheTransformation()
    {
        var obj = new Primitive(new Sphere())
        {
        };
    }
}