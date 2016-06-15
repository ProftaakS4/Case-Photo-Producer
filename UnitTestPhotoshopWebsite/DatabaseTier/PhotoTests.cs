using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.DatabaseTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoshopWebsite.Enumeration;
using System.Data;

namespace PhotoshopWebsite.DatabaseTier.Tests
{
    [TestClass()]
    public class PhotoTests
    {
        Photo photo = new Photo();
        private DatabaseTier.QueryDatabase database = new DatabaseTier.QueryDatabase();

        [TestMethod()]
        public void getPhotosUserTest()
        {

            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_id", new string[] { "int", 4.ToString() });
            DataTable dt = database.CallProcedure("getPhotosUser", parameters);
            Assert.IsNotNull(dt);
        }

        [TestMethod()]
        public void getPhotoTest()
        {
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_photo_ID", new string[] { "int", 4.ToString() });
            DataTable dt = database.CallProcedure("getPhoto", parameters);
            Assert.IsNotNull(dt);
        }
    }
}