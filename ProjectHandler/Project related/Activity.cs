using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Templates;
using VirtualUserDomain;

namespace ProjectRelated
{
    [Serializable]
    public class Activity : ItemModelEntity<ListViewItem>
    {
        private readonly List<string> assignedUserIdentities = new List<string>();
        private readonly List<TimeObject> registeredTimeObjects = new List<TimeObject>();
        
        public Activity(string title, int sWeek, int eWeek, string project, UserManager uManager)
        {
            itemId = title;
            ParentProjectId = project;
            StartWeek = sWeek;
            EndWeek = eWeek;
        }

        public Activity(Activity copy)
        {
            ParentProjectId = null;
            itemId = copy.itemId;
            StartWeek = copy.StartWeek;
            EndWeek = copy.EndWeek;
            assignedUserIdentities = copy.assignedUserIdentities;
            registeredTimeObjects = copy.registeredTimeObjects;
            ParentProjectId = copy.ParentProjectId;
        }

        public string ActivityId
        {
            get => itemId;
            set => itemId = value;
        }

        public int StartWeek { get; set; }

        public int EndWeek { get; set; }

        public string ParentProjectId { get; set; }

        public int EstimatedDuration() => EndWeek - StartWeek;

        public void AssignUser(string userID) => assignedUserIdentities.Add(userID);

        public void AssignUsers(List<string> userIDs)
        {
            foreach (var userId in userIDs)
                assignedUserIdentities.Add(userId);
        }

        public bool IsUserAssigned(UserManager uManager)
        {
            var userName = uManager.currentlyLoggedIn().UserName();
            return assignedUserIdentities.Any(item => item == userName);
        }

        public bool IsUserAssigned(string userName)
        {
            return assignedUserIdentities.Any(item => item == userName);
        }

        public List<string> AssignedUsers()
        {
            return assignedUserIdentities;
        }

        public void ClearAssignedUserIdentities()
        {
            assignedUserIdentities.Clear();
        }

        public void AddTimeObject(TimeObject time)
        {
            time.ParentActivityId = ActivityId;
            registeredTimeObjects.Add(time);
        }

        public List<TimeObject> TimeObjects(string userName) =>
            registeredTimeObjects.Where(item => item.UserName == userName).ToList();

        public List<TimeObject> TimeObjects() => registeredTimeObjects;

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
                models[index++] = tObject.ItemModel();

            return models;
        }

        public ListViewItem[] RegisteredHourItemModels()
        {
            var tObjects = registeredTimeObjects.ToArray();

            var models = new ListViewItem[tObjects.Length];
            var index = 0;

            foreach (var tObject in tObjects)
                models[index++] = tObject.ItemModel();  

            return models;
        }

        public TreeNode AssignedUserModels()
        {
            var rootNode = new TreeNode(id);

            foreach (var userName in assignedUserIdentities)
                rootNode.Nodes.Add(userName);

            return rootNode;
        }

        private ListViewItem ItemTileModel()
        {
            var model = new ListViewItem(id);

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

        private ListViewItem ItemListModel()
        {
            var model = new ListViewItem(id);

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