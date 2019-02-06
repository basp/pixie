namespace Pixie.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Pixie.Core;

    public class Double2x2Tests
    {
        const double epsilon = 0.000000001;

        [Fact]
        public void TestCalculateDeterminant()
        {
            var m = new Double2x2(1, 5, -3, 2);
            Assert.Equal(17, m.Determinant());
        }
    }
}