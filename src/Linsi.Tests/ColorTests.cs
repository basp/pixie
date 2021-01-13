namespace Linsi.Tests
{
    using System.Collections.Generic;
    using Xunit;

    public class ColorTests
    {
        const double epsilon = 0.000001;

        static readonly IEqualityComparer<Color> Comparer =
            Color.GetEqualityComparer(epsilon);

        [Fact]
        public void TestAddColors()
        {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);
            var expected = new Color(1.6, 0.7, 1.00);
            Assert.Equal(expected, c1 + c2, Comparer);
        }

        [Fact]
        public void TestSubtractColors()
        {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);
            var expected = new Color(0.2, 0.5, 0.5);
            Assert.Equal(expected, c1 - c2, Comparer);
        }

        [Fact]
        public void TestScaleColorByScalar()
        {
            var c = new Color(0.2, 0.3, 0.4);
            var expected = new Color(0.4, 0.6, 0.8);
            Assert.Equal(expected, c * 2, Comparer);
        }

        [Fact]
        public void TestHadamardProduct()
        {
            var c1 = new Color(1.0, 0.2, 0.4);
            var c2 = new Color(0.9, 1.0, 0.1);
            var expected = new Color(0.9, 0.2, 0.04);
            Assert.Equal(expected, c1 * c2, Comparer);
        }
    }
}