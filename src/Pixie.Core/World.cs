namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public class World
    {
        public IList<ILight> Lights { get; set; } = new List<ILight>();

        public IList<Shape> Objects { get; set; } = new List<Shape>();

        public IntersectionList Intersect(Ray ray) =>
            this.Intersect(ray, obj => true);

        public IntersectionList Intersect(Ray ray, Func<Shape, bool> predicate)
        {
            Interlocked.Increment(ref Stats.Tests);
            var xs = this.Objects
                .Where(predicate)
                .SelectMany(x => x.Intersect(ray));
            return IntersectionList.Create(xs.ToArray());
        }

        public Color ReflectedColor(Computations comps, int remaining)
        {
            if (remaining <= 0)
            {
                return Color.Black;
            }

            if (comps.Object.Material.Reflective == 0)
            {
                return Color.Black;
            }

            Interlocked.Increment(ref Stats.SecondaryRays);
            var reflectRay = new Ray(comps.OverPoint, comps.Reflectv);
            var color = this.ColorAt(reflectRay, remaining - 1);
            return color * comps.Object.Material.Reflective;
        }

        public Color RefractedColor(Computations comps, int remaining)
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

            Interlocked.Increment(ref Stats.SecondaryRays);
            var cost = Math.Sqrt(1.0 - sin2t);
            var direction = comps.Normalv * (nRatio * cosi - cost) - comps.Eyev * nRatio;
            var refractRay = new Ray(comps.UnderPoint, direction);
            return this.ColorAt(refractRay, remaining - 1) *
                comps.Object.Material.Transparency;
        }

        public Color Shade(Computations comps, int remaining)
        {
            Color res = Color.Black;

            foreach (var light in this.Lights)
            {
                var intensity = light.IntensityAt(comps.OverPoint, this);

                var surface = comps.Object.Material.Li(
                    comps.Object,
                    light,
                    comps.OverPoint,
                    comps.Eyev,
                    comps.Normalv,
                    intensity);

                var reflected = this.ReflectedColor(comps, remaining);
                var refracted = this.RefractedColor(comps, remaining);

                var material = comps.Object.Material;
                if (material.Reflective > 0 && material.Transparency > 0)
                {
                    var reflectance = comps.Schlick();
                    res += surface + reflected * reflectance +
                                        refracted * (1 - reflectance);
                }
                else
                {
                    res += surface + reflected + refracted;
                }
            }

            return res;
        }

        const int RecursiveDepth = 5;

        public Color ColorAt(Ray ray, int remaining = RecursiveDepth)
        {
            var xs = this.Intersect(ray);
            if (xs.TryGetHit(out var i))
            {
                var comps = i.Precompute(ray, xs);
                return this.Shade(comps, remaining);
            }

            return Color.Black;
        }

        public bool IsShadowed(Vector4 lightPosition, Vector4 point)
        {
            Interlocked.Increment(ref Stats.ShadowRays);
            var v = lightPosition - point;
            var distance = v.Magnitude();
            var direction = v.Normalize();
            var r = new Ray(point, direction);
            var xs = this.Intersect(r, obj => obj.Shadow);
            if (xs.TryGetHit(out var i))
            {
                if (i.T < distance)
                {
                    return true;
                }
            }

            return false;
        }

        // public double Shadow(Vector4 point, ILightSource source)
        // {
        //     var lights = source.GetLights().ToList();
        //     var n = lights.Count;
        //     var hits = 0;
        //     foreach (var light in source.GetLights())
        //     {
        //         Interlocked.Increment(ref Stats.ShadowRays);
        //         var v = light.Position - point;
        //         var distance = v.Magnitude();
        //         var direction = v.Normalize();
        //         var r = new Ray(point, direction);
        //         var xs = this.Intersect(r);
        //         if (xs.TryGetHit(out var i))
        //         {
        //             if (i.T < distance)
        //             {
        //                 hits += 1;
        //             }
        //         }
        //     }

        //     return (double)hits / n;
        // }

        // public bool IsShadowed(Vector4 point, ILightSource light)
        // {
        //     var shadow = this.Shadow(point, light);
        //     return Math.Abs(shadow) > 0.00001;
        // }
    }
}