using MainDomain;
using NUnit.Framework;

namespace Tests
{
    public class UserTests
    {
        private MainApp testApp = new MainApp();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UserLoginSuccesfully()
        {
            bool success = testApp.Login("admin", "1234");
            Assert.AreEqual(true, success);
        }

        [Test]
        public void UserLoginAndIsAdmin()
        {
            bool success = testApp.Login("admin", "1234");
            Assert.AreEqual(true, success);
            Assert.IsTrue(testApp.IsAdmin());
        }
    }
}