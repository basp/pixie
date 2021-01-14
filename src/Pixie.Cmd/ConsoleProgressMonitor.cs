namespace Pixie.Cmd
{
    using System;
    using ShellProgressBar;

    public class ConsoleProgressMonitor : IProgressMonitor
    {
        private readonly ProgressBar progressBar;

        public ConsoleProgressMonitor(int rows, string msg = "rendering")
        {
            var options = new ProgressBarOptions
            {
                ForegroundColor = ConsoleColor.Yellow,
                BackgroundColor = ConsoleColor.DarkYellow,
                ProgressCharacter = '─',
            };

            this.progressBar = new ProgressBar(rows, msg, options);
        }

        public void Dispose()
        {
            this.progressBar.Message = "done";
            this.progressBar.Dispose();
        }

        public void OnRowFinished()
        {
            this.progressBar.Tick();
        }
    }
}