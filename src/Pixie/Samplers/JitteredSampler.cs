namespace Pixie
{
    using System;
    using Linie;
    using Serilog;

    public class JitteredSampler : Sampler
    {
        public JitteredSampler(int numberOfSamples, int numberOfSets = 83)
            : base(numberOfSamples, numberOfSets)
        {
        }

        protected override void InitializeSamples()
        {
            var n = (int)Math.Sqrt(this.numberOfSamples);

            Log.Information(
                "{SamplerName} generates {NumberOfSets} sets of {NumberOfSamples} samples (N = {N})",
                nameof(JitteredSampler),
                this.numberOfSets,
                this.numberOfSamples,
                n);

            for (var p = 0; p < this.numberOfSets; p++)
            {
                for (var j = 0; j < n; j++)
                {
                    for (var k = 0; k < n; k++)
                    {
                        var sx = (k + this.Random.NextDouble()) / n;
                        var sy = (j + this.Random.NextDouble()) / n;
                        var sp = new Point2(sx, sy);
                        this.samples.Add(sp);
                    }
                }
            }
        }
    }
}