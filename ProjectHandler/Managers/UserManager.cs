﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProjectRelated;
using Templates;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable once CheckNamespace

namespace UserDomain
{
    [Serializable]
    public class UserManager : AbstractManager
    {

        [field: NonSerialized]
        private UserModel currentlyLoggedIn = null;

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
            return currentlyLoggedIn.Role == UserModel.UserRole.Admin;
        }

        public UserModel loggedIn()
        {
            return currentlyLoggedIn;
        }

        private UserModel verifyCredentials(string userName, string password)
        {
            try
            {
                return (UserModel) Models.First(item => item.ModelIdentity == userName && ((UserModel) item).PassWord == password);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void createUserPrototypes()
        {
            var admin = new UserModel("admin", "1234", UserModel.UserRole.Admin);

            Models.Add(admin);

            /*
             * Initialize five users for testing purposes
             */
            var nUser1 = new UserModel("Jens_Werner2019", "Tango44", UserModel.UserRole.Employee);
            var nUser2 = new UserModel("Niels_Erik1964", "Traktor", UserModel.UserRole.Employee);
            var nUser3 = new UserModel("Bent_Bjerre", "ghb4life", UserModel.UserRole.Employee);
            var nUser4 = new UserModel("Finn_Luger", "hitler", UserModel.UserRole.Employee);
            var nUser5 = new UserModel("Technotonny", "GOA_gartner", UserModel.UserRole.Employee);

            
            AddModel(nUser1);
            AddModel(nUser2);
            AddModel(nUser3);
            AddModel(nUser4);
            AddModel(nUser5);
        }
        

        public override List<string> ListModelIdentities()
        {
            var result = new List<string>();
            foreach (var u in Models)
                result.Add(u.ModelIdentity);

            return result;
        }

        public override void RequestUpdate()
        {
            
        }

        public ListViewItem[] ItemModels(bool fullList = false)
        {
            int uCount = Models.Count,startIndex = 0, index = 0; 
            if (!fullList)
            {
                uCount--;
                startIndex = 1;
            }
            var models = new ListViewItem[uCount];
            for (var i = startIndex; i < Models.Count; i++)
            {
                var u = Models[i];
                models[index++] = u.ItemModel();
            }

            return models;
        }
    }
}