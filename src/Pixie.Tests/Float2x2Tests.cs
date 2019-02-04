namespace Pixie.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Pixie.Core;

    public class Float2x2Tests
    {
        const float epsilon = 0.000000001f;

        [Fact]
        public void TestCalculateDeterminant()
        {
            var m = new Float2x2(1, 5, -3, 2);
            Assert.Equal(17, m.Determinant());
        }
    }
}