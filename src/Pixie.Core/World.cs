namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class World
    {
        public IList<PointLight> Lights { get; set; } = new List<PointLight>();

        public IList<Shape> Objects { get; set; } = new List<Shape>();

        public IntersectionList Intersect(Ray ray)
        {
            var xs = this.Objects.SelectMany(x => x.Intersect(ray));
            return IntersectionList.Create(xs.ToArray());
        }

        public Color Shade(Computations comps)
        {
            Color res = Color.Black;
            foreach(var light in this.Lights)
            {
                var shadow = IsShadowed(comps.OverPoint, light);
                res += comps.Object.Material.Li(
                    light,
                    comps.OverPoint,
                    comps.Eyev,
                    comps.Normalv,
                    shadow);
            }

            return res;
        }

        public Color ColorAt(Ray ray)
        {
            var xs = this.Intersect(ray);
            if (xs.TryGetHit(out var i))
            {
                var comps = i.PrepareComputations(ray);
                return this.Shade(comps);
            }

            return Color.Black;
        }

        public bool IsShadowed(Double4 point, PointLight light)
        {
            var v = light.Position - point;
            var distance = v.Magnitude();
            var direction = v.Normalize();
            var r = new Ray(point, direction);
            var xs = this.Intersect(r);
            if (xs.TryGetHit(out var i))
            {
                return i.T < distance;
            }

            return false;
        }
    }
}