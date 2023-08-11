using Optional.Unsafe;

namespace Pixie.Tests;

public class PrimitiveTests
{
    [Fact]
    public void HasDefaultMaterial()
    {
        var obj = new Primitive(new Sphere());
        Assert.Equal(
            Pixie.Material.Default,
            obj.Material.ValueOrFailure());
    }

    [Fact]
    public void HasAssignedMaterial()
    {
        var mat = new Material();
        var obj = new Primitive(new Sphere(), mat);
        Assert.Equal(mat, obj.Material.ValueOrFailure());
        Assert.NotEqual(mat, Material.Default);
    }
}