namespace Pixie;

public class Primitive
{
    public Primitive(Shape shape)
    {
        this.Shape = shape;
        this.Material = Option.None<Material>();
        this.Transform = new Transform();
    }
    
    public Primitive(Shape shape, Material material)
    {
        this.Shape = shape;
        this.Material = Option.Some(material);
        this.Transform = new Transform();
    }
    
    public Shape Shape { get; }
    
    public Transform Transform { get; set;  }
    
    public Option<Material> Material { get; set; }
    
    public IEnumerable<Intersection> Intersect(Ray ray) =>
        this.Shape
            .Intersect(ray)
            .Select(t => new Intersection(t, this));
}