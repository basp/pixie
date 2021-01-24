namespace Pixie
{
    using Linie;

    public class RandomSampler : Sampler
    {
        public RandomSampler(int numberOfSamples, int numberOfSets = 83)
            : base(numberOfSamples, numberOfSets)
        {
        }

        protected override void InitializeSamples()
        {
            for (var p = 0; p < this.numberOfSets; p++)
            {
                for (var q = 0; q < this.numberOfSamples; q++)
                {
                    var sx = this.Random.NextDouble();
                    var sy = this.Random.NextDouble();
                    var sp = new Point2(sx, sy);
                    this.samples.Add(sp);
                }
            }
        }
    }
}