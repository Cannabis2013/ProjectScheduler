using System.Windows.Forms;
using Projecthandler.User_Management;

namespace Projecthandler.Forms.Dialogs
{
    public partial class SelectUserDialog : Form
    {
        public SelectUserDialog(UserManager uManager)
        {
            InitializeComponent();

            UserListView.Items.AddRange(UserManager.UserListModel());
        }
    }
}