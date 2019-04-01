using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Templates;

namespace ProjectNameSpace
{
    class ProjectDatabase
    {
        public void addProject(Project p) => projects.Add(p);

        public void removeAt(int index) => projects.RemoveAt(index);
        public void remove(Project p) => projects.Remove(p);

        public ListViewItem[] projectItemModels(ItemModelEntity<ListViewItem>.ListMode mode)
        {
            int count = projects.Count, index = 0;
            var models = new ListViewItem[count];

            foreach (var p in projects)
                models[index++] = p.itemModel(mode);

            return models;
        }

        public Project projectAt(int index) => projects[index];

        private readonly List<Project> projects = new List<Project>();
    }
}