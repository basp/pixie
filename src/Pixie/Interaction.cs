// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;
    using Linie;

    /// <summary>
    /// The results of an interaction between a ray and object.
    /// </summary>
    public class Interaction
    {
        public double T { get; set; }

        public Shape Object { get; set; }

        public Vector4 Point {get; set; }

        public Vector4 OverPoint {get; set; }

        public Vector4 UnderPoint {get; set; }

        public Vector4 Eyev {get; set; }

        public Vector4 Normalv {get; set; }

        public Vector4 Reflectv {get; set; }

        public bool Inside {get; set; }

        public double N1 {get; set; }

        public double N2 {get; set; }

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
