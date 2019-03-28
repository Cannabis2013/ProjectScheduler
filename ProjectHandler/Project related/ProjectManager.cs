using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private readonly ProjectDatabase projectDB = new ProjectDatabase();
    }

    class ProjectDatabase
    {
        internal List<Project> projects = new List<Project>();
    }
}
