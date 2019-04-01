﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Projecthandler.Events;
using ProjectNameSpace;
using VirtualUserDomain;

namespace DialogNamespace
{
    public partial class ProjectDialog : Form
    {
        public ProjectDialog()
        {
            InitializeComponent();
            initializeWeekSelectors();

            mode = DialogMode.AddMode;

            UserListView.Items.AddRange(UserManager.userListModel());
        }

        public ProjectDialog(Project p)
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
            /*
             * Initialize the comboboxes
             */

            projectIDSelector.Text = temporaryProject.projectId;
            leaderSelector.Text = temporaryProject.projectLeaderId;

            startWeekSelector.Text = temporaryProject.startWeek.ToString();
            endWeekSelector.Text = temporaryProject.endWeek.ToString();

            /*
             * Removing the assigned user from the total user list
             */

            var pUserList = temporaryProject.assignedUserList();
            var userList = UserManager.allUserNames();

            foreach (var user in pUserList)
                userList.Remove(user);

            /*
             * Add the list to the two listview, non-assigned and assigned userlistviews respectively
             */

            var nonAssignedUsersList = new ListView.ListViewItemCollection(UserListView);

            foreach (var user in userList)
                nonAssignedUsersList.Add(user);

            var assignedUsersList = new ListView.ListViewItemCollection(AssignedUserListView);

            foreach (var user in pUserList)
                assignedUsersList.Add(user);

            updateLeaderComboBoxView();
            leaderSelector.Text = temporaryProject.projectLeaderId;
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

            OnSubmitPushed?.Invoke(this, new SubmitEvent(title, sWeek, eWeek, usernames, pLeader));
        }

        private void invoke_Edit_Mode_Submit()
        {
            temporaryProject.projectId = projectIDSelector.Text;
            temporaryProject.projectLeaderId = leaderSelector.Text;


            if (!int.TryParse(startWeekSelector.Text, out var sWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            if (!int.TryParse(endWeekSelector.Text, out var eWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            var items = AssignedUserListView.Items;

            temporaryProject.unAssignUsers();

            var usernames = new string[items.Count];
            var index = 0;

            foreach (ListViewItem item in items)
                usernames[index++] = item.Text;

            temporaryProject.assignUsersToProject(usernames);

            OnEditPushed?.Invoke(this,null);
        }

        private void leaderSelector_DropDown(object sender, EventArgs e)
        {
            if(leaderSelector.Items.Count < 1)
                updateLeaderComboBoxView();
        }

        private void UserListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var sList = UserListView.SelectedItems;
            var item = sList[0];
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
    }
}
