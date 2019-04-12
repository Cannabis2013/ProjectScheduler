using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Projecthandler;
using TestingFace;
namespace TestProject
{
    
    [Binding]
    public class UserLoginSteps
    {
        ApplicationTestingFacade testApp = new ApplicationTestingFacade();
        private string uName, pass;

        [Given]
        public void GivenTheUserEnterHisUsername_P0_AndPassword_P1(string p0,string p1)
        {
            uName = p0;
            pass = p1;
        }
        
        [Given]
        public void GivenThatTheUserLogsInSuccesfullyWithUsername_P0_AndPassword_P1(string p0,string p1)
        {
            Assert.AreEqual(true,testApp.LoginUser(p0,p1));
        }
        
        [Given]
        public void GivenThatTheUsersUsernameIsAdminWithThePassword_P0(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given]
        public void GivenHeAccidentiallyWasDrunkAndEntersTheRightUsernameButTheWrongPassword_P0(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then]
        public void ThenHeLogsInSuccesfully()
        {
            Assert.AreEqual(true, testApp.LoginUser(uName, pass));
        }
        
        [Then]
        public void ThenTheUsersRoleIsAdmin()
        {
            Assert.IsTrue(testApp.uManager.isAdmin());
        }
        
        [Then]
        public void ThenTheUserManagerReturnsAUserWhichIsNull()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
