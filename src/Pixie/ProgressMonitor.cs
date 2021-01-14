// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    public class ProgressMonitor : IProgressMonitor
    {
        public void Dispose() { }

        public virtual void OnRowFinished() { }
    }
}
