namespace Pixie
{
    using Linie;

    public class SingleSphereTracer : Tracer
    {
        public SingleSphereTracer(World world) : base(world)
        {
        }

        public override Color Trace(Ray4 ray)
        {
            var xs = this.World.Sphere.Intersect(ray);
            if (xs.TryGetHit(out var hit))
            {
                return new Color(1, 0, 0);
            }

            return new Color(0);
        }
    }
}