namespace Pixie.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Group : Shape, IList<Shape>
    {
        private Bounds3 bounds = Bounds3.Infinity;

        private List<Shape> children = new List<Shape>();

        public Shape this[int index]
        {
            get => this.children[index];
            set => this.children[index] = value;
        }

        public int Count => this.children.Count;

        public bool IsReadOnly => false;

        private const double Epsilon = 0.0001;

        private static double Max(double a, double b, double c) =>
            Math.Max(a, Math.Max(b, c));

        private static double Min(double a, double b, double c) =>
            Math.Min(a, Math.Min(b, c));

        private static void CheckAxis(
            double origin,
            double direction,
            double tmin,
            double tmax,
            out double min,
            out double max)
        {
            var tminNum = tmin - origin;
            var tmaxNum = tmax - origin;

            if (Math.Abs(direction) >= Epsilon)
            {
                min = tminNum / direction;
                max = tmaxNum / direction;
            }
            else
            {
                min = tminNum * double.PositiveInfinity;
                max = tmaxNum * double.PositiveInfinity;
            }

            if (min > max)
            {
                var tmp = min;
                min = max;
                max = tmp;
            }
        }

        public bool IntersectBounds(Ray ray)
        {
            var bounds = this.Bounds();

            CheckAxis(
                ray.Origin.X,
                ray.Direction.X,
                bounds.Min.X,
                bounds.Max.X,
                out var xtmin,
                out var xtmax);

            CheckAxis(
                ray.Origin.Y,
                ray.Direction.Y,
                bounds.Min.Y,
                bounds.Max.Y,
                out var ytmin,
                out var ytmax);

            CheckAxis(
                ray.Origin.Z,
                ray.Direction.Z,
                bounds.Min.Z,
                bounds.Max.Z,
                out var ztmin,
                out var ztmax);

            var tmin = Max(xtmin, ytmin, ztmin);
            var tmax = Min(xtmax, ytmax, ztmax);

            if (tmin > tmax)
            {
                return false;
            }

            return true;
        }

        private void UpdateBounds()
        {
            if (this.children.Count == 0)
            {
                this.bounds = Bounds3.Infinity;
            }

            var corners = this.children
                .SelectMany(x => x.Bounds().Corners().Select(c => x.Transform * c))
                .ToList();

            var min = Double4.Point(
                corners.Min(c => c.X),
                corners.Min(c => c.Y),
                corners.Min(c => c.Z));

            var max = Double4.Point(
                corners.Max(c => c.X),
                corners.Max(c => c.Y),
                corners.Max(c => c.Z));

            this.bounds = new Bounds3(min, max);
        }

        public override Bounds3 Bounds() => this.bounds;

        public override IntersectionList LocalIntersect(Ray ray)
        {
            if (!this.IntersectBounds(ray))
            {
                return IntersectionList.Empty();
            }

            var xs = this.children.SelectMany(x => x.Intersect(ray));
            return IntersectionList.Create(xs.ToArray());
        }

        public override Double4 LocalNormalAt(Double4 point)
        {
            throw new NotImplementedException();
        }

        public override bool Includes(Shape obj)
        {
            foreach (var c in this.children)
            {
                if (c.Includes(obj))
                {
                    return true;
                }
            }

            return false;
        }

        public void Add(Shape item)
        {
            item.Parent = this;
            this.children.Add(item);
            this.UpdateBounds();
        }

        public void Clear()
        {
            this.children.Clear();
            this.UpdateBounds();
        }

        public bool Contains(Shape item) =>
            this.children.Contains(item);

        public void CopyTo(Shape[] array, int arrayIndex) =>
            this.children.CopyTo(array, arrayIndex);

        public IEnumerator<Shape> GetEnumerator() =>
            this.children.GetEnumerator();

        public int IndexOf(Shape item) =>
            this.children.IndexOf(item);

        public void Insert(int index, Shape item)
        {
            item.Parent = this;
            this.children.Insert(index, item);
            this.UpdateBounds();
        }

        public bool Remove(Shape item)
        {
            var removed = this.children.Remove(item);
            if (removed)
            {
                item.Parent = null;
                this.UpdateBounds();
            }

            return removed;
        }

        public void RemoveAt(int index)
        {
            this.children[index].Parent = null;
            this.children.RemoveAt(index);
            this.UpdateBounds();
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();
    }
}