namespace Pixie
{
    public abstract class Shape
    {
        protected Matrix4x4 transform = Matrix4x4.Identity;

        protected Matrix4x4 inv = Matrix4x4.Identity;

        protected Matrix4x4 invt = Matrix4x4.Identity;

        public bool Shadow { get; set; } = true;

        public Material Material { get; set; } = new Material();

        public Shape Parent { get; set; } = null;

        public bool HasParent => this.Parent != null;

        public virtual bool Includes(Shape obj) => obj == this;

        public Matrix4x4 Transform
        {
            get => this.transform;
            set
            {
                this.inv = value.Inverse();
                this.invt = this.inv.Transpose();
                this.transform = value;
            }
        }

        public Matrix4x4 Inverse => this.inv;

        public virtual IntersectionList Intersect(Ray ray)
        {
            ray = this.inv * ray;
            return this.LocalIntersect(ray);
        }

        public virtual Vector4 NormalAt(Vector4 point)
        {
            var localPoint = this.WorldToObject(point);
            var localNormal = this.LocalNormalAt(localPoint);
            return this.NormalToWorld(localNormal);
        }

        public abstract IntersectionList LocalIntersect(Ray ray);

        public abstract Vector4 LocalNormalAt(Vector4 point);

        public virtual BoundingBox Bounds() =>
            BoundingBox.Infinity;

        public virtual BoundingBox ParentSpaceBounds() =>
            this.Bounds() * this.transform;

        public virtual void Divide(double threshold)
        {
        }

        public Vector4 WorldToObject(Vector4 point)
        {
            if (this.HasParent)
            {
                point = this.Parent.WorldToObject(point);
            }

            return this.inv * point;
        }

        public Vector4 NormalToWorld(Vector4 n)
        {
            n = this.invt * n;
            n = new Vector4(n.X, n.Y, n.Z, 0);
            n = n.Normalize();

            if (this.HasParent)
            {
                n = this.Parent.NormalToWorld(n);
            }

            return n;
        }
    }
}