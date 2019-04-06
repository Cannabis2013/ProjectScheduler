using System;
using System.Windows.Forms;
using Projecthandler.Events;
using ProjectRelated;
using VirtualUserDomain;

namespace DialogNamespace
{
    public partial class ProjectDialog : Form
    {
        private readonly DialogMode mode;

        private readonly Project temporaryProject;
        private UserManager uManager;

        public ProjectDialog(UserManager uManager)
        {
            this.uManager = uManager;
            InitializeComponent();
            initializeSelectors();

            mode = DialogMode.AddMode;
        }

        public ProjectDialog(Project p, UserManager uManager)
        {
            temporaryProject = p;
            this.uManager = uManager;

            InitializeComponent();
            initializeSelectors();
            initializeDialogValues();

            mode = DialogMode.EditMode;
        }

        private void initializeSelectors()
        {
            for (var i = 1; i <= 52; i++)
            {
                startWeekSelector.Items.Add(i.ToString());
                endWeekSelector.Items.Add(i.ToString());
            }

            startWeekSelector.SelectedIndex = 0;
            endWeekSelector.SelectedIndex = 0;
            updateLeaderComboBoxView();
        }

        private void initializeDialogValues()
        {
            projectIDSelector.Text = temporaryProject.id;
            startWeekSelector.Text = temporaryProject.startWeek.ToString();
            endWeekSelector.Text = temporaryProject.endWeek.ToString();
            leaderSelector.Text = temporaryProject.projectLeaderId;
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
                startWeek = sWeek,
                endWeek = eWeek,
                projectLeaderId = pLeader
            };

            OnSubmitPushed?.Invoke(this, new SubmitEvent(p));
        }

        private void invoke_Edit_Mode_Submit()
        {
            temporaryProject.id = projectIDSelector.Text;
            temporaryProject.projectLeaderId = leaderSelector.Text;

            if (!int.TryParse(startWeekSelector.Text, out var sWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            if (!int.TryParse(endWeekSelector.Text, out var eWeek))
                throw new ArgumentException("Something went wrong in ComboBox: StartWeek");

            temporaryProject.startWeek = sWeek;
            temporaryProject.endWeek = eWeek;

            OnEditPushed?.Invoke(this, new EventArgs());
        }

        private void updateLeaderComboBoxView()
        {
            foreach (var item in uManager.allUserNames())
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