namespace Pixie.Core
{
    public class ProgressMonitor : IProgressMonitor
    {
        public virtual void OnFinished() { }

        public virtual void OnPixelFinished(int row, int col) { }

        public virtual void OnPixelStarted(int row, int col) { }

        public virtual void OnRowFinished(int row) { }

        public virtual void OnRowStarted(int row) { }

        public virtual void OnStarted() { }
    }
}