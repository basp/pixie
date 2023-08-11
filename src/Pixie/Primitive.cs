namespace Pixie;

public class Primitive
{
    public Primitive(Shape shape)
        : this(shape, Pixie.Material.Default)
    {
    }

    public Primitive(Shape shape, Material material)
    {
        this.Shape = shape;
        this.Material = Option.Some(material);
        this.Transform = new Transform();
    }

    public Shape Shape { get; }

    public Transform Transform { get; set; }

    public Option<Material> Material { get; set; }

    public Vector4 GetNormalAt(Vector4 p)
    {
        var objPoint = Vector4.Transform(p, this.Transform.Inverse);
        var objNormal = this.Shape.GetNormalAt(objPoint);
        var n = Vector4.Transform(
            objNormal,
            Matrix4x4.Transpose(this.Transform.Inverse));
        n.W = 0;
        return Vector4.Normalize(n);
    }

    public IEnumerable<Intersection> Intersect(Ray ray)
    {
        ray = Ray.Transform(ray, this.Transform.Inverse);
        return this.Shape
            .Intersect(ray)
            .Select(x => new Intersection(x.T, this.Material));
    }
}