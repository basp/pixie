// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class IntersectionList : IReadOnlyList<Intersection>
    {
        public static IntersectionList Empty =>
            new IntersectionList(new Intersection[0]);

        private readonly IList<Intersection> xs;

        private IntersectionList(IEnumerable<Intersection> xs)
        {
            this.xs = xs.ToList();
        }

        public Intersection this[int index]
        {
            get => this.xs[index];
        }

        public int Count => this.xs.Count;

        public static IntersectionList Create(params Intersection[] intersections)
        {
            var xs = intersections.OrderBy(x => x.T);
            return new IntersectionList(xs);
        }

        public bool TryGetHit(out Intersection hit)
        {
            hit = xs.FirstOrDefault(x => x.T >= 0);
            // Intersection is a struct so we can't really inspect
            // that for null. However, any valid intersection should
            // have an object reference so we can check that instead.
            return hit.Object != null;
        }

        public IEnumerator<Intersection> GetEnumerator() =>
            this.xs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();
    }
}
