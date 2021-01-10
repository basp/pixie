namespace Pixie.Cmd
{
    using System;
    using System.Threading;

    public class ParallelConsoleProgressMonitor : ProgressMonitor
    {
        private int rowsProcessed = 0;
        private readonly int vsize;

        public ParallelConsoleProgressMonitor(int vsize)
        {
            // We assume that we're parallelizing on the vertical 
            // dimenension since rows usually have the biggest workload. 
            // Since we don't know in what order the rows will finish we 
            // will just keep count of the number of rows we need to render.
            this.vsize = vsize;
        }

        public override void OnRowFinished(int row)
        {
            // Since row progress will be fired out of order due to 
            // parallelism, the best we can do is just keep a total 
            // count of the rows processed.
            Interlocked.Increment(ref this.rowsProcessed);
            Console.WriteLine(
                $"{this.rowsProcessed}/{this.vsize}");
        }
    }
}