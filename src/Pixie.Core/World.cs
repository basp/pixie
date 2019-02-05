namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class World
    {
        private readonly IList<PointLight> lights = new List<PointLight>();
        
        private readonly IList<IShape> objects = new List<IShape>();

        public IList<PointLight> Lights => this.lights;

        public IList<IShape> Objects => this.objects;

        public IntersectionList Intersect(Ray ray)
        {
            var xs = this.objects.SelectMany(x => x.Intersect(ray));
            return IntersectionList.Create(xs.ToArray());
        }

        public Color Shade(Computations comps)
        {
            return comps.Object.Material.Li(
                this.lights[0], 
                comps.Point, 
                comps.Eyev, 
                comps.Normalv);
        }
    }

    public class DefaultWorld : World
    {
        public DefaultWorld()
        {
            var s1 = new Sphere
            {
                Material = new Material
                {
                    Color = new Color(0.8f, 1.0f, 0.6f),
                    Diffuse = 0.7f,
                    Specular = 0.2f,
                },
            };

            var s2 = new Sphere
            {
                Transform = Transform.Scale(0.5f, 0.5f, 0.5f),
            };

            var light = new PointLight(
                Float4.Point(-10, 10, -10),
                Color.White);

            this.Objects.Add(s1);
            this.Objects.Add(s2);
            this.Lights.Add(light);
        }
    }
}