using Projecthandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualUserDomain;

namespace ProjectNameSpace
{
    public class ProjectManager
    {
        public ProjectManager()
        {
            projectDB.projects.Add(new Project("ssdf"));
        }

        public void addProject(Project newProject)
        {
            projectDB.projects.Add(newProject);
        }

        public Project project(int index)
        {
            return projectDB.projects[index];
        }

        public List<ListViewItem> projectItemModels() => projectDB.projectItemModels();
        

        private readonly ProjectDatabase projectDB = new ProjectDatabase();
    }

    class ProjectDatabase
    {
        internal List<ListViewItem> projectItemModels()
        {
            List<ListViewItem> models = new List<ListViewItem>();
            foreach (Project p in projects)
            {
                ListViewItem model = new ListViewItem(p.ProjectID);


                StringBuilder startDate = new StringBuilder();
                startDate.Append("Week: ");
                startDate.Append(p.StartWeek);
                model.SubItems.Add(startDate.ToString());

                StringBuilder endDate = new StringBuilder();
                endDate.Append("Week: ");
                endDate.Append(p.estimatedEndWeek);
                model.SubItems.Add(endDate.ToString());

                model.SubItems.Add(p.projectLeaderID);

                models.Add(model);
            }
            return models;
        }

        internal List<Project> projects = new List<Project>();
    }
}
