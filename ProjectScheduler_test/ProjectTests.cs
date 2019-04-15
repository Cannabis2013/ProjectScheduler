using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MainDomain;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ProjectRelated;

namespace Tests
{
    class ProjectTests
    {
        private MainApp testApp = new MainApp();
        
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Add_Project_As_Admin_Success()
        {
            testApp.Login("admin", "1234");
            var ProjectId = "Test project";
            var pLeaderId =
                testApp.UserNames().FirstOrDefault(item => item == "Finn_Luger");
            var ShortDescription = "Test project";
            var sDate = DateTime.Now;
            var eDate = new DateTime(2019, 6, 3);
            var newProject = new ProjectModel
            {
                ModelIdentity = ProjectId,
                projectLeaderId = pLeaderId,
                ShortDescription =  ShortDescription,
                StartDate = sDate,
                EndDate = eDate
            };

            testApp.AddProject(newProject);

            var project = testApp.Project(ProjectId);
            if (project == null)
                Assert.Fail();
            if(project.ModelIdentity != ProjectId)
                Assert.Fail();
            if(project.projectLeaderId != pLeaderId)
                Assert.Fail();
            if(project.StartDate != sDate)
                Assert.Fail();
            if(project.EndDate != eDate)
                Assert.Fail();

            Assert.Pass();
        }

        [Test]
        public void Add_Project_As_User_Fail()
        {
            testApp.Login("Finn_Luger", "hitler");
            var ProjectId = "Test project";
            var pLeaderId =
                testApp.UserNames().FirstOrDefault(item => item == "Finn_Luger");
            var ShortDescription = "Test project";
            var sDate = DateTime.Now;
            var eDate = new DateTime(2019, 6, 3);
            var newProject = new ProjectModel
            {
                ModelIdentity = ProjectId,
                projectLeaderId = pLeaderId,
                ShortDescription = ShortDescription,
                StartDate = sDate,
                EndDate = eDate
            };

            testApp.AddProject(newProject);

            var project = testApp.Project(ProjectId);
            if (project == null)
                Assert.Pass();
            if (project.ModelIdentity != ProjectId)
                Assert.Pass();
            if (project.projectLeaderId != pLeaderId)
                Assert.Pass();
            if (project.StartDate != sDate)
                Assert.Pass();
            if (project.EndDate != eDate)
                Assert.Pass();

            Assert.Fail();
        }
    }
}
