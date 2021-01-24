namespace Pixie
{
    using Linie;

    public class HammersleySampler : Sampler
    {
        public HammersleySampler(int numberOfSamples, int numberOfSets = 83)
            : base(numberOfSamples, numberOfSets)
        {
        }

        protected override void InitializeSamples()
        {
            for (var p = 0; p < this.numberOfSets; p++)
            {
                for (var j = 0; j < this.numberOfSamples; j++)
                {
                    var x = j / this.numberOfSamples;
                    var y = Phi(j);
                    var sp = new Point2(x, y);
                    this.samples.Add(sp);
                }
            }
        }

        private static double Phi(int j)
        {
            var x = 0.0;
            var f = 0.5;

            while (j > 0)
            {
                x += f * (j % 2);
                j /= 2;
                f *= 0.5;
            }

            return x;
        }
    }
}