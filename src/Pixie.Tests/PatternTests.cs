namespace Pixie.Tests
{
    using Xunit;

    public class PatternTests
    {
        private static readonly Color black = new Color(0, 0, 0);
        private static readonly Color white = new Color(1, 1, 1);

        [Fact]
        public void CreateStripePattern()
        {
            var pat = new StripePattern(white, black);
            Assert.Equal(white, pat.A);
            Assert.Equal(black, pat.B);
        }

        [Fact]
        public void StripePatternIsConstantInY()
        {
            var pat = new StripePattern(white, black);
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 0, 0)));
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 1, 0)));
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 2, 0)));
        }

        [Fact]
        public void StripePatternIsConstantInZ()
        {
            var pat = new StripePattern(white, black);
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 0, 0)));
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 0, 1)));
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 0, 2)));
        }

        [Fact]
        public void StripePatternAlternatesInX()
        {
            var pat = new StripePattern(white, black);
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 0, 0)));
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0.9, 0, 0)));
            Assert.Equal(black, pat.GetColor(Vector4.CreatePosition(1, 0, 0)));
            Assert.Equal(black, pat.GetColor(Vector4.CreatePosition(-0.1, 0, 0)));
            Assert.Equal(black, pat.GetColor(Vector4.CreatePosition(-1, 0, 0)));
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(-1.1, 0, 0)));
        }

        [Fact]
        public void DefaultPatternTransformation()
        {
            var pat = new TestPattern();
            Assert.Equal(Matrix4x4.Identity, pat.Transform);
        }

        [Fact]
        public void AssignTransform()
        {
            var pat = new TestPattern
            {
                Transform = Transform.Translate(1, 2, 3),
            };

            Assert.Equal(Transform.Translate(1, 2, 3), pat.Transform);
        }

        [Fact]
        public void PatternWithObjectTransformation()
        {
            var obj = new Sphere()
            {
                Transform = Transform.Scale(2, 2, 2),
            };

            var pat = new TestPattern();
            var c = pat.GetColor(obj, Vector4.CreatePosition(2, 3, 4));
            var expected = new Color(1, 1.5, 2);
            Assert.Equal(expected, c);
        }

        [Fact]
        public void PatternWithPatternTransformation()
        {
            var obj = new Sphere();
            var pat = new TestPattern
            {
                Transform = Transform.Scale(2, 2, 2),
            };

            var c = pat.GetColor(obj, Vector4.CreatePosition(2, 3, 4));
            var expected = new Color(1, 1.5, 2);
            Assert.Equal(expected, c);
        }

        [Fact]
        public void PatternWithObjectAndPatternTransformation()
        {
            var obj = new Sphere()
            {
                Transform = Transform.Scale(2, 2, 2),
            };

            var pat = new TestPattern
            {
                Transform = Transform.Translate(0.5, 1, 1.5),
            };

            var c = pat.GetColor(obj, Vector4.CreatePosition(2.5, 3, 3.5));
            var expected = new Color(0.75, 0.5, 0.25);
            Assert.Equal(expected, c);
        }

        [Fact]
        public void GradientLinearlyInterpolatesBetweenColors()
        {
            var pat = new GradientPattern(white, black);
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 0, 0)));
            Assert.Equal(new Color(0.75, 0.75, 0.75), pat.GetColor(Vector4.CreatePosition(0.25, 0, 0)));
            Assert.Equal(new Color(0.5, 0.5, 0.5), pat.GetColor(Vector4.CreatePosition(0.5, 0, 0)));
            Assert.Equal(new Color(0.25, 0.25, 0.25), pat.GetColor(Vector4.CreatePosition(0.75, 0, 0)));
        }

        [Fact]
        public void RingShouldExtendInXAndY()
        {
            var pat = new RingPattern(white, black);
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 0, 0)));
            Assert.Equal(black, pat.GetColor(Vector4.CreatePosition(1, 0, 0)));
            Assert.Equal(black, pat.GetColor(Vector4.CreatePosition(0, 0, 1)));
            Assert.Equal(black, pat.GetColor(Vector4.CreatePosition(0.708, 0, 0.708)));
        }

        [Fact]
        public void CheckersShouldRepeatInX()
        {
            var pat = new CheckersPattern(white, black);
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 0, 0)));
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0.99, 0, 0)));
            Assert.Equal(black, pat.GetColor(Vector4.CreatePosition(1.01, 0, 0)));
        }

        [Fact]
        public void CheckersShouldRepeatInY()
        {
            var pat = new CheckersPattern(white, black);
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 0, 0)));
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 0.99, 0)));
            Assert.Equal(black, pat.GetColor(Vector4.CreatePosition(0, 1.01, 0)));
        }

        [Fact]
        public void CheckersShouldRepeatInZ()
        {
            var pat = new CheckersPattern(white, black);
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 0, 0)));
            Assert.Equal(white, pat.GetColor(Vector4.CreatePosition(0, 0, 0.99)));
            Assert.Equal(black, pat.GetColor(Vector4.CreatePosition(0, 0, 1.01)));
        }
    }
}