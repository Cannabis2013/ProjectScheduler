using System.Windows.Forms;
using VirtualUserDomain;

namespace DialogNamespace
{
    public partial class SelectUserDialog : Form
    {
        public SelectUserDialog(UserManager uManager)
        {
            InitializeComponent();

            UserListView.Items.AddRange(UserManager.userListModel());
        }
    }
}