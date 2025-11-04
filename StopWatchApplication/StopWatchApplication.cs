using System;
using System.Windows.Forms;

namespace StopwatchApplication
{
    /// <summary>
    /// Program entry point for the Stopwatch Application
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Initializes and runs the stopwatch form.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StopwatchForm());
        }
    }
}