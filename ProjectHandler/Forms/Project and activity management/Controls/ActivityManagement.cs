using System;
using System.Drawing;
using System.Windows.Forms;
using Projecthandler.Forms.Dialogs;
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
            if (TabView.TabPages.Count > 1)
            {
                TabView.SelectedIndex = 1;
                MessageBox.Show("You have to finish your current operation.");
                return;
            }

            var tPage = new TabPage("Add activity");

            const AnchorStyles layoutAnchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            var aControl = new AddActivityControl(pManager,uManager);

            aControl.OnSaveClicked += _OnSaveClicked;
            aControl.OnCancelClicked += _OnCancelClicked;

            aControl.Size = tPage.Size;
            tPage.Controls.Add(aControl);

            aControl.Anchor = layoutAnchor;

            TabView.TabPages.Add(tPage);
            TabView.SelectedTab = tPage;

            /*
             * Implement the activity usercontrol
             */
        }

        private void _OnSaveClicked(object sender, EventArgs e)
        {
            TabView.TabPages.RemoveAt(1);
            TabView.Enabled = true;

            UpdateView();
        }

        private void _OnCancelClicked(object sender, EventArgs e)
        {
            TabView.TabPages.RemoveAt(1);
            TabView.Enabled = true;
        }

        private void _OnEditClicked(object sender, EventArgs e)
        {
            TabView.TabPages.RemoveAt(1);
            TabView.Enabled = true;

            UpdateView();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (TabView.TabPages.Count > 1)
            {
                TabView.SelectedIndex = 1;
                MessageBox.Show("You have to finish your current operation.");
                return;
            }

            var tPage = new TabPage("Edit activity");

            var selectedItem = ActivityListView.SelectedItems[0];
            var activity = pManager.Activity(selectedItem.Text);

            var aControl = new AddActivityControl(activity,pManager,uManager);

            aControl.OnEditClicked += _OnEditClicked;
            aControl.OnCancelClicked += _OnCancelClicked;

            aControl.Size = tPage.Size;
            tPage.Controls.Add(aControl);

            const AnchorStyles layoutAnchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            aControl.Anchor = layoutAnchor;

            TabView.TabPages.Add(tPage);
            TabView.SelectedTab = tPage;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var selectedActivityId = ActivityListView.SelectedItems[0].Text;
            var activity = pManager.Activity(selectedActivityId);
            var parentActivityProjectId = activity.ParentProjectId;
            pManager.removeActivity(parentActivityProjectId, selectedActivityId);

            UpdateView();
        }
    }
}
