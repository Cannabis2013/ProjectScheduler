using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Projecthandler.Project_related;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable once CheckNamespace

namespace Projecthandler.User_Management
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

        public static string GetLocalAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            return "000.000.000.000";
        }

        public static bool LogIn(string userName, string password, string localAddress)
        {
            var user = _userDb.VerifyCredentials(userName, password);
            if (user == null)
                return false;

            user.LocalAddress = localAddress;
            UserLogOut(localAddress, user);

            _currentLoggedIn.Add(user);
            return true;
        }

        public static void Logout(string localAddress)
        {
            UserLogOut(localAddress);
        }

        public static User.UserRole VerifyUserState()
        {
            foreach (var u in _currentLoggedIn)
                if (u.LocalAddress == GetLocalAddress())
                    return u.Role;
            throw new Exception("User not logged in");
        }

        public static User User(string userName)
        {
            return _userDb.User(userName);
        }

        public static User CurrentlyLoggedIn()
        {
            return _currentLoggedIn.Where(item => item.LocalAddress == GetLocalAddress()).ElementAt(0);
        }

        public static ListViewItem[] UserListModel()
        {
            return _userDb.ItemModels();
        }

        public static List<string> AllUserNames()
        {
            return _userDb.AllUserNames().Where(item => item != "admin").ToList();
        }

        /*
         * Return an item model for a given user with the following data:
         * - Username
         * - Full name
         * - Number of activities assigned
         * - Users role
         */

        public ListViewItem UserItemModel(string userName)
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
            uRole.Append(User_Management.User._roleStringRepresentation(user.Role));
            model.SubItems.Add(uRole.ToString());

            return model;
        }

        public static ListViewItem[] UserListViewItems()
        {
            return AllUserNames().Select(item => new ListViewItem(item)).ToArray();
        }

        private static void UserLogOut(string localAddress, User user = null)
        {
            if (user != null)
                _currentLoggedIn.RemoveWhere(c => c.LocalAddress == localAddress && c.UserName() == user.UserName());
            else
                _currentLoggedIn.RemoveWhere(c => c.LocalAddress == localAddress);
        }
    }
}