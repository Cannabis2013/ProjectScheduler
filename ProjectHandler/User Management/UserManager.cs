using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
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

        public string[] allUserNames() => userDb.allUserNames();
    
        private void userLogOut(string localAdress, User user = null)
        {
            if (user != null)
            {
                currentLoggedIn.RemoveWhere(c => c.localAdress == localAdress && c.getUserName() == user.getUserName());
            }
            else
            {
                currentLoggedIn.RemoveWhere(c => c.localAdress == localAdress);
            }
        }

        private HashSet<User> currentLoggedIn = new HashSet<User>();
        private UserDatabase userDb = new UserDatabase();
    }

    class UserDatabase
    {
        public UserDatabase()
        {
            var admin = new User("admin", "1234", User.UserRole.Admin);
            users.Add(admin);

            /*
             * Initialize five users for testing purposes
             */
            var nUser1 = new User("Jens_Werner2019", "Tango44",User.UserRole.Employee);
            var nUser2 = new User("Niels_Erik1964", "Traktor", User.UserRole.Employee);
            var nUser3 = new User("Bent_Bjerre", "ghb4life", User.UserRole.Employee);
            var nUser4 = new User("Finn_Luger_P38", "Elmer_Fjott",User.UserRole.Employee);
            var nUser5 = new User("Technotonny","GOA_gartner", User.UserRole.Employee);

            users.Add(nUser1);
            users.Add(nUser2);
            users.Add(nUser3);
            users.Add(nUser4);
            users.Add(nUser5);
        }

        /*
         * Verify section
         * Verify user credentials and return a copy of the user corresponding to the credentials
         * Check if user already exists
         */

        public User verifyCredentials(string userName, string password)
        {
            foreach (var u in users)
                if (u.getUserName() == userName && u.getPassWord() == password)
                {
                    var userCopy = new User(u.getUserName(), u.getPassWord(), u.role);
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

            var newUser = new User(userName, passWord, role);
            users.Add(newUser);
        }

        public bool removeUser(User user)
        {
            return users.Remove(user);
        }

        /*
         * Retrieve user object by userName
         * Retrieve list of usernames
         */

        public User user(string userName)
        {
            foreach (var u in users)
            {
                if (u.getUserName() == userName)
                    return u;
            }
            return null;
        }

        public string[] allUserNames()
        {
            int count = users.Count, index = 0;
            var result = new string[count];
            foreach (var u in users)
                result[index++] = u.getUserName();

            return result;
        }

        /*
         * Private section
         */

        private bool userNameExist(string username)
        {
            foreach (var u in users)
            {
                if (u.getUserName() == username)
                    return true;
            }
            return false;
        }

        public ListViewItem[] itemModels(bool fullList = false)
        {
            int uCount = fullList ? users.Count : users.Count - 1, index = 0;
            var models = new ListViewItem[uCount];
            foreach (var u in users)
            {
                if(!fullList && u.role == User.UserRole.Admin)
                    continue;

                var model = new ListViewItem(u.getUserName())
                {
                    ImageIndex = 0
                };

                var fullName = new StringBuilder("Full name: ");
                fullName.Append(u.fullName);

                model.SubItems.Add(fullName.ToString());

                var role = new StringBuilder("Users role: ");
                
                if (u.role == User.UserRole.Admin)
                    role.Append("Admin");
                else if (u.role == User.UserRole.Leader)
                    role.Append("Project Leader");
                else
                    role.Append("Employee");

                model.SubItems.Add(role.ToString());
                models[index++] = model;
            }
            return models;
        }

        private HashSet<User> users = new HashSet<User>();
    }
}
