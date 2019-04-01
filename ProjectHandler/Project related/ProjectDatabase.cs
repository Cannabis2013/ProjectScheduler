using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProjectNameSpace
{
    class ProjectDatabase
    {
        public void addProject(Project p) => projects.Add(p);

        public ListViewItem[] projectItemModels()
        {
            int count = projects.Count, index = 0;
            var models = new ListViewItem[count];

            foreach (var p in projects)
            {
                var model = new ListViewItem(p.projectId);

                var startDate = new StringBuilder("Week begin: ");
                startDate.Append(p.startWeek);
                
                model.SubItems.Add(startDate.ToString());

                var endDate = new StringBuilder("Week end: ");
                endDate.Append(p.endWeek);
                model.SubItems.Add(endDate.ToString());

                var userLeader = new StringBuilder("Tech lead: ");
                userLeader.Append(p.projectLeaderId);

                model.SubItems.Add(userLeader.ToString());

                // Set picture index
                model.ImageIndex = 0;
                model.StateImageIndex = 0;

                models[index++] = model;
            }
            return models;
        }

        public Project projectAt(int index) => projects[index];

        private readonly List<Project> projects = new List<Project>();
    }
}