using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoshopWebsite.Enumeration;

namespace PhotoshopWebsite.Domain.Tests
{
    [TestClass()]
    public class PhotoTests
    {
        Photo testPhoto = new Photo(1, 1, 1, "path", "resolution", "description");
        Photo testPhoto2 = new Photo(1, 1, 1, "path", "resolution", "description");
        [TestMethod()]
        public void GetHashCodeTest()
        {
            Assert.Equals(testPhoto.GetHashCode(), testPhoto2.GetHashCode());
        }
        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsTrue(testPhoto.Equals(testPhoto2));
        }
        [TestMethod()]
        public void getTypesTest()
        {
            List<ProductTypes.PTypes> types = new List<ProductTypes.PTypes>();
            types.Add(ProductTypes.PTypes.PHOTO1x2);
            types.Add(ProductTypes.PTypes.PHOTO2x4);
            types.Add(ProductTypes.PTypes.PHOTO5x8);
            Assert.etestPhoto.getTypes(testPhoto.ID.ToString());
            Assert.Equals();
        }
        [TestMethod()]
        public void PhotoConstructortest()
        {
            Assert.Fail();
        }
    }
}