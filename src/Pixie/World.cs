using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

[assembly: InternalsVisibleTo("Pixie.Tests")]
namespace Pixie
{
    public class World
    {
        public IList<ILight> Lights { get; set; } = new List<ILight>();

        public IList<Shape> Objects { get; set; } = new List<Shape>();

        /// <summary>
        /// Intersect a ray with all objects in the world.
        /// </summary>
        public IntersectionList Intersect(Ray ray) =>
            this.Intersect(ray, obj => true);

        /// <summary>
        /// Intersect with a predicate to select a subset 
        /// of shapes from the world.
        /// </summary>
        public IntersectionList Intersect(Ray ray, Func<Shape, bool> predicate)
        {
            Interlocked.Increment(ref Stats.Tests);
            var xs = this.Objects
                .Where(predicate)
                .SelectMany(x => x.Intersect(ray))
                .ToArray();

            return IntersectionList.Create(xs);
        }

        // https://graphicscompendium.com/raytracing/11-fresnel-beer
        // 
        // r  // reflection vector
        // pt // intersection point

        // local_color := blinn_phong(n, l, v) // or cook_torrance
        // reflection_color := raytrace(pt + r * epsilon, r)
        // transmission_color := raytrace(pt + t * epsilon, t)

        // fresnel_reflectance := schlicks_approximation(ior, n, v)
        // local_contribution = (1 - finish.filter) * (1 - finish.reflection)
        // reflection_contribution = (1 - finish.filter) * (finish.reflection) + (finish.filter) * (fresnel_reflectance)
        // transmission_contribution = (finish.filter) * (1 - fresnel_reflectance)

        // total_color :=
        //     local_contribution * local_color +
        //     reflection_contribution * reflection_color +
        //     transmission_contribution * transmission_color
        //
        public Color Render(Interaction si, int remaining)
        {
            Color res = Color.Black;

            foreach (var light in this.Lights)
            {
                var intensity = light.GetIntensity(si.OverPoint, this);

                var surface = si.Object.Material.Li(
                    si.Object,
                    light,
                    si.OverPoint,
                    si.Eyev,
                    si.Normalv,
                    intensity);

                var reflected = this.GetReflectedColor(si, remaining);
                var refracted = this.GetRefractedColor(si, remaining);

                var material = si.Object.Material;
                if (material.Reflective > 0 && material.Transparency > 0)
                {
                    var reflectance = si.SchlicksApproximation();
                    res += surface +
                        reflected * reflectance +
                        refracted * (1 - reflectance);
                }
                else
                {
                    res += surface + reflected + refracted;
                }
            }

            return res;
        }

        const int DefaultRecursiveDepth = 5;

        public Color Trace(Ray ray, int remaining = DefaultRecursiveDepth)
        {
            var xs = this.Intersect(ray);
            if (xs.TryGetHit(out var i))
            {
                var comps = i.Precompute(ray, xs);
                return this.Render(comps, remaining);
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

        internal Color GetReflectedColor(Interaction si, int remaining)
        {
            if (remaining <= 0)
            {
                return Color.Black;
            }

            if (si.Object.Material.Reflective == 0)
            {
                return Color.Black;
            }

            Interlocked.Increment(ref Stats.SecondaryRays);
            var reflectRay = new Ray(si.OverPoint, si.Reflectv);
            var color = this.Trace(reflectRay, remaining - 1);
            return color * si.Object.Material.Reflective;
        }

        internal Color GetRefractedColor(Interaction si, int remaining)
        {
            if (remaining <= 0)
            {
                return Color.Black;
            }

            if (si.Object.Material.Transparency == 0)
            {
                return Color.Black;
            }

            var nRatio = si.N1 / si.N2;
            var cosi = si.Eyev.Dot(si.Normalv);
            var sin2t = nRatio * nRatio * (1 - cosi * cosi);

            if (sin2t > 1)
            {
                return Color.Black;
            }

            Interlocked.Increment(ref Stats.SecondaryRays);
            var cost = Math.Sqrt(1.0 - sin2t);
            var direction = si.Normalv * (nRatio * cosi - cost) - si.Eyev * nRatio;
            var refractRay = new Ray(si.UnderPoint, direction);
            return this.Trace(refractRay, remaining - 1) *
                si.Object.Material.Transparency;
        }
    }
}