using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable once CheckNamespace

namespace VirtualUserDomain
{
    public class UserManager
    {
        public UserManager()
        {}

        /*
         * Local address based on the ipv4 address of the user
         */

        public static string getLocalAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
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

        public static User.UserRole verifyUserState(string localAddress)
        {
            foreach (var u in _currentLoggedIn)
            {
                if (u.localAddress == localAddress)
                    return u.role;
            }
            throw new Exception("User not logged in");
        }

        public static User user(string userName) => _userDb.user(userName);
        public static User currentlyLoggedIn() => _currentLoggedIn.Where(item => item.localAddress == getLocalAddress()).ElementAt(0); 

        public static ListViewItem[] userListModel() => _userDb.itemModels();

        public static List<string> allUserNames() => _userDb.allUserNames().Where(item => item != "admin").ToList();
    
        private static void userLogOut(string localAddress, User user = null)
        {
            if (user != null)
            {
                _currentLoggedIn.RemoveWhere(c => c.localAddress == localAddress && c.id == user.id);
            }
            else
            {
                _currentLoggedIn.RemoveWhere(c => c.localAddress == localAddress);
            }
        }

        private static HashSet<User> _currentLoggedIn = new HashSet<User>();
        private static UserDatabase _userDb = new UserDatabase();
    }
}
