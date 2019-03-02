namespace Pixie.Core
{
    public class Stats
    {
        /// <summary>
        /// The number of intersection tests performed.
        /// </summary>
        public static int Tests = 0;

        /// <summary>
        /// The number of primary rays shot.
        /// </summary>
        public static int PrimaryRays = 0;

        /// <summary>
        /// The number of secondary rays shot.
        /// </summary>
        public static int SecondaryRays = 0;

        /// <summary>
        /// The number of shadow rays shot.
        /// </summary>
        public static int ShadowRays = 0;

        public static void Reset()
        {
            Tests = 0;
            PrimaryRays = 0;
            SecondaryRays = 0;
            ShadowRays = 0;
        }
    }
}