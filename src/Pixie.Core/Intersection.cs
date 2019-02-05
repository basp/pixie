namespace Pixie.Core
{
    public struct Intersection
    {
        // NOTE: This eps is pretty influential.
        // We need it to be low for tests but in conjunction with planes
        // this value will give lots of nice floating point error 
        // visualizations.
        // TODO: Look into this.
        public const float Epsilon = 0.00001f;

        public readonly float T;

        public readonly IShape Object;

        public Intersection(float t, IShape shape)
        {
            this.T = t;
            this.Object = shape;
        }

        public Computations PrepareComputations(Ray r)
        {
            var t = this.T;
            var obj = this.Object;
            var point = r.Position(t);
            var eyev = -r.Direction;
            var normalv = obj.NormalAt(point);
            var inside = false;

            if (normalv.Dot(eyev) < 0)
            {
                inside = true;
                normalv = -normalv;
            }

            var overPoint = point + normalv * Epsilon; 

            return new Computations
            {
                T = t,
                Object = obj,
                Point = point,
                OverPoint = overPoint,
                Eyev = eyev,
                Normalv = normalv,
                Inside = inside,
            };
        }
    }
}