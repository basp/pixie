namespace Pixie.Cmd.Examples
{
    public class PertubedPattern : Pattern
    {
        public PertubedPattern(Pattern a, Pattern b)
        {
            this.A = a;
            this.B = b;
        }

        public Pattern A { get; set; }

        public Pattern B { get; set; }

        public override Color GetColor(Vector4 point)
        {
            // var n = NoiseGenerator.GradientCoherentNoise3D(
            //     point.X,
            //     point.Y,
            //     point.Z);

            var n = 0;

            var ta = n;
            var tb = 1.0 - n;

            var ca = ta * this.A.GetColor(point);
            var cb = tb * this.B.GetColor(point);

            return ca + cb;
        }
    }
}