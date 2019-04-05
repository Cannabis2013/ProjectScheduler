using System;
using System.Windows.Forms;
using Projecthandler.Application_facade;

namespace Projecthandler
{
    internal static class Program
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