using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectRelated;
using VirtualUserDomain;

namespace Projecthandler.Forms.Dialogs
{
    public partial class AddActivityControl : UserControl
    {
        private readonly Activity activity;

        private readonly DialogMode mode;
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

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
            initialize_userListViews();

            Text = @"Create activity";

            mode = DialogMode.AddMode;

            UserListView.Items.AddRange(uManager.userListViewItems());
        }

        public AddActivityControl(Activity activity, ProjectManager pManager, UserManager uManager)
        {
            this.activity = activity;
            this.pManager = pManager;
            this.uManager = uManager;

            Text = @"Edit activity";

            InitializeComponent();
            InitializeDialogValues();

            mode = DialogMode.EditMode;
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

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void InitializeDialogValues()
        {
            IDSelector.Text = activity.ActivityId;
            projectSelector.Items.Add(activity.ParentProjectId);
            projectSelector.Text = activity.ParentProjectId;
            var p = pManager.Project(activity.ParentProjectId);
            InitializeSelectors();
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

        private void InitializeSelectors()
        {
            for (int i = 0; i < 52; i++)
            {
                startWeekSelector.Items.Add(i.ToString());
                endWeekSelector.Items.Add(i.ToString());
            }
        }

    }
}
