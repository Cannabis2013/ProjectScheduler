using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using ProjectNameSpace;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable once CheckNamespace

namespace VirtualUserDomain
{
    public class UserManager
    {
        private static HashSet<User> _currentLoggedIn = new HashSet<User>();
        private static UserDatabase _userDb = new UserDatabase();
        private readonly ProjectManager pManager;

        public UserManager(ProjectManager pManager)
        {
            this.pManager = pManager;
        }

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

        public static bool logIn(string userName, string password, string localAddress)
        {
            var user = _userDb.verifyCredentials(userName, password);
            if (user == null)
                return false;

            user.localAddress = localAddress;
            userLogOut(localAddress, user);

            _currentLoggedIn.Add(user);
            return true;
        }

        public static void logout(string localAddress)
        {
            userLogOut(localAddress);
        }

        public static User.UserRole verifyUserState()
        {
            foreach (var u in _currentLoggedIn)
                if (u.localAddress == getLocalAddress())
                    return u.role;
            throw new Exception("User not logged in");
        }

        public static User user(string userName)
        {
            return _userDb.user(userName);
        }

        public static User currentlyLoggedIn()
        {
            return _currentLoggedIn.Where(item => item.localAddress == getLocalAddress()).ElementAt(0);
        }

        public static ListViewItem[] userListModel()
        {
            return _userDb.itemModels();
        }

        public static List<string> allUserNames()
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

        public ListViewItem userItemModel(string userName)
        {
            var user = UserManager.user(userName);
            var model = new ListViewItem(userName);

            // ReSharper disable once InconsistentNaming
            var FullName = new StringBuilder("Fullname: ");
            FullName.Append(user.fullName());
            model.SubItems.Add(FullName.ToString());

            var numbersOfActivitiesAssigned = pManager.activities(userName).Count;

            var activityCount = new StringBuilder("Number of activities assigned: ");
            activityCount.Append(numbersOfActivitiesAssigned);
            model.SubItems.Add(activityCount.ToString());

            var uRole = new StringBuilder("User role: ");
            uRole.Append(User._roleStringRepresentation(user.role));
            model.SubItems.Add(uRole.ToString());

            return model;
        }

        public static ListViewItem[] userListViewItems()
        {
            return allUserNames().Select(item => new ListViewItem(item)).ToArray();
        }

        private static void userLogOut(string localAddress, User user = null)
        {
            if (user != null)
                _currentLoggedIn.RemoveWhere(c => c.localAddress == localAddress && c.userName() == user.userName());
            else
                _currentLoggedIn.RemoveWhere(c => c.localAddress == localAddress);
        }
    }
}