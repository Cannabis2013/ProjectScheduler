using System;
using System.Linq;
using System.Windows.Forms;
using Projecthandler.UserControls.Dialog_controls;
using ProjectRelated;
using Templates;
using VirtualUserDomain;

namespace Projecthandler.Forms.Dialog_controls
{
    public partial class HourManagement : UserControl, IManagement
    {
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

        public HourManagement(UserManager uManager, ProjectManager pManager)
        {
            this.uManager = uManager;
            this.pManager = pManager;
            InitializeComponent();
            updateView();
        }

        public void updateView()
        {
            HourListView.Clear();
            HourListView.View = View.Details;

            HourListView.Columns.Add("Registration id", 60, HorizontalAlignment.Left);
            HourListView.Columns.Add("User", 60, HorizontalAlignment.Left);
            HourListView.Columns.Add("Original registration date", 60, HorizontalAlignment.Left);
            HourListView.Columns.Add("Work hours registrated", 60, HorizontalAlignment.Left);
            HourListView.Columns.Add("Parent activity", 60, HorizontalAlignment.Left);
            

            ListViewItem[] regObjects = uManager.isAdmin() ? 
                regObjects = pManager.AllHourRegistrationModels().Select(item => item.ItemModel()).ToArray() : 
                regObjects = pManager.AllHourRegistrationModels(uManager.loggedIn().ModelIdentity).Select(item => item.ItemModel())
                    .ToArray();

            HourListView.Items.AddRange(regObjects);
        }

        public void _OnSaveClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void _OnEditClicked(object sender, EventArgs e)
        {
            removeTabPage(1);
            updateView();
        }

        public void _OnCancelClicked(object sender, EventArgs e)
        {
            removeTabPage(1);
            updateView();
        }

        public void addTabPage(string title, Control control)
        {
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
            if (TabView.TabPages.Count > 1)
                return true;

            return false;
        }

        public void updateCurrentTabTitle(string title)
        {
            throw new NotImplementedException();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (HourListView.SelectedItems.Count < 1)
                return;

            var item = HourListView.SelectedItems[0];

            var rObject = pManager.getHourRegistrationModel(item.Text);
            var editHourControl = new EditHourRegistrationControl(pManager, uManager, rObject);
            
            editHourControl.OnEditClicked += _OnEditClicked;
            editHourControl.OnCancelClicked += _OnCancelClicked;

            addTabPage("Edit hour registration",editHourControl);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (HourListView.SelectedItems.Count < 1)
                return;

            var item = HourListView.SelectedItems[0];
            var activityId = item.SubItems[4];
            var activity = pManager.Model(activityId.Text);
            activity.RemoveSubModel(item.Text);

            updateView();
        }
    }
}
