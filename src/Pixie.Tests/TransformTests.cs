namespace Pixie.Tests
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
            var p = Float4.Point(0, 1, 0);
            var halfQuarter = Transform.RotateX((float)Math.PI / 4);
            var fullQuarter = Transform.RotateX((float)Math.PI / 2);
            var halfExpected = Float4.Point(0, (float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2);
            var fullExpected = Float4.Point(0, 0, 1);
            const float eps = 0.0000001f;
            var comparer = Float4.GetEqualityComparer(eps);
            Assert.Equal(halfExpected, halfQuarter * p, comparer);
            Assert.Equal(fullExpected, fullQuarter * p, comparer);
        }

        [Fact]
        public void TestInverseOfXRotationRotatesInOppositeDirection()
        {
            var p = Float4.Point(0, 1, 0);
            var halfQuarterInv = Transform.RotateX((float)Math.PI / 4).Inverse();
            var expected = Float4.Point(0, (float)Math.Sqrt(2) / 2, -(float)Math.Sqrt(2) / 2);
            const float eps = 0.0000001f;
            var comparer = Float4.GetEqualityComparer(eps);
            Assert.Equal(expected, halfQuarterInv * p, comparer);
        }

        [Fact]
        public void TestRotatePointAroundYAxis()
        {
            var p = Float4.Point(0, 0, 1);
            var halfQuarter = Transform.RotateY((float)Math.PI / 4);
            var fullQuarter = Transform.RotateY((float)Math.PI / 2);
            var halfExpected = Float4.Point((float)Math.Sqrt(2) / 2, 0, (float)Math.Sqrt(2) / 2);
            var fullExpected = Float4.Point(1, 0, 0);
            const float eps = 0.0000001f;
            var comparer = Float4.GetEqualityComparer(eps);
            Assert.Equal(halfExpected, halfQuarter * p, comparer);
            Assert.Equal(fullExpected, fullQuarter * p, comparer);
        }


        [Fact]
        public void TestRotatePointAroundZAxis()
        {
            var p = Float4.Point(0, 1, 0);
            var halfQuarter = Transform.RotateZ((float)Math.PI / 4);
            var fullQuarter = Transform.RotateZ((float)Math.PI / 2);
            var halfExpected = Float4.Point(-(float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2, 0);
            var fullExpected = Float4.Point(-1, 0, 0);
            const float eps = 0.0000001f;
            var comparer = Float4.GetEqualityComparer(eps);
            Assert.Equal(halfExpected, halfQuarter * p, comparer);
            Assert.Equal(fullExpected, fullQuarter * p, comparer);
        }

        [Fact]
        public void TestSharingMovesXInProportionToY()
        {
            var t = Transform.Shear(1, 0, 0, 0, 0, 0);
            var p = Float4.Point(2, 3, 4);
            var expected = Float4.Point(5, 3, 4);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestSharingMovesXInProportionToZ()
        {
            var t = Transform.Shear(0, 1, 0, 0, 0, 0);
            var p = Float4.Point(2, 3, 4);
            var expected = Float4.Point(6, 3, 4);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestSharingMovesYInProportionToX()
        {
            var t = Transform.Shear(0, 0, 1, 0, 0, 0);
            var p = Float4.Point(2, 3, 4);
            var expected = Float4.Point(2, 5, 4);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestSharingMovesYInProportionToZ()
        {
            var t = Transform.Shear(0, 0, 0, 1, 0, 0);
            var p = Float4.Point(2, 3, 4);
            var expected = Float4.Point(2, 7, 4);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestSharingMovesZInProportionToX()
        {
            var t = Transform.Shear(0, 0, 0, 0, 1, 0);
            var p = Float4.Point(2, 3, 4);
            var expected = Float4.Point(2, 3, 6);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestSharingMovesZInProportionToY()
        {
            var t = Transform.Shear(0, 0, 0, 0, 0, 1);
            var p = Float4.Point(2, 3, 4);
            var expected = Float4.Point(2, 3, 7);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestIndividualTransformationsAppliedInSequence()
        {
            var a = Transform.RotateX((float)Math.PI / 2);
            var b = Transform.Scale(5, 5, 5);
            var c = Transform.Translate(10, 5, 7);
            var p1 = Float4.Point(1, 0, 1);
            var p2 = a * p1;
            var p3 = b * p2;
            var p4 = c * p3;
            const float eps = 0.000001f;
            var comparer = Float4.GetEqualityComparer(eps);
            Assert.Equal(Float4.Point(1, -1, 0), p2, comparer);
            Assert.Equal(Float4.Point(5, -5, 0), p3, comparer);
            Assert.Equal(Float4.Point(15, 0, 7), p4, comparer);
        }

        [Fact]
        public void TestChainedTransformationsAppliedInReverseOrder()
        {
            var a = Transform.RotateX((float)Math.PI / 2);
            var b = Transform.Scale(5, 5, 5);
            var c = Transform.Translate(10, 5, 7);
            var p = Float4.Point(1, 0, 1);
            var t = c * b * a;
            // Note that we can execute this with higher precision 
            // than if we would apply the transformations in sequence like
            // in the previous test case.
            const float eps = 0.0000001f;
            var comparer = Float4.GetEqualityComparer(eps);
            Assert.Equal(Float4.Point(15, 0, 7), t * p, comparer);
        }

        [Fact]
        public void TestTransformationMatrixForDefaultOrientation()
        {
            var from = Float4.Point(0, 0, 0);
            var to = Float4.Point(0, 0, -1);
            var up = Float4.Vector(0, 1, 0);
            var t = Transform.View(from, to, up);
            Assert.Equal(Float4x4.Identity, t);
        }

        [Fact]
        public void TestViewTransformLookingInPositiveZDirection()
        {
            var from = Float4.Point(0, 0, 0);
            var to = Float4.Point(0, 0, 1);
            var up = Float4.Vector(0, 1, 0);
            var t = Transform.View(from, to, up);
            var expected = Transform.Scale(-1, 1, -1);
            Assert.Equal(expected, t);
        }

        [Fact]
        public void TestViewTransformMovesTheWorld()
        {
            var from = Float4.Point(0, 0, 8);
            var to = Float4.Point(0, 0, 0);
            var up = Float4.Vector(0, 1, 0);
            var t = Transform.View(from, to, up);
            var expected = Transform.Translate(0, 0, -8);
            Assert.Equal(expected, t);
        }

        [Fact]
        public void TestAbitraryViewTransform()
        {
            var from = Float4.Point(1, 3, 2);
            var to = Float4.Point(4, -2, 8);
            var up = Float4.Vector(1, 1, 0);
            var t = Transform.View(from, to, up);
            var expected =
                new Float4x4(
                    -0.50709f, 0.50709f, 0.67612f, -2.36643f,
                    0.76772f, 0.60609f, 0.12122f, -2.82843f,
                    -0.35857f, 0.59761f, -0.71714f, 0.00000f,
                    0.00000f, 0.00000f, 0.00000f, 1.00000f);
            const float epsilon = 0.00001f;
            var comparer = Float4x4.GetEqualityComparer(epsilon);
            Assert.Equal(expected, t, comparer);
        }
    }
}
