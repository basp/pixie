namespace Pixie.Core
{
    public abstract class Shape
    {
        protected Double4x4 transform = Double4x4.Identity;

        protected Double4x4 inv = Double4x4.Identity;

        protected Double4x4 invt = Double4x4.Identity;

        public bool Shadow { get; set; } = true;

        public Material Material { get; set; } = new Material();

        public Shape Parent { get; set; } = null;

        public bool HasParent => this.Parent != null;

        public virtual bool Includes(Shape obj) => obj == this;

        public Double4x4 Transform
        {
            get => this.transform;
            set
            {
                this.inv = value.Inverse();
                this.invt = this.inv.Transpose();
                this.transform = value;
            }
        }

        public Double4x4 Inverse => this.inv;

        public virtual IntersectionList Intersect(Ray ray)
        {
            ray = this.inv * ray;
            return this.LocalIntersect(ray);
        }

        public virtual Double4 NormalAt(Double4 point)
        {
            var localPoint = this.WorldToObject(point);
            var localNormal = this.LocalNormalAt(localPoint);
            return this.NormalToWorld(localNormal);
        }

        public abstract IntersectionList LocalIntersect(Ray ray);

        public abstract Double4 LocalNormalAt(Double4 point);

        public virtual BoundingBox Bounds() =>
            BoundingBox.Infinity;

        public virtual BoundingBox ParentSpaceBounds() =>
            this.Bounds() * this.transform;

        public virtual void Divide(double threshold)
        {
        }

        public Double4 WorldToObject(Double4 point)
        {
            if (this.HasParent)
            {
                point = this.Parent.WorldToObject(point);
            }

            return this.inv * point;
        }

        public Double4 NormalToWorld(Double4 n)
        {
            n = this.invt * n;
            n.W = 0;
            n = n.Normalize();

            if (this.HasParent)
            {
                n = this.Parent.NormalToWorld(n);
            }

            return n;
        }
    }
}