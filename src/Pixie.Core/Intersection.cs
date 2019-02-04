namespace Pixie.Core
{
    public struct Intersection
    {
        public readonly float T;

        public readonly IShape Object;

        public Intersection(float t, IShape shape)
        {
            this.T = t;
            this.Object = shape;
        }
    }
}