using System;
using TechTalk.SpecFlow;
using Projecthandler;
using MainUserSpace;
using VirtualUserDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace TestProject
{
    [Binding]
    public class UserLoginSteps
    {
        [Given]
        public void GivenThatTheUserroleIsAdmin()
        {
            role = User.UserRole.Admin;
        }
        
        [Given]
        public void GivenTheUsernameIs_P0_AndThePasswordIs_P1(string p0, string p1)
        {
            
            uName = p0;
            pass = p1;
        }
        
        [Then]
        public void ThenTheUserSuccesfullyLogsIn()
        {
            app = new MainApp(uName, pass);
            bool userLoginSuccesfully = app.uManager.logIn(uName, pass, UserManager.getLocalAddress());
            Assert.AreEqual(true, userLoginSuccesfully);
        }
        
        [Then]
        public void ThenTheUsersRoleIsAdmin()
        {
            User.UserRole uRole = app.uManager.verifyUserState(UserManager.getLocalAddress());
            Assert.AreEqual(role, uRole);
        }

        [Given]
        public void GivenThatTheUsersUsernameIs_P0_WithThePassword_P1(string p0, string p1)
        {
            uName = p0;
            pass = p1;
        }

        [Given]
        public void GivenHeAccidentiallyWasDrunkAndEntersTheRightUsernameButTheWrongPassword_P0(string p0)
        {
            pass = p0;
        }

        [Then]
        public void ThenTheUserManagerReturnsAUserWhichIsNull()
        {
            app = new MainApp(uName, pass);
            bool userLoginSuccesfully = app.uManager.logIn(uName, pass, UserManager.getLocalAddress());
            Assert.AreEqual(false, userLoginSuccesfully);
        }


        User.UserRole role;
        private string uName, pass;
        private MainApp app;
    }
}
