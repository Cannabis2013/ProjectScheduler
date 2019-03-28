using System.Windows.Forms;
using VirtualUserDomain;
using Projecthandler.Custom_events;
using Projecthandler.Class_forms;
using System;

namespace MainUserSpace
{
    public partial class AdminView : Form
    {
        public AdminView(UserManager manager)
        {
            InitializeComponent();
            ListViewItem item = new ListViewItem();
            uManager = manager;
            
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uManager.logout(UserManager.getLocalAddress());
            logoutEvent?.Invoke(this, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            uManager.logout(UserManager.getLocalAddress());
            closeEvent?.Invoke(this, e);
        }


        public event EventHandler<EventArgs> logoutEvent;
        public event EventHandler<EventArgs> closeEvent;
        private UserManager uManager;
    }

}
