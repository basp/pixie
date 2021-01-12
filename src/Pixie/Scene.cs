namespace Pixie
{
    using System;

    public class Scene
    {
        public Scene(World world, Camera camera)
        {
            this.World = world;
            this.Camera = camera;
        }

        public Camera Camera { get; }

        public World World { get; }

        public Canvas Render() =>
            this.Camera.Render(World);
    }
}