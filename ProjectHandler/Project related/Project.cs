using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Projecthandler.Templates;

namespace Projecthandler.Project_related
{
    [Serializable]
    public class Project : ItemModelEntity<ListViewItem>
    {

        private readonly LinkedList<Activity> projectActivities = new LinkedList<Activity>();
        private string pLeader;
        private int sWeek, eWeek;

        public Project(string projectId)
        {
            EntityTitle = projectId ?? throw new ArgumentNullException(nameof(projectId));
        }

        public int StartWeek
        {
            get => sWeek;
            set => sWeek = value;
        }

        public int EndWeek
        {
            get => eWeek;
            set => eWeek = value;
        }

        public string ProjectLeaderId
        {
            get => pLeader;
            set => pLeader = value;
        }
        public override ListViewItem ItemModel(ListMode mode = ListMode.Tile)
        {
            if (mode == ListMode.Tile)
                return ItemTileModel();

            return ItemListModel();
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

        public Activity Activity(string activityId)
        {
            return projectActivities.Where(item => item.Id == activityId).ToArray()[0];
        }

        public void AddActivity(Activity a)
        {
            projectActivities.AddLast(a);
        }

        public void RemoveActivity(Activity a)
        {
            projectActivities.Remove(a);
        }

        public int EstimatedDuration()
        {
            return EndWeek - StartWeek;
        }

        public List<Activity> AllActivities()
        {
            return projectActivities.ToList();
        }

        public List<Activity> AssignedActivities(string userName)
        {
            var userActivities = projectActivities.Where(item => item.IsUserAssigned(userName));
            return userActivities.Select(item => new Activity(item)).ToList();
        }

        private ListViewItem ItemTileModel()
        {
            var model = new ListViewItem(Id);

            var userLeader = new StringBuilder("Tech lead: ");
            userLeader.Append(ProjectLeaderId);
            model.SubItems.Add(userLeader.ToString());


            var startDate = new StringBuilder("Week begin: ");
            startDate.Append(StartWeek);
            model.SubItems.Add(startDate.ToString());

            var endDate = new StringBuilder("Week end: ");
            endDate.Append(EndWeek);
            model.SubItems.Add(endDate.ToString());


            // Set picture index
            model.ImageIndex = 0;
            model.StateImageIndex = 0;

            return model;
        }

        private ListViewItem ItemListModel()
        {
            var model = new ListViewItem(Id);

            var userLeader = new StringBuilder(ProjectLeaderId);
            model.SubItems.Add(userLeader.ToString());

            model.SubItems.Add(StartWeek.ToString());
            model.SubItems.Add(EndWeek.ToString());

            model.ImageIndex = 0;
            model.StateImageIndex = 0;

            return model;
        }
    }
}