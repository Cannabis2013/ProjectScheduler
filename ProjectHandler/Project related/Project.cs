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
        

        internal LinkedList<Activity> projectActivities = new LinkedList<Activity>();
    }

    public class Activity
    {
        public Activity(string title, HashSet<string> assignedUserIdentities = null)
        {
            this.title = title;
            this.assignedUserIdentities = assignedUserIdentities ?? assignedUserIdentities;
        }

        public Activity(string title) => this.title = title;
        public Activity(){}

        public string title { get; set; }

        public void addTimeObject(TimeObject timeO) => timeObjects.Add(timeO);        

        /*
         * List section
         * The assigned users
         * The TimeObject's that contain the hours spend and the corresponding user
         */

        internal HashSet<string> assignedUserIdentities = new HashSet<string>();
        private List<TimeObject> timeObjects = new List<TimeObject>();
    }
}
