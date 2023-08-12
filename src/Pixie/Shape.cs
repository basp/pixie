namespace Pixie;

public abstract class Shape
{
    public abstract Vector4 GetNormalAt(Vector4 pWorld);
    
    public abstract Option<Intersection> Intersect(Ray ray);

    public virtual IEnumerable<Intersection> IntersectAll(Ray ray)
    {
        throw new NotImplementedException();
    }
}