using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Projecthandler.Abstract_classes_and_interfaces;
using Projecthandler.Class_forms;
using Projecthandler.Custom_events;
using ProjectRelated;
using UserDomain;


namespace MainDomain
{
    public partial class MainApp : IApplicationProgrammableInterface
    {
        private const string FileName = "ProjectFile";
        private ProjectManager pManager;
        private readonly UserManager uManager = new UserManager();

        public MainApp()
        {
            ReadPersistenceData();
        }

        public void WritePersistence()
        {
            Stream saveFileStream = File.Create(FileName);
            var serializer = new BinaryFormatter();
            serializer.Serialize(saveFileStream, pManager);
            saveFileStream.Close();
        }

        private void ReadPersistenceData()
        {
            if (File.Exists(FileName))
            {
                Stream openFileStream = File.OpenRead("ProjectFile");
                var deserializer = new BinaryFormatter();
                try
                {
                    pManager = (ProjectManager)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
                }
                catch (Exception)
                {
                    openFileStream.Close();
                    File.Delete(FileName);
                    pManager = new ProjectManager();
                }
            }
            else
                pManager = new ProjectManager();
        }

        public bool Login(string userName, string password)
        {
            return uManager.logIn(userName, password);
        }

        public void Logut()
        {
            uManager.logout();
        }

        public bool IsAdmin()
        {
            return uManager.isAdmin();
        }

        public UserModel CurrentUserLoggedIn()
        {
            return uManager.loggedIn();
        }

        public List<string> UserNames()
        {
            return uManager.ListModelIdentities();
        }

        public ListViewItem[] UserListModels(bool IncludeAdmin)
        {
            if(uManager.isAdmin())
                return uManager.ItemModels(IncludeAdmin);
            else
                return uManager.ItemModels();
        }

        public string UserAvailability(string username, DateTime sDate, DateTime eDate)
        {
            return pManager.UserAvailability(username, uManager, sDate, eDate);

        }

        public string AddProject(ProjectModel newProject)
        {
            if (!uManager.isAdmin())
                return "Admin privligges required.";

            pManager.AddModel(newProject);

            return "";
        }

        public string RemoveProject(int index)
        {
            if (!uManager.isAdmin())
                return "Admin privilliges required";

            pManager.RemoveModelAt(index);

            return "";
        }

        public string RemoveProject(string identity)
        {
            if (!uManager.isAdmin())
                return "Admin privilliges required";
            
            pManager.RemoveModel(identity);

            return "";
        }

        public void RemoveProject(ProjectModel project)
        {
            throw new NotImplementedException();
        }

        public ProjectModel Project(int index)
        {
            return (ProjectModel) pManager.ModelAt(index);
        }

        public ProjectModel Project(string identity)
        {
            return (ProjectModel) pManager.Model(identity);
        }

        public ListViewItem[] ProjectItemModels()
        {
            return pManager.ProjectItemModels();
        }

        public ListViewItem[] ProjectItemModels(string UserIdentity)
        {
            var projects = pManager.Models.Where(item => ((ProjectModel) item).projectLeaderId == UserIdentity);
            return projects.Select(item => item.ItemModel()).ToArray();
        }

        public void RemoveActivity(string projectId, string activityId)
        {
            var project = pManager.Model(projectId);
            var pUser = ((ProjectModel)project).projectLeaderId;
            if (((ProjectModel) project).projectLeaderId == pUser)
            {
                pManager.RemoveActivityModel(projectId, activityId);
            }
        }

        public ActivityModel Activity(string projectId, string activityId)
        {
            return pManager.ActivityModelById(projectId, activityId);
        }

        public ActivityModel Activity(string activityId)
        {
            return pManager.ActivityModels().FirstOrDefault(item => item.ModelIdentity == activityId);
        }

        public List<ActivityModel> Activities()
        {
            return uManager.isAdmin() ? pManager.ActivityModels() : 
                pManager.ActivityModels(uManager.loggedIn().ModelIdentity);
        }

        public List<ActivityModel> Activities(string userName)
        {
            return pManager.ActivityModels(userName);
        }

        public ListViewItem[] activityItemModels()
        {
            return uManager.isAdmin() ? pManager.ActivityItemModels(uManager) :
                pManager.ActivityItemModels(uManager.loggedIn().ModelIdentity);
        }

        public ListViewItem[] activityItemModels(string userName)
        {
            return pManager.ActivityItemModels(userName);
        }

        public void RegisterHour(string projectId, string activityId, string regId, int hours, string shortDescription)
        {
            var userId = uManager.loggedIn().ModelIdentity;
            var parentActivity = pManager.ActivityModelById(projectId, activityId);
            var rObject = new HourRegistrationModel(regId,
                hours,
                userId,
                shortDescription, 
                parentActivity);
        }

        public void UnRegisterHour(string projectId, string activityId, string regId)
        {
            pManager.UnRegisterHour(projectId,activityId,regId);
        }

        public HourRegistrationModel HourRegistrationModel(string activityId, string regId)
        {
            var activity = pManager.ActivityModels().FirstOrDefault(item => item.ModelIdentity == activityId);
            var ProjectId = activity.ParentModelIdentity();
            return pManager.getHourRegistrationModel(ProjectId, activityId, regId);
        }

        public ListViewItem[] HourRegistrationItemModels()
        {
            if (uManager.isAdmin())
                return pManager.RegistrationItemModels();

            return pManager.RegistrationItemModels(uManager.loggedIn().ModelIdentity);
        }

        public ListViewItem[] HourRegistrationItemModels(string userName)
        {
            return pManager.RegistrationItemModels(userName);
        }

        public void SubScribe(ICustomObserver observer)
        {
            pManager.SubScribe(observer);
        }

        public void UnSubScribe(ICustomObserver observer)
        {
            pManager.UnSubScribe(observer);
        }

        public void UnSubScribeAll()
        {
            pManager.UnSubScribeAll();
        }
    }
}