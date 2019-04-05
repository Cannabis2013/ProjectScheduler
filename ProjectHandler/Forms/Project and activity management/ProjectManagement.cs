using System;
using System.Drawing;
using System.Windows.Forms;
using Projecthandler.Events;
using Projecthandler.Forms.Dialogs;
using Projecthandler.Project_related;
using Projecthandler.Templates;

namespace Projecthandler.Forms.Project_and_activity_management
{
    public partial class ProjectManagement : Form
    {
        private readonly ProjectManager pManager;

        private readonly ListView pView;

        public ProjectManagement(ProjectManager pManager)
        {
            InitializeComponent();
            this.pManager = pManager ?? throw new ArgumentNullException(nameof(pManager));

            pView = ProjectListView;

            UpdateView();
        }

        private void UpdateView()
        {
            pView.Clear();
            pView.View = View.Details;
            pView.TileSize = new Size(120, 80);
            const int columnWidth = 120;
            pView.Columns.Add("Project title", columnWidth, HorizontalAlignment.Left);
            pView.Columns.Add("Project leader", columnWidth, HorizontalAlignment.Left);
            pView.Columns.Add("Start week", columnWidth, HorizontalAlignment.Left);
            pView.Columns.Add("Estimated end week", columnWidth, HorizontalAlignment.Left);
            pView.Items.AddRange(pManager.ProjectItemModels(ItemModelEntity<ListViewItem>.ListMode.List));
        }

        private void _OnSubmitPushed(object sender, SubmitEvent e)
        {
            var p = e.Project();

            pManager.AddProject(p);

            UpdateView();
        }

        private void _OnEditPushed(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void ProjectManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateParentView?.Invoke(this, e);
        }

        private void ProjectListView_DoubleClick(object sender, EventArgs e)
        {
            if (ProjectListView.Items.Count < 1)
                return;

            var cIndex = ProjectListView.SelectedIndices[0];

            var p = pManager.ProjectAt(cIndex);

            var pDialog = new ProjectDialog(p);
            pDialog.OnEditPushed += _OnEditPushed;
            pDialog.ShowDialog(this);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var pDialog = new ProjectDialog();
            pDialog.OnSubmitPushed += _OnSubmitPushed;
            pDialog.ShowDialog(this);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ProjectListView.SelectedItems.Count < 1)
                return;
            var cIndex = ProjectListView.SelectedIndices[0];
            var project = pManager.ProjectAt(cIndex);

            var pDialog = new ProjectDialog(project);
            pDialog.OnEditPushed += _OnEditPushed;
            pDialog.ShowDialog(this);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ProjectListView.SelectedItems.Count != 1)
                return;

            var cIndex = ProjectListView.SelectedIndices[0];
            var buttons = MessageBoxButtons.YesNo;
            if (MessageBox.Show("Are you sure?", "Confirm", buttons) == DialogResult.Yes)
            {
                pManager.RemoveProjectAt(cIndex);
                UpdateView();
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        public event EventHandler<EventArgs> UpdateParentView;
    }
}