using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Projecthandler.Templates_and_interfaces;
using ProjectRelated;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable once CheckNamespace

namespace VirtualUserDomain
{
    [Serializable]
    public class UserManager : AbstractManager<UserModel, ListViewItem>
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

        public UserModel user(string userName)
        {
            foreach (var u in ModelList)
                if (u.ModelIdentity == userName)
                    return u;
            return null;
        }

        public ListViewItem DetailedItemModels(string userName, ProjectManager pManager)
        {
            var u = user(userName);
            var model = new ListViewItem(userName);
            
            var numbersOfActivitiesAssigned = pManager.ActivityModels(userName).Count;
            
            model.SubItems.Add(numbersOfActivitiesAssigned.ToString());

            var userRole = UserModel._roleStringRepresentation(u.Role);

            model.SubItems.Add(userRole);

            return model;
        }

        private UserModel verifyCredentials(string userName, string password)
        {
            try
            {
                return ModelList.First(item => item.ModelIdentity == userName && item.PassWord == password);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void createUserPrototypes()
        {
            var admin = new UserModel("admin", "1234", UserModel.UserRole.Admin);

            ModelList.Add(admin);

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

        public override UserModel Model(string id)
        {
            throw new NotImplementedException();
        }

        public override List<string> ListModelIdentities()
        {
            var result = new List<string>();
            foreach (var u in ModelList)
                result.Add(u.ModelIdentity);

            return result;
        }

        public ListViewItem[] ItemModels(bool fullList = false)
        {
            int uCount = fullList ? ModelList.Count : ModelList.Count - 1, index = 0;
            var models = new ListViewItem[uCount];
            foreach (var u in ModelList)
                models[index++] = u.ItemModel();

            return models;
        }
    }
}