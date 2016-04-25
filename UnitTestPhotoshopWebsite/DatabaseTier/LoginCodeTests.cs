using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.DatabaseTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System;

namespace PhotoshopWebsite.DatabaseTier.Tests
{
    [TestClass()]
    public class LoginCodeTests
    {
        private LoginCode login = new LoginCode();

        [TestMethod()]
        public void getLoginCodeDataTest()
        {
            DataTable getLoginCodeData = login.getLoginCodeData(1);
            Assert.IsNotNull(getLoginCodeData);
        }
    }
}