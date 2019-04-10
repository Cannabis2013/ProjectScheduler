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
using Projecthandler.Templates_and_interfaces;
using ProjectRelated;
using VirtualUserDomain;

namespace Projecthandler.Forms.Dialogs
{
    public partial class AddProjectControl : UserControl, IDialogInterface<EventArgs>
    {
        private readonly DialogMode mode;

        private readonly Project temporaryProject;
        private readonly UserManager uManager;

        public void initializeListControls()
        {
            throw new NotImplementedException();
        }

        public void InitializeDialogValues()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<EventArgs> OnSaveClicked;
        public event EventHandler<EventArgs> OnEditClicked;
        public event EventHandler<EventArgs> OnCancelClicked;

        public AddProjectControl( UserManager uManager)
        {
            this.uManager = uManager;
            InitializeComponent();
            
            updateLeaderComboBoxView();

            mode = DialogMode.AddMode;
        }

        public AddProjectControl(Project p, UserManager uManager)
        {
            temporaryProject = p;
            this.uManager = uManager;

            InitializeComponent();
            updateLeaderComboBoxView();
            initializeDialogValues();

            mode = DialogMode.EditMode;
        }

        private void invoke_Add_Mode_Submit()
        {
            string title = projectIDSelector.Text, pLeader = leaderSelector.Text;

            var p = new Project(title)
            {
                StartDate = StartDateSelector.Value,
                EndDate = EndDateSelector.Value,
                projectLeaderId = pLeader
            };

            OnSaveClicked?.Invoke(this, new SubmitEvent(p));
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

        private void invoke_Edit_Mode_Submit()
        {
            temporaryProject.id = projectIDSelector.Text;
            temporaryProject.projectLeaderId = leaderSelector.Text;
            
            temporaryProject.StartDate = StartDateSelector.Value;
            temporaryProject.EndDate = EndDateSelector.Value;

            OnEditClicked?.Invoke(this, new EventArgs());
        }

        private void updateLeaderComboBoxView()
        {
            foreach (var item in uManager.allUserNames())
                leaderSelector.Items.Add(item);
        }

        private void initializeDialogValues()
        {
            projectIDSelector.Text = temporaryProject.id;
            StartDateSelector.Value = temporaryProject.StartDate;
            EndDateSelector.Value = temporaryProject.EndDate;
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
