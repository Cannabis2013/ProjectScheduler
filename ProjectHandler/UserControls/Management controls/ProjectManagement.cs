using System;
using System.Drawing;
using System.Windows.Forms;
using Projecthandler.Abstract_classes_and_interfaces;
using Projecthandler.Events;
using Projecthandler.Forms.Dialogs;
using ProjectRelated;
using Templates;
using UserDomain;

namespace Projecthandler
{
    public partial class ProjectManagement : UserControl, IManagement, ICustomObserver
    {
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;
        public ProjectManagement(ProjectManager pManager, UserManager uManager)
        {
            this.pManager = pManager;
            this.uManager = uManager;
            InitializeComponent();
            
            pManager.Subscribe(this);

            UpdateView();
        }

        public void UpdateView()
        {
            ProjectListView.Clear();
            ProjectListView.View = View.Details;
            ProjectListView.TileSize = new Size(120, 80);
            const int columnWidth = 120;
            ProjectListView.Columns.Add("Project title", columnWidth, HorizontalAlignment.Left);
            ProjectListView.Columns.Add("Start date", columnWidth, HorizontalAlignment.Left);
            ProjectListView.Columns.Add("Estimated end date", columnWidth, HorizontalAlignment.Left);
            ProjectListView.Columns.Add("Total registered hours", columnWidth, HorizontalAlignment.Left);
            ProjectListView.Items.AddRange(pManager.ProjectItemModels());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            const AnchorStyles layoutAnchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            var pControl = new AddProjectControl(uManager);

            pControl.OnSaveClicked += _OnSaveClicked;
            pControl.OnCancelClicked += _OnCancelClicked;
            
            addTabPage("Add project",pControl);
        }

        public void _OnSaveClicked(object sender, EventArgs e)
        {
            var submitEvent = (SubmitEvent) e;
            var p = submitEvent.Project();
            pManager.AddModel((AbstractModel) p);
            UpdateView();

            removeTabPage(1);
        }

        public void _OnEditClicked(object sender, EventArgs e)
        {
            UpdateView();
            removeTabPage(1);
            
        }

        public void _OnCancelClicked(object sender, EventArgs e)
        {
            TabView.TabPages.RemoveAt(1);
            TabView.Enabled = true;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!uManager.isAdmin())
            {
                MessageBox.Show("Administrator privilliges required YOU FUCKING LOSER.", "Admin required");
                return;
            }

            if (ProjectListView.Items.Count < 1 || ProjectListView.SelectedIndices.Count < 1)
                return;

            var cIndex = ProjectListView.SelectedIndices[0];
            pManager.RemoveModelAt(cIndex);
            UpdateView();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ProjectListView.Items.Count < 1 || ProjectListView.SelectedIndices.Count < 1)
                return;

            var item = ProjectListView.SelectedItems[0];
            var project = (ProjectModel) pManager.Model(item.Text);

            var pControl = new AddProjectControl(project,uManager);
            
            pControl.OnEditClicked += _OnEditClicked;
            pControl.OnCancelClicked += _OnCancelClicked;
            
            addTabPage("Edit project",pControl);
        }

        private void ProjectListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            linkLabel2_LinkClicked(sender,null);
        }

        public void addTabPage(string title, Control control)
        {
            if (!uManager.isAdmin())
            {
                MessageBox.Show(@"Administrator privilliges required YOU FUCKING LOSER.", @"Admin required");
                return;
            }

            if (tabsActive())
            {
                TabView.SelectedIndex = 1;
                MessageBox.Show(@"You have to finish your current operation.");
                return;
            }

            var tPage = new TabPage(title);

            const AnchorStyles layoutAnchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            
            control.Size = tPage.Size;
            tPage.Controls.Add(control);

            control.Anchor = layoutAnchor;

            TabView.TabPages.Add(tPage);
            TabView.SelectedTab = tPage;
        }

        public void removeTabPage(int index)
        {
            TabView.TabPages.RemoveAt(index);
        }

        public bool tabsActive()
        {
            return TabView.TabPages.Count > 1;
        }

        public void updateCurrentTabTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
