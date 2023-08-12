using Optional.Unsafe;

namespace Pixie.Tests;

public class PrimitiveTests
{
    [Fact]
    public void HasDefaultMaterial()
    {
        var obj = new SimplePrimitive(new Sphere());
        Assert.Equal(
            Pixie.Material.Default,
            obj.Material);
    }

    [Fact]
    public void HasAssignedMaterial()
    {
        var mat = new Material();
        var obj = new SimplePrimitive(new Sphere(), mat);
        Assert.Equal(mat, obj.Material);
        Assert.NotEqual(mat, Material.Default);
    }
}