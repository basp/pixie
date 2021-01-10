namespace Pixie.Core
{
    using System;

    /// <summary>
    /// Blends two patterns together using a blender function.
    /// </summary>
    public class BlendedPattern : Pattern
    {
        private static Func<Color,Color,Color> DefaultBlender =
            (c1, c2) => (c1 + c2) * 0.5;

        private readonly Func<Color,Color,Color> blender =
            DefaultBlender;

        public BlendedPattern(Pattern a, Pattern b)
        {
            this.A = a;
            this.B = b;
        }

        public BlendedPattern(
            Pattern a, 
            Pattern b,
            Func<Color,Color,Color> blender)
        {
            this.A = a;
            this.B = b;
            this.blender = blender;
        }

        public Pattern A { get; set; }

        public Pattern B { get; set; }

        public override Color PatternAt(Vector4 point)
        {
            var ca = this.A.PatternAt(this.A.Inverse * point);
            var cb = this.B.PatternAt(this.B.Inverse * point);
            return this.blender(ca, cb);
        }
    }
}