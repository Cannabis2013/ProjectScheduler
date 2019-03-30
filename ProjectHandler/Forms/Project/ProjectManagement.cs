using System.Windows.Forms;
using ProjectNameSpace;
using VirtualUserDomain;
using System.Collections.Generic;

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

        private ListView pView;
        private ProjectManager pManager;
        private UserManager uManager;
    }
}
