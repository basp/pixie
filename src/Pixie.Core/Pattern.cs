namespace Pixie.Core
{
    using System;

    public abstract class Pattern
    {
        protected Double4x4 transform = Double4x4.Identity;

        protected Double4x4 inv = Double4x4.Identity;

        public Double4x4 Transform
        {
            get => this.transform;
            set
            {
                this.transform = value;
                this.inv = value.Inverse();
            }
        }

        public Double4x4 Inverse => this.inv;

        public virtual Color PattenAt(Shape obj, Double4 point)
        {
            point = obj.Inverse * point;
            point = this.Inverse * point;
            return this.PatternAt(point);
        }

        public abstract Color PatternAt(Double4 point);
    }
}