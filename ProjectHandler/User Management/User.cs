using ProjectNameSpace;
using System.Collections.Generic;
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

        public User(string userName, string passWord, UserRole role)
        {
            this.t = userName;
            this.pass = passWord;
            this.role = role;
        }

        /*
         * Constructor section ends
         */

        /*
         * Public method section begins
         * - Assign to project
         * - isAvailableWithinTimespan(int,int) -> Checks if the employee is available over a given timespan
         */

        public void assignProject(Project p) => assignedProjects.Add(p);
        public bool unAssignProject(Project p) => assignedProjects.Remove(p);

        public Availability isAvailableWithinTimeSpan(int fromWeek, int toWeek)
        {
            int partlyOccurences = 0, fullOccurences = 0;
            foreach (var project in assignedProjects)
            {
                var activityEntities = project.activityEntities();
                

                foreach (var item in activityEntities)
                {
                    if (fromWeek < item.startWeek && toWeek > item.endWeek)
                        partlyOccurences++;
                    else if (fromWeek < item.startWeek && item.withinTimespan(toWeek))
                        partlyOccurences++;
                    else if (item.withinTimespan(fromWeek) && toWeek > item.endWeek)
                        partlyOccurences++;
                    else if (item.withinTimespan(fromWeek) && item.withinTimespan(toWeek))
                        fullOccurences++;
                }
            }

            if (fullOccurences >= 20)
                return Availability.NotAvailable;
            return (partlyOccurences + fullOccurences) >= 20 ? Availability.PartlyAvailable : Availability.Available;
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
            assignedProjects.Find(item => item.projectId == projectId).activityItemModels();

        public override ListViewItem itemModel()
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
         * Public getter methods
         * Notice: In this class the inheritet protected field 't' is used as a container for the username id.
         */

        public string userName() => t;
        public string passWord() => pass;

        /*
         * Public method section ends
         */

        /*
         * Private methods
         */

        private string roleStringRepresentation(UserRole r) => r == UserRole.Admin ? "Administrator" : "Employee";

        /*
         * Private methods
         */

        /*
         * Public member fields
         */

        public enum Availability { NotAvailable, PartlyAvailable, Available};
        public string fullName { get; set; }
        public UserRole role { get; }
        public string localAdress { get; set; }
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
