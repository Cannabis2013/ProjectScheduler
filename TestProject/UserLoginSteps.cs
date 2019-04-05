using System;
using TechTalk.SpecFlow;
using Projecthandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Projecthandler.Application_facade;
using Projecthandler.User_Management;

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



        User.UserRole role;
        private string uName, pass;
        private MainApp app;
    }
}
