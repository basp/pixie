namespace Pixie.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Pixie.Core;


    public class ColorTests
    {
        static IEqualityComparer<Color> Comparer =
            new ApproxColorEqualityComparer(0.000001f);

        [Fact]
        public void TestAddColors()
        {
            var c1 = new Color(0.9f, 0.6f, 0.75f);
            var c2 = new Color(0.7f, 0.1f, 0.25f);
            var expected = new Color(1.6f, 0.7f, 1.00f);
            Assert.Equal(expected, c1 + c2, Comparer);
        }

        [Fact]
        public void TestSubtractColors()
        {
            var c1 = new Color(0.9f, 0.6f, 0.75f);
            var c2 = new Color(0.7f, 0.1f, 0.25f);
            var expected = new Color(0.2f, 0.5f, 0.5f);
            Assert.Equal(expected, c1 - c2, Comparer);
        }

        [Fact]
        public void TestScaleColorByScalar()
        {
            var c = new Color(0.2f, 0.3f, 0.4f);
            var expected = new Color(0.4f, 0.6f, 0.8f);
            Assert.Equal(expected, c * 2, Comparer);
        }

        [Fact]
        public void TestHadamardProduct()
        {
            var c1 = new Color(1.0f, 0.2f, 0.4f);
            var c2 = new Color(0.9f, 1.0f, 0.1f);
            var expected = new Color(0.9f, 0.2f, 0.04f);
            Assert.Equal(expected, c1 * c2, Comparer);
        }
    }
}