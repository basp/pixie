namespace Pixie.Core
{
    public interface IShape 
    {
        Intersection[] Intersect(Ray ray);
    }
}