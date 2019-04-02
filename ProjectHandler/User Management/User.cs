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
            this.fullName = fullName;
        }

        /*
         * Constructor section ends
         */

        /*
         * Public method section begins
         * - Assign to project
         * - isAvailableWithinTimespan(int,int) -> Checks if the employee is available over a given timespan
         */

        public void assignToProject(Project p) => assignedProjects.Add(p);
        public bool unAssignProject(Project p) => assignedProjects.Remove(p);

        public Availability isAvailableWithinTimeSpan(int fromWeek, int toWeek)
        {
            int partlyOccurrences = 0, fullOccurrences = 0;
            foreach (var project in assignedProjects)
            {
                // Retrieves a list of user-assigned activities
                var activityEntities = project.activityEntities(userName());
                
                foreach (var item in activityEntities)
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
            }

            if (fullOccurrences >= 20)
                return Availability.NotAvailable;
            return (partlyOccurrences + fullOccurrences) >= 20 ? Availability.PartlyAvailable : Availability.Available;
        }

        public ListViewItem[] assignedProjectModels()
        {
            var models = new ListViewItem[assignedProjects.Count];
            var index = 0;

            foreach (var project in assignedProjects)
            {
                models[index++] = project.itemModel();
            }

            return models;
        }
        public ListViewItem[] assignedActivityModels(string projectId) =>
            assignedProjects.Find(item => item.id == projectId).activityItemModels();

        public override ListViewItem itemModel(ListMode mode = ListMode.Tile)
        {
            var model = new ListViewItem(t);

            // ReSharper disable once InconsistentNaming
            var FullName = new StringBuilder("Fullname: ");
            FullName.Append(fullName);
            model.SubItems.Add(FullName.ToString());

            var activityCount = new StringBuilder("Number of projects assigned: ");
            activityCount.Append(assignedProjects.Count);
            model.SubItems.Add(activityCount.ToString());

            var uRole = new StringBuilder("User role: ");
            uRole.Append(roleStringRepresentation(role));
            model.SubItems.Add(uRole.ToString());

            return model;
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

            foreach (var activity in activities)
            {
                var model = new ListViewItem(activity.Id);
                model.SubItems.Add(activity.estimatedDuration().ToString());
                model.SubItems.Add(activity.totalRegisteredHours(userName()).ToString());
                var projectId = activity.Parent().id;
                model.SubItems.Add(projectId);
            }

            return models;
        }

        /*
         * Public getter methods
         * Notice: In this class the inheritet protected field 't' is used as a container for the username id.
         */

        public string userName() => t;
        public string FullName() => fullName;
        public string passWord() => pass;

        /*
         * Public method section ends
         */

        /*
         * Private methods
         */

        private List<Activity> allUserAssignedActivities()
        {
            var activities = new List<Activity>();
            foreach (var p in assignedProjects)
            {
                var userAssignedActivities = p.assignedActivities(userName());
                activities.AddRange(userAssignedActivities);
            }

            return activities;
        }

        private static string roleStringRepresentation(UserRole r) => r == UserRole.Admin ? "Administrator" : "Employee";

        /*
         * Private methods
         */

        /*
         * Public member fields
         */

        public enum Availability { NotAvailable, PartlyAvailable, Available};

        private readonly string fullName;
        public UserRole role { get; }
        public string localAddress { get; set; }
        public enum UserRole { Admin, Leader, Employee };
        

        /*
         * Public member fields ends
         */


        /*
         * Private member fields
         */


        private string pass { get; }
        private readonly List<Project> assignedProjects = new List<Project>();
    }
}
