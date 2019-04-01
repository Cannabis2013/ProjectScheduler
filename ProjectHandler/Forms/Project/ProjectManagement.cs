using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ProjectNameSpace;
using DialogNamespace;
using Projecthandler.Events;
using Templates;

namespace ProjectNameSpace
{
    public partial class ProjectManagement : Form
    {
        public ProjectManagement(ProjectManager pManager)
        {
            InitializeComponent();
            this.pManager = pManager ?? throw new System.ArgumentNullException(nameof(pManager));

            pView = ProjectListView;
            
            updateView();
        }

        private void updateView()
        {
            pView.Clear();
            pView.View = View.Details;
            pView.TileSize = new Size(120,80);
            const int columnWidth = 120;
            pView.Columns.Add("Project title", columnWidth, HorizontalAlignment.Left);
            pView.Columns.Add("Project leader", columnWidth, HorizontalAlignment.Left);
            pView.Columns.Add("Start week", columnWidth, HorizontalAlignment.Left);
            pView.Columns.Add("Estimated end week", columnWidth, HorizontalAlignment.Left);
            pView.Columns.Add("Number of users assigned", 180, HorizontalAlignment.Left);
            pView.Items.AddRange(pManager.projectItemModels(ItemModelEntity<ListViewItem>.ListMode.List));
        }

        private void _OnSubmitPushed(object sender, SubmitEvent e)
        {
            var p = new Project(e.pTitle) {startWeek = e.sWeek, endWeek = e.eWeek};
            p.assignUsersToProject(e.users);
            p.projectLeaderId = e.pLeader;

            pManager.addProject(p);

            updateView();
        }

        private void _OnEditPushed(object sender, EventArgs e)
        {
            updateView();
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var pDialog = new ProjectDialog();
            pDialog.OnSubmitPushed += _OnSubmitPushed;
            pDialog.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ProjectListView.SelectedItems.Count < 1)
                return;
            var cIndex = ProjectListView.SelectedIndices[0];
            var project = pManager.project(cIndex);

            var pDialog = new ProjectDialog(project);
            pDialog.OnEditPushed += _OnEditPushed;
            pDialog.ShowDialog(this);
        }

        public event EventHandler<EventArgs> updateParentView;

        private readonly ListView pView;
        private readonly ProjectManager pManager;

        private void button3_Click(object sender, EventArgs e)
        {
            if (ProjectListView.SelectedItems.Count != 1)
                return;
            
            var cIndex = ProjectListView.SelectedIndices[0];
            var buttons = MessageBoxButtons.YesNo;
            if(MessageBox.Show("Are you sure?", "Confirm", buttons) == DialogResult.Yes)
            {
                pManager.removeProjectAt(cIndex);
                updateView();
            }
        }

        private void ProjectManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            updateParentView?.Invoke(this, e);
        }

        private void ProjectListView_DoubleClick(object sender, EventArgs e)
        {
            if (ProjectListView.Items.Count < 1)
                return;

            var cIndex = ProjectListView.SelectedIndices[0];

            var p = pManager.project(cIndex);

            var pDialog = new ProjectDialog(p);
            pDialog.OnEditPushed += _OnEditPushed;
            pDialog.ShowDialog(this);
        }
    }
}
