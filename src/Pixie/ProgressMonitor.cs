namespace Pixie
{
    public class ProgressMonitor : IProgressMonitor
    {
        public void Dispose() { }

        public virtual void OnRowFinished() { }
    }
}