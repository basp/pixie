namespace Pixie.Core
{
    public struct Ray
    {
        public readonly Float4 Origin;
        public readonly Float4 Direction;

        public Ray(Float4 origin, Float4 direction)
        {
            this.Origin = origin;
            this.Direction = direction;
        }

        public Float4 Position(float t) =>
            this.Origin + this.Direction * t;
    }
}