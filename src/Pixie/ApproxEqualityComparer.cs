namespace Pixie
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Base class for approximate equality comparers.
    /// </summary>
    internal abstract class ApproxEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly double epsilon;

        protected ApproxEqualityComparer(double epsilon = 0.000001)
        {
            this.epsilon = epsilon;
        }

        public abstract bool Equals(T x, T y);

        public abstract int GetHashCode(T obj);

        protected bool ApproxEqual(double v1, double v2) =>
            Math.Abs(v1 - v2) < this.epsilon;
    }
}