using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectNameSpace;
using Templates;
using VirtualUserDomain;


/*
 * Class Activity has the following important attributes:
 * - Activity identification
 * - Interval weeks like start - and end weeks
 * - Number of assigned users
 * - Total hours registered by all or a single user
 * - A reference to its parent project
 * - A list of assigned TimeObject references to provide an overview of hour registration by the various users
 * - A list of assigned users which is identified by their username attribute
 *
 * More technically it has the following methods described by pseudo:
 * - Id (which in this case gets its value by the inherited field variable 't')
 * - Start and end weeks
 * - Total registered hours(user = null) : if user = null -> Total registered hours by all users,
 *      else total registered hours by the given user
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

        public Activity(string title, int sWeek, int eWeek, List<string> assignedUserIdentities = null, Project parent = null)
        {
            t = title;
            this.p = null;
            this.sWeek = sWeek;
            this.eWeek = eWeek;
            this.assignedUserIdentities = assignedUserIdentities;
        }

        public Activity(string title, int sWeek, int eWeek, Project parent = null)
        {
            t = title;
            this.p = null;
            this.sWeek = sWeek;
            this.eWeek = eWeek;
        }

        public Activity(int sWeek, int eWeek, Project parent = null)
        {
            this.sWeek = sWeek;
            this.eWeek = eWeek;
            this.p = null;
        }

        /*
         * Copy constructor which takes an argument as same type.
         */

        public Activity(Activity copy)
        {
            p = null;
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
         * - get activityId and interval weeks
         * - Assign users to activity
         * - Register hour to activity
         * - Retrieve list of useridentities
         * - Clear assigned useridentities
         * - Retrieve item models
         * -- Retrieve item models for key values overview presentation
         * -- Retrieve item models for assigned users overview presentation
         */


        public string Id
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

        public int estimatedDuration() => eWeek - sWeek;

        /*
         * Assign users to activity
         */

        public void assignUser(string userID)
        {
            UserManager.user(userID).assignToActivity(this);
            assignedUserIdentities.Add(userID);
        }

        public void assignUsers(List<string> userIDs)
        {
            foreach (var userId in userIDs)
            {
                UserManager.user(userId).assignToActivity(this);
                assignedUserIdentities.Add(userId);
            }
        }

        public bool isUserAssigned()
        {
            var userName = UserManager.currentlyLoggedIn().id;
            return assignedUserIdentities.Any(item => item == userName);
        }

        public bool isUserAssigned(string userName)
        {
            return assignedUserIdentities.Any(item => item == userName);
        }

        /*
         * Retrieve a list of assigned usernames
         */

        public List<string> assignedUsers() => assignedUserIdentities;

        /*
         * Clear the assignedUserIdentities list
         */

        public void clearAssignedUserIdentities()
        {
            foreach (var userIdentity in assignedUserIdentities)
                UserManager.user(userIdentity).unAssignFromActivity(this);

            assignedUserIdentities.Clear();
        }

        /*
         * Register hours
         */

        public void addTimeObject(TimeObject time) => registeredTimeObjects.Add(time);

        /*
         * Get total hours registered by a given user
         */

        public int totalRegisteredHours(string userName = null)
        {
            var totalHours = 0;
            if (userName != null)
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
         * ItemModel section
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

        public ListViewItem[] registeredHourItemModels()
        {

            var tObjects = registeredTimeObjects.ToArray();

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

        public TreeNode assignedUserModels()
        {
            var rootNode = new TreeNode(id);

            foreach (var userName in assignedUserIdentities)
                rootNode.Nodes.Add(userName);

            return rootNode;
        }

        /*
         * Get parent project
         * Note: If copy, then Parent returns a null reference
         */

        public Project parent
        {
            get => p;
            set => p = value;
        }

        /*
         * Private methods section begins
         */

        private ListViewItem itemTileModel()
        {
            var model = new ListViewItem(id);

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

        /*
         * Activity item model
         * - Id
         * - Start week
         * - End week
         * - Total registered hours
         * - Number of assigned users
         * - Parent project
         */

        private ListViewItem itemListModel()
        {
            var model = new ListViewItem(id);

            model.SubItems.Add(startWeek.ToString());
            model.SubItems.Add(endWeek.ToString());

            model.SubItems.Add(totalRegisteredHours().ToString());
            
            var totalUsersAssigned = assignedUserIdentities.Count;
            model.SubItems.Add(totalUsersAssigned.ToString());

            model.SubItems.Add(p.id);

            return model;
        }


        /*
         * Private methods section ends
         */

        private int sWeek;
        private int eWeek;
        private readonly List<string> assignedUserIdentities = new List<string>();
        private readonly List<TimeObject> registeredTimeObjects = new List<TimeObject>();
        private Project p;
    }
}
