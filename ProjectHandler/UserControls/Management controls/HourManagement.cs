using System;
using System.Linq;
using System.Windows.Forms;
using Projecthandler.Abstract_classes_and_interfaces;
using Projecthandler.UserControls.Dialog_controls;
using ProjectRelated;
using Templates;
using UserDomain;

namespace Projecthandler.Forms.Dialog_controls
{
    public partial class HourManagement : UserControl, IManagement, ICustomObserver
    {
        private readonly IApplicationProgrammableInterface service;

        public HourManagement(IApplicationProgrammableInterface service)
        {
            this.service = service;
            InitializeComponent();
            UpdateView();
        }

        public void UpdateView()
        {
            HourListView.Clear();
            HourListView.View = View.Details;

            HourListView.Columns.Add("Registration id", 60, HorizontalAlignment.Left);
            HourListView.Columns.Add("User", 60, HorizontalAlignment.Left);
            HourListView.Columns.Add("Original registration date", 60, HorizontalAlignment.Left);
            HourListView.Columns.Add("Work hours registrated", 60, HorizontalAlignment.Left);
            HourListView.Columns.Add("Parent activity", 60, HorizontalAlignment.Left);
            

            ListViewItem[] regObjects = service.IsAdmin() ? 
                regObjects = service.HourRegistrationItemModels() : 
                regObjects = service.HourRegistrationItemModels(service.CurrentUserLoggedIn().ModelIdentity);
;

            HourListView.Items.AddRange(regObjects);
        }

        public void _OnSaveClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void _OnEditClicked(object sender, EventArgs e)
        {
            RemoveTabPage(1);
            UpdateView();
        }

        public void _OnCancelClicked(object sender, EventArgs e)
        {
            RemoveTabPage(1);
            UpdateView();
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
            if (TabView.TabPages.Count > 1)
                return true;

            return false;
        }

        public void UpdateCurrentTabTitle(string title)
        {
            throw new NotImplementedException();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (HourListView.SelectedItems.Count < 1)
                return;

            var item = HourListView.SelectedItems[0];
            var ActivityId = item.SubItems[4].Text;

            var rObject = service.HourRegistrationModel(ActivityId,item.Text);
            var editHourControl = new EditHourRegistrationControl(service, rObject);
            
            editHourControl.OnEditClicked += _OnEditClicked;
            editHourControl.OnCancelClicked += _OnCancelClicked;

            AddTabPage("Edit hour registration",editHourControl);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (HourListView.SelectedItems.Count < 1)
                return;

            var item = HourListView.SelectedItems[0];
            var activityId = item.SubItems[4].Text;
            var activity = service.Activity(activityId);
            activity.RemoveSubModel(item.Text);

            UpdateView();
        }
    }
}
