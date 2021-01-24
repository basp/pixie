namespace Pixie
{
    using System;
    using Linie;
    using Serilog;

    public class DefaultSampler : Sampler
    {
        public DefaultSampler() : this(1, 1)
        {
        }

        public DefaultSampler(int numberOfSamples, int numberOfSets = 83)
            : base(numberOfSamples, numberOfSets)
        {
        }

        protected override void InitializeSamples()
        {
            var n = (int)Math.Sqrt(this.numberOfSamples);

            Log.Information(
                "{SamplerName} generates {NumberOfSets} sets of {NumberOfSamples} samples (N = {N})",
                nameof(DefaultSampler),
                this.numberOfSets,
                this.numberOfSamples,
                n);

            for (var j = 0; j < this.numberOfSets; j++)
            {
                for (var p = 0; p < n; p++)
                {
                    for (var q = 0; q < n; q++)
                    {
                        var x = (q + 0.5) / n;
                        var y = (p + 0.5) / n;
                        var sp = new Point2(x, y);
                        this.samples.Add(sp);
                    }
                }
            }
        }
    }
}