using Projecthandler.Class_forms;
using Projecthandler.Custom_events;
using VirtualUserDomain;
using System;
using System.Windows.Forms;
using ProjectNameSpace;

namespace MainUserSpace
{
    public class MainApp
    {
        public MainApp()
        {
            launchLoginView();
        }

        // For testing purposes
        public MainApp(string p0, string p1)
        {
            launchLoginView(p0,p1);
        }

        private void launchLoginView(string uName = null, string pass = null)
        {
            LoginView lView = new LoginView();

            lView.OnSubmitClicked += loginView_OnSubmitClicked;
            lView.onFormClose += loginView_onFormClose;

            lView.Show();
            if (uName != null && pass != null)
                lView.enterCredentialsManual(uName, pass);
        }

        private void loginView_OnSubmitClicked(object sender, MyEventArgs e)
        {
            LoginView lView = (LoginView) sender;
            if (uManager.logIn(e.arg1, e.arg2, UserManager.getLocalAddress()))
            {
                if (uManager.verifyUserState(UserManager.getLocalAddress()) == User.UserRole.Admin)
                {
                    isLastWindow = false;
                    MainWindow view = new MainWindow(uManager, pManager);
                    view.logoutEvent += mView_logoutEvent;
                    view.closeEvent += mView_closeEvent;

                    lView.Close();
                    view.Show();
                }

            }
            else
            {
                // Do something with LoginView
                lView.setWarningText("Wrong credentials entered. The password is '1234' and username ''admin''. Idiot.");
            }
        }

        private void loginView_onFormClose(object sender, EventArgs e)
        {
            if (isLastWindow)
                Application.Exit();
        }

        private void mView_logoutEvent(object sender, EventArgs e)
        {
            MainWindow view = (MainWindow) sender;
            view.Close();
        }

        private void mView_closeEvent(object sender, EventArgs e)
        {
            isLastWindow = true;
            launchLoginView();
        }

        private bool isLastWindow = true;
        private readonly UserManager uManager = new UserManager();
        private readonly ProjectManager pManager = new ProjectManager();
    }
}
