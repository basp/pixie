// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Linsi
{
    using System;
    using System.Diagnostics;

    public class EFloat
    {
        private float v;

        private float low;

        private float high;

        private double vp;

        public EFloat(float v, float err = 0.0f)
        {
            if (err == 0)
            {
                this.low = v;
                this.high = v;
            }
            else
            {
                this.low = Utils.NextFloatDown(v);
                this.high = Utils.NextFloatUp(v);
            }

            this.v = v;
#if DEBUG
            this.vp = v;
#endif
        }

        public EFloat(float v, double vp, float err = 0.0f)
            : this(v, err)
        {
#if DEBUG
            this.vp = vp;
#else
            throw new NotImplementedException();
#endif
        }

        private EFloat(float v, float low, float high)
        {
            this.v = v;
            this.low = low;
            this.high = high;
        }

        private EFloat(float v, double vp, float low, float high)
            : this(v, low, high)
        {
            this.vp = vp;
        }

        public float LowerBound => this.low;

        public float UpperBound => this.high;

#if DEBUG
        public double PreciseValue => this.vp;
#else
        public double PreciseValue =>
            throw new NotImplementedException();
#endif

        public static EFloat operator +(EFloat a, EFloat b)
        {
            var v = a.v + b.v;
            var low = Utils.NextFloatDown(a.LowerBound + b.LowerBound);
            var high = Utils.NextFloatUp(a.UpperBound + b.UpperBound);
#if DEBUG
            var vp = a.vp + b.vp;
            EFloat.Validate(v, vp, low, high);
            return new EFloat(v, vp, low, high);
#else
            return new EFloat(v, low, high);
#endif
        }

        public static EFloat operator -(EFloat a, EFloat b)
        {
            var v = a.v - b.v;
            var low = Utils.NextFloatDown(a.LowerBound - b.UpperBound);
            var high = Utils.NextFloatUp(a.UpperBound - b.LowerBound);

#if DEBUG
            var vp = a.vp - b.vp;
            EFloat.Validate(v, vp, low, high);
            return new EFloat(v, vp, low, high);
#else
            return new EFloat(v, low, high);
#endif
        }

        public static EFloat operator *(EFloat a, EFloat b)
        {
            var v = a.v * b.v;
            var prod = new[]
            {
                a.LowerBound * b.LowerBound,
                a.UpperBound * b.LowerBound,
                a.LowerBound * b.UpperBound,
                a.UpperBound * b.UpperBound,
            };

            var low = Utils.NextFloatDown(
                Math.Min(
                    Math.Min(prod[0], prod[1]),
                    Math.Min(prod[2], prod[3])));

            var high = Utils.NextFloatUp(
                Math.Max(
                    Math.Max(prod[0], prod[1]),
                    Math.Max(prod[2], prod[3])));

#if DEBUG
            var vp = a.vp * b.vp;
            EFloat.Validate(v, vp, low, high);
            return new EFloat(v, vp, low, high);
#else
            return new EFloat(v, low, high);
#endif
        }

        public static EFloat operator /(EFloat a, EFloat b)
        {
            var v = a.v / b.v;

            float low, high;
            if (b.low < 0 && b.high > 0)
            {
                // Guard against divide by zero;
                // return interval of everything
                low = float.NegativeInfinity;
                high = float.PositiveInfinity;
            }
            else
            {
                var div = new[]
                {
                    a.LowerBound / b.LowerBound,
                    a.UpperBound / b.LowerBound,
                    a.LowerBound / b.UpperBound,
                    a.UpperBound / b.UpperBound,
                };

                low = Utils.NextFloatDown(
                    Math.Min(
                        Math.Min(div[0], div[1]),
                        Math.Min(div[2], div[3])));

                high = Utils.NextFloatUp(
                    Math.Max(
                        Math.Max(div[0], div[1]),
                        Math.Max(div[2], div[3])));
            }

#if DEBUG
            var vp = a.vp / b.vp;
            EFloat.Validate(v, vp, low, high);
            return new EFloat(v, vp, low, high);
#else
            return new EFloat(v, low, high);
#endif
        }

        public static EFloat operator -(EFloat a)
        {
            var v = -a.v;
            var low = -a.high;
            var high = -a.low;
#if DEBUG
            var vp = -a.vp;
            EFloat.Validate(v, vp, low, high);
            return new EFloat(v, vp, low, high);
#else
            return new EFloat(v, low, high);
#endif
        }

        public static EFloat operator +(EFloat a, int b) => a + new EFloat(b);

        public static EFloat operator -(EFloat a, int b) => a - new EFloat(b);

        public static EFloat operator *(EFloat a, int b) => a * new EFloat(b);

        public static EFloat operator /(EFloat a, int b) => a / new EFloat(b);

        public static EFloat operator +(int a, EFloat b) => new EFloat(a) + b;

        public static EFloat operator -(int a, EFloat b) => new EFloat(a) - b;

        public static EFloat operator *(int a, EFloat b) => new EFloat(a) * b;

        public static EFloat operator /(int a, EFloat b) => new EFloat(a) / b;

        public static EFloat operator +(EFloat a, float b) => a + new EFloat(b);

        public static EFloat operator -(EFloat a, float b) => a - new EFloat(b);

        public static EFloat operator *(EFloat a, float b) => a * new EFloat(b);

        public static EFloat operator /(EFloat a, float b) => a / new EFloat(b);

        public static EFloat operator +(float a, EFloat b) => new EFloat(a) + b;

        public static EFloat operator -(float a, EFloat b) => new EFloat(a) - b;

        public static EFloat operator *(float a, EFloat b) => new EFloat(a) * b;

        public static EFloat operator /(float a, EFloat b) => new EFloat(a) / b;

        // Negative values of `f` are not supported
        public static EFloat Sqrt(EFloat f)
        {
            var v = (float)Math.Sqrt(f.v);
            var low = Utils.NextFloatDown((float)Math.Sqrt(f.low));
            var high = Utils.NextFloatUp((float)Math.Sqrt(f.high));
#if DEBUG
            var vp = Math.Sqrt(f.vp);
            EFloat.Validate(v, vp, low, high);
            return new EFloat(v, vp, low, high);
#else
            return new Efloat(v, low, high);
#endif
        }

        public float GetAbsoluteError() =>
            Utils.NextFloatUp(
                Math.Max(
                    Math.Abs(this.high - this.v),
                    Math.Abs(this.v - this.low)));

#if DEBUG
        public float GetRelativeError() =>
            (float)Math.Abs((this.vp - this.v) / this.vp);
#else
        public float GetRelativeError() =>
            throw new NotImplementedException();
#endif

        private static void Validate(float v, double vp, float low, float high)
        {
#if DEBUG
            Debug.Assert(
                low <= high,
                "lower bound must be less than or equal to upper bound");

            Debug.Assert(
                low <= v,
                "lower bound must be less than or equal to value");

            Debug.Assert(
                high >= v,
                "upper bound must be less than or equal to value");

            Debug.Assert(
                low <= vp,
                "lower bound must be less than or equal to precision value");

            Debug.Assert(
                high >= vp,
                "upper bound must be less than or equal to precision value");
#else
            throw new NotImplementedException();
#endif
        }
    }
}
