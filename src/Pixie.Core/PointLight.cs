namespace Pixie.Core
{
    public class PointLight
    {
        public readonly Float4 Position;
        public readonly Color Intensity;

        public PointLight(Float4 position, Color intensity)
        {
            this.Position = position;
            this.Intensity = intensity;
        }
    }
}