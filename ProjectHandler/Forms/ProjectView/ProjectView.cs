using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mng;
using Projecthandler.Events;
using Projecthandler.Forms.Dialogs;
using ProjectRelated;
using Templates;
using VirtualUserDomain;

// ReSharper disable InconsistentNaming

namespace MainUserSpace
{
    public partial class ProjectView : Form
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

            var welcomingText = new StringBuilder("Welcome ");
            var userName = uManager.loggedIn().UserName();
            welcomingText.Append(userName);

            WelcomeLabel.Text = welcomingText.ToString();
            
            updateModelViews();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uManager.logout();
            logoutEvent?.Invoke(this, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HardCloseEvent?.Invoke(this,e);
        }

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            uManager.logout();
            CloseRequest?.Invoke(this, e);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var mng = new Management(pManager,uManager);
            mng.updateParentView += Management_updateParentView;
            mng.ShowDialog(this);
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            linkLabel1_LinkClicked(this, null);
        }

        private void Management_updateParentView(object sender, EventArgs e)
        {
            updateModelViews();
        }

        private void Registration_OnSaveClicked(object sender, EventArgs e)
        {
            var sEvent = (SubmitEvent) e;

            var rObject = sEvent.RegistrationObject();
            var parentActivityId = rObject.ParentActivityId;

            var activity = pManager.Activity(parentActivityId);
            activity.AddRegistrationObject(rObject);

            updateModelViews();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActivityListView.SelectedItems.Count < 1)
                return;
            var activityId = ActivityListView.SelectedItems[0].Text;
            var rDialog = new AddRegistrationDialogForm(pManager,uManager,activityId);
            rDialog.OnSaveClicked += Registration_OnSaveClicked;

            rDialog.ShowDialog(this);
        }

        private void updateModelViews()
        {
            var activityModels = pManager.ProjectActivityItemModels(uManager);
            aView.Clear();
            aView.View = View.Details;

            aView.Columns.Add("Id", 160, HorizontalAlignment.Left);
            aView.Columns.Add("Activity time duration", 160, HorizontalAlignment.Left);
            aView.Columns.Add("Total registered hours", 160, HorizontalAlignment.Left);
            aView.Columns.Add("Project", 160, HorizontalAlignment.Left);

            aView.Items.AddRange(activityModels);

            RegistrationHourListView.Clear();
            RegistrationHourListView.View = View.Details;

            RegistrationHourListView.Columns.Add("Registration id", 60, HorizontalAlignment.Left);
            RegistrationHourListView.Columns.Add("User", 60, HorizontalAlignment.Left);
            RegistrationHourListView.Columns.Add("Original registration date", 60, HorizontalAlignment.Left);
            RegistrationHourListView.Columns.Add("Work hours registrated", 60, HorizontalAlignment.Left);
            RegistrationHourListView.Columns.Add("Parent activity", 60, HorizontalAlignment.Left);

            var listMode = ModelEntity<ListViewItem>.ListMode.List;

            ListViewItem[] regObjects = uManager.isAdmin() ?
                regObjects = pManager.HourRegistrationObjects().Select(item => item.ItemModel(listMode)).ToArray() :
                regObjects = pManager.HourRegistrationObjects(uManager.loggedIn().UserName()).Select(item => item.ItemModel(listMode))
                    .ToArray();

            RegistrationHourListView.Items.AddRange(regObjects);
        }
    }
}