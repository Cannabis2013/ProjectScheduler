using System;
using System.Collections.Generic;
using Projecthandler;
using Projecthandler.Object_wrappers;
using VirtualUserDomain;

namespace ProjectNameSpace
{
    public class Project : IItemModel<ActivityItemModel>
    {
        public Project(string projectID)
        {
            ProjectID = projectID ?? throw new ArgumentNullException(nameof(projectID));
        }

        public string ProjectID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime estimatedEndDate { get; set; }

        public void addActivity(Activity a) => projectActivities.AddLast(a);

        public List<ActivityItemModel> itemModelList()
        {
            throw new NotImplementedException();
        }

        internal LinkedList<Activity> projectActivities = new LinkedList<Activity>();
    }

    public class Activity : IItemModel<UserItemModel>
    {
        public Activity(string title, HashSet<string> assignedUserIdentities = null)
        {
            this.title = title;
            this.assignedUserIdentities = assignedUserIdentities ?? assignedUserIdentities;
        }

        public Activity(string title)
        {
            this.title = title;
        }

        public Activity()
        {}

        public string title { get; set; }
        

        public void addTimeObject(TimeObject timeO) => timeObjects.Add(timeO);

        public List<UserItemModel> itemModelList()
        {
            throw new NotImplementedException();
        }

        /*
         * List section
         * The assigned users
         * The TimeObject's that contain the hours spend and the corresponding user
         */

        internal HashSet<string> assignedUserIdentities = new HashSet<string>();
        private List<TimeObject> timeObjects = new List<TimeObject>();
    }
}
