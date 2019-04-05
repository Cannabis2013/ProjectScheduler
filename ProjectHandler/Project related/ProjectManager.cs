using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Projecthandler.Templates;
using Projecthandler.User_Management;

namespace Projecthandler.Project_related
{

    [Serializable]
    public class ProjectManager
    {

        private readonly List<Project> projects = new List<Project>();       

        public void AddProject(Project p)
        {
            projects.Add(p);
        }

        public void RemoveProjectAt(int index)
        {
            projects.RemoveAt(index);
        }

        public Project ProjectAt(int index)
        {
            return projects.ElementAt(index);
        }

        public Project Project(string projectId)
        {
            return projects.Find(item => item.Id == projectId);
        }

        public List<string> AllProjectIdentities()
        {
            return projects.Select(item => item.Id).ToList();
        }

        public List<string> AllProjectIdentities(string projectLeaderId)
        {
            return projects.Where(item => item.ProjectLeaderId == projectLeaderId).Select(item => item.Id).ToList();
        }

        public List<Activity> Activities(string userName)
        {
            var resultingList = new List<Activity>();
            foreach (var p in projects)
            {
                var userActivities = p.AssignedActivities(userName);
                resultingList.AddRange(userActivities);
            }

            return resultingList;
        }

        public ListViewItem[] ProjectItemModels(ItemModelEntity<ListViewItem>.ListMode mode)
        {
            int count = projects.Count, index = 0;
            var models = new ListViewItem[count];

            foreach (var p in projects)
                models[index++] = p.ItemModel(mode);

            return models;
        }

        public ListViewItem[] ProjectActivityItemModels()
        {
            var models = new List<ListViewItem>();
            if (UserManager.VerifyUserState() == User.UserRole.Admin)
            {
                foreach (var p in projects)
                foreach (var activity in p.AllActivities())
                {
                    var model = activity.ItemModel(ItemModelEntity<ListViewItem>.ListMode.List);
                    models.Add(model);
                }

                return models.ToArray();
            }

            var userId = UserManager.CurrentlyLoggedIn().UserName();

            foreach (var p in projects)
            foreach (var activity in p.AllActivities())
            {
                if (!activity.IsUserAssigned() && p.ProjectLeaderId != userId)
                    continue;

                var model = activity.ItemModel(ItemModelEntity<ListViewItem>.ListMode.List);
                models.Add(model);
            }

            return models.ToArray();
        }

        public ListViewItem[] UserAssignedActivityModels()
        {
            var userName = UserManager.CurrentlyLoggedIn().UserName();
            var assignedActivities = Activities(userName);
            var count = assignedActivities.Count;
            var models = new ListViewItem[count];
            var index = 0;

            foreach (var activity in assignedActivities)
            {
                var model = new ListViewItem(activity.Id);
                model.SubItems.Add(activity.EstimatedDuration().ToString());
                model.SubItems.Add(activity.TotalRegisteredHours(userName).ToString());
                var projectId = activity.ParentProjectId;
                model.SubItems.Add(projectId);

                models[index++] = model;
            }

            return models;
        }

        public IEnumerable<ActivityEntity> UserActivityEntities(string userName)
        {
            return Activities(userName).Select(item =>
                new ActivityEntity(item.StartWeek, item.EndWeek, item.Id)).ToList();
        }
    }
}