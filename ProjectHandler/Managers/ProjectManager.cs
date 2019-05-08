using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Projecthandler.Abstract_classes_and_interfaces;
using Templates;
using UserDomain;

namespace ProjectRelated
{
    [Serializable]
    public class ProjectManager : AbstractManager,ICustomObservable
    {
        protected List<ICustomObserver> observers = new List<ICustomObserver>();

        public ListViewItem[] ProjectItemModels()
        {
            int count = Models.Count, index = 0;
            var models = new ListViewItem[count];

            foreach (var p in Models)
                models[index++] = p.ItemModel();

            return models;
        }

        public void AddActivity(string projectIdentity, ActivityModel activity)
        {
            var project = Model(projectIdentity);
            project.AddSubModel(activity);
        }

        public bool RemoveActivityModel(string projectId, string activityId)
        {
            var p = Model(projectId);
            var activity = p?.SubModel(activityId);
            if (activity == null)
                return false;
            p.RemoveSubModel(activity);
            return true;
        }

        public ActivityModel ActivityModelById(string projectIdentity, string activityIdentity)
        {
            var project = Model(projectIdentity);
            return (ActivityModel) project.SubModels.First(item => item.ModelIdentity == activityIdentity);
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
                var userActivities = project.Activities(userName);
                resultingList.AddRange(userActivities);
            }

            return resultingList;
        }

        public void RegisterHour(string projectId, string activityId, HourRegistrationModel obj)
        {
            var activity = ActivityModelById(projectId, activityId);
            activity.AddSubModel(obj);
        }

        public void UnRegisterHour(string projectId, string activityId, string regId)
        {
            var activity = ActivityModelById(projectId, activityId);
            activity.RemoveSubModel(regId);
        }

        public HourRegistrationModel getHourRegistrationModel(string regId)
        {
            var activities = ActivityModels();

            foreach (var activity in activities)
            {
                var tm = activity.HourRegistrationObjects();
                var rObject = tm.Find(item => item.ModelIdentity == regId);
                if (rObject != null)
                    return rObject;
            }

            return null;
        }

        public HourRegistrationModel getHourRegistrationModel(string projectId, string activityId,string regId)
        {
            var activity = ActivityModelById(projectId, activityId);
            return activity.HourRegistrationObjects().First(item => item.ModelIdentity == regId);
        }

        public ListViewItem[] ActivityItemModels(UserManager uManager)
        {
            var models = new List<ListViewItem>();
            if (uManager.isAdmin())
            {
                foreach (var item in Models)
                {
                    var project = (ProjectModel)item;
                    models.AddRange(project.ActivityItemModels());
                }

                return models.ToArray();
            }

            var userId = uManager.loggedIn().ModelIdentity;

            foreach (var item in Models)
            {
                var project = (ProjectModel)item;
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

        public ListViewItem[] ActivityItemModels(string userName)
        {

            List<ListViewItem> models = new List<ListViewItem>();
            foreach (var p in Models)
            {
                var activities = p.SubModels.Where(item => ((ActivityModel) item).IsUserAssigned(userName));
                 var list = activities.Select(item => item.ItemModel()).ToList();
                models.AddRange(list);
            }

            return models.ToArray();
        }

        public ListViewItem[] RegistrationItemModels()
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

        public ListViewItem[] RegistrationItemModels(string userName)
        {
            var regModels = new List<ListViewItem>();
            

            foreach (var pModel in Models)
            {
                foreach (var activity in pModel.AllSubModels<ActivityModel>())
                {
                    var rObjects = activity.AllSubModels<HourRegistrationModel>().Where(item => item.UserName == userName);
                    var models = rObjects.Select(item => item.ItemModel());
                    regModels.AddRange(models);
                }
            }
            return regModels.ToArray();
        }

        public string UserAvailability(string userName, UserManager uManager, DateTime fromDate, DateTime toDate)
        {
            int partlyOccurrences = 0, fullOccurrences = 0;

            foreach (var item in UserActivityEntities(userName, uManager))
            {
                if(item.TypeOfActivity == ActivityModel.ActivityType.Work_Related)
                {
                    if (DateTime.Compare(fromDate,item.StartDate) < 0  && DateTime.Compare(toDate, item.EndDate) > 0)
                        partlyOccurrences++;
                    else if (DateTime.Compare(fromDate,item.StartDate) < 0 && item.withinTimespan(toDate))
                        partlyOccurrences++;
                    else if (item.withinTimespan(fromDate) && DateTime.Compare(toDate,item.EndDate) > 0)
                        partlyOccurrences++;
                    else if (item.withinTimespan(fromDate) && item.withinTimespan(toDate))
                        fullOccurrences++;
                }
                else
                {
                    if (DateTime.Compare(fromDate, item.StartDate) < 0 && DateTime.Compare(toDate, item.EndDate) > 0)
                        return "Partly available";
                    else if (DateTime.Compare(fromDate, item.StartDate) < 0 && item.withinTimespan(toDate))
                        return "Partly available";
                    else if (item.withinTimespan(fromDate) && DateTime.Compare(toDate, item.EndDate) > 0)
                        return "Partly available";
                    else if (item.withinTimespan(fromDate) && item.withinTimespan(toDate))
                        return "Not available";
                }
            }

            if (fullOccurrences >= 20)
                return "Not available";
            return partlyOccurrences + fullOccurrences >= 20
                ? "Partly available"
                : "Available";
        }

        private IEnumerable<ActivityEntity> UserActivityEntities(string userName,UserManager uManager)
        {
            return ActivityModels(userName).Select(item =>
                new ActivityEntity(item.ModelIdentity, item.StartDate, item.EndDate, item.TypeOfActivity)).ToList();
        }

        public override List<string> ListModelIdentities() => Models.Select(item => item.ModelIdentity).ToList();

        public override void RequestUpdate()
        {
            NotifyObservers();
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.UpdateView();
            }
        }

        public void SubScribe(ICustomObserver observer)
        {
            observers.Add(observer);
        }

        public void UnSubScribe(ICustomObserver observer)
        {
            observers.Remove(observer);
        }

        public void UnSubScribeAll()
        {
            observers.Clear();
        }
    }
}