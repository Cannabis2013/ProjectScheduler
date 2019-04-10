using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Templates;
using VirtualUserDomain;

namespace ProjectRelated
{
    [Serializable]
    public class ProjectManager
    {
        private readonly List<Project> projects = new List<Project>();
        
        public void AddProject(Project p) => projects.Add(p);
        public void RemoveProjectAt(int index) => projects.RemoveAt(index);

        public Project ProjectAt(int index) => projects.ElementAt(index);
        public Project Project(Project p) => projects.Find(item => item.id == p.id);
        public Project Project(string projectId) => projects.Find(item => item.id == projectId);

        public List<string> AllProjectIdentities() => projects.Select(item => item.id).ToList();
        public List<string> AllProjectIdentities(string projectLeaderId) => projects.Where(item => 
            item.projectLeaderId == projectLeaderId).Select(item => item.id).ToList();

        public bool removeActivity(string projectId, string activityId)
        {
            var p = Project(projectId);
            var activity = p?.Activity(activityId);
            if (activity == null)
                return false;
            p.RemoveActivity(activity);
            return true;
        }

        public Activity Activity(string id) => Activities().Find(item => item.ActivityId == id);

        public List<Activity> Activities()
        {
            var resultingList = new List<Activity>();
            foreach (var p in projects)
            {
                var userActivities = p.AllActivities();
                resultingList.AddRange(userActivities);
            }

            return resultingList;
        }

        public List<Activity> Activities(string userName)
        {
            var resultingList = new List<Activity>();
            foreach (var p in projects)
            {
                var userActivities = p.AssignedActivities(userName);
                resultingList.AddRange(userActivities);
            }

            return resultingList;
        }

        public RegistrationObject HourRegistrationObjects(RegistrationObject regObject)
        {
            var activities = Activities();

            foreach (var activity in activities)
            {
                var tm = activity.HourRegistrationObjects().ToList();
                var rObject = tm.Find(item => item == regObject);
                if (rObject != null)
                    return rObject;
            }

            return null;
        }

        public List<RegistrationObject> HourRegistrationObjects()
        {
            var TimeObjects = new List<RegistrationObject>();
            var activities = Activities();

            foreach (var activity in activities)
            {
                var tm = activity.HourRegistrationObjects().ToList();
                TimeObjects.AddRange(tm);
            }
            return TimeObjects;
        }

        public List<RegistrationObject> HourRegistrationObjects(string userName)
        {
            var RegObjects = new List<RegistrationObject>();
            var activities = Activities(userName);

            foreach (var activity in activities)
            {
                var tm = activity.HourRegistrationObjects(userName).ToList();
                RegObjects.AddRange(tm);
            }
            return RegObjects;
        }

        public ListViewItem[] ProjectItemModels(ItemModelEntity<ListViewItem>.ListMode mode = ItemModelEntity<ListViewItem>.ListMode.List)
        {
            int count = projects.Count, index = 0;
            var models = new ListViewItem[count];

            foreach (var p in projects)
                models[index++] = p.ItemModel(mode);

            return models;
        }

        public ListViewItem[] ActivityTimeObjectModels()
        {
            var activities = new List<ListViewItem>();
            foreach (var p in projects)
            {
                foreach (var activity in p.AllActivities())
                {
                    var models = activity.TimeObjectModels().ToList();
                    activities.AddRange(models);
                }
            }

            return activities.ToArray();
        }

        public ListViewItem[] ActivityTimeObjectModels(string userName)
        {
            var TimeObjectModels = new List<ListViewItem>();

            foreach (var p in projects)
            {
                foreach (var activity in p.AllActivities())
                {
                    var models = activity.TimeObjectModels(userName).ToList();
                    TimeObjectModels.AddRange(models);
                }
            }
            return TimeObjectModels.ToArray();
        }

        public ListViewItem[] ProjectActivityItemModels(UserManager uManager)
        {
            var models = new List<ListViewItem>();
            if (uManager.isAdmin())
            {
                foreach (var p in projects)
                foreach (var activity in p.AllActivities())
                {
                    var model = activity.ItemModel(ItemModelEntity<ListViewItem>.ListMode.List);
                    models.Add(model);
                }

                return models.ToArray();
            }

            var userId = uManager.loggedIn().UserName();

            foreach (var p in projects)
            foreach (var activity in p.AllActivities())
            {
                if (!activity.IsUserAssigned(uManager) && p.projectLeaderId != userId)
                    continue;

                var model = activity.ItemModel(ItemModelEntity<ListViewItem>.ListMode.List);
                models.Add(model);
            }

            return models.ToArray();
        }

        public ListViewItem[] UserAssignedActivityModels(UserManager uManager)
        {
            var userName = uManager.loggedIn().UserName();
            var assignedActivities = Activities(userName);
            var count = assignedActivities.Count;
            var models = new ListViewItem[count];
            var index = 0;

            foreach (var activity in assignedActivities)
            {
                var model = new ListViewItem(activity.ActivityId);
                model.SubItems.Add(activity.EstimatedDuration().ToString());
                model.SubItems.Add(activity.TotalRegisteredHours(userName).ToString());
                var projectId = activity.ParentProjectId;
                model.SubItems.Add(projectId);

                models[index++] = model;
            }

            return models;
        }

        public ListViewItem[] UserAssignedActivityModels(string userName)
        {
            var assignedActivities = Activities(userName);
            var count = assignedActivities.Count;
            var models = new ListViewItem[count];
            var index = 0;

            foreach (var activity in assignedActivities)
            {
                var model = new ListViewItem(activity.ActivityId);
                model.SubItems.Add(activity.EstimatedDuration().ToString());
                model.SubItems.Add(activity.TotalRegisteredHours(userName).ToString());
                var projectId = activity.ParentProjectId;
                model.SubItems.Add(projectId);

                models[index++] = model;
            }

            return models;
        }

        public User.Availability IsUserAvailableWithinTimeSpan(string userName, UserManager uManager, DateTime fromDate, DateTime toDate)
        {
            int partlyOccurrences = 0, fullOccurrences = 0;

            foreach (var item in UserActivityEntities(userName, uManager))
            {
                if (fromDate.CompareTo(item.StartDate) < 0  && toDate.CompareTo(item.EndDate) > 0)
                    partlyOccurrences++;
                else if (fromDate.CompareTo(item.StartDate) < 0 && item.withinTimespan(toDate))
                    partlyOccurrences++;
                else if (item.withinTimespan(fromDate) && toDate.CompareTo(item.EndDate) > 0)
                    partlyOccurrences++;
                else if (item.withinTimespan(fromDate) && item.withinTimespan(toDate))
                    fullOccurrences++;
            }

            if (fullOccurrences >= 20)
                return User.Availability.NotAvailable;
            return partlyOccurrences + fullOccurrences >= 20
                ? User.Availability.PartlyAvailable
                : User.Availability.Available;
        }

        public IEnumerable<ActivityEntity> UserActivityEntities(string userName,UserManager uManager)
        {
            return Activities(userName).Select(item =>
                new ActivityEntity(item.id, item.StartDate, item.EndDate)).ToList();
        }
    }
}