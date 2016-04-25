using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.Controller;

namespace UnitTestPhotoshopWebsite.Controller
{
    [TestClass]
    public class PhotoControllerTests
    {
        PhotoController instance = new PhotoController();
        [TestMethod]
        public void TestgetUserPhotoIDs()
        {
            var result = instance.getUserPhotoIDs("4");
            var expresult = 1;
            Assert.AreEqual(expresult.ToString(), result[0].ToString());

            result = instance.getUserPhotoIDs("200000");
            expresult = 0;
            Assert.AreEqual(expresult, result.Count);
        }

        [TestMethod]
        public void TestgetPhoto()
        {
            var result = instance.getPhoto("1");
            var expresult = 1;
            Assert.AreEqual(expresult, result.MapID);

            result = instance.getPhoto("200000");
            Assert.IsNull(result);
        }
    }
}
