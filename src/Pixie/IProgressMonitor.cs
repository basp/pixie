// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;
    
    public interface IProgressMonitor : IDisposable
    {
        void OnRowFinished();
    }
}
