namespace Pixie.Core
{
    public abstract class Shape
    {
        protected Float4x4 transform = Float4x4.Identity;

        protected Float4x4 inv = Float4x4.Identity;

        public Material Material { get; set; } = new Material();

        public Float4x4 Transform
        {
            get => this.transform;
            set 
            {
                this.inv = value.Inverse();
                this.transform = value;
            }
        }

        public Float4x4 Inverse => this.inv;

        public virtual IntersectionList Intersect(Ray ray)
        {
            ray = this.Transform.Inverse() * ray;
            return this.LocalIntersect(ray);
        }

        public abstract IntersectionList LocalIntersect(Ray ray);

        public abstract Float4 NormalAt(Float4 point);
    }
}