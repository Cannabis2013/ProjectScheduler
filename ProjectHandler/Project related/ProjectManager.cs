using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NUnit.Framework;
using Templates;
using VirtualUserDomain;

namespace ProjectNameSpace
{

    /*
     * ProjectManager
     * - Add/create project
     * - Edit projects
     * - Remove Projects
     * - Add activity to project
     * - Project manipulation
     * -- Add activity to a given project
     * -- Delete activity within a given project
     * -- Register work hour to a given activity in a given project
     * - Retrieve project activities
     */

    public class ProjectManager
    {
        public ProjectManager()
        {
            var p = new Project("Project TEST")
            {
                startWeek = 1,
                endWeek = 4,
                projectLeaderId = "Finn_Luger"
            };
            projects.Add(p);
        }

        /*
         * Public methods section begins
         * - Add projects
         * - Remove projects
         * - Get activities
         */

        public void addProject(Project p)
        {
            projects.Add(p);
        }

        public void removeProjectAt(int index) => projects.RemoveAt(index);
        public void removeProject(Project p) => projects.Remove(p);

        public Project projectAt(int index) => projects.ElementAt(index);
        public Project project(Project p) => projects.Find(item => item.id == p.id);
        public Project project(string projectId) => projects.Find(item => item.id == projectId);
        public List<string> allProjectIdentities() => projects.Select(item => item.id).ToList();
        public List<string> allProjectIdentities(string projectLeaderId) => 
            projects.Where(item => item.projectLeaderId == projectLeaderId).Select(item => item.id).ToList();


        /*
         * Item models section begins
         * - Returns a list of Project Item models with the following columndata:
         * -- ProjectId
         * -- Projectleader username
         * -- Start and estimated end week
         */

        public ListViewItem[] projectItemModels(ItemModelEntity<ListViewItem>.ListMode mode)
        {
            int count = projects.Count, index = 0;
            var models = new ListViewItem[count];

            foreach (var p in projects)
                models[index++] = p.itemModel(mode);

            return models;
        }

        /*
         * Models all the user time registration for visual representation in a ListView
         */

        public ListViewItem[] registeredUserHourModels(string userName = null)
        {
            var activities = new List<ListViewItem>();
            if(userName != null)
            {
                foreach (var p in projects)
                {
                    foreach (var activity in p.allActivities())
                    {
                        var models = activity.registeredHourItemModels(userName).ToList();
                        activities.AddRange(models);
                    }
                }
            }
            else
            {
                foreach (var p in projects)
                {
                    foreach (var activity in p.allActivities())
                    {
                        var models = activity.registeredHourItemModels().ToList();
                        activities.AddRange(models);
                    }
                }
            }

            return activities.ToArray();
        }

        /*
         * Returns a list of Activity item models where each item has the following column data:
         * - Id
         * - Start week
         * - End week
         * - Total registered hours
         * - Number of assigned users
         * - Parent project
         *
         * Furthermore there is some access restrictions:
         * - Admin can retrieve all activities
         * - Projectleaders has access to activities in their corresponding project
         * - Employees has no access.
         */

        public ListViewItem[] projectActivityItemModels()
        {
            var models = new List<ListViewItem>();
            if (UserManager.verifyUserState() == User.UserRole.Admin)
            {
                foreach (var p in projects)
                {
                    foreach (var activity in p.allActivities())
                    {
                        var model = activity.itemModel(ItemModelEntity<ListViewItem>.ListMode.List);
                        models.Add(model);
                    }
                }

                return models.ToArray();
            }

            var userId = UserManager.currentlyLoggedIn().id;

            foreach (var p in projects)
            {
                foreach (var activity in p.allActivities())
                {
                    if(!activity.isUserAssigned() && p.projectLeaderId != userId)
                        continue;

                    var model = activity.itemModel(ItemModelEntity<ListViewItem>.ListMode.List);
                    models.Add(model);
                }
            }
            return models.ToArray();
        }
        
        /*
         * Item models section ends
         */
        private readonly List<Project> projects = new List<Project>();
    }
}
