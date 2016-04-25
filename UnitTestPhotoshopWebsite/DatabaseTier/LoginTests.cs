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
        
       

        [TestMethod()]
        public void loginUserTest()
        {
            bool result;
            result = login.loginUser("c.kleijnen@fontys.nl", "carlikleijnen");
            Assert.IsTrue(result);
            result = login.loginUser("c.kleijnen@fontys.nl", "nopassword");
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void getUserIDTest()
        {
            int result = login.getUserID("c.kleijnen@fontys.nl");
            Assert.AreEqual(4, result);
            result = login.getUserID("cgstyle@gmail.com");
            Assert.AreEqual(-1, result);            
        }

        [TestMethod()]
        public void getUserDataTest()
        {
            Dictionary<string, string> result = login.getUserData(4);
            Assert.IsNotNull(result);            
        }
    }
}