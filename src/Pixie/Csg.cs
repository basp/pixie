namespace Pixie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public enum Operation
    {
        None,
        Union,
        Intersect,
        Difference,
    }

    public class Csg : Shape
    {
        public Csg(Operation op, Shape left, Shape right)
        {
            this.Operation = op;
            this.Left = left;
            this.Right = right;
            left.Parent = this;
            right.Parent = this;
        }

        public Operation Operation { get; }

        public Shape Left { get; }

        public Shape Right { get; }

        public static bool IntersectionAllowed(
            Operation op,
            bool lhit,
            bool inl,
            bool inr)
        {
            if (op == Operation.Union)
            {
                return (lhit && !inr) || (!lhit && !inl);
            }

            if (op == Operation.Intersect)
            {
                return (lhit && inr) || (!lhit && inl);
            }

            if (op == Operation.Difference)
            {
                return (lhit && !inr) || (!lhit && inl);
            }

            return false;
        }

        public IntersectionList FilterIntersections(IntersectionList xs)
        {
            var inl = false;
            var inr = false;
            var result = new List<Intersection>();

            foreach (var i in xs)
            {
                var lhit = this.Left.Includes(i.Object);
                if (IntersectionAllowed(this.Operation, lhit, inl, inr))
                {
                    result.Add(i);
                }

                if (lhit)
                {
                    inl = !inl;
                }
                else
                {
                    inr = !inr;
                }
            }

            return IntersectionList.Create(result.ToArray());
        }

        public override IntersectionList LocalIntersect(Ray ray)
        {
            var bounds = this.GetBounds();
            if (!bounds.Intersect(ray))
            {
                return IntersectionList.Empty();
            }

            var xs = new List<Intersection>();
            xs.AddRange(this.Left.Intersect(ray));
            xs.AddRange(this.Right.Intersect(ray));
            return this.FilterIntersections(
                IntersectionList.Create(xs.ToArray()));
        }

        public override Vector4 GetLocalNormal(Vector4 point)
        {
            throw new NotImplementedException();
        }

        public override bool Includes(Shape obj) =>
            this.Left.Includes(obj) || this.Right.Includes(obj);

        public override BoundingBox GetBounds()
        {
            var box = BoundingBox.Empty;
            box += this.Left.GetParentSpaceBounds();
            box += this.Right.GetParentSpaceBounds();
            return box;
        }

        public override void Divide(double threshold)
        {
            this.Left.Divide(threshold);
            this.Right.Divide(threshold);
        }
    }
}