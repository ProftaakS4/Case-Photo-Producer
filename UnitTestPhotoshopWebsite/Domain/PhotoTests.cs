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
            Assert.IsNotNull(testPhoto.GetHashCode());
        }
        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsTrue(testPhoto.Equals(testPhoto2));
        }
        // This test doesnt work due to the database connection
        [TestMethod()]
        public void getTypesTest()
        {
            List<ProductTypes.PTypes> types = new List<ProductTypes.PTypes>();
            types.Add(ProductTypes.PTypes.PHOTO1x2);
            types.Add(ProductTypes.PTypes.PHOTO2x4);
            types.Add(ProductTypes.PTypes.PHOTO5x8);
            CollectionAssert.AreEqual(testPhoto.getTypes(testPhoto.ID), types);
        }
        [TestMethod()]
        public void PhotoConstructortest()
        {
            Assert.IsInstanceOfType(testPhoto, typeof(Photo));
        }
    }
}