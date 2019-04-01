using System;
using System.Windows.Forms;
using ProjectNameSpace;
using DialogNamespace;
using Projecthandler.Events;

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
            pView.Items.AddRange(pManager.projectItemModels());
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
            updateParentView?.Invoke(this,e);
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
            var cIndex = ProjectListView.SelectedIndices[0];
            var project = pManager.project(cIndex);

            var pDialog = new ProjectDialog(project);
            pDialog.OnEditPushed += _OnEditPushed;
            pDialog.ShowDialog(this);
        }

        public event EventHandler<EventArgs> updateParentView;

        private readonly ListView pView;
        private readonly ProjectManager pManager;
    }
}
