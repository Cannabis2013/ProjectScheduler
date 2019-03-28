using System;
using System.Collections.Generic;

namespace VirtualUserDomain
{
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
                if (u.UserName == userName && u.Password == password)
                {
                    User userCopy = new User(u.UserName, u.Password, u.Role);
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


        private HashSet<User> Users = new HashSet<User>();
    }

    public class User
    {
        public User(string userName, string passWord, UserRole role)
        {
            
            this.UserName = userName;
            this.Password = passWord;
            this.Role = role;
        }

        internal string UserName { get; }
        internal string Password { get; }

        public UserRole Role { get; }

        public enum UserRole { Admin, leader, employee };

        public string LocalAdress { get; set; }
    }
}
