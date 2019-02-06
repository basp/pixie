namespace Pixie.Core
{
    public abstract class Shape
    {
        protected Double4x4 transform = Double4x4.Identity;

        protected Double4x4 inv = Double4x4.Identity;

        public Material Material { get; set; } = new Material();

        public Double4x4 Transform
        {
            get => this.transform;
            set 
            {
                this.inv = value.Inverse();
                this.transform = value;
            }
        }

        public Double4x4 Inverse => this.inv;

        public virtual IntersectionList Intersect(Ray ray)
        {
            ray = this.Transform.Inverse() * ray;
            return this.LocalIntersect(ray);
        }

        public abstract IntersectionList LocalIntersect(Ray ray);

        public abstract Double4 NormalAt(Double4 point);
    }
}