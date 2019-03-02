namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class BoundsTests
    {
        [Fact]
        public void CreatingAnEmptyBoundingBox()
        {
            var box = BoundingBox.Empty;
            Assert.Equal(
                Double4.Point(
                    double.PositiveInfinity,
                    double.PositiveInfinity,
                    double.PositiveInfinity),
                box.Min);

            Assert.Equal(
                Double4.Point(
                    double.NegativeInfinity,
                    double.NegativeInfinity,
                    double.NegativeInfinity),
                box.Max);
        }
    }
}