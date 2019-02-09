namespace Pixie.Core
{
    public struct Bounds3
    {
        public readonly Double4 Min;
        public readonly Double4 Max;

        public Bounds3(Double4 min, Double4 max)
        {
            this.Min = min;
            this.Max = max;
        }
    }
}