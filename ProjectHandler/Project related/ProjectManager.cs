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

        public Activity Activity(string id) => Activities().Find(item => item.ActivityId == id);

        public bool removeActivity(string projectId, string activityId)
        {
            var p = Project(projectId);
            var activity = p?.Activity(activityId);
            if (activity == null)
                return false;
            p.RemoveActivity(activity);
            return true;
        }

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

        public List<TimeObject> ActivityTimeObjects()
        {
            var TimeObjects = new List<TimeObject>();
            var activities = Activities();

            foreach (var activity in activities)
            {
                var tm = activity.TimeObjects().ToList();
                TimeObjects.AddRange(tm);
            }
            return TimeObjects;
        }

        public List<TimeObject> ActivityTimeObjects(string userName)
        {
            var TimeObjects = new List<TimeObject>();
            var activities = Activities(userName);

            foreach (var activity in activities)
            {
                var tm = activity.TimeObjects(userName).ToList();
                TimeObjects.AddRange(tm);
            }
            return TimeObjects;
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
            if (uManager.verifyUserState() == User.UserRole.Admin)
            {
                foreach (var p in projects)
                foreach (var activity in p.AllActivities())
                {
                    var model = activity.ItemModel(ItemModelEntity<ListViewItem>.ListMode.List);
                    models.Add(model);
                }

                return models.ToArray();
            }

            var userId = uManager.currentlyLoggedIn().UserName();

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
            var userName = uManager.currentlyLoggedIn().UserName();
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

        public User.Availability IsUserAvailableWithinTimeSpan(string userName, UserManager uManager, int fromWeek, int toWeek)
        {
            int partlyOccurrences = 0, fullOccurrences = 0;
            foreach (var item in UserActivityEntities(userName,uManager))
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

        public IEnumerable<ActivityEntity> UserActivityEntities(string userName,UserManager uManager)
        {
            return Activities(userName).Select(item =>
                new ActivityEntity(item.StartWeek, item.EndWeek, item.id)).ToList();
        }
    }
}