using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoshopWebsite.Controller.Tests
{
    [TestClass()]
    public class UserTests
    {
        User user;

        [TestMethod()]
        public void loginUserTest()
        {
            user = new User("Carli", "Kleijnen", "Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test");

            User newUser = user.loginUser("c.kleijnen@fontys.nl", "carlikleijnen");
            Assert.AreEqual(newUser.Firstname, user.Firstname);

            newUser = user.loginUser("test@test.me", "password");
            Assert.IsNull(newUser);

        }

        [TestMethod()]
        public void getUserDataTest()
        {
            user = new User("Carli", "Kleijnen", "Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test");
            User newUser = user.getUserData("c.kleijnen@fontys.nl");
            Assert.AreEqual(user.Firstname, newUser.Firstname);

            newUser = user.getUserData("test@test.me");
            Assert.IsNull(newUser);
        }
    }
}