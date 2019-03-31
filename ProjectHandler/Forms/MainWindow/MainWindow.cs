using System.Windows.Forms;
using VirtualUserDomain;
using System;
using ProjectNameSpace;
using System.Collections.Generic;
using Projecthandler.Forms.Project;

namespace MainUserSpace
{
    public partial class MainWindow : Form
    {

        public MainWindow(UserManager uManager, ProjectManager pManager)
        {
            InitializeComponent();
            ListViewItem item = new ListViewItem();
            this.uManager = uManager;
            this.pManager = pManager;
            pView = ProjectListView;
            aView = ActivityListView;

            updateProjectListView();
        }

        private void updateProjectListView()
        {
            pView.Clear();
            pView.Items.AddRange(pManager.projectItemModels());
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uManager.logout(UserManager.getLocalAddress());
            logoutEvent?.Invoke(this, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            uManager.logout(UserManager.getLocalAddress());
            closeEvent?.Invoke(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(uManager.verifyUserState(UserManager.getLocalAddress()) == User.UserRole.Admin)
            {
                var pMng = new ProjectManagement(pManager,uManager);
                pMng.updateParentView += _updateParentView;
                pMng.ShowDialog(this);
            }
        }

        private void _updateParentView(object sender, EventArgs e)
        {
            updateProjectListView();
        }

        private void ProjectListView_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems = pView.SelectedItems;
            ListViewItem item = selectedItems[0];
            int index = pView.Items.IndexOf(item);
            
            Project p = pManager.project(index);

            ListViewItem[] aModels = p.activityItemModels();

            ActivityListView.Items.AddRange(aModels);
        }

        public event EventHandler<EventArgs> logoutEvent;
        public event EventHandler<EventArgs> closeEvent;
        private UserManager uManager;
        private ProjectManager pManager;
        private ListView pView, aView;
    }
}
