namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public abstract class ApproxEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly float epsilon;

        public ApproxEqualityComparer(float epsilon = 0.000001f)
        {
            this.epsilon = epsilon;
        }

        public abstract bool Equals(T x, T y);

        public abstract int GetHashCode(T obj);

        protected bool ApproxEqual(float v1, float v2) =>
            Math.Abs(v1 - v2) < this.epsilon;
    }
}