// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
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
        public static int PrimaryRay4s = 0;

        /// <summary>
        /// The number of secondary rays shot.
        /// </summary>
        public static int SecondaryRay4s = 0;

        /// <summary>
        /// The number of shadow rays shot.
        /// </summary>
        public static int ShadowRay4s = 0;

        public static void Reset()
        {
            Tests = 0;
            PrimaryRay4s = 0;
            SecondaryRay4s = 0;
            ShadowRay4s = 0;
        }
    }
}
