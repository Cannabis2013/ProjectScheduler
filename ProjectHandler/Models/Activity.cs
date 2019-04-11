using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Projecthandler.Templates_and_interfaces;
using Templates;
using VirtualUserDomain;

namespace ProjectRelated
{
    [Serializable]
    public class ActivityModel : AbstractModel<ListViewItem,HourRegistrationModel>
    {
        private readonly List<string> assignedUserIdentities = new List<string>();
        private readonly List<HourRegistrationModel> registrationObjects = new List<HourRegistrationModel>();

        private DateTime startDate, endDate;
        
        public ActivityModel(string title, DateTime sDate, DateTime eDate, string project, UserManager uManager)
        {
            ModelIdentity = title;
            ParentProjectId = project;
            startDate = sDate;
            endDate = eDate;

            Childrens = new List<HourRegistrationModel>();
        }

        public ActivityModel(ActivityModel copy)
        {
            Childrens = copy.Childrens;
            ParentProjectId = null;
            ModelIdentity = copy.ModelIdentity;
            startDate = copy.startDate;
            endDate = copy.endDate;
            assignedUserIdentities = copy.assignedUserIdentities;
            registrationObjects = copy.registrationObjects;
            ParentProjectId = copy.ParentProjectId;
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
            var userName = uManager.loggedIn().ModelIdentity;
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

        public void AddRegistrationObject(HourRegistrationModel rObject)
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

        public List<HourRegistrationModel> HourRegistrationObjects(string userName) =>
            registrationObjects.Where(item => item.ModelIdentity == userName).ToList();

        public List<HourRegistrationModel> HourRegistrationObjects() => registrationObjects;

        public int TotalRegisteredHours(string userName = null)
        {
            var totalHours = 0;
            if (userName != null)
            {
                foreach (var T in registrationObjects)
                {
                    if (userName == T.ModelIdentity)
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

        public ListViewItem[] RegistrationObjectItemModels(string userName)
        {
            var userTimeObjects = registrationObjects.Where(item => item.ModelIdentity == userName).ToArray();
            
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
            var rootNode = new TreeNode(ModelIdentity);

            foreach (var userName in assignedUserIdentities)
                rootNode.Nodes.Add(userName);

            return rootNode;
        }

        private ListViewItem ItemTileModel()
        {
            var model = new ListViewItem(ModelIdentity);

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

        public override ListViewItem ItemModel()
        {
            var model = new ListViewItem(ModelIdentity);

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