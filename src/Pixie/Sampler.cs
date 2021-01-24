namespace Pixie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linie;

    public abstract class Sampler
    {
        protected int numberOfSamples;

        protected int numberOfSets;

        protected List<Point2> samples;

        protected List<int> shuffledIndices;

        // the current number of sample points used
        protected ulong count = 0;

        protected int jump = 0;

        protected Sampler(int numberOfSamples, int numberOfSets = 83)
        {
            this.numberOfSamples = numberOfSamples;
            this.numberOfSets = numberOfSets;

            var size = this.numberOfSamples * this.numberOfSets;
            this.samples = new List<Point2>(size);
            this.shuffledIndices = new List<int>(size);
            this.InitializeShuffledIndices();
            this.InitializeSamples();
        }

        public Random Random { get; set; } = new Random();

        protected abstract void InitializeSamples();

        public Point2 SampleUnitSquare()
        {
            if (this.count % (ulong)this.numberOfSamples == 0)
            {
                this.jump = (this.Random.Next() % this.numberOfSets) * this.numberOfSamples;
            }

            var si = (int)((ulong)this.jump + count++ % (ulong)this.numberOfSamples);
            var i = this.jump + this.shuffledIndices[si];
            return this.samples[i];
        }

        // setup numberOfSets shuffled index arrays of length numberOfSamples
        // these are used to indirect the sampling to get a better pattern
        private void InitializeShuffledIndices()
        {
            for (var p = 0; p < this.numberOfSets; p++)
            {
                var indices = Enumerable
                    .Range(0, this.numberOfSamples)
                    .OrderBy(_ => this.Random.Next());

                this.shuffledIndices.AddRange(indices);
            }
        }
    }
}