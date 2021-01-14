// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using Linsi;

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

        public override Color GetColor(Vector4 point)
        {
            var ta = this.Op.GetColor(point);
            var tb = Color.White - ta;

            var ca = ta * this.A.GetColor(this.A.Inverse * point);
            var cb = tb * this.B.GetColor(this.B.Inverse * point);

            return ta * ca + tb * cb;
        }
    }
}
