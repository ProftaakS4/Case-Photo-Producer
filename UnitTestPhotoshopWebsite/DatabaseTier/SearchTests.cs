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
    public class SearchTests
    {
        Search search = new Search();

        [TestMethod()]
        public void searchPhotoTest()
        {
            string searchPhoto = search.searchPhoto("carli", 4);
            Assert.IsNotNull(searchPhoto);
           
        }
    }
}