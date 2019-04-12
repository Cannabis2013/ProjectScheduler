using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Projecthandler.Class_forms;
using Projecthandler.Custom_events;
using ProjectRelated;
using UserDomain;


namespace MainDomain
{
    public partial class MainApp
    {
        private const string FileName = "ProjectFile";
        private readonly ProjectManager pManager;
        private readonly UserManager uManager = new UserManager();

        private bool isLastWindow = true;

        public MainApp()
        {
            if (File.Exists(FileName))
            {
                Stream openFileStream = File.OpenRead("ProjectFile");
                var deserializer = new BinaryFormatter();
                try
                {
                    pManager = (ProjectManager)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
                }
                catch (Exception)
                {
                    openFileStream.Close();
                    File.Delete(FileName);
                    pManager = new ProjectManager();
                }   
            }
            else
                pManager = new ProjectManager();


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
            lView.onFormClose += loginView_onFormClose;

            lView.Show();
            if (uName != null && pass != null)
                lView.enterCredentialsManual(uName, pass);
        }

        private void loginView_OnSubmitClicked(object sender, CredentialArguments e)
        {
            var lView = (LoginView) sender;
            if (uManager.logIn(e.arg1, e.arg2))
            {
                isLastWindow = false;
                var view = new ProjectView(pManager,uManager);
                view.logoutEvent += _logoutEvent;
                view.CloseRequest += _CloseRequest;
                view.HardCloseEvent += _HardCloseEvent;

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
            if (!isLastWindow)
                return;

            Persistence();
            Application.Exit();
        }

        private void _logoutEvent(object sender, EventArgs e)
        {
            var view = (ProjectView) sender;
            view.Close();
        }

        private void _CloseRequest(object sender, EventArgs e)
        {
            isLastWindow = true;
            LaunchLoginView();
        }

        private void _HardCloseEvent(object sender, EventArgs e)
        {
            Persistence();
            Application.Exit();
        }

        private void Persistence()
        {
            Stream saveFileStream = File.Create(FileName);
            var serializer = new BinaryFormatter();
            serializer.Serialize(saveFileStream, pManager);
            saveFileStream.Close();
        }
    }
}