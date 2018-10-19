namespace Pixie
{
    using System;

    public struct Vector4 : IEquatable<Vector4>
    {
        public Vector4(float value)
        {
            W = value;
            X = value;
            Y = value;
            Z = value;
        }

        public Vector4(float w, float x, float y, float z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }

        public float W;
        public float X;
        public float Y;
        public float Z;

        public bool Equals(Vector4 other) =>
            this.W == other.W &&
            this.X == other.X &&
            this.Y == other.Y &&
            this.Z == other.Z;
    }
}