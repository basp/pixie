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
            var p = Double4.Point(-3, 4, 5);
            var expected = Double4.Point(2, 1, 7);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestMultiplyByInverseOfTranslationMatrix()
        {
            var t = Transform.Translate(5, -3, 2).Inverse();
            var p = Double4.Point(-3, 4, 5);
            var expected = Double4.Point(-8, 7, 3);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestTranslationDoesNotAffectVectors()
        {
            var t = Transform.Translate(5, -3, 2);
            var v = Double4.Vector(-3, 4, 5);
            Assert.Equal(v, t * v);
        }

        [Fact]
        public void TestScalingMatrixAppliedToPoint()
        {
            var t = Transform.Scale(2, 3, 4);
            var p = Double4.Point(-4, 6, 8);
            var expected = Double4.Point(-8, 18, 32);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestScalingMatrixAppliedToVector()
        {
            var t = Transform.Scale(2, 3, 4);
            var p = Double4.Vector(-4, 6, 8);
            var expected = Double4.Vector(-8, 18, 32);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestMultiplyByTheInverseOfScalingMatrix()
        {
            var t = Transform.Scale(2, 3, 4).Inverse();
            var v = Double4.Vector(-4, 6, 8);
            var expected = Double4.Vector(-2, 2, 2);
            Assert.Equal(expected, t * v);
        }

        [Fact]
        public void TestReflectionIsScalingByNegativeValue()
        {
            var t = Transform.Scale(-1, 1, 1);
            var p = Double4.Point(2, 3, 4);
            var expected = Double4.Point(-2, 3, 4);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestRotatePointAroundXAxis()
        {
            var p = Double4.Point(0, 1, 0);
            var halfQuarter = Transform.RotateX(Math.PI / 4);
            var fullQuarter = Transform.RotateX(Math.PI / 2);
            var halfExpected = Double4.Point(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2);
            var fullExpected = Double4.Point(0, 0, 1);
            const double eps = 0.0000001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(halfExpected, halfQuarter * p, comparer);
            Assert.Equal(fullExpected, fullQuarter * p, comparer);
        }

        [Fact]
        public void TestInverseOfXRotationRotatesInOppositeDirection()
        {
            var p = Double4.Point(0, 1, 0);
            var halfQuarterInv = Transform.RotateX(Math.PI / 4).Inverse();
            var expected = Double4.Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            const double eps = 0.0000001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(expected, halfQuarterInv * p, comparer);
        }

        [Fact]
        public void TestRotatePointAroundYAxis()
        {
            var p = Double4.Point(0, 0, 1);
            var halfQuarter = Transform.RotateY(Math.PI / 4);
            var fullQuarter = Transform.RotateY(Math.PI / 2);
            var halfExpected = Double4.Point(Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2);
            var fullExpected = Double4.Point(1, 0, 0);
            const double eps = 0.0000001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(halfExpected, halfQuarter * p, comparer);
            Assert.Equal(fullExpected, fullQuarter * p, comparer);
        }


        [Fact]
        public void TestRotatePointAroundZAxis()
        {
            var p = Double4.Point(0, 1, 0);
            var halfQuarter = Transform.RotateZ(Math.PI / 4);
            var fullQuarter = Transform.RotateZ(Math.PI / 2);
            var halfExpected = Double4.Point(-Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0);
            var fullExpected = Double4.Point(-1, 0, 0);
            const double eps = 0.0000001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(halfExpected, halfQuarter * p, comparer);
            Assert.Equal(fullExpected, fullQuarter * p, comparer);
        }

        [Fact]
        public void TestSharingMovesXInProportionToY()
        {
            var t = Transform.Shear(1, 0, 0, 0, 0, 0);
            var p = Double4.Point(2, 3, 4);
            var expected = Double4.Point(5, 3, 4);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestSharingMovesXInProportionToZ()
        {
            var t = Transform.Shear(0, 1, 0, 0, 0, 0);
            var p = Double4.Point(2, 3, 4);
            var expected = Double4.Point(6, 3, 4);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestSharingMovesYInProportionToX()
        {
            var t = Transform.Shear(0, 0, 1, 0, 0, 0);
            var p = Double4.Point(2, 3, 4);
            var expected = Double4.Point(2, 5, 4);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestSharingMovesYInProportionToZ()
        {
            var t = Transform.Shear(0, 0, 0, 1, 0, 0);
            var p = Double4.Point(2, 3, 4);
            var expected = Double4.Point(2, 7, 4);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestSharingMovesZInProportionToX()
        {
            var t = Transform.Shear(0, 0, 0, 0, 1, 0);
            var p = Double4.Point(2, 3, 4);
            var expected = Double4.Point(2, 3, 6);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestSharingMovesZInProportionToY()
        {
            var t = Transform.Shear(0, 0, 0, 0, 0, 1);
            var p = Double4.Point(2, 3, 4);
            var expected = Double4.Point(2, 3, 7);
            Assert.Equal(expected, t * p);
        }

        [Fact]
        public void TestIndividualTransformationsAppliedInSequence()
        {
            var a = Transform.RotateX(Math.PI / 2);
            var b = Transform.Scale(5, 5, 5);
            var c = Transform.Translate(10, 5, 7);
            var p1 = Double4.Point(1, 0, 1);
            var p2 = a * p1;
            var p3 = b * p2;
            var p4 = c * p3;
            const double eps = 0.000001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(Double4.Point(1, -1, 0), p2, comparer);
            Assert.Equal(Double4.Point(5, -5, 0), p3, comparer);
            Assert.Equal(Double4.Point(15, 0, 7), p4, comparer);
        }

        [Fact]
        public void TestChainedTransformationsAppliedInReverseOrder()
        {
            var a = Transform.RotateX(Math.PI / 2);
            var b = Transform.Scale(5, 5, 5);
            var c = Transform.Translate(10, 5, 7);
            var p = Double4.Point(1, 0, 1);
            var t = c * b * a;
            // Note that we can execute this with higher precision 
            // than if we would apply the transformations in sequence like
            // in the previous test case.
            const double eps = 0.0000001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(Double4.Point(15, 0, 7), t * p, comparer);
        }

        [Fact]
        public void TestTransformationMatrixForDefaultOrientation()
        {
            var from = Double4.Point(0, 0, 0);
            var to = Double4.Point(0, 0, -1);
            var up = Double4.Vector(0, 1, 0);
            var t = Transform.View(from, to, up);
            Assert.Equal(Double4x4.Identity, t);
        }

        [Fact]
        public void TestViewTransformLookingInPositiveZDirection()
        {
            var from = Double4.Point(0, 0, 0);
            var to = Double4.Point(0, 0, 1);
            var up = Double4.Vector(0, 1, 0);
            var t = Transform.View(from, to, up);
            var expected = Transform.Scale(-1, 1, -1);
            Assert.Equal(expected, t);
        }

        [Fact]
        public void TestViewTransformMovesTheWorld()
        {
            var from = Double4.Point(0, 0, 8);
            var to = Double4.Point(0, 0, 0);
            var up = Double4.Vector(0, 1, 0);
            var t = Transform.View(from, to, up);
            var expected = Transform.Translate(0, 0, -8);
            Assert.Equal(expected, t);
        }

        [Fact]
        public void TestAbitraryViewTransform()
        {
            var from = Double4.Point(1, 3, 2);
            var to = Double4.Point(4, -2, 8);
            var up = Double4.Vector(1, 1, 0);
            var t = Transform.View(from, to, up);
            var expected =
                new Double4x4(
                    -0.50709, 0.50709, 0.67612, -2.36643,
                    0.76772, 0.60609, 0.12122, -2.82843,
                    -0.35857, 0.59761, -0.71714, 0.00000,
                    0.00000, 0.00000, 0.00000, 1.00000);
            const double epsilon = 0.00001;
            var comparer = Double4x4.GetEqualityComparer(epsilon);
            Assert.Equal(expected, t, comparer);
        }
    }
}
