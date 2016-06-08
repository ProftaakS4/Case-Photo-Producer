﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod()]
        public void getPhotosUserTest()
        {
            DataTable getPhotosUser = photo.getPhotosUser(4);
            Assert.IsNotNull(getPhotosUser);
        }

        [TestMethod()]
        public void getTypesTest()
        {
            List<ProductTypes.PTypes> getTypes = photo.getTypes(1);
            Assert.IsNotNull(getTypes);
        }

        [TestMethod()]
        public void getPhotoTest()
        {
            DataTable getPhoto = photo.getPhoto(4);
            Assert.IsNotNull(getPhoto);
        }
    }
}