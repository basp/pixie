namespace Pixie.Cmd
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using Pixie.Core;

    public class ParallelConsoleProgressMonitor : ProgressMonitor
    {
        private int rowsProcessed = 0;
        private readonly int vsize;

        public ParallelConsoleProgressMonitor(int vsize)
        {
            this.vsize = vsize;
        }

        public override void OnRowFinished(int row)
        {
            Interlocked.Increment(ref this.rowsProcessed);
            Console.WriteLine(
                $"{this.rowsProcessed}/{this.vsize}");
        }
    }
}