using System;
using System.Windows.Forms;
using Projecthandler.Events;
using Projecthandler.Project_related;
using Projecthandler.User_Management;

namespace Projecthandler.Forms.Dialogs
{
    public partial class ProjectDialog : Form
    {
        private readonly DialogMode mode;

        private readonly Project temporaryProject;

        public ProjectDialog()
        {
            InitializeComponent();
            InitializeSelectors();

            mode = DialogMode.AddMode;
        }

        public ProjectDialog(Project p)
        {
            temporaryProject = p;

            InitializeComponent();
            InitializeSelectors();
            InitializeDialogValues();

            mode = DialogMode.EditMode;
        }

        private void InitializeSelectors()
        {
            for (var i = 1; i <= 52; i++)
            {
                startWeekSelector.Items.Add(i.ToString());
                endWeekSelector.Items.Add(i.ToString());
            }

            startWeekSelector.SelectedIndex = 0;
            endWeekSelector.SelectedIndex = 0;
            UpdateLeaderComboBoxView();
        }

        private void InitializeDialogValues()
        {
            projectIDSelector.Text = temporaryProject.Id;
            startWeekSelector.Text = temporaryProject.StartWeek.ToString();
            endWeekSelector.Text = temporaryProject.EndWeek.ToString();
            leaderSelector.Text = temporaryProject.ProjectLeaderId;
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (projectIDSelector.Text == "" || startWeekSelector.Text == "")
                return;

            if (mode == DialogMode.AddMode)
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

            var p = new Project(title)
            {
                StartWeek = sWeek,
                EndWeek = eWeek,
                ProjectLeaderId = pLeader
            };

            OnSubmitPushed?.Invoke(this, new SubmitEvent(p));
        }

        private void invoke_Edit_Mode_Submit()
        {
            temporaryProject.Id = projectIDSelector.Text;
            temporaryProject.ProjectLeaderId = leaderSelector.Text;

            if (!int.TryParse(startWeekSelector.Text, out var sWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            if (!int.TryParse(endWeekSelector.Text, out var eWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            temporaryProject.StartWeek = sWeek;
            temporaryProject.EndWeek = eWeek;

            OnEditPushed?.Invoke(this, new EventArgs());
        }

        private void UpdateLeaderComboBoxView()
        {
            foreach (var item in UserManager.AllUserNames())
                leaderSelector.Items.Add(item);
        }

        public event EventHandler<SubmitEvent> OnSubmitPushed;
        public event EventHandler<EventArgs> OnEditPushed;

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private enum DialogMode
        {
            AddMode,
            EditMode
        }
    }
}