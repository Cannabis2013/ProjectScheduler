using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ProjectNameSpace;

namespace ProjectNameSpace
{
    public class Activity
    {

        /*
         * Constructor section
         * - Activity(Activity title, assigned users)
         * - Activity(Activity title)
         * - Default constructor with no parameters
         */

        public Activity(string title, HashSet<string> assignedUserIdentities = null)
        {
            this.title = title;
            this.assignedUserIdentities = assignedUserIdentities ?? assignedUserIdentities;
        }

        public Activity(string title) => this.title = title;
        public Activity() { }

        /*
         * Public fields section
         * - Title : string
         */

        public string title { get; set; }

        /*
         * public methods section
         * - Assign users to activity
         * - Register hour to activity
         * - Retrieve item models
         * -- Retrieve item models for key values overview presentation
         * -- Retrieve item models for assigned users overview presentation
         */

        public void assignUser(string userID) => assignedUserIdentities.Add(userID);
        public void assignUsers(List<string> userIDs)
        {
            foreach (string userID in userIDs)
                assignedUserIdentities.Add(userID);
        }

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
         * Private fields section
         * - The assigned users
         * - The TimeObject's that contain the hours spend and the corresponding user
         */
        
        private HashSet<string> assignedUserIdentities = new HashSet<string>();
        private List<TimeObject> timeObjects = new List<TimeObject>();
    }
}
