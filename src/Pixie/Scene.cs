namespace Pixie
{
    using System;

    public class Scene
    {
        public Scene(World world, Camera camera)
        {
            this.World = world;
            this.Camera = camera;
            this.SamplerFactory = () => new DefaultSampler(world, camera);
        }

        public Camera Camera { get; }

        public World World { get; }

        public Func<ISampler> SamplerFactory
        {
            get;
            set;
        }

        public Func<int, int, IProgressMonitor> ProgressMonitorFactory
        {
            get;
            set;
        }

        public Canvas Render() => this.Camera.Render(this.SamplerFactory);
    }
}