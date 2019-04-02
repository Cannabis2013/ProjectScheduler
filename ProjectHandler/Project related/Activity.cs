using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectNameSpace;
using Templates;


/*
 * Class Activity has the following important pseudo attributes:
 * - ActivityId (which in this case is the inherited field variable 't')
 * - Start and end weeks
 * - User assignment:
 * -- assignUser()
 * -- assignUsers()
 * - Register hours:
 * -- addTimeObject()
 */

namespace ProjectNameSpace
{
    public class Activity : ItemModelEntity<ListViewItem>
    {

        /*
         * Constructor section
         * - Activity(Activity title, assigned users)
         * - Activity(Activity title)
         * - Default constructor with no parameters
         */

        public Activity(string title, int sWeek, int eWeek, HashSet<string> assignedUserIdentities = null)
        {
            t = title;
            this.sWeek = sWeek;
            this.eWeek = eWeek;
            this.assignedUserIdentities = assignedUserIdentities ?? assignedUserIdentities;
        }

        public Activity(string title, int sWeek, int eWeek)
        {
            t = title;
            this.sWeek = sWeek;
            this.eWeek = eWeek;
        }

        public Activity(int sWeek, int eWeek)
        {
            this.sWeek = sWeek;
            this.eWeek = eWeek;
        }

        /*
         * Copy constructor which takes an argument as same type.
         */

        public Activity(Activity copy)
        {
            t = copy.t;
            sWeek = copy.sWeek;
            eWeek = copy.eWeek;
            assignedUserIdentities = copy.assignedUserIdentities;
            registeredTimeObjects = copy.registeredTimeObjects;
        }

        /*
         * Constructor section ends
         */

        /*
         * public methods section
         * - Retrieve a model list for view representation
         * - Assign users to activity
         * - Register hour to activity
         * - Retrieve item models
         * -- Retrieve item models for key values overview presentation
         * -- Retrieve item models for assigned users overview presentation
         */

        public override ListViewItem itemModel(ListMode mode = ListMode.Tile)
        {
            return mode == ListMode.Tile ? itemTileModel() : itemListModel();
        }

        public ListViewItem[] registeredHourItemModels(string userName)
        {
            var userTimeObjects = registeredTimeObjects.Where(item => item.UserName == userName).ToArray();

            var tObjects = userTimeObjects.Select(item => new TimeObject(item)).ToArray();

            var models = new ListViewItem[tObjects.Length];
            var index = 0;

            foreach (var tObject in tObjects)
            {
                var model = new ListViewItem(tObject.UserName);
                model.SubItems.Add(tObject.Hours().ToString());
                model.SubItems.Add(tObject.Week().ToString());

                models[index++] = model;
            }

            return models;

        }

        public string activityId
        {
            get => t;
            set => t = value;
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

        /*
         * Assign users to activity
         */

        public void assignUser(string userID) => assignedUserIdentities.Add(userID);
        public void assignUsers(List<string> userIDs)
        {
            foreach (var userId in userIDs)
                assignedUserIdentities.Add(userId);
        }

        public bool isUserAssigned(string userName)
        {
            return assignedUserIdentities.Any(item => item == userName);
        }

        /*
         * Register hours
         */

        public void addTimeObject(TimeObject time) => registeredTimeObjects.Add(time);

        public List<ListViewItem> assignedUserModels()
        {
            var models = new List<ListViewItem>();
            foreach (var userName in assignedUserIdentities)
            {
                var model = new ListViewItem(userName);
                var totalHours = new StringBuilder("Total hours registered: ");
                
                totalHours.Append(totalRegisteredHours(userName));
                model.SubItems.Add(totalHours.ToString());

                models.Add(model);
            }
            return models;
        }

        /*
         * Private methods section begins
         */

        private ListViewItem itemTileModel()
        {
            var model = new ListViewItem(title);

            var assignedHours = new StringBuilder("Total assigned hours: ");
            var totalHours = totalRegisteredHours();
            assignedHours.Append(totalHours.ToString());
            model.SubItems.Add(assignedHours.ToString());

            var assignedUsers = new StringBuilder("Active users: ");
            var totalUsersAssigned = assignedUserIdentities.Count;
            assignedUsers.Append(totalUsersAssigned.ToString());

            model.SubItems.Add(assignedUsers.ToString());

            return model;
        }

        private ListViewItem itemListModel()
        {
            var model = new ListViewItem(title);
            
            model.SubItems.Add(totalRegisteredHours().ToString());
            
            var totalUsersAssigned = assignedUserIdentities.Count;
            model.SubItems.Add(totalUsersAssigned.ToString());

            return model;
        }

        private int totalRegisteredHours(string userName = null)
        {
            var totalHours = 0;
            if(userName != null)
            {
                foreach (var T in registeredTimeObjects)
                {
                    if (userName == T.UserName)
                        totalHours += T.Hours();
                }
            }
            else
            {
                foreach (var T in registeredTimeObjects)
                {
                    totalHours += T.Hours();
                }
            }
            return totalHours;
        }

        /*
         * Private methods section ends
         */

        private int sWeek;
        private int eWeek;
        private readonly HashSet<string> assignedUserIdentities = new HashSet<string>();
        private readonly List<TimeObject> registeredTimeObjects = new List<TimeObject>();
    }
}
