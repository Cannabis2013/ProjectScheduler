using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using ProjectRelated;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable once CheckNamespace

namespace VirtualUserDomain
{
    [Serializable]
    public class UserManager
    {
        [field: NonSerialized]
        private HashSet<User> _currentLoggedIn = new HashSet<User>();
        private UserDatabase _userDb = new UserDatabase();
        

        /*
         * Local address based on the ipv4 address of the user
         */

        public static string getLocalAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            return "000.000.000.000";
        }

        public bool logIn(string userName, string password, string localAddress)
        {
            var user = _userDb.verifyCredentials(userName, password);
            if (user == null)
                return false;

            user.LocalAddress = localAddress;
            UserLogOut(localAddress, user);

            _currentLoggedIn.Add(user);
            return true;
        }

        public void logout(string localAddress)
        {
            UserLogOut(localAddress);
        }

        public User.UserRole verifyUserState()
        {
            foreach (var u in _currentLoggedIn)
                if (u.LocalAddress == getLocalAddress())
                    return u.Role;
            throw new Exception("User not logged in");
        }

        public User User(string userName)
        {
            return _userDb.user(userName);
        }

        public User currentlyLoggedIn()
        {
            return _currentLoggedIn.Where(item => item.LocalAddress == getLocalAddress()).ElementAt(0);
        }

        public ListViewItem[] userListModel()
        {
            return _userDb.itemModels();
        }

        public List<string> allUserNames()
        {
            return _userDb.allUserNames().Where(item => item != "admin").ToList();
        }

        /*
         * Return an item model for a given user with the following data:
         * - Username
         * - Full name
         * - Number of activities assigned
         * - Users role
         */

        public ListViewItem UserItemModel(string userName, ProjectManager pManager)
        {
            var user = User(userName);
            var model = new ListViewItem(userName);

            // ReSharper disable once InconsistentNaming
            var FullName = new StringBuilder("Fullname: ");
            FullName.Append(user.FullName());
            model.SubItems.Add(FullName.ToString());

            var numbersOfActivitiesAssigned = pManager.Activities(userName).Count;

            var activityCount = new StringBuilder("Number of activities assigned: ");
            activityCount.Append(numbersOfActivitiesAssigned);
            model.SubItems.Add(activityCount.ToString());

            var uRole = new StringBuilder("User role: ");
            uRole.Append(VirtualUserDomain.User._roleStringRepresentation(user.Role));
            model.SubItems.Add(uRole.ToString());

            return model;
        }

        public ListViewItem[] userListViewItems()
        {
            return allUserNames().Select(item => new ListViewItem(item)).ToArray();
        }

        private void UserLogOut(string localAddress, User user = null)
        {
            if (user != null)
                _currentLoggedIn.RemoveWhere(c => c.LocalAddress == localAddress && c.UserName() == user.UserName());
            else
                _currentLoggedIn.RemoveWhere(c => c.LocalAddress == localAddress);
        }
    }
}