using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DialogNamespace;
using VirtualUserDomain;

namespace ProjectRelated
{
    public partial class ActivityManagement : Form
    {
        private readonly ListView aView;
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

        public ActivityManagement(ProjectManager pManager, UserManager uManager)
        {
            InitializeComponent();
            this.pManager = pManager ?? throw new ArgumentNullException(nameof(pManager));
            this.uManager = uManager;

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

        private void ProjectManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            updateParentView?.Invoke(this, e);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void ActivityListView_DoubleClick(object sender, EventArgs e)
        {
            linkLabel2_LinkClicked(sender, null);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var projects = pManager.AllProjectIdentities(uManager.currentlyLoggedIn().UserName());
            if (!projects.Any() && uManager.verifyUserState() != User.UserRole.Admin)
            {
                MessageBox.Show(@"You aren't project leader on any projects you fucking loser.");
            }
            else
            {
                var aDialog = new ActivityDialog(pManager, uManager);
                aDialog.OnSubmitPushed += _OnSubmitPushed;
                aDialog.ShowDialog(this);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ActivityListView.Items.Count < 1)
                return;
            var projects = pManager.AllProjectIdentities(uManager.currentlyLoggedIn().UserName());
            if (!projects.Any() && uManager.verifyUserState() != User.UserRole.Admin)
            {
                MessageBox.Show(@"You aren't project leader on any projects you fucking loser.");
            }
            else
            {
                var items = ActivityListView.SelectedItems;
                var activityId = items[0].Text;
                var projectId = items[0].SubItems[5];
                var a = pManager.Project(projectId.Text).Activity(activityId);

                var pDialog = new ActivityDialog(a, pManager, uManager);
                pDialog.OnEditPushed += _OnEditPushed;
                pDialog.ShowDialog(this);
            }
        }

        public event EventHandler<EventArgs> updateParentView;

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ActivityListView.Items.Count < 1)
                return;

            var activityItemModel = ActivityListView.SelectedItems[0];
            var activity = pManager.Activity(activityItemModel.Text);
            pManager.Project(activity.ParentProjectId).RemoveActivity(activity);

            UpdateView();
        }
    }
}