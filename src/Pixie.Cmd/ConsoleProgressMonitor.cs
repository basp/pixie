namespace Pixie.Cmd
{
    using System;
    using System.Diagnostics;
    using Pixie.Core;

    public class ConsoleProgressMonitor : ProgressMonitor
    {
        private readonly int rows;
        private readonly Stopwatch sw = new Stopwatch();

        public ConsoleProgressMonitor(int rows)
        {
            this.rows = rows;
        }

        public override void OnRowStarted(int row)
        {
            this.sw.Start();
        }

        public override void OnRowFinished(int row)
        {
            this.sw.Stop();
            Console.WriteLine($"{row++}/{rows} ({sw.Elapsed})");
            this.sw.Reset();
        }
    }
}