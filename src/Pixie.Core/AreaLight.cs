namespace Pixie.Core
{
    public class AreaLight : ILight
    {
        public AreaLight(
            Double4 corner,
            Double4 fullUvec,
            int usteps,
            Double4 fullVvec,
            int vsteps,
            Color intensity)
        {
            this.Corner = corner;
            this.Uvec = fullUvec / usteps;
            this.Usteps = usteps;
            this.Vvec = fullVvec / vsteps;
            this.Vsteps = vsteps;
            this.Intensity = intensity;
            this.Position = corner + 0.5 * (fullUvec + fullVvec);
        }

        public Double4 Corner { get; set; }

        public Double4 Uvec { get; set; }

        public int Usteps { get; set; }

        public Double4 Vvec { get; set; }

        public int Vsteps { get; set; }

        public int Samples => Usteps * Vsteps;

        public Color Intensity { get; set; }

        public Double4 Position { get; }

        public bool Equals(ILight other)
        {
            throw new System.NotImplementedException();
        }

        public double IntensityAt(Double4 point, World w)
        {
            throw new System.NotImplementedException();
        }
    }
}