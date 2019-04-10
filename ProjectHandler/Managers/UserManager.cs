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
        private readonly HashSet<User> users = new HashSet<User>();

        [field: NonSerialized]
        private User currentlyLoggedIn = null;

        public UserManager()
        {
            createUserPrototypes();
        }

        public bool logIn(string userName, string password)
        {
            var user = verifyCredentials(userName, password);
            if (user == null)
                return false;

            currentlyLoggedIn = user;

            return true;
        }

        public void logout()
        {
            currentlyLoggedIn = null;
        }

        public bool isAdmin()
        {
            return currentlyLoggedIn.Role == User.UserRole.Admin;
        }

        public User loggedIn()
        {
            return currentlyLoggedIn;
        }

        public User user(string userName)
        {
            foreach (var u in users)
                if (u.UserName() == userName)
                    return u;
            return null;
        }

        public List<string> allUserNames()
        {
            var result = new List<string>();
            foreach (var u in users)
                result.Add(u.UserName());

            return result;
        }

        public ListViewItem UserItemModel(string userName, ProjectManager pManager)
        {
            var u = user(userName);
            var model = new ListViewItem(userName);
            
            var numbersOfActivitiesAssigned = pManager.Activities(userName).Count;

            var activityCount = new StringBuilder("Number of activities assigned: ");
            activityCount.Append(numbersOfActivitiesAssigned);
            model.SubItems.Add(activityCount.ToString());

            var uRole = new StringBuilder("User role: ");
            uRole.Append(VirtualUserDomain.User._roleStringRepresentation(u.Role));
            model.SubItems.Add(uRole.ToString());

            return model;
        }

        public ListViewItem[] itemModels(bool fullList = false)
        {
            int uCount = fullList ? users.Count : users.Count - 1, index = 0;
            var models = new ListViewItem[uCount];
            foreach (var u in users)
            {
                if (!fullList && u.Role == User.UserRole.Admin)
                    continue;

                var model = new ListViewItem(u.UserName())
                {
                    ImageIndex = 0
                };
                
                var role = new StringBuilder("Users role: ");

                role.Append(u.Role == User.UserRole.Admin ? "Admin" : "Employee");

                model.SubItems.Add(role.ToString());
                models[index++] = model;
            }

            return models;
        }

        public ListViewItem[] userListViewItems()
        {
            return allUserNames().Select(item => new ListViewItem(item)).ToArray();
        }

        private User verifyCredentials(string userName, string password)
        {
            try
            {
                return users.First(item => item.UserName() == userName && item.PassWord() == password);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void createUserPrototypes()
        {
            var admin = new User("admin", "1234", User.UserRole.Admin);

            users.Add(admin);

            /*
             * Initialize five users for testing purposes
             */
            var nUser1 = new User("Jens_Werner2019", "Tango44", User.UserRole.Employee);
            var nUser2 = new User("Niels_Erik1964", "Traktor", User.UserRole.Employee);
            var nUser3 = new User("Bent_Bjerre", "ghb4life", User.UserRole.Employee);
            var nUser4 = new User("Finn_Luger", "hitler", User.UserRole.Employee);
            var nUser5 = new User("Technotonny", "GOA_gartner", User.UserRole.Employee);

            users.Add(nUser1);
            users.Add(nUser2);
            users.Add(nUser3);
            users.Add(nUser4);
            users.Add(nUser5);
        }
    }
}