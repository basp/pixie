namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Compares two values for approximate equality. The value
    /// of epsilon is the absolute tolerance allowing the equality
    /// test to pass.
    /// </summary>
    /// <remarks>
    /// You don't really need to deal with approximate equality often
    /// except in test cases. Types that need this support a 
    /// static `GetEqualityComparer(double)` factory method to make it 
    /// easier to get an approximate comparer for a particur type.
    /// </remarks>
    internal abstract class ApproxEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly double epsilon;

        public ApproxEqualityComparer(double epsilon = 0.000001)
        {
            this.epsilon = epsilon;
        }

        public abstract bool Equals(T x, T y);

        public abstract int GetHashCode(T obj);

        protected bool ApproxEqual(double v1, double v2) =>
            Math.Abs(v1 - v2) < this.epsilon;
    }
}