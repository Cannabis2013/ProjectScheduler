﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Projecthandler.User_Management
{
    internal class UserDatabase
    {
        private readonly HashSet<User> users = new HashSet<User>();

        public UserDatabase()
        {
            var admin = new User("admin", "1234", User_Management.User.UserRole.Admin, "Martin Hansen");

            users.Add(admin);

            /*
             * Initialize five users for testing purposes
             */
            var nUser1 = new User("Jens_Werner2019", "Tango44", User_Management.User.UserRole.Employee, "Jens Werner");
            var nUser2 = new User("Niels_Erik1964", "Traktor", User_Management.User.UserRole.Employee, "Niels Pede Erik");
            var nUser3 = new User("Bent_Bjerre", "ghb4life", User_Management.User.UserRole.Employee, "Bent Bjerre");
            var nUser4 = new User("Finn_Luger", "hitler", User_Management.User.UserRole.Employee, "Engel Franz");
            var nUser5 = new User("Technotonny", "GOA_gartner", User_Management.User.UserRole.Employee, "Tonny Jørgensen");

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

        public User VerifyCredentials(string userName, string password)
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

        /*
         * The admin section
         * Admin is allowed to create and delete users
         */

        public void CreateUser(string userName, string passWord, User.UserRole role, string fullName)
        {
            if (UserNameExist(userName))
                return;

            var newUser = new User(userName, passWord, role, fullName);
            users.Add(newUser);
        }

        public bool RemoveUser(User user)
        {
            return users.Remove(user);
        }

        /*
         * Retrieve user object by userName
         * Retrieve list of usernames
         */

        public User User(string userName)
        {
            foreach (var u in users)
                if (u.UserName() == userName)
                    return u;
            return null;
        }

        public List<string> AllUserNames()
        {
            var result = new List<string>();
            foreach (var u in users)
                result.Add(u.UserName());

            return result;
        }

        /*
         * Private section
         */

        private bool UserNameExist(string username)
        {
            foreach (var u in users)
                if (u.UserName() == username)
                    return true;
            return false;
        }

        public ListViewItem[] ItemModels(bool fullList = false)
        {
            int uCount = fullList ? users.Count : users.Count - 1, index = 0;
            var models = new ListViewItem[uCount];
            foreach (var u in users)
            {
                if (!fullList && u.Role == User_Management.User.UserRole.Admin)
                    continue;

                var model = new ListViewItem(u.UserName())
                {
                    ImageIndex = 0
                };

                var fullName = new StringBuilder("Full name: ");
                fullName.Append(u.FullName());

                model.SubItems.Add(fullName.ToString());

                var role = new StringBuilder("Users role: ");

                role.Append(u.Role == User_Management.User.UserRole.Admin ? "Admin" : "Employee");

                model.SubItems.Add(role.ToString());
                models[index++] = model;
            }

            return models;
        }
    }
}