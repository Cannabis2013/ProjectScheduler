using System;
using System.Windows.Forms;
using ProjectNameSpace;
using VirtualUserDomain;
using DialogNamespace;
using Projecthandler.Events;

namespace ProjectNameSpace
{
    public partial class ProjectManagement : Form
    {
        public ProjectManagement(ProjectManager pManager, UserManager uManager)
        {
            InitializeComponent();
            this.pManager = pManager ?? throw new System.ArgumentNullException(nameof(pManager));
            this.uManager = uManager ?? throw new System.ArgumentNullException(nameof(uManager));

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
            var p = new Project(e.pTitle) {startWeek = e.sWeek, estimatedEndWeek = e.eWeek};
            p.assignUsersToProject(e.users);
            p.projectLeaderId = e.pLeader;

            pManager.addProject(p);

            updateView();
        }

        

        private void button4_Click(object sender, System.EventArgs e)
        {
            updateParentView?.Invoke(this,e);
            Close();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var pDialog = new AddProjectDialog(uManager);
            pDialog.OnSubmitPushed += _OnSubmitPushed;
            pDialog.ShowDialog(this);
        }

        public event EventHandler<EventArgs> updateParentView;

        private readonly ListView pView;
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;
    }
}
