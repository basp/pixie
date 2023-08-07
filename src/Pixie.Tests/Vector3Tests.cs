namespace Linie.Tests;

public class Vector3Tests
{
    [Fact]
    public void TestCreation()
    {
        var u = Vector3.Create(1.0, 2, 3);

        Assert.Equal(1, u.X);
        Assert.Equal(2, u.Y);
        Assert.Equal(3, u.Z);
    }

    [Fact]
    public void TestIndexing()
    {
        var u = Vector3.Create(1.0, 2, 3);
        
        Assert.Equal(1, u[0]);
        Assert.Equal(2, u[1]);
        Assert.Equal(3, u[2]);

        Assert.Equal(u[0], u.X);
        Assert.Equal(u[1], u.Y);
        Assert.Equal(u[2], u.Z);
    }

    [Fact]
    public void TestEquality()
    {
        var u = Vector3.Create(1.0, 2, 3);
        var v = Vector3.Create(1.0, 2, 3);
        var w = Vector3.Create(2.0, 3, 4);

        Assert.Equal(u, v);
        Assert.Equal(v, u);

        Assert.NotEqual(u, w);
        Assert.NotEqual(v, w);
    }
}