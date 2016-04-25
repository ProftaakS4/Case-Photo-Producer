using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.DatabaseTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoshopWebsite.DatabaseTier.Tests
{
    [TestClass()]
    public class LoginTests
    {
        //private Login login = new Login();

        [TestMethod()]
        public void loginUserTest()
        {
            DatabaseTier.Login login = new DatabaseTier.Login();
            bool result = login.loginUser("c.kleijnen@fontys.nl", "carlikleijnen");
            Assert.IsTrue(result);
            
        }

        [TestMethod()]
        public void getUserIDTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void getUserDataTest()
        {
            Assert.Fail();
        }
    }
}