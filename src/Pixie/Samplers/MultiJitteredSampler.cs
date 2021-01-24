namespace Pixie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linie;
    using Serilog;

    public class MultiJitteredSampler : Sampler
    {
        public MultiJitteredSampler(int numberOfSamples, int numberOfSets = 83)
            : base(numberOfSamples, numberOfSets)
        {
        }

        protected override void InitializeSamples()
        {
            var n = (int)Math.Sqrt(this.numberOfSamples);
            var subcellWidth = 1.0 / this.numberOfSamples;

            Log.Information(
                "{SamplerName} generates {NumberOfSets} sets of {NumberOfSamples} sample(s) (N = {N}, subcell width = {subcellWidth})",
                nameof(MultiJitteredSampler),
                this.numberOfSets,
                this.numberOfSamples,
                n,
                subcellWidth);

            for (var p = 0; p < this.numberOfSets; p++)
            {
                var set = this.GetSampleSet(n, subcellWidth);
                var xs = set.Select(p => p.X).OrderBy(_ => Random.Next());
                var ys = set.Select(p => p.Y).OrderBy(_ => Random.Next());
                var shuffled = xs.Zip(ys, (x, y) => new Point2(x, y));
                this.samples.AddRange(set);
            }
        }

        private IEnumerable<Point2> GetSampleSet(int n, double sw)
        {
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    var x = (i * n + j) * sw + RandomDouble(0, sw);
                    var y = (j * n + i) * sw + RandomDouble(0, sw);
                    yield return new Point2(x, y);
                }
            }
        }

        private double RandomDouble(double l, double h) =>
            Random.NextDouble() * (h - l) + l;
    }
}