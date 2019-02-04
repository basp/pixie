namespace Pixie
{
    using System;
    
    // http://hamelot.io/visualization/using-ffmpeg-to-convert-a-set-of-images-into-a-video/
    // ffmpeg -r 30 -f image2 -s 1920x1080 -i frame_%d.png -crf 25 -pix_fmt yuv420p -v verbose out.mp4

    // Bernstein polynomials
    // ---------------------
    // b0_0(x) = 1,         
    // b0_1(x) = 1 - x,         b1_1(x) = x
    // b0_2(x) = (1 - x)^2,     b1_2(x) = 2x(1 - x),        b2_2(x) = x^2
    // b0_3(x) = (1 - x)^3,     b1_3(x) = 3x(1 - x)^2,      b2_3(x) = 3x^2(1 - x),      b3_3 = x^3

    public static class Bernstein2
    {
        public static Vector2 Line(Vector2 p1, Vector2 p2, float t)
        {
            var x = B0_1(t) * p1.X + B1_1(t) * p2.X;
            var y = B0_1(t) * p1.Y + B1_1(t) * p2.Y;
            return new Vector2(x, y);
        }

        public static Vector2 Parabola(Vector2 p1, Vector2 p2, Vector2 p3, float t)
        {
            var x = Bernstein2.B0_3(t) * p1.X + Bernstein2.B1_3(t) * p2.X + Bernstein2.B2_3(t) * p3.X + Bernstein2.B3_3(t) * p3.X;
            var y = Bernstein2.B0_3(t) * p1.Y + Bernstein2.B1_3(t) * p2.Y + Bernstein2.B2_3(t) * p3.Y + Bernstein2.B3_3(t) * p3.Y;
            return new Vector2(x, y);
        }

        public static float B0_0(float t) => 1.0f;

        public static float B0_1(float t) => 1.0f - t;

        public static float B0_2(float t) => (1.0f - t) * (1.0f - t);

        public static float B0_3(float t) => (1.0f - t) * (1.0f - t) * (1.0f - t);

        public static float B1_1(float t) => t;

        public static float B1_2(float t) => 2 * t * (1 - t);

        public static float B1_3(float t) => 3 * t * (1 - t) * (1 - t);

        public static float B2_2(float t) => t * t;

        public static float B2_3(float t) => 3 * t * t * (1 - t);

        public static float B3_3(float t) => t * t * t;
    }
}