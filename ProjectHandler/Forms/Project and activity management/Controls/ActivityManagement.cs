using System;
using System.Drawing;
using System.Windows.Forms;
using ProjectRelated;
using VirtualUserDomain;

namespace Projecthandler.Forms.Project_and_activity_management.Controls
{
    public partial class ActivityManagement : UserControl
    {
        private readonly ListView aView;
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

        public event EventHandler<EventArgs> updateParentView;

        public ActivityManagement(ProjectManager pManager, UserManager uManager)
        {
            this.pManager = pManager;
            this.uManager = uManager;
            InitializeComponent();

            aView = ActivityListView;

            UpdateView();
        }

        private void UpdateView()
        {
            aView.Clear();
            aView.View = View.Details;
            aView.TileSize = new Size(120, 80);
            const int columnWidth = 120;
            aView.Columns.Add("Activity title", columnWidth, HorizontalAlignment.Left);
            aView.Columns.Add("Start week", columnWidth, HorizontalAlignment.Left);
            aView.Columns.Add("Estimated end week", columnWidth, HorizontalAlignment.Left);
            aView.Columns.Add("Total registered hours", columnWidth, HorizontalAlignment.Left);
            aView.Columns.Add("Assigned users", columnWidth, HorizontalAlignment.Left);
            aView.Columns.Add("Project", columnWidth, HorizontalAlignment.Left);
            aView.Items.AddRange(pManager.ProjectActivityItemModels(uManager));
        }

        private void _OnSubmitPushed(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void _OnEditPushed(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /*
             * Implement the activity usercontrol
             */
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
