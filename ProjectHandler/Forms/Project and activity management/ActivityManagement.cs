using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProjectNameSpace;
using DialogNamespace;
using Projecthandler.Events;
using Templates;
using VirtualUserDomain;

namespace ProjectNameSpace
{
    public partial class ActivityManagement : Form
    {
        public ActivityManagement(ProjectManager pManager)
        {
            InitializeComponent();
            this.pManager = pManager ?? throw new System.ArgumentNullException(nameof(pManager));

            pView = ActivityListView;
            
            updateView();
        }

        private void updateView()
        {
            pView.Clear();
            pView.View = View.Details;
            pView.TileSize = new Size(120,80);
            const int columnWidth = 120;
            pView.Columns.Add("Activity title", columnWidth, HorizontalAlignment.Left);
            pView.Columns.Add("Start week", columnWidth, HorizontalAlignment.Left);
            pView.Columns.Add("Estimated end week", columnWidth, HorizontalAlignment.Left);
            pView.Columns.Add("Total registered hours", columnWidth, HorizontalAlignment.Left);
            pView.Columns.Add("Assigned users", columnWidth, HorizontalAlignment.Left);
            pView.Columns.Add("Project", columnWidth, HorizontalAlignment.Left);
            pView.Items.AddRange(pManager.projectActivityItemModels());
        }

        private void _OnSubmitPushed(object sender, SubmitEvent e)
        {


            updateView();
        }

        private void _OnEditPushed(object sender, EventArgs e)
        {
            updateView();
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

        public event EventHandler<EventArgs> updateParentView;

        private readonly ListView pView;
        private readonly ProjectManager pManager;

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var projects = pManager.projects()
                .Where(item => item.projectLeaderId == UserManager.currentlyLoggedIn().id);
            if (!projects.Any())
            {
                MessageBox.Show(@"You aren't project leader on any projects you fucking loser.");
            }
            else
            {
                var aDialog = new ActivityDialog(pManager);
                aDialog.OnSubmitPushed += _OnSubmitPushed;
                aDialog.ShowDialog(this);
                
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ActivityListView.Items.Count < 1)
                return;

            var items = ActivityListView.SelectedItems;
            var activityId = items[0].Text;
            var projectId = items[0].SubItems[5];
            var a = pManager.project(projectId.Text).activity(activityId);

            var pDialog = new ActivityDialog(a, pManager);
            pDialog.OnEditPushed += _OnEditPushed;
            pDialog.ShowDialog(this);
        }
    }
}
