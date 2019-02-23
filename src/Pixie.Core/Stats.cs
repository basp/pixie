namespace Pixie.Core
{
    public class Stats
    {
        public int Tests = 0;
        public int PrimaryRays = 0;
        public int SecondaryRays = 0;
        public int ShadowRays = 0;
        public double RaysPerPixel = 0.0;

        public void Reset()
        {
            Tests = 0;
            PrimaryRays = 0;
            SecondaryRays = 0;
            ShadowRays = 0;
            RaysPerPixel = 0.0;
        }
    }
}