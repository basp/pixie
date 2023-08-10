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
        var m = Matrix4x4.CreateTranslation(2, 3, 4);
        var t = new Transform(m);
        var obj = new Primitive(new Sphere())
        {
            Transform = t,
        };

        Assert.Equal(t, obj.Transform);
    }
}