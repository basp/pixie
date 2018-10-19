namespace Pixie.Facts
{
    using System;
    using Pixie;
    using Xunit;

    public class MatrixFacts
    {
        [Fact]
        public void ContructWithRows()
        {
            var row1 = new Vector4(0.11f, 0.12f, 0.13f, 0.14f);
            var row2 = new Vector4(0.21f, 0.22f, 0.23f, 0.24f);
            var row3 = new Vector4(0.31f, 0.32f, 0.33f, 0.34f);
            var row4 = new Vector4(0.41f, 0.42f, 0.43f, 0.44f);            
            
            var m = new Matrix(row1, row2, row3, row4);
            
            Assert.Equal(0.11f, m.M11);
            Assert.Equal(0.12f, m.M12);
            Assert.Equal(0.13f, m.M13);
            Assert.Equal(0.14f, m.M14);

            Assert.Equal(0.21f, m.M21);
            Assert.Equal(0.22f, m.M22);
            Assert.Equal(0.23f, m.M23);
            Assert.Equal(0.24f, m.M24);

            Assert.Equal(0.31f, m.M31);
            Assert.Equal(0.32f, m.M32);
            Assert.Equal(0.33f, m.M33);
            Assert.Equal(0.34f, m.M34);

            Assert.Equal(0.41f, m.M41);
            Assert.Equal(0.42f, m.M42);
            Assert.Equal(0.43f, m.M43);
            Assert.Equal(0.44f, m.M44);
        }     
    }
}
