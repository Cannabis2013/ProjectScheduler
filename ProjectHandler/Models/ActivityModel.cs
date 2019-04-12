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
    public class ActivityModel : AbstractModel<ProjectModel,HourRegistrationModel>
    {
        private readonly List<string> assignedUserIdentities = new List<string>();

        private DateTime startDate, endDate;
        
        public ActivityModel(string title, DateTime sDate, DateTime eDate, ProjectModel project, UserManager uManager)
        {
            ModelIdentity = title;
            Parent = project;
            startDate = sDate;
            endDate = eDate;

            project.AddSubModel(this);
        }

        public ActivityModel(ActivityModel copy)
        {
            AllSubModels = copy.AllSubModels;
            ModelIdentity = copy.ModelIdentity;
            startDate = copy.startDate;
            endDate = copy.endDate;
            assignedUserIdentities = copy.assignedUserIdentities;
            AllSubModels = copy.AllSubModels;
            Parent = copy.Parent;
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

        public List<HourRegistrationModel> HourRegistrationObjects(string userName) =>
            AllSubModels.Where(item => item.UserName == userName).ToList();

        public List<HourRegistrationModel> HourRegistrationObjects() => AllSubModels;

        public int TotalRegisteredHours(string userName = null)
        {
            var totalHours = 0;
            if (userName != null)
            {
                foreach (var T in AllSubModels)
                {
                    if (userName == T.ModelIdentity)
                        totalHours += T.Hours;
                }
            }
            else
            {
                foreach (var T in AllSubModels)
                    totalHours += T.Hours;
            }

            return totalHours;
        }

        public ListViewItem[] RegistrationObjectItemModels(string userName)
        {
            var userTimeObjects = AllSubModels.Where(item => item.ModelIdentity == userName).ToArray();
            
            var models = new ListViewItem[userTimeObjects.Length];
            var index = 0;

            foreach (var tObject in userTimeObjects)
                models[index++] = tObject.ItemModel();

            return models;
        }

        public ListViewItem[] RegistrationObjectModels()
        {
            var tObjects = AllSubModels.ToArray();

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

            model.SubItems.Add(ParentModelIdentity());

            return model;
        }

        public override void RemoveSubModel(string SubModelId)
        {
            throw new NotImplementedException();
        }

        public override HourRegistrationModel SubModel(string SubModelIdentity)
        {
            throw new NotImplementedException();
        }
    }
}