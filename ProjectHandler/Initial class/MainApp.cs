using Projecthandler.Class_forms;
using Projecthandler.Custom_events;
using VirtualUserDomain;
using System;

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
                    AdminView view = new AdminView(uManager);
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

        private void mView_logoutEvent(object sender, EventArgs e)
        {
            AdminView view = (AdminView) sender;
            view.Close();
        }

        private void mView_closeEvent(object sender, EventArgs e)
        {
            launchLoginView();
        }
        
        private UserManager uManager = new UserManager();
    }
}
