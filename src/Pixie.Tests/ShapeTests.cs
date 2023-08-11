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

    [Fact]
    public void TheNormalOnASphereAtAPointOnTheXAxis()
    {
        var s = new Sphere();
        var p = new Vector3(1, 0, 0).AsPosition();
        var n = s.GetNormalAt(p);
        var want = new Vector3(1, 0, 0).AsDirection();
        var ans = s.GetNormalAt(p);
        Assert.Equal(want, ans);
    }

    [Fact]
    public void TheNormalOnASphereAtAPointOnTheYAxis()
    {
        var s = new Sphere();
        var n = s.GetNormalAt(new Vector4(1, 0, 0, 1));
        Assert.Equal(new Vector4(1, 0, 0, 0), n);
    }

    [Fact]
    public void TheNormalOnASphereAtAPointOnTheZAxis()
    {
        var s = new Sphere();
        var n = s.GetNormalAt(new Vector4(0, 0, 1, 1));
        Assert.Equal(new Vector4(0, 0, 1, 0), n);
    }

    private static readonly float Sqrt3Over3 = MathF.Sqrt(3) / 3;
    
    [Fact]
    public void TheNormalOnASphereAtANonAxialPoint()
    {
        var s = new Sphere();
        var ans = s.GetNormalAt(
            new Vector4(
                ShapeTests.Sqrt3Over3,
                ShapeTests.Sqrt3Over3,
                ShapeTests.Sqrt3Over3,
                1));
        var want = new Vector4(
            ShapeTests.Sqrt3Over3,
            ShapeTests.Sqrt3Over3,
            ShapeTests.Sqrt3Over3,
            0);
        var cmp = new Vector4Comparer(1e-5f);
        Assert.Equal(want, ans, cmp);
    }

    [Fact]
    public void TheNormalIsANormalizedVector()
    {
        var s = new Sphere();
        var n = s.GetNormalAt(
            new Vector4(
                ShapeTests.Sqrt3Over3,
                ShapeTests.Sqrt3Over3,
                ShapeTests.Sqrt3Over3,
                0));
        var cmp = new Vector4Comparer(1e-5f);
        Assert.Equal(Vector4.Normalize(n), n, cmp);
    }

    [Fact]
    public void TheNormalOnATranslatedSphere()
    {
        var obj = new Primitive(new Sphere())
        {
            Transform = new Transform(
                Matrix4x4.CreateTranslation(0, 1, 0)),
        };
        
        var pWorld = new Vector3(0, 1.70711f, -0.70711f).AsPosition();
        var pObj = Vector4.Transform(pWorld, obj.Transform.Inverse);
        var nObj = pObj - new Vector4(0, 0, 0, 1);
        var nWorld = Vector4.Transform(nObj, Matrix4x4.Transpose(obj.Transform.Inverse));
        nWorld.W = 0;
        nWorld = Vector4.Normalize(nWorld);
        
        var ans = obj.GetNormalAt(new Vector4(0, 1.70711f, -0.70711f, 1));
        
        var want = new Vector4(
            0, 
            0.70711f, 
            -0.70711f,
            0);
        var cmp = new Vector4Comparer(1e-4f);
        Assert.Equal(want, ans, cmp);
    }
}