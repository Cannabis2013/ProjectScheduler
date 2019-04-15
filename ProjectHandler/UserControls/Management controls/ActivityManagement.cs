using System;
using System.Drawing;
using System.Windows.Forms;
using Projecthandler.Abstract_classes_and_interfaces;
using Projecthandler.Forms.Dialogs;
using ProjectRelated;
using Templates;
using UserDomain;

namespace Projecthandler.Forms.Project_and_activity_management.Controls
{
    public partial class ActivityManagement : UserControl, IManagement, ICustomObserver
    {
        private readonly ListView aView;
        private readonly IApplicationProgrammableInterface service;

        public event EventHandler<EventArgs> updateParentView;

        public ActivityManagement(IApplicationProgrammableInterface service)
        {
            this.service = service;
            InitializeComponent();

            aView = ActivityListView;

            UpdateView();
        }

        public void UpdateView()
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
            aView.Items.AddRange(service.IsAdmin()
                ? service.activityItemModels()
                : service.activityItemModels(service.CurrentUserLoggedIn().ModelIdentity));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var aControl = new AddActivityControl(service);

            aControl.OnSaveClicked += _OnSaveClicked;
            aControl.OnCancelClicked += _OnCancelClicked;
            
            AddTabPage("Add Activity",aControl);

            /*
             * Implement the activity usercontrol
             */
        }

        public void _OnSaveClicked(object sender, EventArgs e)
        {
            RemoveTabPage(1);
            UpdateView();
        }

        public void _OnCancelClicked(object sender, EventArgs e)
        {
            RemoveTabPage(1);
        }

        public void _OnEditClicked(object sender, EventArgs e)
        {
            updateParentView?.Invoke(this, e);
            RemoveTabPage(1);
            UpdateView();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ActivityListView.SelectedItems.Count < 1)
                return;
            
            var selectedItem = ActivityListView.SelectedItems[0];
            var activity = service.Activity(selectedItem.Text);

            var aControl = new AddActivityControl(service,activity);

            aControl.OnEditClicked += _OnEditClicked;
            aControl.OnCancelClicked += _OnCancelClicked;
            
            AddTabPage("Edit activity",aControl);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var selectedActivityId = ActivityListView.SelectedItems[0].Text;
            var activity = service.Activity(selectedActivityId);
            var parentProjectId = activity.ParentModelIdentity();
            service.RemoveActivity(parentProjectId, selectedActivityId);
            updateParentView?.Invoke(this, e);
            UpdateView();
        }

        private void ActivityListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            linkLabel2_LinkClicked(this,null);
        }

        public void AddTabPage(string title, Control control)
        {
            if (TabsActive())
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

        public void RemoveTabPage(int index)
        {
            TabView.TabPages.RemoveAt(index);
        }

        public bool TabsActive()
        {
            return TabView.TabPages.Count > 1;
        }

        public void UpdateCurrentTabTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
