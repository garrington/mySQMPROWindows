using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
// added for single instance
using System.Threading;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace mySQMPRO
{
    static class Program
    {
        // GUId of mySQMPRO
        static Mutex mutex = new Mutex(true, "{6d59e847-c784-4852-b5d1-e880a1d8c50d}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new mySQMPRO());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("Only one instance of mySQMPRO can be run at a time.", "mySQMPRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            // Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new mySQMPRO());
        }
    }
}
