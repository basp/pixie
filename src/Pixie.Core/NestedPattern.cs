namespace Pixie.Core
{
    using System;

    public class NestedPattern : Pattern
    {
        public NestedPattern(Pattern op, Pattern a, Pattern b)
        {
            this.Op = op;
            this.A = a;
            this.B = b;
        }

        public Pattern Op { get; set; }

        public Pattern A { get; set; }

        public Pattern B { get; set; }

        public override Color PatternAt(Double4 point)
        {
            var ta = this.Op.PatternAt(point);
            var tb = Color.White - ta;

            var ca = ta * this.A.PatternAt(this.A.Inverse * point);
            var cb = tb * this.B.PatternAt(this.B.Inverse * point);

            return ta * ca + tb * cb;
        }
    }
}