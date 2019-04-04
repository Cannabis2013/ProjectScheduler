using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Templates;
using VirtualUserDomain;

namespace ProjectNameSpace
{
    /*
     * ProjectManager
     * - Add/create project
     * - Edit projects
     * - Remove Projects
     * - Add activity to project
     * - Project manipulation
     * -- Add activity to a given project
     * -- Delete activity within a given project
     * -- Register work hour to a given activity in a given project
     * - Retrieve project activities
     */

    public class ProjectManager
    {
        /*
         * Private fields section begins
         */
        private readonly List<Project> projects = new List<Project>();

        /*
         * Private fields section ends
         */

        public ProjectManager()
        {
            var p = new Project("Project TEST")
            {
                startWeek = 1,
                endWeek = 4,
                projectLeaderId = "Finn_Luger"
            };
            projects.Add(p);
        }

        /*
         * Public properties section begins
         * - Add/remove projects
         * - Get activities
         */

        public void addProject(Project p)
        {
            projects.Add(p);
        }

        public void removeProjectAt(int index)
        {
            projects.RemoveAt(index);
        }

        public void removeProject(Project p)
        {
            projects.Remove(p);
        }

        public Project projectAt(int index)
        {
            return projects.ElementAt(index);
        }

        public Project project(Project p)
        {
            return projects.Find(item => item.id == p.id);
        }

        public Project project(string projectId)
        {
            return projects.Find(item => item.id == projectId);
        }

        public List<string> allProjectIdentities()
        {
            return projects.Select(item => item.id).ToList();
        }

        public List<string> allProjectIdentities(string projectLeaderId)
        {
            return projects.Where(item => item.projectLeaderId == projectLeaderId).Select(item => item.id).ToList();
        }

        // Activities section begins

        public Activity activity(string id) => activities().Find(item => item.Id == id);

        public List<Activity> activities()
        {
            var resultingList = new List<Activity>();
            foreach (var p in projects)
            {
                var userActivities = p.allActivities();
                resultingList.AddRange(userActivities);
            }

            return resultingList;
        }

        public List<Activity> activities(string userName)
        {
            var resultingList = new List<Activity>();
            foreach (var p in projects)
            {
                var userActivities = p.assignedActivities(userName);
                resultingList.AddRange(userActivities);
            }

            return resultingList;
        }

        // Activities section ends


        /*
         * Item models section begins
         * - Returns a list of Project Item models with the following columndata:
         * -- ProjectId
         * -- Project leader username
         * -- Start and estimated end week
         */

        public ListViewItem[] projectItemModels(ItemModelEntity<ListViewItem>.ListMode mode)
        {
            int count = projects.Count, index = 0;
            var models = new ListViewItem[count];

            foreach (var p in projects)
                models[index++] = p.itemModel(mode);

            return models;
        }

        /*
         * Models all the user time registration for visual representation in a ListView
         */

        public ListViewItem[] registeredUserHourModels(string userName = null)
        {
            var activities = new List<ListViewItem>();
            if (userName != null)
                foreach (var p in projects)
                foreach (var activity in p.allActivities())
                {
                    var models = activity.registeredHourItemModels(userName).ToList();
                    activities.AddRange(models);
                }
            else
                foreach (var p in projects)
                foreach (var activity in p.allActivities())
                {
                    var models = activity.registeredHourItemModels().ToList();
                    activities.AddRange(models);
                }

            return activities.ToArray();
        }

        /*
         * Returns a list of Activity item models where each item has the following column data:
         * - Id
         * - Start week
         * - End week
         * - Total registered hours
         * - Number of assigned users
         * - Parent project
         *
         * Furthermore there is some access restrictions:
         * - Admin can retrieve all activities
         * - Projectleaders has access to activities in their corresponding project
         * - Employees has no access.
         */

        public ListViewItem[] projectActivityItemModels()
        {
            var models = new List<ListViewItem>();
            if (UserManager.verifyUserState() == User.UserRole.Admin)
            {
                foreach (var p in projects)
                foreach (var activity in p.allActivities())
                {
                    var model = activity.itemModel(ItemModelEntity<ListViewItem>.ListMode.List);
                    models.Add(model);
                }

                return models.ToArray();
            }

            var userId = UserManager.currentlyLoggedIn().userName();

            foreach (var p in projects)
            foreach (var activity in p.allActivities())
            {
                if (!activity.isUserAssigned() && p.projectLeaderId != userId)
                    continue;

                var model = activity.itemModel(ItemModelEntity<ListViewItem>.ListMode.List);
                models.Add(model);
            }

            return models.ToArray();
        }

        /*
         * User assigned activity item models
         * - Activity id
         * - Estimated duration
         * - Total registered hours by a given user
         * - Parent project identification
         */

        public ListViewItem[] userAssignedActivityModels()
        {
            var userName = UserManager.currentlyLoggedIn().userName();
            var assignedActivities = activities(userName);
            var count = assignedActivities.Count;
            var models = new ListViewItem[count];
            var index = 0;

            foreach (var activity in assignedActivities)
            {
                var model = new ListViewItem(activity.Id);
                model.SubItems.Add(activity.estimatedDuration().ToString());
                model.SubItems.Add(activity.totalRegisteredHours(userName).ToString());
                var projectId = activity.parentProjectId;
                model.SubItems.Add(projectId);

                models[index++] = model;
            }

            return models;
        }

        public ListViewItem[] userAssignedActivityModels(string userName)
        {
            var assignedActivities = activities(userName);
            var count = assignedActivities.Count;
            var models = new ListViewItem[count];
            var index = 0;

            foreach (var activity in assignedActivities)
            {
                var model = new ListViewItem(activity.Id);
                model.SubItems.Add(activity.estimatedDuration().ToString());
                model.SubItems.Add(activity.totalRegisteredHours(userName).ToString());
                var projectId = activity.parentProjectId;
                model.SubItems.Add(projectId);

                models[index++] = model;
            }

            return models;
        }

        /*
         * Item models section ends
         */

        /*
         * User activities section begins
         

        /*
         * Checks if the user is available within a given timespan
         */

        public User.Availability isUserAvailableWithinTimeSpan(string userName, int fromWeek, int toWeek)
        {
            int partlyOccurrences = 0, fullOccurrences = 0;
            foreach (var item in userActivityEntities(userName))
                if (fromWeek < item.startWeek && toWeek > item.endWeek)
                    partlyOccurrences++;
                else if (fromWeek < item.startWeek && item.withinTimespan(toWeek))
                    partlyOccurrences++;
                else if (item.withinTimespan(fromWeek) && toWeek > item.endWeek)
                    partlyOccurrences++;
                else if (item.withinTimespan(fromWeek) && item.withinTimespan(toWeek))
                    fullOccurrences++;

            if (fullOccurrences >= 20)
                return User.Availability.NotAvailable;
            return partlyOccurrences + fullOccurrences >= 20
                ? User.Availability.PartlyAvailable
                : User.Availability.Available;
        }

        public IEnumerable<ActivityEntity> userActivityEntities(string userName)
        {
            return activities(userName).Select(item =>
                new ActivityEntity(item.startWeek, item.endWeek, item.id)).ToList();
        }
    }
}