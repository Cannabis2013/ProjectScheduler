using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Projecthandler.Class_forms;
using Projecthandler.Custom_events;
using ProjectNameSpace;
using VirtualUserDomain;

namespace MainUserSpace
{
    public class MainApp
    {
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

        private bool isLastWindow = true;

        public MainApp()
        {
            if (File.Exists("ProjectFile"))
            {
                Stream openFileStream = File.OpenRead("ProjectFile");
                BinaryFormatter deserializer = new BinaryFormatter();
                pManager = (ProjectManager)deserializer.Deserialize(openFileStream);

                openFileStream.Close();
            }
            else
            {
                pManager = new ProjectManager();
            }
            
            uManager = new UserManager(pManager);

            launchLoginView();
        }

        // For testing purposes
        public MainApp(string p0, string p1)
        {
            launchLoginView(p0, p1);
        }

        private void launchLoginView(string uName = null, string pass = null)
        {
            var lView = new LoginView();

            lView.OnSubmitClicked += loginView_OnSubmitClicked;
            lView.onFormClose += loginView_onFormClose;

            lView.Show();
            if (uName != null && pass != null)
                lView.enterCredentialsManual(uName, pass);
        }

        private void loginView_OnSubmitClicked(object sender, MyEventArgs e)
        {
            var lView = (LoginView) sender;
            if (UserManager.logIn(e.arg1, e.arg2, UserManager.getLocalAddress()))
            {
                isLastWindow = false;
                var view = new MainWindow(pManager);
                view.logoutEvent += mView_logoutEvent;
                view.closeEvent += mView_closeEvent;

                lView.Close();
                view.Show();
            }
            else
            {
                // Do something with LoginView
                lView.setWarningText("Wrong credentials entered.");
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
            launchLoginView();
        }
    }
}