namespace Pixie.Core
{
    public interface IShape
    {
        Float4x4 Transform { get; set; }

        Float4x4 Inverse { get; }
        
        Float4 NormalAt(Float4 point);

        IntersectionList Intersect(Ray ray);
    }
}