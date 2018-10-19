namespace Pixie.Facts
{
    using System;
    using Pixie;
    using Xunit;

    public class Vector2Facts
    {
        [Fact]
        public void ConstructWithValue()
        {
            const float value = 0.123f;
            var v = new Vector2(value);
            Assert.Equal(value, v.X);
            Assert.Equal(value, v.Y);
        }

        [Fact]
        public void ContructWithDimensions()
        {
            const float x = 0.123f;
            const float y = 0.456f;
            var v = new Vector2(x, y);
            Assert.Equal(x, v.X);
            Assert.Equal(y, v.Y);
        }
    }
}
