namespace Pixie.Core
{
    using System;

    public struct Computations
    {
        public double T;

        public Shape Object;

        public Double4 Point;

        public Double4 OverPoint;

        public Double4 UnderPoint;

        public Double4 Eyev;

        public Double4 Normalv;

        public Double4 Reflectv;

        public bool Inside;

        public double N1;

        public double N2;

        public double Schlick()
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