namespace Pixie.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

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

        public override Bounds3 Bounds()
        {
            throw new NotImplementedException();
        }

        public override IntersectionList LocalIntersect(Ray ray)
        {
            var xs = this.children.SelectMany(x => x.Intersect(ray));
            return IntersectionList.Create(xs.ToArray());
        }

        public override Double4 LocalNormalAt(Double4 point)
        {
            throw new NotImplementedException();
        }

        public void Add(Shape item)
        {
            item.Parent = this;
            this.children.Add(item);
        }

        public void Clear() =>
            this.children.Clear();

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