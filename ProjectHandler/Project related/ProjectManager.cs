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
                projectLeaderId = "Finn_Luger_P38"
            };
            projectDb.addProject(p);
        }

        /*
         * Public methods section begins
         * - Add projects
         * - Remove projects
         * - Get activities
         */

        public void addProject(Project newProject)
        {
            projectDb.addProject(newProject);
        }

        public void removeProjectAt(int index) => projectDb.removeAt(index);
        public void removeProject(Project p) => projectDb.remove(p);

        public Project projectAt(int index) => projectDb.projectAt(index);
        public Project project(string projectId) => projectDb.project(projectId);
        public List<Project> projects() => projectDb.allProjects();

        /*
         * Item models section begins
         */

        public ListViewItem[] projectItemModels(ItemModelEntity<ListViewItem>.ListMode mode) => projectDb.projectItemModels(mode) ?? throw new ArgumentNullException("No items to pass.");

        public ListViewItem[] registeredUserHourModels(string userName = null)
        {
            var activities = new List<ListViewItem>();
            if(userName != null)
            {
                foreach (var p in projectDb.allProjects())
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
                foreach (var p in projectDb.allProjects())
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

        public ListViewItem[] projectActivityItemModels()
        {
            var models = new List<ListViewItem>();
            if (UserManager.verifyUserState(UserManager.getLocalAddress()) == User.UserRole.Admin)
            {
                foreach (var p in projectDb.allProjects())
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
            foreach (var p in projectDb.allProjects())
            {
                foreach (var activity in p.allActivities())
                {
                    if(!activity.isUserAssigned() || p.projectLeaderId == userId)
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

        private readonly ProjectDatabase projectDb = new ProjectDatabase();
    }
}
