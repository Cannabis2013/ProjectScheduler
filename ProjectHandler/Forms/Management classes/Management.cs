using System;
using System.Windows.Forms;
using Projecthandler;
using Projecthandler.Abstract_classes_and_interfaces;
using Projecthandler.Forms.Dialog_controls;
using Projecthandler.Forms.Project_and_activity_management.Controls;
using ProjectRelated;
using UserDomain;

namespace Mng
{
    public partial class Management : Form
    {
        private readonly IApplicationProgrammableInterface service;
        private readonly ProjectManagement pManagement;
        private readonly ActivityManagement aManagement;
        private readonly HourManagement hManagement;

        private readonly int MainLayoutCount;

        public event EventHandler<EventArgs> updateParentView;

        public Management(IApplicationProgrammableInterface service)
        {
            InitializeComponent();

            this.service = service;

            pManagement = new ProjectManagement(service);
            aManagement = new ActivityManagement(service);
            hManagement = new HourManagement(service);

            pManagement.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            aManagement.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            hManagement.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            service.SubScribe(pManagement);
            service.SubScribe(aManagement);
            service.SubScribe(hManagement);

            MainLayoutCount = MainLayout.Controls.Count;

            MenuSelectorView.ExpandAll();
        }

        private void MenuSelectorView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void MenuSelectorView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void MenuSelectorView_MouseClick(object sender, MouseEventArgs e)
        {
            var node = MenuSelectorView.GetNodeAt(e.Location);

            if (MainLayout.Controls.Count > MainLayoutCount)
                MainLayout.Controls.RemoveAt(MainLayoutCount);

            if (node != null && node.Text == @"Project management")
            {
                try
                {
                    MainLayout.Controls.Add(pManagement);
                    MainLayout.SetRowSpan(pManagement,2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (node != null && node.Text == "Activity management")
            {
                MainLayout.Controls.Add(aManagement);
                MainLayout.SetRowSpan(aManagement,2);
            }

            if (node != null && node.Text == @"Hour management")
            {
                MainLayout.Controls.Add(hManagement);
                MainLayout.SetRowSpan(hManagement,2);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
