using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VirtualUserDomain;

namespace ProjectNameSpace
{
    public class Project
    {

        /*
         * Constructor section
         */

        public Project(string projectId, List<string> assignedUsers)
        {
            this.projectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
            assignedUserIdentities = assignedUsers;
        }

        public Project(string projectId)
        {
            this.projectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
        }

        /*
         * Constructor section ends
         */

        /*
         * Public methods begin
         */

        public ListViewItem itemModel()
        {
            var model = new ListViewItem(title);

            var startDate = new StringBuilder("Week begin: ");
            startDate.Append(startWeek);

            model.SubItems.Add(startDate.ToString());

            var endDate = new StringBuilder("Week end: ");
            endDate.Append(endWeek);
            model.SubItems.Add(endDate.ToString());

            var userLeader = new StringBuilder("Tech lead: ");
            userLeader.Append(projectLeaderId);

            model.SubItems.Add(userLeader.ToString());

            // Set picture index
            model.ImageIndex = 0;
            model.StateImageIndex = 0;

            return model;
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

        public void assignUserToProject(string userId) => assignedUserIdentities.Add(userId);

        public void assignUsersToProject(string[] users, UserManager uManager = null)
        {
            foreach (var u in users)
            {
                UserManager.user(u).assignProject(this);
                assignedUserIdentities.Add(u);
            }
        }

        public void unAssignUsers()
        {
            foreach (var uId in assignedUserIdentities)
                UserManager.user(uId).unAssignProject(this);

            assignedUserIdentities.Clear();
        }

        public void addActivity(Activity a) => projectActivities.AddLast(a);
        public int estimatedDuration() => endWeek - startWeek;

        public List<string> assignedUserList() => assignedUserIdentities;

        /*
         * Public methods ends
         */

        /*
         * Public fields section
         */

        public string projectId
        {
            get => title;
            set => title = value;
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
            get => pLeader;
            set => pLeader = value;
        }

        /*
             * Create a list of Activity entities for user statistic purposes.
             */

        public List<ActivityEntity> activityEntities()
        {
            var entities = new List<ActivityEntity>();
            foreach (var item in projectActivities)
            {
                var entity = new ActivityEntity(item.startWeek, item.endWeek, item.title);
                entities.Add(entity);
            }

            return entities;
        }

        /*
        * Private fields section
        */

        private string title, pLeader;
        private int sWeek, eWeek;

        private readonly List<string> assignedUserIdentities = new List<string>();
        private readonly LinkedList<Activity> projectActivities = new LinkedList<Activity>();
    }
}