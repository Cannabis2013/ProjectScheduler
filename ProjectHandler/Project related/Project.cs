using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjectNameSpace
{
    public class Project
    {
        public Project(string projectId, HashSet<string> assignedUsers)
        {
            this.projectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
            assignedUserIdentities = assignedUsers;
        }

        public Project(string projectId)
        {
            this.projectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
        }

        public ListViewItem[] activityItemModels()
        {
            int count = projectActivities.Count, index = 0;
            var models = new ListViewItem[count];
            foreach (var a in projectActivities)
            {
                models[index] = a.activityItemModel();
                index++;
            }
            return models;
        }

        public Activity activity(int index)
        {
            var i = 0;
            foreach(var a in projectActivities)
            {
                if (i++ == index)
                    return a;
            }
            return null;
        }

        public void assignUserToProject(string userId) => assignedUserIdentities.Add(userId);

        public void assignUsersToProject(string[] users)
        {
            foreach (var u in users)
                assignedUserIdentities.Add(u);
        }

        public void addActivity(Activity a) => projectActivities.AddLast(a);
        public int estimatedDuration() => estimatedEndWeek - startWeek;
        

        /*
         * Public fields section
         */

        public string projectId { get; set; }
        public int startWeek { get; set; }
        public int estimatedEndWeek { get; set; }
        public string projectLeaderId { get; set; }

        /*
         * Private fields section
         */

        private readonly HashSet<string> assignedUserIdentities = new HashSet<string>();
        private readonly LinkedList<Activity> projectActivities = new LinkedList<Activity>();
    }
}