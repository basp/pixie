namespace Pixie.Core
{
    public struct Ray
    {
        public readonly Double4 Origin;
        public readonly Double4 Direction;

        public Ray(Double4 origin, Double4 direction)
        {
            this.Origin = origin;
            this.Direction = direction;
        }

        public Double4 Position(double t) =>
            this.Origin + this.Direction * t;
    }
}