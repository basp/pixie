namespace Pixie
{
    using System;
    
    public interface IProgressMonitor : IDisposable
    {
        void OnRowFinished();
    }
}