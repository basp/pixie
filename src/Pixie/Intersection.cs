namespace Pixie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linsi;

    public struct Intersection : IEquatable<Intersection>
    {
        public const double Epsilon = 0.00001;

        public readonly double T;

        public readonly Shape Object;

        public Intersection(double t, Shape shape)
        {
            this.T = t;
            this.Object = shape;
        }

        public bool Equals(Intersection other) =>
            this.T == other.T &&
            this.Object == other.Object;

        public Interaction Precompute(Ray r) =>
            Precompute(r, IntersectionList.Create(this));

        public Interaction Precompute(Ray r, IntersectionList xs)
        {
            var t = this.T;
            var obj = this.Object;
            var point = r.GetPosition(t);
            var eyev = -r.Direction;
            var normalv = obj.GetNormal(point);
            var inside = false;

            if (normalv.Dot(eyev) < 0)
            {
                inside = true;
                normalv = -normalv;
            }

            var overPoint = point + normalv * Epsilon;
            var underPoint = point - normalv * Epsilon;

            var reflectv = r.Direction.Reflect(normalv);
            this.CalculateRefraction(xs, out var n1, out var n2);

            return new Interaction
            {
                T = t,
                Object = obj,
                Point = point,
                OverPoint = overPoint,
                UnderPoint = underPoint,
                Eyev = eyev,
                Normalv = normalv,
                Reflectv = reflectv,
                Inside = inside,
                N1 = n1,
                N2 = n2,
            };
        }

        private void CalculateRefraction(IntersectionList xs, out double n1, out double n2)
        {
            n1 = 0;
            n2 = 0;
            var containers = new List<Shape>();
            foreach (var i in xs)
            {
                if (i.Equals(this))
                {
                    if (containers.Count == 0)
                    {
                        n1 = 1.0;
                    }
                    else
                    {
                        n1 = containers.Last().Material.RefractiveIndex;
                    }
                }

                if (containers.Contains(i.Object))
                {
                    containers.Remove(i.Object);
                }
                else
                {
                    containers.Add(i.Object);
                }

                if(i.Equals(this))
                {
                    if(containers.Count == 0)
                    {
                        n2 = 1.0;
                    }
                    else
                    {
                        n2 = containers.Last().Material.RefractiveIndex;
                    }

                    break;
                }
            }
        }
    }
}