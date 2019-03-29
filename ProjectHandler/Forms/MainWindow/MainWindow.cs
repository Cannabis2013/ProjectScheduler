using System.Windows.Forms;
using VirtualUserDomain;
using System;
using ProjectNameSpace;
using System.Collections.Generic;

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
            List<ListViewItem> models = pManager.projectItemModels();
            foreach (ListViewItem model in models)
            {
                pView.Items.Add(model);
            }
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


        public event EventHandler<EventArgs> logoutEvent;
        public event EventHandler<EventArgs> closeEvent;
        private UserManager uManager;
        private ProjectManager pManager;
        private ListView pView, aView;

        private void ProjectListView_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems = pView.SelectedItems;
            ListViewItem item = selectedItems[0];
            int index = pView.Items.IndexOf(item);
            
            Project p = pManager.project(index);

            ListViewItem[] aModels = p.activityItemModels();

            ActivityListView.Items.AddRange(aModels);
        }
        
    }

}
