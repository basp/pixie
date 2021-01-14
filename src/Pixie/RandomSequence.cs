// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;

    public class RandomSequence : ISequence
    {
        private readonly Random rng = new Random();

        public double Next() => rng.NextDouble();
    }
}
