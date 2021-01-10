namespace Pixie.Cmd
{
    using System;

    public class DefaultProgressMonitor : IProgressMonitor
    {
        public void Dispose() { }

        public void OnRowFinished() { }
    }
}