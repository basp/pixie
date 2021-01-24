// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;

    public class Scene
    {
        public Scene(World world, Camera camera)
        {
            this.World = world;
            this.Camera = camera;
            this.SamplerFactory = () => new OldDefaultSampler(world, camera);
            this.ProgressMonitorFactory = camera.ProgressMonitorFactory;
        }

        public Camera Camera { get; }

        public World World { get; }

        public bool IsSuperSampling { get; } = false;

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
