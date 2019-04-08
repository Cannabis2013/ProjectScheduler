using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projecthandler.Events;
using ProjectRelated;
using VirtualUserDomain;

namespace Projecthandler.Forms.Dialogs
{
    public partial class AddProjectControl : UserControl
    {
        private readonly DialogMode mode;

        private readonly Project temporaryProject;
        private readonly UserManager uManager;

        public event EventHandler<SubmitEvent> OnSaveClicked;
        public event EventHandler<EventArgs> OnEditClicked;
        public event EventHandler<EventArgs> OnCancelClicked;

        public AddProjectControl( UserManager uManager)
        {
            this.uManager = uManager;
            InitializeComponent();

            initializeSelectors();

            mode = DialogMode.AddMode;
        }

        public AddProjectControl(Project p, UserManager uManager)
        {
            temporaryProject = p;
            this.uManager = uManager;

            InitializeComponent();
            initializeSelectors();
            initializeDialogValues();

            mode = DialogMode.EditMode;
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

            OnSaveClicked?.Invoke(this, new SubmitEvent(p));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (projectIDSelector.Text == "" || startWeekSelector.Text == "")
                return;

            if (mode == DialogMode.AddMode)
                invoke_Add_Mode_Submit();
            else
                invoke_Edit_Mode_Submit();
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

            OnEditClicked?.Invoke(this, new EventArgs());
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

        private void updateLeaderComboBoxView()
        {
            foreach (var item in uManager.allUserNames())
                leaderSelector.Items.Add(item);
        }

        private void initializeDialogValues()
        {
            projectIDSelector.Text = temporaryProject.id;
            startWeekSelector.Text = temporaryProject.startWeek.ToString();
            endWeekSelector.Text = temporaryProject.endWeek.ToString();
            leaderSelector.Text = temporaryProject.projectLeaderId;
        }

        private enum DialogMode
        {
            AddMode,
            EditMode
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnCancelClicked?.Invoke(this,e);
        }
    }
}
