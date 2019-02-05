namespace Pixie.Core
{
    public interface IMaterial
    {
        Color Li(
            PointLight light, 
            Float4 position,
            Float4 eyev,
            Float4 normalv,
            bool shadow);
    }
}