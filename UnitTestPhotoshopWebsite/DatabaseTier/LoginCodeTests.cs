using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.DatabaseTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PhotoshopWebsite.DatabaseTier.Tests
{
    [TestClass()]
    public class LoginCodeTests
    {
        private DatabaseTier.QueryDatabase database = new DatabaseTier.QueryDatabase();

        [TestMethod()]
        public void getLoginCodeDataTest()
        {
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_photographer_ID", new string[] { "int", 1.ToString() });
            DataTable dt = database.CallProcedure("getLoginCodesFromPhotographer", parameters);
            Assert.IsNotNull(dt);
        }
    }
}