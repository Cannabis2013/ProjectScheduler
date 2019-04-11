using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Projecthandler.Templates_and_interfaces;
using Templates;
using VirtualUserDomain;

namespace ProjectRelated
{
    [Serializable]
    public class ProjectModel : AbstractModel<ListViewItem,ActivityModel>
    {
        private readonly List<ActivityModel> projectActivities = new List<ActivityModel>();
        private string pLeaderId, shortDescription;
        private DateTime startDate, endDate;

        public DateTime StartDate
        {
            get => startDate;
            set => startDate = value;
        }

        public DateTime EndDate
        {
            get => endDate;
            set => endDate = value;
        }

        public string projectLeaderId
        {
            get => pLeaderId;
            set => pLeaderId = value;
        }

        public string ShortDescription
        {
            get => shortDescription;
            set => shortDescription = value;
        }

        public ListViewItem[] ActivityItemModels()
        {
            int count = projectActivities.Count, index = 0;
            var models = new ListViewItem[count];
            foreach (var a in projectActivities)
            {
                models[index] = a.ItemModel();
                index++;
            }

            return models;
        }

        public ActivityModel Activity(int index)
        {
            var i = 0;
            foreach (var a in projectActivities)
                if (i++ == index)
                    return a;

            return null;
        }

        public ActivityModel Activity(string activityId) => projectActivities.Where(item => item.ModelIdentity == activityId).ToArray()[0];

        public void AddActivity(ActivityModel a) => projectActivities.Add(a);

        public void RemoveActivity(ActivityModel a) => projectActivities.Remove(a);
        

        public List<ActivityModel> AllActivities() => projectActivities.ToList();

        public List<ActivityModel> AssignedActivities(string userName)
        {
            var userActivities = projectActivities.Where(item => item.IsUserAssigned(userName));
            return userActivities.Select(item => new ActivityModel(item)).ToList();
        }

        public override ListViewItem ItemModel()
        {
            var model = new ListViewItem(ModelIdentity);

            var userLeader = new StringBuilder(projectLeaderId);
            model.SubItems.Add(userLeader.ToString());

            model.SubItems.Add(StartDate.ToString());
            model.SubItems.Add(EndDate.ToString());

            model.ImageIndex = 0;
            model.StateImageIndex = 0;

            return model;
        }
    }
}