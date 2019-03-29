using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
            return "000.000.000.000";
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

        public User user(string userName)
        {
            foreach(User u in userDB.Users)
            {
                if (u.UserName == userName)
                    return u;
            }
            return null;
        }

        public List<ListViewItem> userListModel() => userDB.itemModels();

        public string[] allUserNames()
        {
            int count = userDB.Users.Count, index = 0;
            string[] result = new string[count];
            foreach (User u in userDB.Users)
                result[index++] = u.UserName;

            return result;
        }
    
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

    class UserDatabase
    {
        public UserDatabase()
        {
            User admin = new User("admin", "1234", User.UserRole.Admin);
            Users.Add(admin);
        }

        /*
         * Verify section
         * Verify user credentials and return a copy of the user corresponding to the credentials
         * Check if user already exists
         */

        public User verifyCredentials(string userName, string password)
        {
            foreach (User u in Users)
                if (u.UserName == userName && u.passWord == password)
                {
                    User userCopy = new User(u.UserName, u.passWord, u.Role);
                    return userCopy;
                }

            return null;
        }

        /*
         * The admin section
         * Admin is allowed to create and delete users
         */

        public void createUser(string userName, string passWord, User.UserRole role)
        {
            if (userNameExist(userName))
                return;

            User newUser = new User(userName, passWord, role);
            Users.Add(newUser);
        }

        public bool removeUser(User user)
        {
            return Users.Remove(user);
        }

        /*
         * Private section
         */

        private bool userNameExist(string username)
        {
            foreach (User u in Users)
            {
                if (u.UserName == username)
                    return true;
            }
            return false;
        }

        public List<ListViewItem> itemModels()
        {
            List<ListViewItem> models = new List<ListViewItem>();
            foreach (User u in Users)
            {
                ListViewItem model = new ListViewItem(u.UserName);

                StringBuilder fullName = new StringBuilder("Full name: ");
                fullName.Append(u.fullName);

                model.SubItems.Add(fullName.ToString());

                StringBuilder role = new StringBuilder("Users role: ");
                
                if (u.Role == User.UserRole.Admin)
                    role.Append("Admin");
                else if (u.Role == User.UserRole.leader)
                    role.Append("Project Leader");
                else
                    role.Append("Employee");

                model.SubItems.Add(role.ToString());
                models.Add(model);
            }
            return models;
        }

        internal HashSet<User> Users = new HashSet<User>();
    }
}
