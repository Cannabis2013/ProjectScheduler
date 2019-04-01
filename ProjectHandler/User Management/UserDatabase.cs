using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace VirtualUserDomain
{
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
                if (u.userName() == userName && u.passWord() == password)
                {
                    var userCopy = new User(u.userName(), u.passWord(), u.role);
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
                if (u.userName() == userName)
                    return u;
            }
            return null;
        }

        public string[] allUserNames()
        {
            int count = users.Count, index = 0;
            var result = new string[count];
            foreach (var u in users)
                result[index++] = u.userName();

            return result;
        }

        /*
         * Private section
         */

        private bool userNameExist(string username)
        {
            foreach (var u in users)
            {
                if (u.userName() == username)
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

                var model = new ListViewItem(u.userName())
                {
                    ImageIndex = 0
                };

                var fullName = new StringBuilder("Full name: ");
                fullName.Append(u.fullName);

                model.SubItems.Add(fullName.ToString());

                var role = new StringBuilder("Users role: ");
                
                if(u.role == User.UserRole.Admin)
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