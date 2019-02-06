namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    class TestPattern : Pattern
    {
        public override Color PatternAt(Double4 point) =>
            new Color(point.X, point.Y, point.Z);
    }

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
            Assert.Equal(white, pat.PatternAt(Double4.Point(0, 0, 0)));
            Assert.Equal(white, pat.PatternAt(Double4.Point(0, 1, 0)));
            Assert.Equal(white, pat.PatternAt(Double4.Point(0, 2, 0)));
        }

        [Fact]
        public void StripePatternIsConstantInZ()
        {
            var pat = new StripePattern(white, black);
            Assert.Equal(white, pat.PatternAt(Double4.Point(0, 0, 0)));
            Assert.Equal(white, pat.PatternAt(Double4.Point(0, 0, 1)));
            Assert.Equal(white, pat.PatternAt(Double4.Point(0, 0, 2)));
        }

        [Fact]
        public void StripePatternAlternatesInX()
        {
            var pat = new StripePattern(white, black);
            Assert.Equal(white, pat.PatternAt(Double4.Point(0, 0, 0)));
            Assert.Equal(white, pat.PatternAt(Double4.Point(0.9, 0, 0)));
            Assert.Equal(black, pat.PatternAt(Double4.Point(1, 0, 0)));
            Assert.Equal(black, pat.PatternAt(Double4.Point(-0.1, 0, 0)));
            Assert.Equal(black, pat.PatternAt(Double4.Point(-1, 0, 0)));
            Assert.Equal(white, pat.PatternAt(Double4.Point(-1.1, 0, 0)));
        }

        [Fact]
        public void DefaultPatternTransformation()
        {
            var pat = new TestPattern();
            Assert.Equal(Double4x4.Identity, pat.Transform);
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
            var c = pat.PattenAt(obj, Double4.Point(2, 3, 4));
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

            var c = pat.PattenAt(obj, Double4.Point(2, 3, 4));
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

            var c = pat.PattenAt(obj, Double4.Point(2.5, 3, 3.5));
            var expected = new Color(0.75, 0.5, 0.25);
            Assert.Equal(expected, c);
        }
    }
}