using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LogMonitor.Properties;
using Microsoft.Win32;

namespace LogMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           
            //Show system tray icon
            using (ProcessIcon pi = new ProcessIcon())
            {
                pi.Display();

                Application.Run();
            }
            
        }
    }
}
