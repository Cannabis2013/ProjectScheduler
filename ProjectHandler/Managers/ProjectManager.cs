using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Projecthandler.Templates_and_interfaces;
using Templates;
using VirtualUserDomain;

/*
 * Fields:
 * - All ItemList
 * Method/properties:
 * - Add/remove project
 * - Get project (3 overloads)
 * - All project id's
 */

namespace ProjectRelated
{
    [Serializable]
    public class ProjectManager : AbstractManager<ProjectModel,ListViewItem>
    {

        public bool removeActivityModel(string projectId, string activityId)
        {
            var p = Model(projectId);
            var activity = p?.Activity(activityId);
            if (activity == null)
                return false;
            p.RemoveActivity(activity);
            return true;
        }

        public ActivityModel getActivityModel(string id) => ActivityModels().Find(item => item.ModelIdentity == id);

        public List<ActivityModel> ActivityModels()
        {
            var resultingList = new List<ActivityModel>();
            foreach (var p in ModelList)
            {
                var userActivities = p.AllActivities();
                resultingList.AddRange(userActivities);
            }

            return resultingList;
        }

        public List<ActivityModel> ActivityModels(string userName)
        {
            var resultingList = new List<ActivityModel>();
            foreach (var p in ModelList)
            {
                var userActivities = p.AssignedActivities(userName);
                resultingList.AddRange(userActivities);
            }

            return resultingList;
        }

        public HourRegistrationModel getHourRegistrationModel(string regId)
        {
            var activities = ActivityModels();

            foreach (var activity in activities)
            {
                var tm = activity.HourRegistrationObjects().ToList();
                var rObject = tm.Find(item => item.RegistrationId == regId);
                if (rObject != null)
                    return rObject;
            }

            return null;
        }

        public List<HourRegistrationModel> GetHourRegistrationModels()
        {
            var TimeObjects = new List<HourRegistrationModel>();
            var activities = ActivityModels();

            foreach (var activity in activities)
            {
                var tm = activity.HourRegistrationObjects().ToList();
                TimeObjects.AddRange(tm);
            }
            return TimeObjects;
        }

        public List<HourRegistrationModel> GetHourRegistrationModels(string userName)
        {
            var RegObjects = new List<HourRegistrationModel>();
            var activities = ActivityModels(userName);

            foreach (var activity in activities)
            {
                var tm = activity.HourRegistrationObjects(userName).ToList();
                RegObjects.AddRange(tm);
            }
            return RegObjects;
        }

        public ListViewItem[] ProjectItemModels()
        {
            int count = ModelList.Count, index = 0;
            var models = new ListViewItem[count];

            foreach (var p in ModelList)
                models[index++] = p.ItemModel();

            return models;
        }

        public ListViewItem[] ActivityTimeObjectModels()
        {
            var activities = new List<ListViewItem>();
            foreach (var p in ModelList)
            {
                foreach (var activity in p.AllActivities())
                {
                    var models = activity.RegistrationObjectModels().ToList();
                    activities.AddRange(models);
                }
            }

            return activities.ToArray();
        }

        public ListViewItem[] ActivityTimeObjectModels(string userName)
        {
            var TimeObjectModels = new List<ListViewItem>();

            foreach (var p in ModelList)
            {
                foreach (var activity in p.AllActivities())
                {
                    var models = activity.RegistrationObjectItemModels(userName).ToList();
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
                foreach (var p in ModelList)
                foreach (var activity in p.AllActivities())
                {
                    var model = activity.ItemModel();
                    models.Add(model);
                }

                return models.ToArray();
            }

            var userId = uManager.loggedIn().ModelIdentity;

            foreach (var p in ModelList)
            foreach (var activity in p.AllActivities())
            {
                if (!activity.IsUserAssigned(uManager) && p.projectLeaderId != userId)
                    continue;

                var model = activity.ItemModel();
                models.Add(model);
            }

            return models.ToArray();
        }

        public ListViewItem[] UserAssignedActivityModels(UserManager uManager)
        {
            var userName = uManager.loggedIn().ModelIdentity;
            var assignedActivities = ActivityModels(userName);
            var count = assignedActivities.Count;
            var models = new ListViewItem[count];
            var index = 0;

            foreach (var activity in assignedActivities)
            {
                var model = new ListViewItem(activity.ModelIdentity);
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
            var assignedActivities = ActivityModels(userName);
            var count = assignedActivities.Count;
            var models = new ListViewItem[count];
            var index = 0;

            foreach (var activity in assignedActivities)
            {
                var model = new ListViewItem(activity.ModelIdentity);
                model.SubItems.Add(activity.EstimatedDuration().ToString());
                model.SubItems.Add(activity.TotalRegisteredHours(userName).ToString());
                var projectId = activity.ParentProjectId;
                model.SubItems.Add(projectId);

                models[index++] = model;
            }

            return models;
        }

        public UserModel.Availability IsUserAvailableWithinTimeSpan(string userName, UserManager uManager, DateTime fromDate, DateTime toDate)
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
                return UserModel.Availability.NotAvailable;
            return partlyOccurrences + fullOccurrences >= 20
                ? UserModel.Availability.PartlyAvailable
                : UserModel.Availability.Available;
        }

        public IEnumerable<ActivityEntity> UserActivityEntities(string userName,UserManager uManager)
        {
            return ActivityModels(userName).Select(item =>
                new ActivityEntity(item.ModelIdentity, item.StartDate, item.EndDate)).ToList();
        }
        public override ProjectModel Model(string id)
        {
            return ModelList.Find(item => item.ModelIdentity == id);
        }
        

        public override List<string> ListModelIdentities() => ModelList.Select(item => item.ModelIdentity).ToList();
    }
}