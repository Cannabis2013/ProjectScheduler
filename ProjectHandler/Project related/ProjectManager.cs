using System;
using System.Windows.Forms;

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
            var p = new Project("Project TEST");
            p.startWeek = 1;
            p.estimatedEndWeek = 4;
            p.projectLeaderId = "Niels_Henrik";
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
}
