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
        private Login login = new Login();
        private bool result;

        [TestMethod()]
        public void loginUserTest()
        {            
            result = login.loginUser("c.kleijnen@fontys.nl", "carlikleijnen");
            //Assert.IsTrue(result);
            Assert.Equals(true, result);
            
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