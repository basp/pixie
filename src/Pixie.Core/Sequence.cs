namespace Pixie.Core
{
    public class Sequence : ISequence
    {
        private readonly double[] values;

        private int i = 0;

        public Sequence(params double[] values)
        {
            this.values = values;
        }

        public double Next()
        {
            var v = this.values[this.i % this.values.Length];
            this.i++;
            return v;
        }
    }
}