// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;
    using Linie;

    /// <summary>
    /// Blends two patterns together using a blender function.
    /// </summary>
    public class BlendedPattern : Pattern
    {
        private static Func<Color, Color, Color> DefaultBlender =
            (c1, c2) => (c1 + c2) * 0.5;

        private readonly Func<Color, Color, Color> blender =
            DefaultBlender;

        public BlendedPattern(Pattern a, Pattern b)
        {
            this.A = a;
            this.B = b;
        }

        public BlendedPattern(
            Pattern a,
            Pattern b,
            Func<Color, Color, Color> blender)
        {
            this.A = a;
            this.B = b;
            this.blender = blender;
        }

        public Pattern A { get; set; }

        public Pattern B { get; set; }

        public override Color GetColor(Vector4 point)
        {
            var ca = this.A.GetColor(this.A.Inverse * point);
            var cb = this.B.GetColor(this.B.Inverse * point);
            return this.blender(ca, cb);
        }
    }
}
