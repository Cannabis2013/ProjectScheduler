using System;
using System.Collections.Generic;
using System.IO;
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

        public UserModel CurrentUserLoggedIn()
        {
            return uManager.loggedIn();
        }

        public ListViewItem[] UserListModels()
        {
            if(uManager.isAdmin())
                return uManager.ItemModels(true);
            else
                return uManager.ItemModels();
        }

        public string AddProject(string ProjectTitel, string projectLeaderIdentity, DateTime startDate, DateTime endDate,
            string shortDescription)
        {
            if (!uManager.isAdmin())
                return "Admin privligges required.";
            var newProject = new ProjectModel();
            newProject.ModelIdentity = ProjectTitel;
            newProject.projectLeaderId = projectLeaderIdentity;
            newProject.StartDate = startDate;
            newProject.EndDate = endDate;
            newProject.ShortDescription = shortDescription;

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

        public string AddActivity(string projectId, string id, string[] assignedUsers, DateTime startDate, DateTime endDate)
        {
            var project = pManager.Model(projectId);
            var pUser = ((ProjectModel)project).projectLeaderId;
            if (uManager.loggedIn().ModelIdentity == pUser)
            {
                var activityModel = new ActivityModel(id,project,startDate,endDate,assignedUsers);
                return "";
            }
            else
                return "Only users which is project leader of the given project is allowed to create activities."
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

        public List<ActivityModel> Activities()
        {
            return uManager.isAdmin() ? pManager.ActivityModels() : 
                pManager.ActivityModels(uManager.loggedIn().ModelIdentity);
        }

        public List<ActivityModel> Activities(string userName)
        {
            return pManager.ActivityModels(userName);
        }

        public ListViewItem[] activityModels()
        {
            return uManager.isAdmin() ? pManager.ActivityItemModels(uManager) :
                pManager.ActivityItemModels(uManager.loggedIn().ModelIdentity);
        }

        public ListViewItem[] activityModels(string userName)
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

        public HourRegistrationModel HourRegistrationModel(string projectId, string activityId, string regId)
        {
            return pManager.getHourRegistrationModel(projectId, activityId, regId);
        }

        public ListViewItem[] HourRegistrationItemModels()
        {
            if (uManager.isAdmin())
                return pManager.RegistrationItemModels();

            return pManager.RegistrationItemModels(uManager.loggedIn().ModelIdentity);
        }

        public ListViewItem[] HourRegistrationModel(string userName)
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