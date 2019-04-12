using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProjectRelated;
using Templates;
using UserDomain;

namespace Projecthandler.Forms.Dialogs
{
    public partial class AddActivityControl : UserControl, IDialogInterface<EventArgs>
    {
        private readonly ActivityModel activity;

        private readonly DialogMode mode;
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

        public event EventHandler<EventArgs> OnCancelClicked;
        public event EventHandler<EventArgs> OnSaveClicked;
        public event EventHandler<EventArgs> OnEditClicked;

        private enum DialogMode
        {
            AddMode,
            EditMode
        }

        public AddActivityControl(ProjectManager pManager, UserManager uManager)
        {
            this.pManager = pManager;
            this.uManager = uManager;

            InitializeComponent();
            initializeListControls();

            Text = @"Create activity";

            mode = DialogMode.AddMode;

            UserListView.Items.AddRange(uManager.ItemModels());
        }

        public AddActivityControl(ActivityModel activity, ProjectManager pManager, UserManager uManager)
        {
            this.activity = activity;
            this.pManager = pManager;
            this.uManager = uManager;

            Text = @"Edit activity";

            InitializeComponent();
            InitializeDialogValues();

            mode = DialogMode.EditMode;
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        public void initializeListControls()
        {
            if (uManager.isAdmin())
            {
                var projects = pManager.ListModelIdentities().ToArray();
                // ReSharper disable once CoVariantArrayConversion
                projectSelector.Items.AddRange(projects);
            }
            else
            {
                var cUser = uManager.loggedIn();
                var projectIdentities = pManager.ListModelIdentities().ToArray();
                ;
                // ReSharper disable once CoVariantArrayConversion
                projectSelector.Items.AddRange(projectIdentities);
            }
        }

        public void InitializeDialogValues()
        {
            IDSelector.Text = activity.ModelIdentity;
            projectSelector.Items.Add(activity.ParentModelIdentity());
            projectSelector.Text = activity.ParentModelIdentity();
            projectSelector_SelectionChangeCommitted(this,new EventArgs());
            var assignedUsers = activity.AssignedUsers();
            var assignedUserModels = assignedUsers.Select(item =>
                new ListViewItem
                {
                    Text = item,
                    ImageIndex = 0
                }).ToArray();

            StartDateSelector.Value = activity.StartDate;
            EndDateSelector.Value = activity.EndDate;

            AssignedUserListView.Items.AddRange(assignedUserModels);

            var availableUsers = uManager.ListModelIdentities().Where(item => !assignedUsers.Contains(item)).ToList();
            var availableUserModels = availableUsers.Select(item =>
                new ListViewItem
                {
                    Text = item,
                    ImageIndex = 0
                }).ToArray();

            UserListView.Items.AddRange(availableUserModels);
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
            foreach (var item in currentItems)
            {
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

            DateTime startDate = StartDateSelector.Value, endDate = EndDateSelector.Value;
            var project = (ProjectModel) pManager.Model(projectIdentity);
            var activity = new ActivityModel(identity, startDate, endDate, project, uManager);


            foreach (ListViewItem item in items)
                usernames.Add(item.Text);

            activity.AssignUsers(usernames);
            
            OnSaveClicked?.Invoke(this, new EventArgs());
        }

        private void invoke_Edit_Mode_Submit()
        {
            activity.ModelIdentity = IDSelector.Text;

            var projectId = projectSelector.Text;

            if (projectId != activity.ParentModelIdentity())
            {
                var oldProject = pManager.Model(activity.ParentModelIdentity());
                oldProject.RemoveSubModel(activity);
                var newProject = pManager.Model(projectId);
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
            var project = (ProjectModel) pManager.Model(selectedItem);

            StartDateSelector.MinDate = project.StartDate;
            StartDateSelector.MaxDate = project.EndDate;

            EndDateSelector.MinDate = project.StartDate;
            EndDateSelector.MaxDate = project.EndDate;
        }

        public void updateView()
        {
            throw new NotImplementedException();
        }
    }
}
