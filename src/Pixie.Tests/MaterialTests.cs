using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Pixie.Tests;

public class MaterialTests
{
    private readonly Vector4 position = new Vector3(0, 0, 0).AsPosition();
    private readonly Material m = Material.Default;

    [Fact]
    public void EyeBetweenLightAndSurface()
    {
        var eyev = new Vector3(0, 0, -1).AsDirection();
        var normalv = new Vector3(0, 0, -1).AsDirection();
        var light = new PointLight(
            new Vector3(0, 0, -10).AsPosition(),
            new Vector3(1, 1, 1));
        var ans = this.m.GetLighting(
            light,
            this.position,
            eyev,
            normalv);
        var want = new Vector3(1.9f, 1.9f, 1.9f);
        Assert.Equal(want, ans);
    }

    [Fact]
    public void EyeOffsetBy45Degrees()
    {
        var eyev = new Vector3(
                0,
                MathF.Sqrt(2) / 2,
                -MathF.Sqrt(2) / 2)
            .AsDirection();
        var normalv = new Vector3(0, 0, -1)
            .AsDirection();
        var light = new PointLight(
            new Vector3(0, 0, -10).AsPosition(),
            new Vector3(1, 1, 1));
        var ans = this.m.GetLighting(
            light, 
            this.position, 
            eyev, 
            normalv);
        var want = new Vector3(1, 1, 1);
        Assert.Equal(want, ans);
    }

    [Fact]
    public void EyeOppositeSurfaceLightOffsetBy45Degrees()
    {
        var eyev = new Vector3(0, 0, -1)
            .AsDirection();
        var normalv = new Vector3(0, 0, -1)
            .AsDirection();
        var light = new PointLight(
            new Vector3(0, 10, -10).AsPosition(),
            new Vector3(1, 1, 1));
        var ans = this.m.GetLighting(
            light, 
            this.position, 
            eyev, 
            normalv);
        var want = new Vector3(0.7364f, 0.7364f, 0.7364f);
        var cmp = new Vector3Comparer(1e-5f);
        Assert.Equal(want, ans, cmp);
    }

    [Fact]
    public void EyeInPathOfReflectionVector()
    {
        var eyev = new Vector3(0, -MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2)
            .AsDirection();
        var normalv = new Vector3(0, 0, -1)
            .AsDirection();
        var light = new PointLight(
            new Vector3(0, 10, -10).AsPosition(),
            new Vector3(1, 1, 1));
        var ans = this.m.GetLighting(
            light, 
            this.position, 
            eyev, 
            normalv);
        var want = new Vector3(1.6364f, 1.6364f, 1.6364f);
        var cmp = new Vector3Comparer(1e-4f);
        Assert.Equal(want, ans, cmp);
    }

    [Fact]
    public void LightBehindSurface()
    {
        var eyev = new Vector3(0, 0, -1)
            .AsDirection();
        var normalv = new Vector3(0, 0, -1)
            .AsDirection();
        var light = new PointLight(
            new Vector3(0, 0, 10).AsPosition(),
            new Vector3(1, 1, 1));
        var ans = this.m.GetLighting(
            light, 
            this.position, 
            eyev, 
            normalv);
        var want = new Vector3(0.1f, 0.1f, 0.1f);
        Assert.Equal(want, ans);
    }
}