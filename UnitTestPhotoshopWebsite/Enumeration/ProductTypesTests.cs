using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class ProductTypesTests
    {
        [TestMethod()]
        public void PTypeTest()
        {
            ShoppingbasketItem item;

            item = new ShoppingbasketItem(1, "test1", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.PHOTO1x2, 0.1);
            Assert.AreEqual(ProductTypes.PTypes.PHOTO1x2, item.Product);

            item = new ShoppingbasketItem(2, "test2", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.PHOTO2x4, 0.1);
            Assert.AreEqual(ProductTypes.PTypes.PHOTO2x4, item.Product);

            item = new ShoppingbasketItem(3, "test3", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.PHOTO5x8, 0.1);
            Assert.AreEqual(ProductTypes.PTypes.PHOTO5x8, item.Product);

            item = new ShoppingbasketItem(4, "test4", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.MUISMAT, 0.1);
            Assert.AreEqual(ProductTypes.PTypes.MUISMAT, item.Product);

            item = new ShoppingbasketItem(5, "test5", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.TASSEN, 0.1);
            Assert.AreEqual(ProductTypes.PTypes.TASSEN, item.Product);

            item = new ShoppingbasketItem(6, "test6", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.TSHIRT, 0.1);
            Assert.AreEqual(ProductTypes.PTypes.TSHIRT, item.Product);

            item = new ShoppingbasketItem(7, "test7", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.MOK, 0.1);
            Assert.AreEqual(ProductTypes.PTypes.MOK, item.Product);

            item = new ShoppingbasketItem(8, "test", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.CANVAS, 0.1);
            Assert.AreEqual(ProductTypes.PTypes.CANVAS, item.Product);

            item = new ShoppingbasketItem(9, "test9", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.DIBOND, 0.1);
            Assert.AreEqual(ProductTypes.PTypes.DIBOND, item.Product);
        }

        [TestMethod()]
        public void getPTypeTest()
        {
            ProductTypes.PTypes result;
            ProductTypes.PTypes expResult;

            result = ProductTypes.getPType("PHOTO1x2");
            expResult = ProductTypes.PTypes.PHOTO1x2;
            Assert.AreEqual(expResult, result);

            result = ProductTypes.getPType("PHOTO2x4");
            expResult = ProductTypes.PTypes.PHOTO2x4;
            Assert.AreEqual(expResult, result);

            result = ProductTypes.getPType("PHOTO5x8");
            expResult = ProductTypes.PTypes.PHOTO5x8;
            Assert.AreEqual(expResult, result);

            result = ProductTypes.getPType("MUISMAT");
            expResult = ProductTypes.PTypes.MUISMAT;
            Assert.AreEqual(expResult, result);

            result = ProductTypes.getPType("TASSEN");
            expResult = ProductTypes.PTypes.TASSEN;
            Assert.AreEqual(expResult, result);

            result = ProductTypes.getPType("TSHIRT");
            expResult = ProductTypes.PTypes.TSHIRT;
            Assert.AreEqual(expResult, result);

            result = ProductTypes.getPType("MOK");
            expResult = ProductTypes.PTypes.MOK;
            Assert.AreEqual(expResult, result);

            result = ProductTypes.getPType("CANVAS");
            expResult = ProductTypes.PTypes.CANVAS;
            Assert.AreEqual(expResult, result);

            result = ProductTypes.getPType("DIBOND");
            expResult = ProductTypes.PTypes.DIBOND;
            Assert.AreEqual(expResult, result);

            result = ProductTypes.getPType("default");
            expResult = ProductTypes.PTypes.PHOTO1x2;
            Assert.AreEqual(expResult, result);
        }
    }
}