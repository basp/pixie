// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;
    using Linie;

    /// <summary>
    /// The results of an interaction between a Ray4 and object.
    /// </summary>
    public struct Interaction
    {
        public double T;

        public Shape Object;

        public Vector4 Point;

        public Vector4 OverPoint;

        public Vector4 UnderPoint;

        public Vector4 Eyev;

        public Vector4 Normalv;

        public Vector4 Reflectv;

        public bool Inside;

        public double N1;

        public double N2;

        public double SchlicksApproximation()
        {
            var cos = this.Eyev.Dot(this.Normalv);
            if (this.N1 > this.N2)
            {
                var n = this.N1 / this.N2;
                var sin2t = n * n * (1.0 - cos * cos);
                if (sin2t > 1.0)
                {
                    return 1.0;
                }

                var cost = Math.Sqrt(1.0 - sin2t);
                cos = cost;
            }

            var r0 = Math.Pow((this.N1 - this.N2) / (this.N1 + this.N2), 2);
            return r0 + (1 - r0) * Math.Pow(1 - cos, 5);
        }
    }
}
