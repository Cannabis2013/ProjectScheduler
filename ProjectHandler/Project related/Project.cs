using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Templates;
using VirtualUserDomain;

namespace ProjectNameSpace
{
    public class Project : ItemModelEntity<ListViewItem>
    {

        /*
         * Constructor section
         */

        public Project(string projectId)
        {
            t = projectId ?? throw new ArgumentNullException(nameof(projectId));
        }

        /*
         * Constructor section ends
         */

        /*
         * Public methods begin
         */

        public override ListViewItem itemModel(ListMode mode = ListMode.Tile)
        {
            if (mode == ListMode.Tile)
                return itemTileModel();

            return itemListModel();
        }

        public ListViewItem[] activityItemModels()
        {
            int count = projectActivities.Count, index = 0;
            var models = new ListViewItem[count];
            foreach (var a in projectActivities)
            {
                models[index] = a.itemModel();
                index++;
            }

            return models;
        }

        public Activity activity(int index)
        {
            var i = 0;
            foreach (var a in projectActivities)
            {
                if (i++ == index)
                    return a;
            }

            return null;
        }
        

        public void addActivity(Activity a) => projectActivities.AddLast(a);
        public int estimatedDuration() => endWeek - startWeek;
        

        /*
         * Public methods ends
         */

        /*
         * Public fields section
         */

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
            get => pLeader;
            set => pLeader = value;
        }

        /*
        * Create a list of Activity entities for user statistic purposes.
        */

        public List<ActivityEntity> activityEntities(string userName = null)
        {
            var activities = (userName == null) ? projectActivities.ToList() : 
                projectActivities.Where(item => item.isUserAssigned(userName));

            return activities.Select(item => 
                new ActivityEntity(item.startWeek, item.endWeek, item.id)).ToList();
        }

        public List<Activity> assignedActivities(string userName)
        {
            var userActivities = projectActivities.Where(item => item.isUserAssigned(userName));
            return userActivities.Select(item => new Activity(item)).ToList();
        }
        

        /*
         * Private methods section begins
         */

        private ListViewItem itemTileModel()
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


            // Set picture index
            model.ImageIndex = 0;
            model.StateImageIndex = 0;

            return model;
        }

        private ListViewItem itemListModel()
        {
            var model = new ListViewItem(id);

            var userLeader = new StringBuilder(projectLeaderId);
            model.SubItems.Add(userLeader.ToString());

            model.SubItems.Add(startWeek.ToString());
            model.SubItems.Add(endWeek.ToString());
            
            // Set picture index
            model.ImageIndex = 0;
            model.StateImageIndex = 0;

            return model;
        }

        /*
         * Private methods section ends
         */

        /*
        * Private fields section
        */

        private string pLeader;
        private int sWeek, eWeek;
        
        private readonly LinkedList<Activity> projectActivities = new LinkedList<Activity>();
    }
}