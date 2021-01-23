// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Linie;

    public class Group : Shape, IList<Shape>
    {
        private List<Shape> children = new List<Shape>();

        public Shape this[int index]
        {
            get => this.children[index];
            set => this.children[index] = value;
        }

        public int Count => this.children.Count;

        public bool IsReadOnly => false;

        public override BoundingBox GetBounds()
        {
            var box = BoundingBox.Empty;
            foreach (var child in this.children)
            {
                box += child.GetParentSpaceBounds();
            }

            return box;
        }

        public override IntersectionList LocalIntersect(Ray4 ray)
        {
            var bounds = this.GetBounds();
            if (!bounds.Intersect(ray))
            {
                return IntersectionList.Empty();
            }

            var xs = this.children.SelectMany(x => x.Intersect(ray));
            return IntersectionList.Create(xs.ToArray());
        }

        public override Vector4 GetLocalNormal(Vector4 point)
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

        public void Partition(
            out IList<Shape> left,
            out IList<Shape> right)
        {
            left = new List<Shape>();
            right = new List<Shape>();

            this.GetBounds().Split(out var leftBox, out var rightBox);
            var ss = this.children.ToArray();
            foreach (var s in ss)
            {
                var sBounds = s.GetParentSpaceBounds();

                if (leftBox.Contains(sBounds))
                {
                    this.Remove(s);
                    left.Add(s);
                    continue;
                }

                if (rightBox.Contains(sBounds))
                {
                    this.Remove(s);
                    right.Add(s);
                    continue;
                }
            }
        }

        public void Subgroup(params Shape[] shapes)
        {
            var subgroup = new Group();
            foreach (var s in shapes)
            {
                subgroup.Add(s);
            }

            this.Add(subgroup);
        }

        public override void Divide(double threshold)
        {
            if (threshold <= this.Count)
            {
                this.Partition(out var left, out var right);
                if (left.Count > 0)
                {
                    this.Subgroup(left.ToArray());
                }
                if (right.Count > 0)
                {
                    this.Subgroup(right.ToArray());
                }
            }

            foreach (var s in this.children)
            {
                s.Divide(threshold);
            }
        }

        public void Add(Shape item)
        {
            item.Parent = this;
            this.children.Add(item);
        }

        public void Clear()
        {
            this.children.Clear();
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
        }

        public bool Remove(Shape item)
        {
            var removed = this.children.Remove(item);
            if (removed)
            {
                item.Parent = null;
            }

            return removed;
        }

        public void RemoveAt(int index)
        {
            this.children[index].Parent = null;
            this.children.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();
    }
}
