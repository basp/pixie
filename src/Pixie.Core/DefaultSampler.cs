namespace Pixie.Core
{
    using System.Threading;

    public class DefaultSampler : ISampler
    {
        private readonly World world;
        private readonly Camera camera;

        public DefaultSampler(World world, Camera camera)
        {
            this.world = world;
            this.camera = camera;
        }

        public Color Sample(int x, int y)
        {
            Interlocked.Increment(ref Camera.Stats.PrimaryRays);
            var ray = this.camera.RayForPixel(x, y);
            return world.ColorAt(ray);
        }
    }
}