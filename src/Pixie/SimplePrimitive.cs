namespace Pixie;

public class SimplePrimitive : Primitive
{
    private readonly Shape shape;
    
    public SimplePrimitive(Shape shape)
        : this(shape, Pixie.Material.Default)
    {
    }

    public SimplePrimitive(Shape shape, Material material)
    {
        this.shape = shape;
        this.Material = material;
        this.Transform = new Transform();
    }

    public Material Material { get; set; }

    public override Vector4 GetNormalAt(Vector4 p)
    {
        var objPoint = Vector4.Transform(p, this.Transform.Inverse);
        var objNormal = this.shape.GetNormalAt(objPoint);
        var n = Vector4.Transform(
            objNormal,
            Matrix4x4.Transpose(this.Transform.Inverse));
        n.W = 0;
        return Vector4.Normalize(n);
    }

    public override Option<Intersection> Intersect(Ray ray)
    {
        ray = Ray.Transform(ray, this.Transform.Inverse);
        return this.shape
            .Intersect(ray)
            .Map(x => new Intersection(x.T, this.Material));
    }
}