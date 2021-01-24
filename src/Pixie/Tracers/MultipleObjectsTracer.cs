using Linie;

namespace Pixie
{
    public class MultipleObjectsTracer : Tracer
    {
        public MultipleObjectsTracer(World world) : base(world)
        {
        }

        public override Color Trace(Ray4 ray)
        {
            var ix = this.World.Intersect(ray);
            if(ix.TryGetHit(out var hit))
            {
                return hit.Object.Color;
            }

            return new Color(0);
        }
    }
}