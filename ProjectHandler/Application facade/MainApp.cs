using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Projecthandler.Events;
using Projecthandler.Forms.Login;
using Projecthandler.Forms.MainWindow;
using Projecthandler.Project_related;
using Projecthandler.User_Management;

namespace Projecthandler.Application_facade
{
    public class MainApp
    {
        private readonly ProjectManager projectManager;
        private readonly UserManager userManager;

        private bool isLastWindow = true;

        public MainApp()
        {
            if (File.Exists("ProjectFile"))
            {
                Stream openFileStream = File.OpenRead("ProjectFile");
                BinaryFormatter deserializer = new BinaryFormatter();
                projectManager = (ProjectManager)deserializer.Deserialize(openFileStream);

                openFileStream.Close();
            }
            else
            {
                projectManager = new ProjectManager();
            }
            
            userManager = new UserManager(projectManager);

            LaunchLoginView();
        }

        // For testing purposes
        public MainApp(string p0, string p1)
        {
            LaunchLoginView(p0, p1);
        }

        private void LaunchLoginView(string uName = null, string pass = null)
        {
            var lView = new LoginView();

            lView.OnSubmitClicked += loginView_OnSubmitClicked;
            lView.OnFormClose += loginView_onFormClose;

            lView.Show();
            if (uName != null && pass != null)
                lView.EnterCredentialsManual(uName, pass);
        }

        private void loginView_OnSubmitClicked(object sender, MyEventArgs e)
        {
            var lView = (LoginView) sender;
            if (UserManager.LogIn(e.Arg1, e.Arg2, UserManager.GetLocalAddress()))
            {
                isLastWindow = false;
                var view = new MainWindow(projectManager);
                view.logoutEvent += mView_logoutEvent;
                view.closeEvent += mView_closeEvent;

                lView.Close();
                view.Show();
            }
            else
            {
                // Do something with LoginView
                lView.SetWarningText("Wrong credentials entered.");
            }
        }

        private void loginView_onFormClose(object sender, EventArgs e)
        {
            if (isLastWindow)
                Application.Exit();
        }

        private void mView_logoutEvent(object sender, EventArgs e)
        {
            var view = (MainWindow) sender;
            view.Close();
        }

        private void mView_closeEvent(object sender, EventArgs e)
        {
            isLastWindow = true;
            LaunchLoginView();
        }
    }
}