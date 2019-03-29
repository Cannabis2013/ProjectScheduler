using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WrapperDomain;

namespace ProjectNameSpace
{
    public class Activity
    {
        public Activity(string title, HashSet<string> assignedUserIdentities = null)
        {
            this.title = title;
            this.assignedUserIdentities = assignedUserIdentities ?? assignedUserIdentities;
        }

        public Activity(string title) => this.title = title;
        public Activity() { }

        public string title { get; set; }

        public void addTimeObject(TimeObject timeO) => timeObjects.Add(timeO);

        public ListViewItem activityItemModel()
        {
            ListViewItem model = new ListViewItem(title);

            StringBuilder assignedHours = new StringBuilder("Total assigned hours: ");
            int totalHours = totalRegisteredHours();
            assignedHours.Append(totalHours.ToString());
            model.SubItems.Add(assignedHours.ToString());

            StringBuilder assignedUsers = new StringBuilder("Active users: ");
            int totalUsersAssigned = assignedUserIdentities.Count;
            assignedUsers.Append(totalUsersAssigned.ToString());

            model.SubItems.Add(assignedUsers.ToString());

            return model;
        }

        public List<ListViewItem> assignedUserModels()
        {
            List<ListViewItem> models = new List<ListViewItem>();
            foreach (string userName in assignedUserIdentities)
            {
                ListViewItem model = new ListViewItem(userName);
                StringBuilder totalHours = new StringBuilder("Total hours registered: ");
                
                totalHours.Append(totalRegisteredHours(userName));
                model.SubItems.Add(totalHours.ToString());

                models.Add(model);
            }
            return models;
        }

        private int totalRegisteredHours(string userName = null)
        {
            int totalHours = 0;
            if(userName != null)
            {
                foreach (TimeObject t in timeObjects)
                {
                    if (userName == t.userName)
                        totalHours += t.gethours();
                }
            }
            else
            {
                foreach (TimeObject t in timeObjects)
                {
                    totalHours += t.gethours();
                }
            }

            return totalHours;
        }

        /*
         * List section
         * The assigned users
         * The TimeObject's that contain the hours spend and the corresponding user
         */

        private HashSet<string> assignedUserIdentities = new HashSet<string>();
        private List<TimeObject> timeObjects = new List<TimeObject>();
    }
}
