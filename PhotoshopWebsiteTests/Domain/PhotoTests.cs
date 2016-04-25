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
        Photo testPhoto = new Photo(1, 1, 1, "path","resolution","description");
        Photo testPhoto2 = new Photo(1, 1, 1, "path", "resolution", "description");
        [TestMethod()]
        public void GetHashCodeTest()
        {          
            Assert.Equals(testPhoto.GetHashCode(),testPhoto.ID.GetHashCode());
        }
        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsTrue(testPhoto.Equals(testPhoto2));
        }
        //[TestMethod()]
        //public void getTypesTest()
        //{
        //    Assert.Equals();
        //}
        //[TestMethod()]
        //public void PhotoConstructortest()
        //{
        //    Assert.Fail();
        //}
    }
}