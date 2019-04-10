using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projecthandler;
using Projecthandler.Forms.Dialog_controls;
using Projecthandler.Forms.Project_and_activity_management.Controls;
using ProjectRelated;
using VirtualUserDomain;

namespace Mng
{
    public partial class Management : Form
    {
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

        private readonly int MainLayoutCount;

        public event EventHandler<EventArgs> updateParentView;

        public Management(ProjectManager pManager, UserManager uManager)
        {
            InitializeComponent();
            this.pManager = pManager;
            this.uManager = uManager;

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
                    var pManagement = new ProjectManagement(pManager, uManager)
                    {
                        Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
                    };

                    MainLayout.Controls.Add(pManagement);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (node != null && node.Text == "Activity management")
            {
                var aManagement = new ActivityManagement(pManager,uManager);
                aManagement.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                aManagement.updateParentView += _updateParentView;
                MainLayout.Controls.Add(aManagement);
            }

            if (node != null && node.Text == @"Hour management")
            {
                var hourManagement = new HourManagement(uManager,pManager);
                hourManagement.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                MainLayout.Controls.Add(hourManagement);
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

        private void _updateParentView(object sender, EventArgs e) => updateParentView?.Invoke(sender, e);

        private void Management_FormClosed(object sender, FormClosedEventArgs e) => updateParentView?.Invoke(sender, e);
    }
}
