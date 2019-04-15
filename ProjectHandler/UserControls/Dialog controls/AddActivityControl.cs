using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Projecthandler.Abstract_classes_and_interfaces;
using ProjectRelated;
using Templates;
using UserDomain;

namespace Projecthandler.Forms.Dialogs
{
    public partial class AddActivityControl : UserControl, IDialogInterface<EventArgs>
    {
        private readonly ActivityModel activity;

        private readonly DialogMode mode;
        private readonly IApplicationProgrammableInterface service;

        public event EventHandler<EventArgs> OnCancelClicked;
        public event EventHandler<EventArgs> OnSaveClicked;
        public event EventHandler<EventArgs> OnEditClicked;

        private enum DialogMode
        {
            AddMode,
            EditMode
        }

        // Add constructor
        public AddActivityControl(IApplicationProgrammableInterface service)
        {
            this.service = service;

            InitializeComponent();
            InitializeControls();

            Text = @"Create activity";

            mode = DialogMode.AddMode;

            
        }

        // Edit constructor
        public AddActivityControl(IApplicationProgrammableInterface service, ActivityModel activity)
        {
            this.service = service;
            this.activity = activity;

            Text = @"Edit activity";

            InitializeComponent();
            if (activity.TypeOfActivity == ActivityModel.ActivityType.Absent_Related)
            {
                UserListView.Enabled = false;
                AssignedUserListView.Enabled = false;
                UnAssignUserLink.Enabled = false;
                AssignUserLink.Enabled = false;
            }
            else
                InitializeDialogValues();

            mode = DialogMode.EditMode;
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        public void InitializeControls()
        {
            if (service.IsAdmin())
            {
                var projects = service.ProjectItemModels().Select(item => item.Text).ToArray();
                // ReSharper disable once CoVariantArrayConversion
                projectSelector.Items.AddRange(projects);
            }
            else
            {
                var CurrentUserIdentity = service.CurrentUserLoggedIn().ModelIdentity;
                var projects = service.ProjectItemModels(CurrentUserIdentity);

                // ReSharper disable once CoVariantArrayConversion
                projectSelector.Items.AddRange(projects);
            }

            UserListView.Items.AddRange(service.UserListModels(false));
        }

        public void InitializeDialogValues()
        {
            IDSelector.Text = activity.ModelIdentity;
            projectSelector.Items.Add(activity.ParentModelIdentity());
            projectSelector.Text = activity.ParentModelIdentity();
            projectSelector_SelectionChangeCommitted(this,new EventArgs());

            StartDateSelector.Value = activity.StartDate;
            EndDateSelector.Value = activity.EndDate;

            var assignedUsers = activity.AssignedUsers();
            var assignedUserModels = service.UserListModels(false).Where(item => assignedUsers.Contains(item.Text));
            AssignedUserListView.Items.AddRange(assignedUserModels.ToArray());

            var availableUserModels = service.UserListModels(false).Where(item => !assignedUsers.Contains(item.Text)).ToList();
            
            UserListView.Items.AddRange(availableUserModels.ToArray());

            ReInitializeUserList(StartDateSelector.Value,EndDateSelector.Value);
        }

        private void Link_Remove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (AssignedUserListView.Items.Count <= 0)
                return;

            var currentItems = AssignedUserListView.SelectedItems;
            foreach (var item in currentItems)
            {
                AssignedUserListView.Items.Remove((ListViewItem)item);
                UserListView.Items.Add((ListViewItem)item);
            }
        }

        private void Link_Add_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (UserListView.Items.Count <= 0)
                return;

            var currentItems = UserListView.SelectedItems;
            foreach (var item in currentItems.Cast<ListViewItem>())
            {
                if (item.SubItems[2].Text == "Not available")
                    return;
                UserListView.Items.Remove((ListViewItem)item);
                AssignedUserListView.Items.Add((ListViewItem)item);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnCancelClicked?.Invoke(sender,e);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (IDSelector.Text == "")
                return;

            if (mode == DialogMode.AddMode)
                invoke_Add_Mode_Submit();
            else
                invoke_Edit_Mode_Submit();
        }

