using System;
using System.Collections.Generic;
using Projecthandler;
using Projecthandler.Object_wrappers;
using VirtualUserDomain;

namespace ProjectNameSpace
{
    public class Project
    {
        public Project(string projectID)
        {
            ProjectID = projectID ?? throw new ArgumentNullException(nameof(projectID));
        }

        public string ProjectID { get; set; }
        public int StartWeek { get; set; }
        public int estimatedEndWeek { get; set; }
        public string projectLeaderID { get; set; }

        public int estimatedDuration() => estimatedEndWeek - StartWeek;

        public void addActivity(Activity a) => projectActivities.AddLast(a);
        
        private LinkedList<Activity> projectActivities = new LinkedList<Activity>();
    }
}
