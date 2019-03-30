using ProjectNameSpace;
using System.Collections.Generic;

namespace VirtualUserDomain
{
    public class User
    {
        public User(string userName, string passWord, UserRole role)
        {
            
            this.userName = userName;
            this.passWord = passWord;
            this.Role = role;
        }

        public availability isAvailableWithinTimeSpan(int fromWeek, int toWeek)
        {
            if (assignedActivities.Count < 20)
                return availability.available;

            int partlyOccurences = 0, fullOccurences = 0;
            foreach (assignedActivityItem item in assignedActivities)
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
                return availability.notAvailable;
            else if ((partlyOccurences + fullOccurences) >= 20)
                return availability.partlyAvailable;
            else
                return availability.available;
        }

        public enum availability { notAvailable, partlyAvailable, available};
        public string fullName { get; set; }
        public UserRole Role { get; }
        public string LocalAdress { get; set; }
        public enum UserRole { Admin, leader, employee };

        public string UserName() => userName;
        public string PassWord() => passWord;

        private string userName { get; }
        private string passWord { get; }

        private List<assignedActivityItem> assignedActivities = new List<assignedActivityItem>();
    }

    public struct assignedActivityItem
    {
        internal bool withinTimeSpan(int w)
        {
            if (w >= fromWeek && w <= toWeek)
                return true;
            else
                return false;
        }

        internal int fromWeek { get; set; }
        internal int toWeek { get; set; }
        internal string activityIdentity { get; set; }
        internal Activity activity { get; set; }
    }
}
