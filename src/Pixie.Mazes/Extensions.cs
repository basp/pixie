namespace Pixie.Mazes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class Extensions
    {
        public static Cell Sample(this IEnumerable<Cell> self, Random rng)
        {
            var index = rng.Next(self.Count());
            return self.ElementAt(index);
        }
    }
}
