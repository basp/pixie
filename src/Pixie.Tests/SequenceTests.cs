namespace Pixie.Tests
{
    using Xunit;

    public class SequenceTests
    {
        [Fact]
        public void NumberGeneratorReturnsCyclicSequence()
        {
            var gen = new Sequence(0.1, 0.5, 1.0);
            Assert.Equal(0.1, gen.Next());
            Assert.Equal(0.5, gen.Next());
            Assert.Equal(1.0, gen.Next());
            Assert.Equal(0.1, gen.Next());
        }
    }
}