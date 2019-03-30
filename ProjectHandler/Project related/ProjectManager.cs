using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using VirtualUserDomain;

namespace ProjectNameSpace
{

    /*
     * ProjectManager
     * - Add/create project
     * - Edit projects
     * - Remove Projects
     * - Project manipulation
     * -- Add activity to a given project
     * -- Delete activity within a given project
     * -- Register work hour to a given activity in a given project
     * - Retrieve project activities
     * 
     * ProjectDatabase
     * - Contains the projects
     * - Create item model lists for projects
     * 
     * Project
     * - Fields and properties related to the project like:
     * -- ProjectID
     * -- Start and estimated end date on a weekly basis
     * -- Project leader user ID
     * -- A list of activities
     */

    public class ProjectManager
    {
        public ProjectManager()
        {
            Project p = new Project("Project TEST");
            p.StartWeek = 1;
            p.estimatedEndWeek = 4;
            p.projectLeaderID = "Niels_Henrik";
            projectDB.addProject(p);
        }

        public void addProject(Project newProject)
        {
            projectDB.addProject(newProject);
        }

        public Project project(int index) => projectDB.projectAt(index);

        public ListViewItem[] projectItemModels() => projectDB.projectItemModels() ?? throw new ArgumentNullException("No items to pass.");


        private readonly ProjectDatabase projectDB = new ProjectDatabase();
    }

    class ProjectDatabase
    {
        public void addProject(Project p) => projects.Add(p);

        public ListViewItem[] projectItemModels()
        {
            int count = projects.Count;
            ListViewItem[] models = new ListViewItem[count];
            int index = 0;
            foreach (Project p in projects)
            {
                ListViewItem model = new ListViewItem(p.ProjectID);

                StringBuilder startDate = new StringBuilder("Week begin: ");
                startDate.Append(p.StartWeek);
                model.SubItems.Add(startDate.ToString());

                StringBuilder endDate = new StringBuilder("Week end: ");
                endDate.Append(p.estimatedEndWeek);
                model.SubItems.Add(endDate.ToString());

                StringBuilder userLeader = new StringBuilder("Tech lead: ");
                userLeader.Append(p.projectLeaderID);

                model.SubItems.Add(userLeader.ToString());

                // Set picture index
                model.ImageIndex = 0;
                model.StateImageIndex = 0;

                models[index++] = model; ;
            }
            return models;
        }

        public Project projectAt(int index) => projects[index];

        private List<Project> projects = new List<Project>();
    }

    /*
     * Project class
     */

    public class Project
    {
        public Project(string projectID, HashSet<string> assignedUsers)
        {
            ProjectID = projectID ?? throw new ArgumentNullException(nameof(projectID));
            assignedUserIdentities = assignedUsers;
        }

        public Project(string projectID)
        {
            ProjectID = projectID ?? throw new ArgumentNullException(nameof(projectID));
        }

        public ListViewItem[] activityItemModels()
        {
            int count = projectActivities.Count, index = 0;
            ListViewItem[] models = new ListViewItem[count];
            foreach (Activity a in projectActivities)
            {
                models[index] = a.activityItemModel();
                index++;
            }
            return models;
        }

        public Activity activity(int index)
        {
            int i = 0;
            foreach(Activity a in projectActivities)
            {
                if (i++ == index)
                    return a;
            }
            return null;
        }

        public void assignUserToProject(string userID) => assignedUserIdentities.Add(userID);
        public void addActivity(Activity a) => projectActivities.AddLast(a);
        public int estimatedDuration() => estimatedEndWeek - StartWeek;
        
        /*
         * Public fields section
         */

        public string ProjectID { get; set; }
        public int StartWeek { get; set; }
        public int estimatedEndWeek { get; set; }
        public string projectLeaderID { get; set; }

        internal HashSet<string> assignedUserIdentities = new HashSet<string>();
        internal LinkedList<Activity> projectActivities = new LinkedList<Activity>();
    }
}
