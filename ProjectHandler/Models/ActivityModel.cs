using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Templates;
using UserDomain;

namespace ProjectRelated
{
    [Serializable]
    public class ActivityModel : AbstractModel
    {
        private readonly List<string> assignedUserIdentities;
        private DateTime startDate, endDate;
        private readonly ActivityType Type = ActivityType.Work_Related;

        public enum ActivityType
        {
            Work_Related,
            Absent_Related
        };
        
        public ActivityModel(string activityTitle,AbstractModel parentProjectModel, 
            DateTime sDate, 
            DateTime eDate, 
            List<string> assignedUsers)
        {
            ModelIdentity = activityTitle;
            startDate = sDate;
            endDate = eDate;
            assignedUserIdentities = assignedUsers;

            parentProjectModel.AddSubModel(this);
            Parent = parentProjectModel;
        }

        public ActivityModel(string activityTitle, DateTime sDate, DateTime eDate)
        {
            ModelIdentity = activityTitle;
            startDate = sDate;
            endDate = eDate;

            Type = ActivityType.Absent_Related;
        }

        public ActivityModel(ActivityModel copy)
        {
            SubModels = copy.SubModels;
            ModelIdentity = copy.ModelIdentity;
            startDate = copy.startDate;
            endDate = copy.endDate;
            assignedUserIdentities = copy.assignedUserIdentities;
            Parent = copy.Parent;
        }

        public ActivityType TypeOfActivity => Type;

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
            AllSubModels<HourRegistrationModel>().Where(item => item.UserName == userName).ToList();

        public List<HourRegistrationModel> HourRegistrationObjects() => 
            AllSubModels<HourRegistrationModel>();

        public int TotalRegisteredHours(string userName = null)
        {
            var totalHours = 0;
            if (userName != null)
            {
                foreach (var rObject in AllSubModels<HourRegistrationModel>())
                {
                    if (userName == rObject.UserName)
                        totalHours += rObject.Hours;
                }
            }
            else
            {
                foreach (var rObject in AllSubModels<HourRegistrationModel>())
                    totalHours += rObject.Hours;
            }

            return totalHours;
        }

        public TreeNode AssignedUserModels()
        {
            var rootNode = new TreeNode(ModelIdentity);

            foreach (var userName in assignedUserIdentities)
                rootNode.Nodes.Add(userName);

            return rootNode;
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
    }
}