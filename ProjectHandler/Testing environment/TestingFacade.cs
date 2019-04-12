using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainDomain;
using NUnit.Framework;
using ProjectRelated;
using UserDomain;

namespace TestingFace
{
    public class ApplicationTestingFacade
    {
        public ProjectManager pManager;
        public UserManager uManager;
        public  ProjectView pView;

        public ApplicationTestingFacade()
        {
            pManager = new ProjectManager();
            uManager = new UserManager();
        }

        public bool LoginUser(string uName, string pass)
        {
            return uManager.logIn(uName, pass);
        }

        public void LaunchProjectView()
        {
            pView = new ProjectView(pManager,uManager);
        }
    }
}

namespace MainDomain
{
    public partial class ProjectManagement
    {
        public void test()
        {
            
        }
    }

}
