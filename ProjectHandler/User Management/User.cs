using ProjectNameSpace;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Templates;


// ReSharper disable once CheckNamespace

namespace VirtualUserDomain
{
    public class User : ItemModelEntity<ListViewItem>
    {
        /*
         * Constructor section begins
         */

        public User(string userName, string passWord, UserRole role, string fullName)
        {
            this.t = userName;
            this.pass = passWord;
            this.role = role;
            this.fName = fullName;
        }

        /*
         * Constructor section ends
         */

        /*
         * Public method section begins
         * - Assign to activity
         * - Unassign from activity
         * - isAvailableWithinTimespan(int,int) -> Checks if the employee is available over a given timespan
         */

        public void assignToActivity(Activity activity) => assignedActivities.Add(activity);
        public void unAssignFromActivity(Activity activity) => assignedActivities.Remove(activity);
        public void clearActivityAssignments() => assignedActivities.Clear();

        public Availability isAvailableWithinTimeSpan(int fromWeek, int toWeek)
        {
            int partlyOccurrences = 0, fullOccurrences = 0;
            foreach (var item in activityEntities())
            {
                if (fromWeek < item.startWeek && toWeek > item.endWeek)
                    partlyOccurrences++;
                else if (fromWeek < item.startWeek && item.withinTimespan(toWeek))
                    partlyOccurrences++;
                else if (item.withinTimespan(fromWeek) && toWeek > item.endWeek)
                    partlyOccurrences++;
                else if (item.withinTimespan(fromWeek) && item.withinTimespan(toWeek))
                    fullOccurrences++;
            }

            if (fullOccurrences >= 20)
                return Availability.NotAvailable;
            return (partlyOccurrences + fullOccurrences) >= 20 ? Availability.PartlyAvailable : Availability.Available;
        }
        
        public override ListViewItem itemModel(ListMode mode = ListMode.Tile)
        {
            return mode != ListMode.Tile ? itemListModel() : itemTileModel();
        }

        /*
         * Retrieve models for all user assigned activities with the following data:
         * - Activity id
         * - Activity duration
         * - Total registered user hours
         * - The parent project id
         */

        public ListViewItem[] assignedActivityModels()
        {
            var activities = allUserAssignedActivities();

            var count = activities.Count;
            var models = new ListViewItem[count];

            foreach (var activity in assignedActivities)
            {
                var model = new ListViewItem(activity.Id);
                model.SubItems.Add(activity.estimatedDuration().ToString());
                model.SubItems.Add(activity.totalRegisteredHours(id).ToString());
                var projectId = activity.parent.id;
                model.SubItems.Add(projectId);
            }

            return models;
        }

        /*
         * Public getter methods
         * Notice: In this class the inheritet protected field 't' is used as a container for the username id.
         */
         
        public string fullName() => fName;
        public string passWord() => pass;

        public int assignedActivityCount() => assignedActivities.Count;

        /*
         * Public method section ends
         */

        /*
         * Private methods begins
         */

        private List<ActivityEntity> activityEntities()
        {
            return assignedActivities.Select(item =>
                new ActivityEntity(item.startWeek, item.endWeek, item.id)).ToList();
        }

        private List<Activity> allUserAssignedActivities()
        {
            return assignedActivities;
        }

        private static string roleStringRepresentation(UserRole r) => r == UserRole.Admin ? "Administrator" : "Employee";

        private ListViewItem itemListModel()
        {
            var model = new ListViewItem(t);

            // ReSharper disable once InconsistentNaming
            var FullName = new StringBuilder("Fullname: ");
            FullName.Append(fName);
            model.SubItems.Add(FullName.ToString());

            var activityCount = new StringBuilder("Number of activities assigned: ");
            activityCount.Append(assignedActivities.Count);
            model.SubItems.Add(activityCount.ToString());

            var uRole = new StringBuilder("User role: ");
            uRole.Append(roleStringRepresentation(role));
            model.SubItems.Add(uRole.ToString());

            return model;
        }

        private ListViewItem itemTileModel()
        {
            return null;
        }

        /*
         * Private methods ends
         */

        /*
         * Public member fields
         */

        public enum Availability { NotAvailable, PartlyAvailable, Available};

        public UserRole role { get; }
        public string localAddress { get; set; }
        public enum UserRole { Admin, Employee };
        

        /*
         * Public member fields ends
         */

        /*
         * Private member fields begins
         */

        private List<Activity> assignedActivities = new List<Activity>();
        private string pass;
        private readonly string fName;

        /*
         * Private member fields ends
         */

    }
}
