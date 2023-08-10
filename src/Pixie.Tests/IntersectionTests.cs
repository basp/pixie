using Optional.Unsafe;

namespace Pixie.Tests;

public class IntersectionTests
{
    [Fact]
    public void IntersectSphereAtTwoPoints()
    {
        var r = new Ray(
            new Vector3(0, 0, -5),
            new Vector3(0, 0, 1));
        var s = new Sphere();
        var xs = s.Intersect(r).ToList();
        Assert.Equal(2, xs.Count);
        Assert.Equal(4.0, xs[0]);
        Assert.Equal(6.0, xs[1]);
    }

    [Fact]
    public void IntersectSphereAtATangent()
    {
        var r = new Ray(
            new Vector3(0, 1, -5),
            new Vector3(0, 0, 1));
        var s = new Sphere();
        var xs = s.Intersect(r).ToList();
        Assert.Equal(2, xs.Count);
        Assert.Equal(5.0, xs[0]);
        Assert.Equal(5.0, xs[1]);
    }

    [Fact]
    public void RayMissesSphere()
    {
        var r = new Ray(
            new Vector3(0, 2, -5),
            new Vector3(0, 0, 1));
        var s = new Sphere();
        var xs = s.Intersect(r);
        Assert.Empty(xs);
    }

    [Fact]
    public void RayFromInsideSphere()
    {
        var r = new Ray(
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1));
        var s = new Sphere();
        var xs = s.Intersect(r).ToList();
        Assert.Equal(2, xs.Count);
        Assert.Equal(-1, xs[0]);
        Assert.Equal(1, xs[1]);
    }

    [Fact]
    public void SphereBehindRay()
    {
        var r = new Ray(
            new Vector3(0, 0, 5),
            new Vector3(0, 0, 1));
        var s = new Sphere();
        var xs = s.Intersect(r).ToList();
        Assert.Equal(2, xs.Count);
        Assert.Equal(-6.0, xs[0]);
        Assert.Equal(-4.0, xs[1]);
    }

    [Fact]
    public void IntersectPrimitive()
    {
        var r = new Ray(
            new Vector3(0, 0, 5),
            new Vector3(0, 0, 1));
        var obj = new Primitive(new Sphere());
        var xs = obj.Intersect(r).ToList();
        Assert.Equal(2, xs.Count);
        Assert.Equal(obj, xs[0].Obj);
        Assert.Equal(obj, xs[1].Obj);
    }

    [Fact]
    public void AllIntersectionsHavePositiveT()
    {
        var obj = new Primitive(new Sphere());
        var i1 = new Intersection(1, obj);
        var i2 = new Intersection(2, obj);
        var xs = new[] { i1, i2 };
        var i = xs.GetHit();
        Assert.True(i.HasValue);
        Assert.Equal(i1, i.ValueOrFailure());
    }

    [Fact]
    public void SomeIntersectionsHaveNegativeT()
    {
        var obj = new Primitive(new Sphere());
        var i1 = new Intersection(-1, obj);
        var i2 = new Intersection(1, obj);
        var xs = new[] { i2, i1 };
        var i = xs.GetHit();
        Assert.True(i.HasValue);
        Assert.Equal(i2, i.ValueOrFailure());
    }

    [Fact]
    public void AllIntersectionsHaveNegativeT()
    {
        var obj = new Primitive(new Sphere());
        var i1 = new Intersection(-2, obj);
        var i2 = new Intersection(-1, obj);
        var xs = new[] { i2, i1 };
        var i = xs.GetHit();
        Assert.False(i.HasValue);
    }

    [Fact]
    public void TheHitIsAlwaysTheLowestNonNegativeIntersection()
    {
        var obj = new Primitive(new Sphere());
        var i1 = new Intersection(5, obj);
        var i2 = new Intersection(7, obj);
        var i3 = new Intersection(-3, obj);
        var i4 = new Intersection(2, obj);
        var xs = new[] { i1, i2, i3, i4 };
        var i = xs.GetHit();
        Assert.True(i.HasValue);
        Assert.Equal(i4, i.ValueOrFailure());
    }
}

