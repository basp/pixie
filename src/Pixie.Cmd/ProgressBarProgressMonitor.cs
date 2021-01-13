namespace Pixie.Cmd
{
    using System;
    using ShellProgressBar;

    public class ProgressBarProgressMonitor : IProgressMonitor
    {
        private readonly ProgressBar progressBar;

        public ProgressBarProgressMonitor(int rows)
        {
            var options = new ProgressBarOptions
            {
                ForegroundColor = ConsoleColor.Yellow,
                BackgroundColor = ConsoleColor.DarkYellow,
                ProgressCharacter = '─',
            };

            this.progressBar = new ProgressBar(rows, "rendering", options);
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