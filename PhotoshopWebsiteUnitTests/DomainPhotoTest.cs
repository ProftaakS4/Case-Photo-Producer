using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.Domain;

namespace PhotoshopWebsiteUnitTests
{
    [TestClass]
    public class DomainPhotoTest
    {
        [TestMethod]
        public void Testequalsshouldbeabltocomapretwoobjectsbyhash()
        {
            Photo testPicture = new Photo(1,1,1,"image","resolution","description");
            Photo testPicture2 = new Photo(1, 1, 1, "image", "resolution", "description");
            Assert.IsTrue(testPicture.Equals(testPicture2));
        }
    }
}
