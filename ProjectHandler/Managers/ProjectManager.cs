using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Templates;
using VirtualUserDomain;

/*
 *
 */

namespace ProjectRelated
{
    [Serializable]
    public class ProjectManager : AbstractManager
    {

        public bool RemoveActivityModel(string projectId, string activityId)
        {
            var p = Model(projectId);
            var activity = p?.SubModel(activityId);
            if (activity == null)
                return false;
            p.RemoveSubModel(activity);
            return true;
        }

        public List<ActivityModel> ActivityModels()
        {
            var resultingList = new List<ActivityModel>();
            foreach (var p in Models)
            {
                var userActivities = p.AllSubModels<ActivityModel>();
                resultingList.AddRange(userActivities);
            }

            return resultingList;
        }

        public List<ActivityModel> ActivityModels(string userName)
        {
            var resultingList = new List<ActivityModel>();
            foreach (var model in Models)
            {
                var project = (ProjectModel) model;
                var userActivities = project.AssignedActivitiesModels(userName);
                resultingList.AddRange(userActivities);
            }

            return resultingList;
        }

        public HourRegistrationModel getHourRegistrationModel(string regId)
        {
            var activities = Models.Select(item => (ActivityModel) item);

            foreach (var activity in activities)
            {
                var tm = activity.HourRegistrationObjects().ToList();
                var rObject = tm.Find(item => item.ModelIdentity == regId);
                if (rObject != null)
                    return rObject;
            }

            return null;
        }

        public List<HourRegistrationModel> AllHourRegistrationModels()
        {
            var TimeObjects = new List<HourRegistrationModel>();
            var activities = Models.Select(item => (ActivityModel) item);

            foreach (var activity in activities)
            {
                var tm = activity.HourRegistrationObjects().ToList();
                TimeObjects.AddRange(tm);
            }
            return TimeObjects;
        }

        public List<HourRegistrationModel> AllHourRegistrationModels(string userName)
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
            int count = Models.Count, index = 0;
            var models = new ListViewItem[count];

            foreach (var p in Models)
                models[index++] = p.ItemModel();

            return models;
        }

        public ListViewItem[] AllActivitySubModels()
        {
            var activities = new List<ListViewItem>();
            foreach (var p in Models)
            {
                foreach (var T in p.AllSubModels<ActivityModel>())
                {
                    var activity = (ActivityModel) T;
                    var models = activity.allSubItemModels();
                    activities.AddRange(models);
                }
            }

            return activities.ToArray();
        }

        public ListViewItem[] ActivityRegistrationItemModels(string userName)
        {
            var TimeObjectModels = new List<ListViewItem>();

            foreach (var pModel in Models)
            {
                foreach (var activity in pModel.AllSubModels<ActivityModel>())
                {
                    var rObjects = activity.AllSubModels<HourRegistrationModel>().Where(item => item.UserName == userName);
                    var models = rObjects.Select(item => item.ItemModel());
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
                foreach (var item in Models)
                {
                    var project = (ProjectModel) item;
                    models.AddRange(project.ActivityItemModels());
                }

                return models.ToArray();
            }

            var userId = uManager.loggedIn().ModelIdentity;

            foreach (var item in Models)
            {
                var project = (ProjectModel) item;
                foreach (var activity in project.AllSubModels<ActivityModel>())
                {

                    if (!activity.IsUserAssigned(uManager) && project.projectLeaderId != userId)
                        continue;

                    var model = activity.ItemModel();
                    models.Add(model);
                }
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
                var projectId = activity.ParentModelIdentity();
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
                var projectId = activity.ParentModelIdentity();
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

        public override List<string> ListModelIdentities() => Models.Select(item => item.ModelIdentity).ToList();
    }
}