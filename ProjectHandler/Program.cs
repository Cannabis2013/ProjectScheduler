using System;
using System.Windows.Forms;
using MainDomain;
using Projecthandler.Class_forms;

namespace Projecthandler
{
    internal class Program
    {
        private readonly MainApp app = new MainApp();
        private LoginView view;
        private bool ExitApplication = true;

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var program = new Program();
            Application.Run();
        }

        public Program()
        {
            LaunchLoginView();
        }

        private void LaunchLoginView()
        {
            view = new LoginView(app);
            view.OnAccessGranted += _OnAccessGranted;
            view.onFormClose += LoginView_OnFormClose;

            view.Show();
        }

        private void _OnAccessGranted(object sender, EventArgs e)
        {
            ExitApplication = false;
            var pView = new ProjectView(app);

            pView.LogoutEvent += ProjectView_LogutEvent;
            pView.HardCloseEvent += ProjectView_HardCloseEvent;

            pView.Show();

        }

        private void LoginView_OnFormClose(object sender, EventArgs e)
        {
            if (ExitApplication)
            {
                app.WritePersistence();
                Application.Exit();
            }
            
            view.Dispose();
        }

        private void ProjectView_LogutEvent(object sender, EventArgs e)
        {
            var view = (ProjectView) sender;
            view.Dispose();
            ExitApplication = true;
            LaunchLoginView();
        }

        private void ProjectView_HardCloseEvent(object sender, EventArgs e)
        {
            app.WritePersistence();
            var pView = (ProjectView) sender;
            pView.Dispose();
            Application.Exit();
        }
        
    }
}