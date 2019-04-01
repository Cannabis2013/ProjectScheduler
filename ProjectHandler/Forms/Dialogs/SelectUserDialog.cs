using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
