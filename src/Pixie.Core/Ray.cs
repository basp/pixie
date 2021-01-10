namespace Pixie.Core
{
    public struct Ray
    {
        public readonly Vector4 Origin;
        public readonly Vector4 Direction;

        public Ray(Vector4 origin, Vector4 direction)
        {
            this.Origin = origin;
            this.Direction = direction;
        }

        public Vector4 Position(double t) =>
            this.Origin + this.Direction * t;
    }
}