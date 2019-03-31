using System.Windows.Forms;
using ProjectNameSpace;
using VirtualUserDomain;
using System.Collections.Generic;
using DialogNamespace;

namespace Projecthandler.Forms.Project
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

        private void updateView() => pView.Items.AddRange(pManager.projectItemModels());

        private readonly ListView pView;
        private readonly ProjectManager pManager;
        private UserManager uManager;

        private void button4_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            AddProjectDialog pDialog = new AddProjectDialog(uManager);
            pDialog.ShowDialog(this);
        }
    }
}
