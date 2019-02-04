namespace Pixie.Test
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Pixie.Core;

    public class TransformationTests
    {
        [Fact]
        public void TestMultiplyByTranslationMatrix()
        {
            var t = Transform.Translate(5, -3, 2);
            var p = Float4.Point(-3, 4, 5);
            var expected = Float4.Point(2, 1, 7);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestMultiplyByInverseOfTranslationMatrix()
        {
            var t = Transform.Translate(5, -3, 2).Inverse();
            var p = Float4.Point(-3, 4, 5);
            var expected = Float4.Point(-8, 7, 3);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestTranslationDoesNotAffectVectors()
        {
            var t = Transform.Translate(5, -3, 2);
            var v = Float4.Vector(-3, 4, 5);
            Assert.Equal(v, t * v);
        }

        [Fact]
        public void TestScalingMatrixAppliedToPoint()
        {
            var t = Transform.Scale(2, 3, 4);
            var p = Float4.Point(-4, 6, 8);
            var expected = Float4.Point(-8, 18, 32);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestScalingMatrixAppliedToVector()
        {
            var t = Transform.Scale(2, 3, 4);
            var p = Float4.Vector(-4, 6, 8);
            var expected = Float4.Vector(-8, 18, 32);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestMultiplyByTheInverseOfScalingMatrix()
        {
            var t = Transform.Scale(2, 3, 4).Inverse();
            var v = Float4.Vector(-4, 6, 8);
            var expected = Float4.Vector(-2, 2, 2);
            Assert.Equal(expected, t * v);
        }

        [Fact]
        public void TestReflectionIsScalingByNegativeValue()
        {
            var t = Transform.Scale(-1, 1, 1);
            var p = Float4.Point(2, 3, 4);
            var expected = Float4.Point(-2, 3, 4);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestRotatePointAroundXAxis()
        {

        }

        [Fact]
        public void TestInverseOfXRotationRotatesInOppositeDirection()
        {

        }
    }
}
