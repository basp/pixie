// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Linsi
{
    public struct Ray
    {
        public readonly Vector4 Origin;
        
        public readonly Vector4 Direction;

        public Ray(Vector4 origin, Vector4 direction)
        {
            this.Origin = origin;
            this.Direction = direction;
        }

        /// <summary>
        /// Return a position along this ray at distance t.
        /// </summary>
        /// <remarks>
        /// Equivalent to calling <c>GetPosition(double)</c>.
        /// </remarks>
        public Vector4 this[double t]
        {
            get => this.Origin + (t * this.Direction);
        }

        /// <summary>
        /// Return a position along this ray at distance t.
        /// </summary>
        /// <remarks>
        /// Alternative for indexer <c>this[double]</c>.
        /// </remarks>
        public Vector4 GetPosition(double t) => this[t];
    }
}
