using System;
using System.Windows.Forms;
using VirtualUserDomain;
namespace Projecthandler.Forms.Project
{
    public partial class AddProjectDialog : Form
    {
        public AddProjectDialog(UserManager uManager)
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs> on_Submit_Pushed;
    }
}
