namespace Pixie
{
    using Linie;

    public abstract class Tracer : ITracer
    {
        public Tracer(World world)
        {
            this.World = world;
        }

        protected World World { get; private set; }

        public virtual Color Trace(Ray4 ray) => new Color(0);
    }
}