namespace Linsi.Tests
{
    using Xunit;

    public class Vector2Tests
    {
        [Fact]
        public void TestCtor()
        {
            var u = new Vector2(2, 3);
            Assert.Equal(2, u.X);
            Assert.Equal(3, u.Y);
        }

        [Fact]
        public void TestVectorAddition()
        {
            var u = new Vector2(2, 3);
            var v = new Vector2(-1, -2);
            var w = u + v;
            Assert.Equal(1, w.X);
            Assert.Equal(1, w.Y);
        }
    }
}