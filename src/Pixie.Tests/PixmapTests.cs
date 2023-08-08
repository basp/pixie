namespace Pixie.Tests;

public class PixmapTests
{
    [Fact]
    public void ItsJustATwoDimensionalArrayOfColorsBasically()
    {
        var ppm = new Pixmap(16, 16)
        {
            [0, 0] = new(1, 1, 1),
            [13, 12] = new(0.5, 0.5, 0.25),
            [12, 13] = new(0.25, -0.5, 0.5),
            [15, 15] = new(1, 1, 1),
        };

        // We need to be sure the indexer works at least since this is the
        // most important operation. A <c>Pixmap</c> is conceptually just a 
        // 2D container of <c>Color</c> values.
        for (var j = 0; j < ppm.Height; j++)
        {
            for (var i = 0; i < ppm.Width; i++)
            {
                var want = (i, j) switch
                {
                    (0, 0) => new Color<double>(1),
                    (13, 12) => new Color<double>(0.5, 0.5, 0.25),
                    (12, 13) => new Color<double>(0.25, -0.5, 0.5),
                    (15, 15) => new Color<double>(1),
                    _ => new Color<double>(0),
                };

                var ans = ppm[i, j];
                Assert.Equal(want, ans);
            }
        }

        // These are only checked in DEBUG config. Internally, the <c>Pixmap<c>
        // is represented as a linear (1D) array of colors. When building with
        // a RELEASE config these bounds will not be properly checked. For
        // example, indexing a <c>Pixmap(3,3)</c> using <c>ppm[4,1]</c> (even
        // though this is clearly out of bounds) will be accepted in RELEASE
        // but not in DEBUG builds.
        Assert.Throws<IndexOutOfRangeException>(() => ppm[16, 16]);
        Assert.Throws<IndexOutOfRangeException>(() => ppm[-1, -1]);
        Assert.Throws<IndexOutOfRangeException>(() => ppm[16, 0]);
        Assert.Throws<IndexOutOfRangeException>(() => ppm[0, 16]);
        Assert.Throws<IndexOutOfRangeException>(() => ppm[15, -1]);
        Assert.Throws<IndexOutOfRangeException>(() => ppm[-1, 15]);
    }
}