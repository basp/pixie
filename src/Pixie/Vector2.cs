namespace Pixie
{
    using System;

    public struct Vector2 : IEquatable<Vector2>
    {
        public Vector2(float value)
        {
            X = value;
            Y = value;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 One = new Vector2(1, 1);
        
        public static Vector2 UnitX = new Vector2(1, 0);
        
        public static Vector2 UnitY = new Vector2(0, 1);
        
        public static Vector2 Zero = new Vector2(0, 0);

        public float X;
        
        public float Y;

        public bool Equals(Vector2 other) => 
            this.X == other.X && 
            this.Y == other.Y;
    }
}
