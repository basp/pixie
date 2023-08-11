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
        Assert.Equal(4.0, xs[0].T);
        Assert.Equal(6.0, xs[1].T);
    }

    [Fact]
    public void IntersectSphereAtATangent()
    {
        var r = new Ray(
            new Vector3(0, 1, -5),
            new Vector3(0, 0, 1));
        var s = new Sphere();
        var xs = s.Intersect(r).ToList();
        Assert.Single(xs);
        Assert.Equal(5.0, xs[0].T);
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
        Assert.Equal(-1, xs[0].T);
        Assert.Equal(1, xs[1].T);
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
        Assert.Equal(-6.0, xs[0].T);
        Assert.Equal(-4.0, xs[1].T);
    }

    record Foo : Material
    {
    }

    [Fact]
    public void IntersectPrimitive()
    {
        var r = new Ray(
            new Vector3(0, 0, 5),
            new Vector3(0, 0, 1));
        var mat = new Foo();
        var obj = new Primitive(new Sphere(), mat);
        var xs = obj.Intersect(r).ToList();
        Assert.Equal(2, xs.Count);
        Assert.Equal(mat, xs[0].Material.ValueOrFailure());
        Assert.Equal(mat, xs[1].Material.ValueOrFailure());
    }

    [Fact]
    public void AllIntersectionsHavePositiveT()
    {
        var obj = new Primitive(new Sphere());
        var i1 = new Intersection(1);
        var i2 = new Intersection(2);
        var xs = new[] { i1, i2 };
        var i = xs.GetHit();
        Assert.True(i.HasValue);
        Assert.Equal(i1, i.ValueOrFailure());
    }

    [Fact]
    public void SomeIntersectionsHaveNegativeT()
    {
        var obj = new Primitive(new Sphere());
        var i1 = new Intersection(-1);
        var i2 = new Intersection(1);
        var xs = new[] { i2, i1 };
        var i = xs.GetHit();
        Assert.True(i.HasValue);
        Assert.Equal(i2, i.ValueOrFailure());
    }

    [Fact]
    public void AllIntersectionsHaveNegativeT()
    {
        var obj = new Primitive(new Sphere());
        var i1 = new Intersection(-2);
        var i2 = new Intersection(-1);
        var xs = new[] { i2, i1 };
        var i = xs.GetHit();
        Assert.False(i.HasValue);
    }

    [Fact]
    public void TheHitIsAlwaysTheLowestNonNegativeIntersection()
    {
        var obj = new Primitive(new Sphere());
        var i1 = new Intersection(5);
        var i2 = new Intersection(7);
        var i3 = new Intersection(-3);
        var i4 = new Intersection(2);
        var xs = new[] { i1, i2, i3, i4 };
        var i = xs.GetHit();
        Assert.True(i.HasValue);
        Assert.Equal(i4, i.ValueOrFailure());
    }

    [Fact]
    public void IntersectScaledSphereWithRay()
    {
        var ray = new Ray(
            new Vector3(0, 0, -5),
            new Vector3(0, 0, 1));
        var t = new Transform(Matrix4x4.CreateScale(2, 2, 2));
        var obj = new Primitive(new Sphere())
        {
            Transform = t,
        };
        var xs = obj.Intersect(ray).ToList();
        Assert.Equal(2, xs.Count);
        Assert.Equal(3, xs[0].T);
        Assert.Equal(7, xs[1].T);
    }
}