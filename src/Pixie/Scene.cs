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
            this.ProgressMonitorFactory = (rows, cols) => new ProgressMonitor();
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
            get { return this.Camera.ProgressMonitorFactory; }
            set { this.Camera.ProgressMonitorFactory = value; }
        }

        public Canvas Render() => this.Camera.Render(this.SamplerFactory);
    }
}