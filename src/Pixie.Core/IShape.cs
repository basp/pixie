namespace Pixie.Core
{
    public interface IShape
    {
        Float4x4 Transform { get; set; }
        
        IntersectionList Intersect(Ray ray);
    }
}