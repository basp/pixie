namespace Pixie;

public abstract class Primitive
{
    public Transform Transform { get; init; }

    public abstract Vector4 GetNormalAt(Vector4 point);
    
    public abstract Option<Intersection> Intersect(Ray ray);
}