using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ProjectRelated;
using UserDomain;

namespace Projecthandler.Abstract_classes_and_interfaces
{
    public interface IApplicationProgrammableInterface
    {
        // User section
        bool Login(string userName, string password);
        void Logut();

        UserModel CurrentUserLoggedIn();

        ListViewItem[] UserListModels();

        // Project section

        string AddProject(string ProjectTitel, string projectLeaderIdentity,
            DateTime startDate,
            DateTime endDate, string shortDescription);
        string RemoveProject(int index);
        string RemoveProject(string identity);
        void RemoveProject(ProjectModel project);

        ProjectModel Project(int index);
        ProjectModel Project(string identity);

        ListViewItem[] ProjectItemModels();

        // Activities
        
        string AddActivity(string projectId, string id, string[] assignedUsers,
            DateTime startDate,
            DateTime endDate);
        void RemoveActivity(string projectid, string activityId);

        ActivityModel Activity(string projectId, string activityId);
        List<ActivityModel> Activities();
        List<ActivityModel> Activities(string userName);

        ListViewItem[] activityModels();
        ListViewItem[] activityModels(string userName);

        // Hour registrations

        void RegisterHour(string projectId, string activityId,
            string regId, int hours, string shortDescription);

        void UnRegisterHour(string projectId, string activityId, string regId);

        HourRegistrationModel HourRegistrationModel(string projectId, string activityId, string regId);
        ListViewItem[] HourRegistrationItemModels();
        ListViewItem[] HourRegistrationModel(string userNames);

        // Observer

        void SubScribe(ICustomObserver observer);
        void UnSubScribe(ICustomObserver observer);
        void UnSubScribeAll();
    }
}