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

        bool IsAdmin();

        UserModel CurrentUserLoggedIn();

        List<string> UserNames();

        ListViewItem[] UserListModels(bool IncludeAdmin);

        // Project section

        string AddProject(ProjectModel newProject);
        string RemoveProject(int index);
        string RemoveProject(string identity);
        void RemoveProject(ProjectModel project);

        ProjectModel Project(int index);
        ProjectModel Project(string identity);

        ListViewItem[] ProjectItemModels();
        ListViewItem[] ProjectItemModels(string UserIdentity);

        // Activities
        
        void RemoveActivity(string projectid, string activityId);

        ActivityModel Activity(string projectId, string activityId);
        ActivityModel Activity(string activityId);
        List<ActivityModel> Activities();
        List<ActivityModel> Activities(string userName);

        ListViewItem[] activityItemModels();
        ListViewItem[] activityItemModels(string userName);

        // Hour registrations

        void RegisterHour(string projectId, string activityId,
            string regId, int hours, string shortDescription);

        void UnRegisterHour(string projectId, string activityId, string regId);

        HourRegistrationModel HourRegistrationModel(string activityId, string regId);
        ListViewItem[] HourRegistrationItemModels();
        ListViewItem[] HourRegistrationItemModels(string userNames);

        // Observer

        void SubScribe(ICustomObserver observer);
        void UnSubScribe(ICustomObserver observer);
        void UnSubScribeAll();
    }
}