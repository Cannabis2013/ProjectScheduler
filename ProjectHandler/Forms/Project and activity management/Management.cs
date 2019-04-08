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
using Projecthandler.Forms.Project_and_activity_management.Controls;
using ProjectRelated;
using VirtualUserDomain;

namespace Mng
{
    public partial class Management : Form
    {
        private ProjectManager pManager;
        private UserManager uManager;

        public event EventHandler<EventArgs> updateParentView;

        public Management(ProjectManager pManager, UserManager uManager)
        {
            InitializeComponent();
            this.pManager = pManager;
            this.uManager = uManager;

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
            if (node != null && node.Text == "Project management")
            {
                try
                {
                    var pManagement = new ProjectManagement(pManager, uManager);
                    pManagement.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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

                /*
                 * Remember to implement a slot for invoking updateParentView eventhandler
                 */
            }

            return;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Management_FormClosed(object sender, FormClosedEventArgs e)
        {
            updateParentView?.Invoke(sender, e);
        }
    }
}
