﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework;
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
            initializeSelectors();

            mode = DialogMode.AddMode;
        }

        public ProjectDialog(Project p)
        {
            temporaryProject = p;

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
            

            OnSubmitPushed?.Invoke(this, new SubmitEvent(title, sWeek, eWeek, pLeader));
        }

        private void invoke_Edit_Mode_Submit()
        {
            
        }

        private void updateLeaderComboBoxView()
        {
            foreach (string item in UserManager.allUserNames())
                leaderSelector.Items.Add(item);
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
