namespace Pixie.Core
{
    using System;

    public class RandomSequence : ISequence
    {
        private readonly Random rng = new Random();

        public double Next() => rng.NextDouble();
    }
}