using ProjectNameSpace;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace

namespace VirtualUserDomain
{
    public class User
    {
        public User(string userName, string passWord, UserRole role)
        {
            
            this.userName = userName;
            this.passWord = passWord;
            this.role = role;
        }

        public Availability isAvailableWithinTimeSpan(int fromWeek, int toWeek)
        {
            if (assignedActivities.Count < 20)
                return Availability.Available;

            int partlyOccurences = 0, fullOccurences = 0;
            foreach (var item in assignedActivities)
            {
                if(fromWeek < item.fromWeek && toWeek > item.toWeek)
                    partlyOccurences++;
                else if (fromWeek < item.fromWeek && item.withinTimeSpan(toWeek))
                    partlyOccurences++;
                else if (item.withinTimeSpan(fromWeek) && toWeek > item.toWeek)
                    partlyOccurences++;
                else if(item.withinTimeSpan(fromWeek) && item.withinTimeSpan(toWeek))
                    fullOccurences++;
            }

            if (fullOccurences >= 20)
                return Availability.NotAvailable;
            else if ((partlyOccurences + fullOccurences) >= 20)
                return Availability.PartlyAvailable;
            else
                return Availability.Available;
        }

        public enum Availability { NotAvailable, PartlyAvailable, Available};
        public string fullName { get; set; }
        public UserRole role { get; }
        public string localAdress { get; set; }
        public enum UserRole { Admin, Leader, Employee };

        public string getUserName() => userName;
        public string getPassWord() => passWord;

        private string userName { get; }
        private string passWord { get; }

        private List<AssignedActivityItem> assignedActivities = new List<AssignedActivityItem>();
    }

    public struct AssignedActivityItem
    {
        internal bool withinTimeSpan(int w) => w >= fromWeek && w <= toWeek;

        public int fromWeek { get; set; }
        public int toWeek { get; set; }
        public string activityIdentity { get; set; }
        public Activity activity { get; set; }
    }
}
