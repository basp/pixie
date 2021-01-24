namespace Pixie
{
    public class ViewPlane
    {
        private double gamma;

        private double inverseGamma;

        public int HorizontalResolution { get; set; }

        public int VerticalResolution { get; set; }

        public double PixelSize { get; set; }

        public int NumberOfSamples { get; set; }

        public double Gamma
        {
            get => this.gamma;
            set
            {
                this.gamma = value;
                this.inverseGamma = 1 / value;
            }
        }

        public double InverseGamma => this.inverseGamma;
    }
}