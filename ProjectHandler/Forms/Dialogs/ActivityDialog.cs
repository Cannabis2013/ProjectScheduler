using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProjectRelated;
using VirtualUserDomain;

namespace DialogNamespace
{
    public partial class ActivityDialog : Form
    {
        private readonly Activity activity;

        private readonly DialogMode mode;
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

        public ActivityDialog(ProjectManager pManager, UserManager uManager)
        {
            this.pManager = pManager;
            this.uManager = uManager;
            InitializeComponent();
            initialize_userListViews();

            Text = @"Create activity";

            mode = DialogMode.AddMode;

            UserListView.Items.AddRange(uManager.userListViewItems());
        }

        public ActivityDialog(Activity a, ProjectManager pManager, UserManager uManager)
        {
            activity = a;
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

        private void InitializeSelectors(int sVal, int eVal)
        {
            startWeekSelector.Items.Clear();
            endWeekSelector.Items.Clear();
            if (eVal < sVal)
            {
                for (var i = eVal; i < 52; i++)
                {
                    startWeekSelector.Items.Add(i.ToString());
                    endWeekSelector.Items.Add(i.ToString());
                }

                for (var i = 0; i < 52; i++)
                {
                    startWeekSelector.Items.Add(i.ToString());
                    endWeekSelector.Items.Add(i.ToString());
                }

                startWeekSelector.SelectedIndex = 0;
                endWeekSelector.SelectedIndex = 0;
            }
            else
            {
                for (var i = sVal; i <= eVal; i++)
                {
                    startWeekSelector.Items.Add(i.ToString());
                    endWeekSelector.Items.Add(i.ToString());
                }
            }

            if (mode == DialogMode.EditMode)
            {
                startWeekSelector.Text = activity.StartWeek.ToString();
                endWeekSelector.Text = activity.EndWeek.ToString();
            }
        }

        private void initialize_userListViews()
        {
            if (uManager.verifyUserState() == User.UserRole.Admin)
            {
                var projects = pManager.AllProjectIdentities().ToArray();
                // ReSharper disable once CoVariantArrayConversion
                projectSelector.Items.AddRange(projects);
            }
            else
            {
                var cUser = uManager.currentlyLoggedIn();
                var projectIdentities = pManager.AllProjectIdentities(cUser.UserName()).ToArray();
                ;
                // ReSharper disable once CoVariantArrayConversion
                projectSelector.Items.AddRange(projectIdentities);
            }
        }

        private void InitializeDialogValues()
        {
            IDSelector.Text = activity.ActivityId;
            projectSelector.Items.Add(activity.ParentProjectId);
            projectSelector.Text = activity.ParentProjectId;
            var p = pManager.Project(activity.ParentProjectId);
            InitializeSelectors(p.startWeek,p.endWeek);
            startWeekSelector.Text = activity.StartWeek.ToString();
            endWeekSelector.Text = activity.EndWeek.ToString();
            var assignedUsers = activity.AssignedUsers();
            var assignedUserModels = assignedUsers.Select(item =>
                new ListViewItem
                {
                    Text = item,
                    ImageIndex = 0
                }).ToArray();

            AssignedUserListView.Items.AddRange(assignedUserModels);

            var availableUsers = uManager.allUserNames().Where(item => !assignedUsers.Contains(item)).ToList();
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

            var a = new Activity(activityTitle, sWeek, eWeek, projectTitle,uManager);


            foreach (ListViewItem item in items)
                usernames.Add(item.Text);

            a.AssignUsers(usernames);

            var p = pManager.Project(projectTitle);
            p.AddActivity(a);

            OnSubmitPushed?.Invoke(this, new EventArgs());

            Close();
        }

        private void invoke_Edit_Mode_Submit()
        {
            activity.ActivityId = IDSelector.Text;

            var projectId = projectSelector.Text;

            if (projectId != activity.ParentProjectId)
            {
                var p = pManager.Project(activity.ParentProjectId);
                p.RemoveActivity(activity);
                activity.ParentProjectId = projectId;
                p.AddActivity(activity);
            }

            if (!int.TryParse(startWeekSelector.Text, out var sWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            if (!int.TryParse(endWeekSelector.Text, out var eWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            activity.StartWeek = sWeek;
            activity.EndWeek = eWeek;

            var items = AssignedUserListView.Items;
            var usernames = new List<string>();

            activity.ClearAssignedUserIdentities();

            foreach (ListViewItem item in items)
                usernames.Add(item.Text);

            activity.AssignUsers(usernames);

            OnEditPushed?.Invoke(this,new EventArgs());
            Close();
        }

        private void UserListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var sList = UserListView.SelectedItems;
            if (sList.Count > 1)
                return;

            var item = sList[0];
            linkLabel2_LinkClicked(this, null);
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

        private void projectSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cIndex = projectSelector.SelectedIndex;
            if (cIndex != -1)
            {
                startWeekSelector.Enabled = true;
                endWeekSelector.Enabled = true;

                var p = pManager.ProjectAt(cIndex);
                InitializeSelectors(p.startWeek, p.endWeek);
            }
            else
            {
                startWeekSelector.Enabled = false;
                endWeekSelector.Enabled = false;
            }
        }

        private enum DialogMode
        {
            AddMode,
            EditMode
        }
    }
}