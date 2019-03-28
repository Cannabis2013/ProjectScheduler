using System.Windows.Forms;

namespace VirtualUserDomain
{
    public class UserItemModel : ListViewItem
    {
        public UserItemModel()
        {
        }

        public UserItemModel(ref User user)
        {
            Text = user.UserName;
            
        }
    }
}
