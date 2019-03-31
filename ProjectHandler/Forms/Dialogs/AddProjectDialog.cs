using System;
using System.Windows.Forms;
using VirtualUserDomain;

namespace DialogNamespace
{
    public partial class AddProjectDialog : Form
    {
        public AddProjectDialog(UserManager uManager)
        {
            InitializeComponent();
            this.uManager = uManager;
        }

        public event EventHandler<EventArgs> On_Submit_Pushed;

        protected virtual void On_On_Submit_Pushed(object sender, EventArgs e)
        {
            On_Submit_Pushed?.Invoke(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var uDialog = new SelectUserDialog(uManager);
            uDialog.ShowDialog(this);

        }

        private UserManager uManager;
    }
}
