using System;
using System.Windows.Forms;
using Projecthandler.Events;
using ProjectRelated;
using Templates;
using UserDomain;

namespace Projecthandler.Forms.Dialogs
{
    public partial class AddProjectControl : UserControl, IDialogInterface<EventArgs>
    {
        private readonly DialogMode mode;
        private readonly ListViewItem[] UserList;
        private readonly ProjectModel temporaryProject;

        public void initializeListControls()
        {
            throw new NotImplementedException();
        }

        public void InitializeDialogValues()
        {
            projectIDSelector.Text = temporaryProject.ModelIdentity;
            StartDateSelector.Value = temporaryProject.StartDate;
            EndDateSelector.Value = temporaryProject.EndDate;
            leaderSelector.Text = temporaryProject.projectLeaderId;
            DescriptionBoxSelector.Text = temporaryProject.ShortDescription;
        }

        public event EventHandler<EventArgs> OnSaveClicked;
        public event EventHandler<EventArgs> OnEditClicked;
        public event EventHandler<EventArgs> OnCancelClicked;

        public AddProjectControl(ListViewItem[] UserList)
        {
            this.UserList = UserList;
            InitializeComponent();
            
            updateLeaderComboBoxView();

            mode = DialogMode.AddMode;
        }

        public AddProjectControl(ProjectModel p, ListViewItem[] UserList)
        {
            temporaryProject = p;
            this.UserList = UserList;

            InitializeComponent();
            updateLeaderComboBoxView();
            InitializeDialogValues();

            mode = DialogMode.EditMode;
        }

        private void invoke_Add_Mode_Submit()
        {
            string title = projectIDSelector.Text, pLeader = leaderSelector.Text;

            var p = new ProjectModel()
            {
                ModelIdentity = title,
                StartDate = StartDateSelector.Value,
                EndDate = EndDateSelector.Value,
                projectLeaderId = pLeader,
                ShortDescription = DescriptionBoxSelector.Text
            };

            OnSaveClicked?.Invoke(this, new SubmitEvent(p));
        }

        private void invoke_Edit_Mode_Submit()
        {
            temporaryProject.ModelIdentity = projectIDSelector.Text;
            temporaryProject.projectLeaderId = leaderSelector.Text;
            
            temporaryProject.StartDate = StartDateSelector.Value;
            temporaryProject.EndDate = EndDateSelector.Value;
            temporaryProject.ShortDescription = DescriptionBoxSelector.Text;

            OnEditClicked?.Invoke(this, new EventArgs());
        }

        private void updateLeaderComboBoxView()
        {
            foreach (var item in UserList)
                leaderSelector.Items.Add(item.Text);
        }

        private enum DialogMode
        {
            AddMode,
            EditMode
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnCancelClicked?.Invoke(this, e);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (projectIDSelector.Text == "")
                return;

            if (mode == DialogMode.AddMode)
                invoke_Add_Mode_Submit();
            else
                invoke_Edit_Mode_Submit();
        }
    }
}
