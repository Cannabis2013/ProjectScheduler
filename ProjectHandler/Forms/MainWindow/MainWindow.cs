using System.Windows.Forms;
using VirtualUserDomain;
using System;
using System.Drawing;
using System.Text;
using ProjectNameSpace;
using Templates;
// ReSharper disable InconsistentNaming

namespace MainUserSpace
{
    public partial class MainWindow : Form
    {

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

            if (UserManager.verifyUserState(UserManager.getLocalAddress()) != User.UserRole.Admin)
            {
                updateActivityView();
            }
        }

        private void updateActivityView()
        {
            User cUser = UserManager.currentlyLoggedIn();
            var assignedActivityModels = cUser.assignedActivityModels();
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
            if (UserManager.verifyUserState(UserManager.getLocalAddress()) == User.UserRole.Admin)
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
            pMng.ShowDialog(this);
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            linkLabel1_LinkClicked(this,e: null);
        }

        public event EventHandler<EventArgs> logoutEvent;
        public event EventHandler<EventArgs> closeEvent;
        private ProjectManager pManager;
        private ListView aView;
        private AnchorStyles anchorStyles;
    }
}
