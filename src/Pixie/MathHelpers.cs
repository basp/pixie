namespace Pixie
{
    public static class MathHelpers
    {
        public static float Lerp(float v0, float v1, float t) => 
            (1 - t) * v0 + t * v1;
        
        public static float Clamp(float v, float min, float max)
        {
            if (v < min) return min;
            if (v > max) return max;
            return v;
        }
    }
}