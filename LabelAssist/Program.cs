using System;
using System.Windows.Forms;

namespace LabelAssist
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Enables modern Windows visual styles
            ApplicationConfiguration.Initialize();

            // Start the main window
            Application.Run(new MainForm());
        }
    }
}
