using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projecthandler.Events;
using Projecthandler.Forms.Dialogs;
using ProjectRelated;
using Templates;
using VirtualUserDomain;

namespace Projecthandler
{
    public partial class ProjectManagement : UserControl
    {
        private ProjectManager pManager;
        private UserManager uManager;
        public ProjectManagement(ProjectManager pManager, UserManager uManager)
        {
            this.pManager = pManager;
            this.uManager = uManager;
            InitializeComponent();

            updateView();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (TabView.TabPages.Count > 1)
            {
                TabView.SelectedIndex = 1;
                MessageBox.Show("You have to finish your current operation.");
                return;
            }

            var tPage = new TabPage("Add project");

            const AnchorStyles layoutAnchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            var pControl = new AddProjectControl(uManager);

            pControl.OnSaveClicked += _OnSaveClicked;
            pControl.OnCancelClicked += _OnCancelClicked;

            pControl.Size = tPage.Size;
            tPage.Controls.Add(pControl);
            
            pControl.Anchor = layoutAnchor;

            TabView.TabPages.Add(tPage);
            TabView.SelectedTab = tPage;
            
        }

        private void _OnSaveClicked(object sender, SubmitEvent e)
        {
            var p = e.project();
            pManager.AddProject(p);
            updateView();

            TabView.TabPages.RemoveAt(1);
        }

        private void _OnEditClicked(object sender, EventArgs e)
        {
            updateView();
            TabView.TabPages.RemoveAt(1);
        }

        private void _OnCancelClicked(object sender, EventArgs e)
        {
            TabView.TabPages.RemoveAt(1);
            TabView.Enabled = true;
        }

        private void updateView()
        {
            ProjectListView.Items.Clear();
            var models = pManager.ProjectItemModels();
            ProjectListView.Items.AddRange(models);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ProjectListView.Items.Count < 1 || ProjectListView.SelectedIndices.Count < 1)
                return;

            var cIndex = ProjectListView.SelectedIndices[0];
            pManager.RemoveProjectAt(cIndex);
            updateView();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (TabView.TabPages.Count > 1)
            {
                TabView.SelectedIndex = 1;
                MessageBox.Show("You have to finish your current operation.");
                return;
            }

            var tPage = new TabPage("Edit project");

            const AnchorStyles layoutAnchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            var item = ProjectListView.SelectedItems[0];
            var p = pManager.Project(item.Text);

            var pControl = new AddProjectControl(p,uManager);
            
            pControl.OnEditClicked += _OnEditClicked;
            pControl.OnCancelClicked += _OnCancelClicked;

            pControl.Size = tPage.Size;
            tPage.Controls.Add(pControl);

            pControl.Anchor = layoutAnchor;

            TabView.TabPages.Add(tPage);
            TabView.SelectedTab = tPage;
        }
    }
}
