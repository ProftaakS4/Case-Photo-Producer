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
    public class SearchTests
    {
        [TestMethod()]
        public void searchPhotoTest()
        {
            QueryDatabase database = new QueryDatabase();
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_user_ID", new string[] { "int", "4" });
            parameters.Add("p_searchedText", new string[] { "string", "carli" });
            DataTable dt = database.CallProcedure("searchPhoto", parameters);
            Assert.IsNotNull(dt);

        }
    }
}