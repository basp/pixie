namespace Pixie.Core
{
    using System;

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

        public override Color PatternAt(Double4 point)
        {
            var ca = this.A.PatternAt(point);
            var cb = this.B.PatternAt(point);
            return this.blender(ca, cb);
        }
    }
}