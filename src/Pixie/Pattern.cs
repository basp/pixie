namespace Pixie
{
    using System;

    public abstract class Pattern
    {
        protected Matrix4x4 transform = Matrix4x4.Identity;

        protected Matrix4x4 inv = Matrix4x4.Identity;

        public Matrix4x4 Transform
        {
            get => this.transform;
            set
            {
                this.transform = value;
                this.inv = value.Inverse();
            }
        }

        public Matrix4x4 Inverse => this.inv;

        public virtual Color PatternAt(Shape obj, Vector4 point)
        {
            point = obj.WorldToObject(point);
            point = this.Inverse * point;
            return this.PatternAt(point);
        }

        public abstract Color PatternAt(Vector4 point);
    }
}