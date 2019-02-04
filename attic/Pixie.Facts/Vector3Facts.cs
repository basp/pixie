namespace Pixie.Facts
{
    using System;
    using Pixie;
    using Xunit;

    public class Vector3Facts
    {
        [Fact]
        public void ConstructWithValue()
        {
            const float value = 0.123f;
            
            var v = new Vector3(value);
            
            Assert.Equal(value, v.X);
            Assert.Equal(value, v.Y);
            Assert.Equal(value, v.Z);
        }

        [Fact]
        public void ContructWithDimensions()
        {
            const float x = 0.123f;
            const float y = 0.456f;
            const float z = 0.789f;

            var v = new Vector3(x, y, z);
            
            Assert.Equal(x, v.X);
            Assert.Equal(y, v.Y);
            Assert.Equal(z, v.Z);
        }
    }
}
