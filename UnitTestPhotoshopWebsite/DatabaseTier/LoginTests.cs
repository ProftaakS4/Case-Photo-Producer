using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.DatabaseTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhotoshopWebsite.DatabaseTier.Tests
{
    [TestClass()]
    public class LoginTests
    {
        private Login login = new Login();
        private DatabaseTier.QueryDatabase database = new DatabaseTier.QueryDatabase();
        
       

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
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_emailaddress", new string[] { "string", "c.kleijnen@fontys.nl" });
            DataTable dt = database.CallProcedure("getUserID", parameters);
            int result = int.Parse(dt.Rows[0][0].ToString());
            Assert.AreEqual(4, result);
            parameters = new Dictionary<string, string[]>();
            parameters.Add("p_emailaddress", new string[] { "string", "cgstyle@gmail.com" });
            dt = database.CallProcedure("getUserID", parameters);
            result = int.Parse(dt.Rows[0][0].ToString());
            Assert.AreEqual(-1, result);            
        }

        [TestMethod()]
        public void getUserDataTest()
        {
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_id", new string[] { "int", 4.ToString() });
            DataTable dt = database.CallProcedure("getUserInformation", parameters);
            Assert.IsNotNull(dt);            
        }
    }
}