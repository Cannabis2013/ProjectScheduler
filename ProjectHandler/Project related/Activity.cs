using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Projecthandler.Templates;
using Projecthandler.User_Management;


namespace Projecthandler.Project_related
{
    [Serializable]
    public class Activity : ItemModelEntity<ListViewItem>
    {
        private readonly List<string> assignedUserIdentities = new List<string>();
        private readonly List<TimeObject> registeredTimeObjects = new List<TimeObject>();

        public Activity(string title, int sWeek, int eWeek, string project)
        {
            EntityTitle = title;
            ParentProjectId = project;
            StartWeek = sWeek;
            EndWeek = eWeek;
        }

        public Activity(Activity copy)
        {
            ParentProjectId = null;
            EntityTitle = copy.EntityTitle;
            StartWeek = copy.StartWeek;
            EndWeek = copy.EndWeek;
            assignedUserIdentities = copy.assignedUserIdentities;
            registeredTimeObjects = copy.registeredTimeObjects;
            ParentProjectId = copy.ParentProjectId;
        }

        public override string Id
        {
            get => EntityTitle;
            set => EntityTitle = value;
        }

        public int StartWeek { get; set; }

        public int EndWeek { get; set; }

        public string ParentProjectId { get; set; }

        public int EstimatedDuration()
        {
            return EndWeek - StartWeek;
        }

        public void AssignUser(string userId)
        {
            assignedUserIdentities.Add(userId);
        }

        public void AssignUsers(List<string> userIDs)
        {
            foreach (var userId in userIDs) assignedUserIdentities.Add(userId);
        }

        public bool IsUserAssigned()
        {
            var userName = UserManager.CurrentlyLoggedIn().UserName();
            return assignedUserIdentities.Any(item => item == userName);
        }

        public bool IsUserAssigned(string userName)
        {
            return assignedUserIdentities.Any(item => item == userName);
        }

        /*
         * Retrieve a list of assigned usernames
         */

        public List<string> AssignedUsers()
        {
            return assignedUserIdentities;
        }

        /*
         * Clear the assignedUserIdentities list
         */

        public void ClearAssignedUserIdentities()
        {
            assignedUserIdentities.Clear();
        }

        /*
         * Register hours
         */

        public void AddTimeObject(TimeObject time)
        {
            time.Owner = this;
            registeredTimeObjects.Add(time);
        }

        /*
         * Get total hours registered by a given user
         */

        public int TotalRegisteredHours(string userName = null)
        {
            var totalHours = 0;
            if (userName != null)
            {
                foreach (var T in registeredTimeObjects)
                {
                    if (userName == T.UserName)
                        totalHours += T.Hours;
                }
            }
            else
            {
                foreach (var T in registeredTimeObjects)
                    totalHours += T.Hours;
            }

            return totalHours;
        }

        /*
         * ItemModel section
         */

        public override ListViewItem ItemModel(ListMode mode = ListMode.Tile)
        {
            return mode == ListMode.Tile ? ItemTileModel() : ItemListModel();
        }

        public ListViewItem[] RegisteredHourItemModels(string userName)
        {
            var userTimeObjects = registeredTimeObjects.Where(item => item.UserName == userName).ToArray();

            var tObjects = userTimeObjects.Select(item => new TimeObject(item)).ToArray();

            var models = new ListViewItem[tObjects.Length];
            var index = 0;

            foreach (var tObject in tObjects)
            {
                var model = new ListViewItem(tObject.UserName);
                model.SubItems.Add(tObject.Hours.ToString());
                model.SubItems.Add(tObject.Week().ToString());

                models[index++] = model;
            }

            return models;
        }

        public ListViewItem[] RegisteredHourItemModels()
        {
            var tObjects = registeredTimeObjects.ToArray();

            var models = new ListViewItem[tObjects.Length];
            var index = 0;

            foreach (var tObject in tObjects)
            {
                var model = new ListViewItem(tObject.UserName);
                model.SubItems.Add(tObject.Hours.ToString());
                model.SubItems.Add(tObject.Week().ToString());

                models[index++] = model;
            }

            return models;
        }

        public TreeNode AssignedUserModels()
        {
            var rootNode = new TreeNode(base.Id);

            foreach (var userName in assignedUserIdentities)
                rootNode.Nodes.Add(userName);

            return rootNode;
        }

        /*
         * Private methods section begins
         */

        private ListViewItem ItemTileModel()
        {
            var model = new ListViewItem(base.Id);

            var assignedHours = new StringBuilder("Total assigned hours: ");
            var totalHours = TotalRegisteredHours();
            assignedHours.Append(totalHours.ToString());
            model.SubItems.Add(assignedHours.ToString());

            var assignedUsers = new StringBuilder("Active users: ");
            var totalUsersAssigned = assignedUserIdentities.Count;
            assignedUsers.Append(totalUsersAssigned.ToString());

            model.SubItems.Add(assignedUsers.ToString());

            return model;
        }

        /*
         * Activity item model
         * - Id
         * - Start week
         * - End week
         * - Total registered hours
         * - Number of assigned users
         * - Parent project
         */

        private ListViewItem ItemListModel()
        {
            var model = new ListViewItem(base.Id);

            model.SubItems.Add(StartWeek.ToString());
            model.SubItems.Add(EndWeek.ToString());

            model.SubItems.Add(TotalRegisteredHours().ToString());

            var totalUsersAssigned = assignedUserIdentities.Count;
            model.SubItems.Add(totalUsersAssigned.ToString());

            model.SubItems.Add(ParentProjectId);

            return model;
        }
    }
}