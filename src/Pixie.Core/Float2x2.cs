namespace Pixie.Core
{
    public struct Float2x2
    {
        private readonly float[] data;

        public Float2x2(float v)
        {
            this.data = new []
            {
                v, v,
                v, v,
            };
        }

        public Float2x2(float m00, float m01,
                        float m10, float m11)
        {
            this.data = new [] 
            {
                m00, m01,
                m10, m11,
            };
        }

        public float this[int row, int col]
        {
            get => this.data[row * 2 + col];
            set => this.data[row * 2 + col] = value;
        }

        public override string ToString() =>
            $"({string.Join(", ", data)})";
    }

    public static class Float2x2Extensions
    {
        public static float Determinant(this Float2x2 m) =>
            m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
    }
}