        private void invoke_Add_Mode_Submit()
        {
            string identity = IDSelector.Text, projectIdentity = projectSelector.Text;

            if (projectIdentity == null)
            {
                MessageBox.Show(@"You have to assign the activity to a project!");
                return;
            }

            var items = AssignedUserListView.Items;
            var usernames = new List<string>();
            foreach (ListViewItem item in items)
                usernames.Add(item.Text);

            DateTime startDate = StartDateSelector.Value, endDate = EndDateSelector.Value;
            var project = service.Project(projectIdentity);
            var newActivity = new ActivityModel(identity, project, startDate, endDate, usernames);
            
            
            OnSaveClicked?.Invoke(this, new EventArgs());
        }

        private void invoke_Edit_Mode_Submit()
        {
            activity.ModelIdentity = IDSelector.Text;

            var projectId = projectSelector.Text;

            if (projectId != activity.ParentModelIdentity())
            {
                var ProjectId = activity.ParentModelIdentity();
                var oldProject = service.Project(projectId);
                oldProject.RemoveSubModel(activity);
                var newProject = service.Project(projectId);
                newProject.AddSubModel(activity);
            }
            
            activity.StartDate = StartDateSelector.Value;
            activity.EndDate = EndDateSelector.Value;

            var items = AssignedUserListView.Items;
            var usernames = new List<string>();

            activity.ClearAssignedUserIdentities();

            foreach (ListViewItem item in items)
                usernames.Add(item.Text);

            activity.AssignUsers(usernames);

            OnEditClicked?.Invoke(this, new EventArgs());
        }

        private void UserListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var sList = UserListView.SelectedItems;
            if (sList.Count > 1)
                return;

            var item = sList[0];
            Link_Add_LinkClicked(sender,null);
        }

        private void projectSelector_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var selectedItem = projectSelector.Text;
            var project = service.Project(selectedItem);

            StartDateSelector.MinDate = project.StartDate;
            StartDateSelector.MaxDate = project.EndDate;

            EndDateSelector.MinDate = project.StartDate;
            EndDateSelector.MaxDate = project.EndDate;
        }

        public void updateView()
        {
            throw new NotImplementedException();
        }

        private void StartDateSelector_ValueChanged(object sender, EventArgs e)
        {
            ReInitializeUserList(StartDateSelector.Value,EndDateSelector.Value);
        }

        private void EndDateSelector_ValueChanged(object sender, EventArgs e)
        {
            ReInitializeUserList(StartDateSelector.Value, EndDateSelector.Value);
        }

        private void ReInitializeUserList(DateTime sDate, DateTime eDate)
        {   
            foreach (var item in UserListView.Items.Cast<ListViewItem>().ToList())
            {
                if(item.SubItems.Count > 2)
                    item.SubItems.RemoveAt(2);

                var availability = service.UserAvailability(item.Text, sDate, eDate);
                if (availability == "Available")
                {
                    item.ForeColor = Color.Green;
                    var ItemCol = new ListViewItem.ListViewSubItem(item, "Available") {ForeColor = Color.Green};
                    item.SubItems.Add(ItemCol);
                }
                else if (availability == "Partly available")
                {
                    item.ForeColor = Color.Yellow;
                    var ItemCol = new ListViewItem.ListViewSubItem(item, "Partly available") { ForeColor = Color.Yellow};
                    item.SubItems.Add(ItemCol);
                }
                else
                {
                    item.ForeColor = Color.Red;
                    var ItemCol = new ListViewItem.ListViewSubItem(item, "Not available") { ForeColor = Color.Red };
                    item.SubItems.Add(ItemCol);
                }
            }

            var AssignedUserListItems = AssignedUserListView.Items.Cast<ListViewItem>().ToList();
            foreach (var item in AssignedUserListItems)
            {
                var availability = service.UserAvailability(item.Text, sDate, eDate);
                if (availability == "Not Available")
                {
                    var ItemClone = (ListViewItem) item.Clone();
                    AssignedUserListView.Items.Remove(item);
                    ItemClone.ForeColor = Color.Red;
                    var ItemCol = new ListViewItem.ListViewSubItem(item, "Not available") { ForeColor = Color.Red };
                    ItemClone.SubItems.Add(ItemCol);
                    UserListView.Items.Add(ItemClone);
                }
            }

        }
    }
}
