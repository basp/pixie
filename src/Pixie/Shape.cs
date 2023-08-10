namespace Pixie;

public abstract class Shape
{
    public abstract Vector4 GetNormalAt(Vector4 p);
    
    public abstract IEnumerable<float> Intersect(Ray ray);
}