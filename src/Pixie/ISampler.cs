// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;
    using Linie;

    /// <summary>
    /// Returns a color value for the given pixel coordinate.
    /// </summary>
    [Obsolete]
    public interface ISampler
    {
        Color Sample(int x, int y);
    }
}
