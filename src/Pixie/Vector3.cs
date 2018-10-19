namespace Pixie
{
    using System;

    public struct Vector3 : IEquatable<Vector3>
    {
        public Vector3(float value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(Vector2 value, float z)
        {
            X = value.X;
            Y = value.Y;
            Z = z;
        }

        public static Vector3 One = new Vector3(1, 1, 1);
        
        public static Vector3 UnitX = new Vector3(1, 0, 0);
        
        public static Vector3 UnitY = new Vector3(0, 1, 0);
        
        public static Vector3 UnitZ = new Vector3(0, 0, 1);

        public static Vector3 Zero = new Vector3(0, 0, 0);

        public float X;
        
        public float Y;

        public float Z;

        public bool Equals(Vector3 other) => 
            this.X == other.X && 
            this.Y == other.Y &&
            this.Z == other.Z;
    }
}
