using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace VirtualUserDomain
{
    public class UserManager
    {
        public UserManager()
        {}

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
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public bool logIn(string userName, string password, string localAdress)
        {
            User user = userDB.verifyCredentials(userName, password);
            if (user == null)
                return false;

            user.LocalAdress = localAdress;
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
            foreach (User u in currentLoggedIn)
            {
                if (u.LocalAdress == localAdress)
                    return u.Role;
            }
            throw new Exception("User not logged in");
        }

        public List<ListViewItem> userListModel() => userDB.itemModels();
    
        private void userLogOut(string localAdress, User user = null)
        {
            if (user != null)
            {
                currentLoggedIn.RemoveWhere(c => c.LocalAdress == localAdress && c.UserName == user.UserName);
            }
            else
            {
                currentLoggedIn.RemoveWhere(c => c.LocalAdress == localAdress);
            }
        }
        

        private HashSet<User> currentLoggedIn = new HashSet<User>();
        private UserDatabase userDB = new UserDatabase();
    }
}
