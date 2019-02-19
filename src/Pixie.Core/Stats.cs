namespace Pixie.Core
{
    public static class Stats
    {
        public static int Tests = 0;
        public static int PrimaryRays = 0;
        public static int SecondaryRays = 0;
        public static int ShadowRays = 0;
        public static double RaysPerPixel = 0.0;

        public static void Reset()
        {
            Tests = 0;
            PrimaryRays = 0;
            SecondaryRays = 0;
            ShadowRays = 0;
            RaysPerPixel = 0.0;
        }
    }
}