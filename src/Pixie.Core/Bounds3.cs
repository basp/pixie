namespace Pixie.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public struct Bounds3
    {
        public readonly Double4 Min;
        public readonly Double4 Max;

        public Bounds3(Double4 min, Double4 max)
        {
            this.Min = min;
            this.Max = max;
        }

        public IEnumerable<Double4> Corners()
        {
            return new List<Double4>()
            {
                this.Min,
                Double4.Point(this.Min.X, this.Min.Y, this.Max.Z),
                Double4.Point(this.Min.X, this.Max.Y, this.Min.Z),
                Double4.Point(this.Min.X, this.Max.Y, this.Max.Z),
                Double4.Point(this.Max.X, this.Min.Y, this.Min.Z),
                Double4.Point(this.Max.X, this.Min.Y, this.Max.Z),
                Double4.Point(this.Max.X, this.Max.Y, this.Min.Z),
                this.Max,
            };
        }
    }
}