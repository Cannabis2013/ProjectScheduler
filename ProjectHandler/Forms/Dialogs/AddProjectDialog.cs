using System;
using System.Windows.Forms;
using Projecthandler.Events;
using VirtualUserDomain;

namespace DialogNamespace
{
    public partial class AddProjectDialog : Form
    {
        public AddProjectDialog(UserManager uManager)
        {
            InitializeComponent();
            initializeWeekSelectors();

            UserListView.Items.AddRange(uManager.userListModel());
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
        public event EventHandler<SubmitEvent> OnSubmitPushed;

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (projectIDSelector.Text == "" || startWeekSelector.Text == "")
                return;

            string title = projectIDSelector.Text, pLeader = leaderSelector.Text;
            int sWeek = 0, eWeek = 0;

            if(!int.TryParse(startWeekSelector.Text,out sWeek) && 
               !int.TryParse(endWeekSelector.Text,out eWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            var items = AssignedUserListView.Items;
            var usernames = new string[items.Count];
            var index = 0;

            foreach (ListViewItem item in items)
                usernames[index++] = item.Text;

            OnSubmitPushed?.Invoke(this,new SubmitEvent(title,sWeek,eWeek,usernames,pLeader));

            Close();
        }

        private void leaderSelector_DropDown(object sender, EventArgs e)
        {
            foreach (ListViewItem item in AssignedUserListView.Items)
                leaderSelector.Items.Add(item.Text);
        }
    }
}
