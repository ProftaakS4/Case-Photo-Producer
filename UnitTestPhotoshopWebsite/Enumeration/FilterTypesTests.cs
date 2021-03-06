﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.Domain;
using PhotoshopWebsite.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoshopWebsite.Enumeration.Tests
{
    [TestClass()]
    public class FilterTypesTests
    {
        [TestMethod()]
        public void FTypeTest()
        {
            ShoppingbasketItem item;

            item = new ShoppingbasketItem(1, "test1", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.CANVAS, 0.1);
            Assert.AreEqual(FilterTypes.FTypes.COLOR, item.Filter);

            item = new ShoppingbasketItem(2, "test2", FilterTypes.FTypes.BLACKWHITE, ProductTypes.PTypes.CANVAS, 0.1);
            Assert.AreEqual(FilterTypes.FTypes.BLACKWHITE, item.Filter);

            item = new ShoppingbasketItem(3, "test3", FilterTypes.FTypes.SEPIA, ProductTypes.PTypes.CANVAS, 0.1);
            Assert.AreEqual(FilterTypes.FTypes.SEPIA, item.Filter);
        }

        [TestMethod()]
        public void getFTypeTest()
        {
            FilterTypes.FTypes result;
            FilterTypes.FTypes expResult;

            result = FilterTypes.GetFType("COLOR");
            expResult = FilterTypes.FTypes.COLOR;
            Assert.AreEqual(expResult, result);

            result = FilterTypes.GetFType("BLACKWHITE");
            expResult = FilterTypes.FTypes.BLACKWHITE;
            Assert.AreEqual(expResult, result);

            result = FilterTypes.GetFType("SEPIA");
            expResult = FilterTypes.FTypes.SEPIA;
            Assert.AreEqual(expResult, result);

            result = FilterTypes.GetFType("default");
            expResult = FilterTypes.FTypes.COLOR;
            Assert.AreEqual(expResult, result);
        }
    }
}