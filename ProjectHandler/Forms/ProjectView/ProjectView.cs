using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mng;
using Projecthandler.Abstract_classes_and_interfaces;
using Projecthandler.Events;
using Projecthandler.Forms.Dialogs;
using ProjectRelated;
using UserDomain;

// ReSharper disable InconsistentNaming

namespace MainDomain
{
    public partial class ProjectView : Form, ICustomObserver
    {
        private readonly ListView aView;
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;
        
        public event EventHandler<EventArgs> logoutEvent;
        public event EventHandler<EventArgs> CloseRequest;
        public event EventHandler<EventArgs> HardCloseEvent;

        public ProjectView(ProjectManager pManager, UserManager uManager)
        {
            InitializeComponent();

            var item = new ListViewItem();

            this.pManager = pManager;
            this.uManager = uManager;
            aView = ActivityListView;

            pManager.SubScribe(this);

            var welcomingText = new StringBuilder("Welcome ");
            var userName = uManager.loggedIn().ModelIdentity;
            welcomingText.Append(userName);

            WelcomeLabel.Text = welcomingText.ToString();

            UpdateView();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uManager.logout();
            logoutEvent?.Invoke(this, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pManager.UnSubScribeAll();
            HardCloseEvent?.Invoke(this,e);
        }

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            uManager.logout();
            pManager.UnSubScribeAll();
            CloseRequest?.Invoke(this, e);
        }

        public void ManagementLink_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var mng = new Management(pManager,uManager);
            mng.ShowDialog(this);
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagementLink_Clicked(this, null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (uManager.isAdmin())
            {
                MessageBox.Show(@"Admin not allowed to register hour objects. Its beneath your paygrade. Sorry.");
                return;
            }

            if (ActivityListView.SelectedItems.Count < 1)
                return;
            var activityId = ActivityListView.SelectedItems[0].Text;
            var rDialog = new AddRegistrationDialogForm(pManager,uManager,activityId);

            rDialog.ShowDialog(this);
        }

        /*
         * Testing
         */


        public void UpdateView()
        {
            var activityModels = pManager.ActivityItemModels(uManager);
            aView.Clear();
            aView.View = View.Details;

            int columnWidth = 160;

            aView.Columns.Add("Activity title", columnWidth, HorizontalAlignment.Left);
            aView.Columns.Add("Start week", columnWidth, HorizontalAlignment.Left);
            aView.Columns.Add("Estimated end week", columnWidth, HorizontalAlignment.Left);
            aView.Columns.Add("Total registered hours", columnWidth, HorizontalAlignment.Left);
            aView.Columns.Add("Assigned users", columnWidth, HorizontalAlignment.Left);
            aView.Columns.Add("Project", columnWidth, HorizontalAlignment.Left);

            aView.Items.AddRange(activityModels);

            RegistrationHourListView.Clear();
            RegistrationHourListView.View = View.Details;

            columnWidth = 120;

            RegistrationHourListView.Columns.Add("Registration id", columnWidth, HorizontalAlignment.Left);
            RegistrationHourListView.Columns.Add("User", columnWidth, HorizontalAlignment.Left);
            RegistrationHourListView.Columns.Add("Original registration date", columnWidth, HorizontalAlignment.Left);
            RegistrationHourListView.Columns.Add("Work hours registrated", columnWidth, HorizontalAlignment.Left);
            RegistrationHourListView.Columns.Add("Parent activity", columnWidth, HorizontalAlignment.Left);


            ListViewItem[] regObjects = uManager.isAdmin() ?
                regObjects = pManager.RegistrationItemModels() :
                regObjects = pManager.RegistrationItemModels(uManager.loggedIn().ModelIdentity);

            RegistrationHourListView.Items.AddRange(regObjects);
        }
    }
}