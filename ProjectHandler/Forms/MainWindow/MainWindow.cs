using System;
using System.Text;
using System.Windows.Forms;
using ProjectNameSpace;
using VirtualUserDomain;

// ReSharper disable InconsistentNaming

namespace MainUserSpace
{
    public partial class MainWindow : Form
    {
        private readonly ListView aView;
        private readonly ProjectManager pManager;
        private AnchorStyles anchorStyles;

        public MainWindow(ProjectManager pManager)
        {
            InitializeComponent();

            var item = new ListViewItem();

            this.pManager = pManager;
            aView = ActivityListView;

            var welcomingText = new StringBuilder("Welcome ");
            var userName = UserManager.currentlyLoggedIn().fullName();
            welcomingText.Append(userName);

            WelcomeLabel.Text = welcomingText.ToString();

            if (UserManager.verifyUserState() != User.UserRole.Admin) updateActivityView();
        }

        private void updateActivityView()
        {
            var assignedActivityModels = pManager.userAssignedActivityModels();
            aView.Clear();
            aView.View = View.Details;

            aView.Columns.Add("Id", 160, HorizontalAlignment.Left);
            aView.Columns.Add("Activity time duration", 160, HorizontalAlignment.Left);
            aView.Columns.Add("Total registered hours", 160, HorizontalAlignment.Left);
            aView.Columns.Add("Project", 160, HorizontalAlignment.Left);

            aView.Items.AddRange(assignedActivityModels);
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserManager.logout(UserManager.getLocalAddress());
            logoutEvent?.Invoke(this, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserManager.logout(UserManager.getLocalAddress());
            closeEvent?.Invoke(this, e);
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (UserManager.verifyUserState() == User.UserRole.Admin)
            {
                var pMng = new ProjectManagement(pManager);
                pMng.ShowDialog(this);
            }
            else
            {
                MessageBox.Show(@"Administrator privilliges required YOU FUCKING NAZI PIG!");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var pMng = new ActivityManagement(pManager);
            pMng.updateParentView += _updateParentView;
            pMng.ShowDialog(this);
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            linkLabel1_LinkClicked(this, null);
        }

        private void _updateParentView(object sender, EventArgs e)
        {
            updateActivityView();
        }

        public event EventHandler<EventArgs> logoutEvent;
        public event EventHandler<EventArgs> closeEvent;
    }
}