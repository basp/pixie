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

        public Color ReflectedColor(Computations comps, int remaining = 5)
        {
            if (remaining <= 0)
            {
                return Color.Black;
            }

            if (comps.Object.Material.Reflective == 0)
            {
                return Color.Black;
            }

            var reflectRay = new Ray(comps.OverPoint, comps.Reflectv);
            var color = this.ColorAt(reflectRay, remaining - 1);
            return color * comps.Object.Material.Reflective;
        }

        public Color RefractedColor(Computations comps, int remaining = 5)
        {
            if (remaining <= 0)
            {
                return Color.Black;
            }

            if (comps.Object.Material.Transparency == 0)
            {
                return Color.Black;
            }

            var nRatio = comps.N1 / comps.N2;
            var cosi = comps.Eyev.Dot(comps.Normalv);
            var sin2t = nRatio * nRatio * (1 - cosi * cosi);

            if (sin2t > 1)
            {
                return Color.Black;
            }

            var cost = Math.Sqrt(1.0 - sin2t);
            var direction = comps.Normalv * (nRatio * cosi - cost) - comps.Eyev * nRatio;
            var refractRay = new Ray(comps.UnderPoint, direction);

            return this.ColorAt(refractRay, remaining - 1) *
                comps.Object.Material.Transparency;
        }

        public Color Shade(Computations comps, int remaining = 5)
        {
            Color res = Color.Black;
            foreach (var light in this.Lights)
            {
                var shadow = this.IsShadowed(comps.OverPoint, light);

                res += comps.Object.Material.Li(
                    comps.Object,
                    light,
                    comps.OverPoint,
                    comps.Eyev,
                    comps.Normalv,
                    shadow);

                res += this.ReflectedColor(comps, remaining);
                res += this.RefractedColor(comps, remaining);
            }

            return res;
        }

        public Color ColorAt(Ray ray, int remaining = 5)
        {
            var xs = this.Intersect(ray);
            if (xs.TryGetHit(out var i))
            {
                var comps = i.PrepareComputations(ray, xs);
                return this.Shade(comps, remaining);
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