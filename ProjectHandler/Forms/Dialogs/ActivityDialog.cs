using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProjectNameSpace;
using VirtualUserDomain;
using System.Diagnostics;

namespace DialogNamespace
{
    public partial class ActivityDialog : Form
    {
        private readonly Activity activity;

        private readonly DialogMode mode;
        private readonly ProjectManager pManager;

        public ActivityDialog(ProjectManager pManager)
        {
            this.pManager = pManager;
            InitializeComponent();
            initializeSelectors();

            Text = @"Create activity";

            mode = DialogMode.AddMode;

            UserListView.Items.AddRange(UserManager.userListModel());
        }

        public ActivityDialog(Activity a, ProjectManager pManager)
        {
            activity = a;
            this.pManager = pManager;

            Text = @"Edit activity";

            InitializeComponent();
            initializeSelectors();
            initializeDialogValues();

            mode = DialogMode.EditMode;
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void initializeSelectors(int sVal, int eVal)
        {
            if (eVal < sVal)
            {

            }
            else
            {
                
                for (var i = sVal; i <= eVal; i++)
                    startWeekSelector.Items.Add(i.ToString());
                for (int i = 0; i < UPPER; i++)
                {
                    
                }

            }

            startWeekSelector.SelectedIndex = 0;
            endWeekSelector.SelectedIndex = 0;
            if (UserManager.verifyUserState() == User.UserRole.Admin)
            {
                var projects = pManager.allProjectIdentities().ToArray();
                // ReSharper disable once CoVariantArrayConversion
                projectSelector.Items.AddRange(projects);
            }
            else
            {
                var cUser = UserManager.currentlyLoggedIn();
                var projectIdentities = pManager.allProjectIdentities(cUser.userName()).ToArray();
                ;
                // ReSharper disable once CoVariantArrayConversion
                projectSelector.Items.AddRange(projectIdentities);
            }
        }

        private void initializeDialogValues()
        {
            IDSelector.Text = activity.Id;
            startWeekSelector.Text = activity.startWeek.ToString();
            endWeekSelector.Text = activity.endWeek.ToString();
            projectSelector.Text = activity.parentProjectId;
            var assignedUsers = activity.assignedUsers();
            var assignedUserModels = assignedUsers.Select(item =>
                new ListViewItem
                {
                    Text = item,
                    ImageIndex = 0
                }).ToArray();

            AssignedUserListView.Items.AddRange(assignedUserModels);

            var availableUsers = UserManager.allUserNames().Where(item => !assignedUsers.Contains(item)).ToList();
            var availableUserModels = availableUsers.Select(item =>
                new ListViewItem
                {
                    Text = item,
                    ImageIndex = 0
                }).ToArray();

            UserListView.Items.AddRange(availableUserModels);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (IDSelector.Text == "" || startWeekSelector.Text == "")
                return;

            if (mode == DialogMode.AddMode)
                invoke_Add_Mode_Submit();
            else
                invoke_Edit_Mode_Submit();
        }

        private void invoke_Add_Mode_Submit()
        {
            string activityTitle = IDSelector.Text, projectTitle = projectSelector.Text;

            if (projectTitle == null)
            {
                MessageBox.Show(@"You have to assign the activity to a project!");
                return;
            }

            if (!int.TryParse(startWeekSelector.Text, out var sWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            if (!int.TryParse(endWeekSelector.Text, out var eWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            var items = AssignedUserListView.Items;

            var usernames = new List<string>();

            var a = new Activity(activityTitle, sWeek, eWeek, projectTitle);


            foreach (ListViewItem item in items)
                usernames.Add(item.Text);

            a.assignUsers(usernames);

            var p = pManager.project(projectTitle);
            p.addActivity(a);

            OnSubmitPushed?.Invoke(this, new EventArgs());

            Close();
        }

        private void invoke_Edit_Mode_Submit()
        {
            activity.Id = IDSelector.Text;

            var projectId = projectSelector.Text;

            if (projectId != activity.parentProjectId)
            {
                var p = pManager.project(activity.parentProjectId);
                p.removeActivity(activity);
                activity.parentProjectId = projectId;
                p.addActivity(activity);
            }

            if (!int.TryParse(startWeekSelector.Text, out var sWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            if (!int.TryParse(endWeekSelector.Text, out var eWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            activity.startWeek = sWeek;
            activity.endWeek = eWeek;

            var items = AssignedUserListView.Items;
            var usernames = new List<string>();

            activity.clearAssignedUserIdentities();

            foreach (ListViewItem item in items)
                usernames.Add(item.Text);

            activity.assignUsers(usernames);

            Close();
        }

        private void leaderSelector_DropDown(object sender, EventArgs e)
        {
            if (projectSelector.Items.Count < 1)
                updateLeaderComboBoxView();
        }

        private void UserListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var sList = UserListView.SelectedItems;
            if (sList.Count > 1)
                return;

            var item = sList[0];
            linkLabel2_LinkClicked(this, null);
        }

        private void updateLeaderComboBoxView()
        {
            foreach (ListViewItem item in AssignedUserListView.Items)
                projectSelector.Items.Add(item.Text);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (AssignedUserListView.Items.Count <= 0)
                return;

            var currentItems = AssignedUserListView.SelectedItems;
            foreach (var item in currentItems)
            {
                AssignedUserListView.Items.Remove((ListViewItem) item);
                UserListView.Items.Add((ListViewItem) item);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (UserListView.Items.Count <= 0)
                return;

            var currentItems = UserListView.SelectedItems;
            foreach (var item in currentItems)
            {
                UserListView.Items.Remove((ListViewItem) item);
                AssignedUserListView.Items.Add((ListViewItem) item);
            }
        }

        public event EventHandler<EventArgs> OnSubmitPushed;
        public event EventHandler<EventArgs> OnEditPushed;

        private enum DialogMode
        {
            AddMode,
            EditMode
        }

        private void projectSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cIndex = projectSelector.SelectedIndex;
            if (cIndex != -1)
            {
                startWeekSelector.Enabled = true;
                endWeekSelector.Enabled = true;


            }
            else
            {
                
            }
        }
    }
}