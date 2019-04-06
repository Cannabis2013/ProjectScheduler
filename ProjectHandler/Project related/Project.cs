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
    public class Project : ItemModelEntity<ListViewItem>
    {
        private readonly List<Activity> projectActivities = new List<Activity>();
        private string pLeaderId;
        private int sWeek, eWeek;


        public Project(string projectId)
        {
            itemId = projectId ?? throw new ArgumentNullException(nameof(projectId));
        }

        public int startWeek
        {
            get => sWeek;
            set => sWeek = value;
        }

        public int endWeek
        {
            get => eWeek;
            set => eWeek = value;
        }

        public string projectLeaderId
        {
            get => pLeaderId;
            set => pLeaderId = value;
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
        
        public int EstimatedDuration() => endWeek - startWeek;

        public List<Activity> AllActivities() => projectActivities.ToList();

        public List<Activity> AssignedActivities(string userName, UserManager uManager)
        {
            var userActivities = projectActivities.Where(item => item.IsUserAssigned(userName));
            return userActivities.Select(item => new Activity(item,uManager)).ToList();
        }

        private ListViewItem ItemTileModel()
        {
            var model = new ListViewItem(id);

            var userLeader = new StringBuilder("Tech lead: ");
            userLeader.Append(projectLeaderId);
            model.SubItems.Add(userLeader.ToString());


            var startDate = new StringBuilder("Week begin: ");
            startDate.Append(startWeek);
            model.SubItems.Add(startDate.ToString());

            var endDate = new StringBuilder("Week end: ");
            endDate.Append(endWeek);
            model.SubItems.Add(endDate.ToString());
            
            model.ImageIndex = 0;
            model.StateImageIndex = 0;

            return model;
        }

        private ListViewItem ItemListModel()
        {
            var model = new ListViewItem(id);

            var userLeader = new StringBuilder(projectLeaderId);
            model.SubItems.Add(userLeader.ToString());

            model.SubItems.Add(startWeek.ToString());
            model.SubItems.Add(endWeek.ToString());
            
            model.ImageIndex = 0;
            model.StateImageIndex = 0;

            return model;
        }
    }
}