using Projecthandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectNameSpace
{
    public class ProjectManager : IItemModel<ProjectItemModel>
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

        public List<ProjectItemModel> itemModelList()
        {
            throw new NotImplementedException();
        }

        private readonly ProjectDatabase projectDB = new ProjectDatabase();
    }

    class ProjectDatabase
    {
        internal List<Project> projects = new List<Project>();
    }
}
