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

        public Vector4(float x, float y, float z, float w)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }

        public Vector4(Vector3 value, float w)
        {
            W = w;
            X = value.X;
            Y = value.Y;
            Z = value.Z;
        }

        public Vector4(Vector2 value, float z, float w)
        {
            W = w;
            X = value.X;
            Y = value.Y;
            Z = z;
        }

        public static Vector4 One = new Vector4(1, 1, 1, 1);

        public static Vector4 UnitW = new Vector4(0, 0, 0, 1);
        
        public static Vector4 UnitX = new Vector4(1, 0, 0, 0);
        
        public static Vector4 UnitY = new Vector4(0, 1, 0, 0);
        
        public static Vector4 UnitZ = new Vector4(0, 0, 1, 0);

        public static Vector4 Zero = new Vector4(0, 0, 0, 0);

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