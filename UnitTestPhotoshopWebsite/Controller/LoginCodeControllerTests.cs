using System;
using PhotoshopWebsite.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestPhotoshopWebsite.Controller
{
    [TestClass]
    public class LoginCodeControllerTests
    {
        [TestMethod]
        public void TestgetLoginCodeData()
        {
            LoginCodeController lc;
            lc = new LoginCodeController(1);
            var result = lc.loginCodes;
            var expresult = 1;
            Assert.AreEqual(expresult, result[0].ID);

            lc = new LoginCodeController(20000);
            result = lc.loginCodes;
            expresult = 0;
            Assert.AreEqual(expresult, result.Count);
        }
    }
}
