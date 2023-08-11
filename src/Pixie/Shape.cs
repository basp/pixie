namespace Pixie;

public abstract class Shape
{
    public abstract Vector4 GetNormalAt(Vector4 pWorld);
    
    public abstract IEnumerable<Intersection> Intersect(Ray ray);
}