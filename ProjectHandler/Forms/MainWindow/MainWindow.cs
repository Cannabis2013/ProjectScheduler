using System;
using System.Text;
using System.Windows.Forms;
using Mng;
using ProjectRelated;
using VirtualUserDomain;

// ReSharper disable InconsistentNaming

namespace MainUserSpace
{
    public partial class MainWindow : Form
    {
        private readonly ListView aView;
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

        public MainWindow(ProjectManager pManager, UserManager uManager)
        {
            InitializeComponent();

            var item = new ListViewItem();

            this.pManager = pManager;
            this.uManager = uManager;
            aView = ActivityListView;

            var welcomingText = new StringBuilder("Welcome ");
            var userName = uManager.currentlyLoggedIn().FullName();
            welcomingText.Append(userName);

            WelcomeLabel.Text = welcomingText.ToString();

            if (uManager.verifyUserState() != User.UserRole.Admin)
                _updateParentView(this,null);
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uManager.logout(UserManager.getLocalAddress());
            logoutEvent?.Invoke(this, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HardCloseEvent?.Invoke(this,e);
        }

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            uManager.logout(UserManager.getLocalAddress());
            CloseRequest?.Invoke(this, e);
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var mng = new Management(pManager,uManager);
            mng.updateParentView += _updateParentView;
            mng.ShowDialog(this);
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            linkLabel1_LinkClicked(this, null);
        }

        private void _updateParentView(object sender, EventArgs e)
        {
            var assignedActivityModels = pManager.UserAssignedActivityModels(uManager);
            aView.Clear();
            aView.View = View.Details;

            aView.Columns.Add("Id", 160, HorizontalAlignment.Left);
            aView.Columns.Add("Activity time duration", 160, HorizontalAlignment.Left);
            aView.Columns.Add("Total registered hours", 160, HorizontalAlignment.Left);
            aView.Columns.Add("Project", 160, HorizontalAlignment.Left);

            aView.Items.AddRange(assignedActivityModels);
        }

        public event EventHandler<EventArgs> logoutEvent;
        public event EventHandler<EventArgs> CloseRequest;
        public event EventHandler<EventArgs> HardCloseEvent;
    }
}