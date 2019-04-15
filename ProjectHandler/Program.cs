using System;
using System.Windows.Forms;
using MainDomain;

namespace Projecthandler
{
    internal class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var app = new MainApp();
            Application.Run();
        }
    }
}