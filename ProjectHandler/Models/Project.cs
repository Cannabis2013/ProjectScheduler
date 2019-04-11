using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Templates;
using VirtualUserDomain;

namespace ProjectRelated
{
    [Serializable]
    public class Project : ModelEntity<ListViewItem>
    {
        private readonly List<Activity> projectActivities = new List<Activity>();
        private string pLeaderId, shortDescription;
        private DateTime startDate, endDate;


        public Project(string projectId)
        {
            itemId = projectId ?? throw new ArgumentNullException(nameof(projectId));
        }

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

        public override ListViewItem ItemModel(ListMode mode = ListMode.Tile)
        {
            return mode == ListMode.Tile ? ItemTileModel() : ItemListModel();
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

        public Activity Activity(int index)
        {
            var i = 0;
            foreach (var a in projectActivities)
                if (i++ == index)
                    return a;

            return null;
        }

        public Activity Activity(string activityId) => projectActivities.Where(item => item.ActivityId == activityId).ToArray()[0];

        public void AddActivity(Activity a) => projectActivities.Add(a);

        public void RemoveActivity(Activity a) => projectActivities.Remove(a);
        

        public List<Activity> AllActivities() => projectActivities.ToList();

        public List<Activity> AssignedActivities(string userName)
        {
            var userActivities = projectActivities.Where(item => item.IsUserAssigned(userName));
            return userActivities.Select(item => new Activity(item)).ToList();
        }

        private ListViewItem ItemTileModel()
        {
            var model = new ListViewItem(id);

            var userLeader = new StringBuilder("Tech lead: ");
            userLeader.Append(projectLeaderId);
            model.SubItems.Add(userLeader.ToString());


            var sDate = new StringBuilder("Start date: ");
            sDate.Append(StartDate);
            model.SubItems.Add(sDate.ToString());

            var eDate = new StringBuilder("End date: ");
            eDate.Append(EndDate);
            model.SubItems.Add(eDate.ToString());
            
            model.ImageIndex = 0;
            model.StateImageIndex = 0;

            return model;
        }

        private ListViewItem ItemListModel()
        {
            var model = new ListViewItem(id);

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