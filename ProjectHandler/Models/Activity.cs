using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Templates;
using VirtualUserDomain;

namespace ProjectRelated
{
    [Serializable]
    public class Activity : ModelEntity<ListViewItem>
    {
        private readonly List<string> assignedUserIdentities = new List<string>();
        private readonly List<RegistrationObject> registrationObjects = new List<RegistrationObject>();

        private DateTime startDate, endDate;
        
        public Activity(string title, DateTime sDate, DateTime eDate, string project, UserManager uManager)
        {
            itemId = title;
            ParentProjectId = project;
            startDate = sDate;
            endDate = eDate;
        }

        public Activity(Activity copy)
        {
            ParentProjectId = null;
            itemId = copy.itemId;
            startDate = copy.startDate;
            endDate = copy.endDate;
            assignedUserIdentities = copy.assignedUserIdentities;
            registrationObjects = copy.registrationObjects;
            ParentProjectId = copy.ParentProjectId;
        }

        public string ActivityId
        {
            get => itemId;
            set => itemId = value;
        }

        public DateTime StartDate
        {
            get => startDate;
            set => startDate = value;
        }

        public DateTime EndDate
        {
            get => endDate;
            set => endDate = value;
        }

        public int StartWeek()
        {
            var ciCurr = CultureInfo.CurrentCulture;
             return ciCurr.Calendar.GetWeekOfYear(startDate,CalendarWeekRule.FirstFourDayWeek,DayOfWeek.Monday);
        }

        public int EndWeek()
        {
            var ciCurr = CultureInfo.CurrentCulture;
            return ciCurr.Calendar.GetWeekOfYear(endDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public string ParentProjectId { get; set; }

        public int EstimatedDuration() => EndWeek() - StartWeek();

        public void AssignUser(string userID) => assignedUserIdentities.Add(userID);

        public void AssignUsers(List<string> userIDs)
        {
            foreach (var userId in userIDs)
                assignedUserIdentities.Add(userId);
        }

        public bool IsUserAssigned(UserManager uManager)
        {
            var userName = uManager.loggedIn().UserName();
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

        public void AddRegistrationObject(RegistrationObject rObject)
        {
            registrationObjects.Add(rObject);
        }

        public void removeRegistrationObject(string regId)
        {
            for (var i = 0; i < registrationObjects.Count; i++)
            {
                var rObject = registrationObjects[i];
                if (rObject.RegistrationId == regId)
                {
                    registrationObjects.RemoveAt(i);
                    return;
                }
            }
        }

        public List<RegistrationObject> HourRegistrationObjects(string userName) =>
            registrationObjects.Where(item => item.UserName == userName).ToList();

        public List<RegistrationObject> HourRegistrationObjects() => registrationObjects;

        public int TotalRegisteredHours(string userName = null)
        {
            var totalHours = 0;
            if (userName != null)
            {
                foreach (var T in registrationObjects)
                {
                    if (userName == T.UserName)
                        totalHours += T.Hours;
                }
            }
            else
            {
                foreach (var T in registrationObjects)
                    totalHours += T.Hours;
            }

            return totalHours;
        }

        public override ListViewItem ItemModel(ListMode mode = ListMode.Tile)
        {
            return mode == ListMode.Tile ? ItemTileModel() : ItemListModel();
        }

        public ListViewItem[] RegistrationObjectItemModels(string userName)
        {
            var userTimeObjects = registrationObjects.Where(item => item.UserName == userName).ToArray();
            
            var models = new ListViewItem[userTimeObjects.Length];
            var index = 0;

            foreach (var tObject in userTimeObjects)
                models[index++] = tObject.ItemModel();

            return models;
        }

        public ListViewItem[] RegistrationObjectModels()
        {
            var tObjects = registrationObjects.ToArray();

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

            model.SubItems.Add(StartWeek().ToString());
            model.SubItems.Add(EndWeek().ToString());

            model.SubItems.Add(TotalRegisteredHours().ToString());

            var totalUsersAssigned = assignedUserIdentities.Count;
            model.SubItems.Add(totalUsersAssigned.ToString());

            model.SubItems.Add(ParentProjectId);

            return model;
        }
    }
}