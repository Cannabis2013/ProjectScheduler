using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Projecthandler.Events;
using ProjectNameSpace;
using VirtualUserDomain;

namespace DialogNamespace
{
    public partial class ActivityDialog : Form
    {
        public ActivityDialog()
        {
            InitializeComponent();
            initializeWeekSelectors();

            mode = DialogMode.AddMode;

            UserListView.Items.AddRange(UserManager.userListModel());
        }

        public ActivityDialog(Project p)
        {
            temporaryProject = p;

            InitializeComponent();
            initializeWeekSelectors();
            initializeDialogValues();

            mode = DialogMode.EditMode;
        }

        private void initializeWeekSelectors()
        {
            for (var i = 1; i <= 52; i++)
            {
                startWeekSelector.Items.Add(i.ToString());
                endWeekSelector.Items.Add(i.ToString());
            }

            startWeekSelector.SelectedIndex = 0;
            endWeekSelector.SelectedIndex = 0;
        }

        private void initializeDialogValues()
        {
            
        }
        
        private void AddButton_Click(object sender, EventArgs e)
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

        private void DeleteButton_Click(object sender, EventArgs e)
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

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (projectIDSelector.Text == "" || startWeekSelector.Text == "")
                return;

            if(mode == DialogMode.AddMode)
                invoke_Add_Mode_Submit();
            else
                invoke_Edit_Mode_Submit();

            Close();
        }

        private void invoke_Add_Mode_Submit()
        {
            string title = projectIDSelector.Text, pLeader = leaderSelector.Text;


            if (!int.TryParse(startWeekSelector.Text, out var sWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            if (!int.TryParse(endWeekSelector.Text, out var eWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            var items = AssignedUserListView.Items;

            var usernames = new string[items.Count];
            var index = 0;

            foreach (ListViewItem item in items)
                usernames[index++] = item.Text;

            OnSubmitPushed?.Invoke(this, new SubmitEvent(title, sWeek, eWeek, pLeader));
        }

        private void invoke_Edit_Mode_Submit()
        {
            
        }

        private void leaderSelector_DropDown(object sender, EventArgs e)
        {
            if(leaderSelector.Items.Count < 1)
                updateLeaderComboBoxView();
        }

        private void UserListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var sList = UserListView.SelectedItems;
            if (sList.Count > 1)
                return;

            var item = sList[0];
            AddButton_Click(this,new EventArgs());
        }

        private void updateLeaderComboBoxView()
        {
            foreach (ListViewItem item in AssignedUserListView.Items)
                leaderSelector.Items.Add(item.Text);
        }

        public event EventHandler<SubmitEvent> OnSubmitPushed;
        public event EventHandler<EventArgs> OnEditPushed; 

        private DialogMode mode;
        private enum DialogMode { AddMode, EditMode};

        private Project temporaryProject = null;

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
