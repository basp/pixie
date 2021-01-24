namespace Pixie
{
    using System.Collections.Generic;
    using System.Linq;
    using Linie;

    public class NRooksSampler : Sampler
    {
        public NRooksSampler(int numberOfSamples, int numberOfSets = 83)
            : base(numberOfSamples, numberOfSets)
        {
        }

        protected override void InitializeSamples()
        {
            for (var p = 0; p < this.numberOfSets; p++)
            {
                var tmp = this.GetSampleSet();
                var xc = tmp.Select(p => p.X).OrderBy(_ => this.Random.Next());
                var yc = tmp.Select(p => p.Y).OrderBy(_ => this.Random.Next());
                var shuffled = xc.Zip(yc, (x, y) => new Point2(x, y));
                this.samples.AddRange(shuffled);
            }
        }

        private IEnumerable<Point2> GetSampleSet()
        {
            for (var j = 0; j < this.numberOfSamples; j++)
            {
                var x = (j + this.Random.NextDouble()) / this.numberOfSamples;
                var y = (j + this.Random.NextDouble()) / this.numberOfSamples;
                yield return new Point2(x, y);
            }
        }
    }
}