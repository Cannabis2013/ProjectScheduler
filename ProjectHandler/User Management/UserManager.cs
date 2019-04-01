using System;
using System.Collections.Generic;
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
         * Local adress based on the ipv4 adress of the user
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

        public bool logIn(string userName, string password, string localAdress)
        {
            var user = userDb.verifyCredentials(userName, password);
            if (user == null)
                return false;

            user.localAdress = localAdress;
            userLogOut(localAdress, user);

            currentLoggedIn.Add(user);
            return true;
        }

        public void logout(string localAdress)
        {
            userLogOut(localAdress);
        }

        public User.UserRole verifyUserState(string localAdress)
        {
            foreach (var u in currentLoggedIn)
            {
                if (u.localAdress == localAdress)
                    return u.role;
            }
            throw new Exception("User not logged in");
        }

        public User user(string userName) => userDb.user(userName);

        public ListViewItem[] userListModel() => userDb.itemModels();

        public List<string> allUserNames() => userDb.allUserNames();
    
        private void userLogOut(string localAdress, User user = null)
        {
            if (user != null)
            {
                currentLoggedIn.RemoveWhere(c => c.localAdress == localAdress && c.userName() == user.userName());
            }
            else
            {
                currentLoggedIn.RemoveWhere(c => c.localAdress == localAdress);
            }
        }

        private HashSet<User> currentLoggedIn = new HashSet<User>();
        private UserDatabase userDb = new UserDatabase();
    }
}
