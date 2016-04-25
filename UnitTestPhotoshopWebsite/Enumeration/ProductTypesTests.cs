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

            item = new ShoppingbasketItem(1, "test1", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.PHOTO1x2);
            Assert.Equals(ProductTypes.PTypes.PHOTO1x2, item.product);

            item = new ShoppingbasketItem(2, "test2", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.PHOTO2x4);
            Assert.Equals(ProductTypes.PTypes.PHOTO2x4, item.product);

            item = new ShoppingbasketItem(3, "test3", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.PHOTO5x8);
            Assert.Equals(ProductTypes.PTypes.PHOTO5x8, item.product);

            item = new ShoppingbasketItem(4, "test4", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.MUISMAT);
            Assert.Equals(ProductTypes.PTypes.MUISMAT, item.product);

            item = new ShoppingbasketItem(5, "test5", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.TASSEN);
            Assert.Equals(ProductTypes.PTypes.TASSEN, item.product);

            item = new ShoppingbasketItem(6, "test6", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.TSHIRT);
            Assert.Equals(ProductTypes.PTypes.TSHIRT, item.product);

            item = new ShoppingbasketItem(7, "test7", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.MOK);
            Assert.Equals(ProductTypes.PTypes.MOK, item.product);

            item = new ShoppingbasketItem(8, "test", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.CANVAS);
            Assert.Equals(ProductTypes.PTypes.CANVAS, item.product);

            item = new ShoppingbasketItem(9, "test9", FilterTypes.FTypes.COLOR, ProductTypes.PTypes.DIBOND);
            Assert.Equals(ProductTypes.PTypes.DIBOND, item.product);
        }

        [TestMethod()]
        public void getPTypeTest()
        {
            ProductTypes.PTypes result;
            ProductTypes.PTypes expResult;

            result = ProductTypes.getPType("PHOTO1x2");
            expResult = ProductTypes.PTypes.PHOTO1x2;
            Assert.Equals(expResult, result);

            result = ProductTypes.getPType("PHOTO2x4");
            expResult = ProductTypes.PTypes.PHOTO2x4;
            Assert.Equals(expResult, result);

            result = ProductTypes.getPType("PHOTO5x8");
            expResult = ProductTypes.PTypes.PHOTO5x8;
            Assert.Equals(expResult, result);

            result = ProductTypes.getPType("MUISMAT");
            expResult = ProductTypes.PTypes.MUISMAT;
            Assert.Equals(expResult, result);

            result = ProductTypes.getPType("TASSEN");
            expResult = ProductTypes.PTypes.TASSEN;
            Assert.Equals(expResult, result);

            result = ProductTypes.getPType("TSHIRT");
            expResult = ProductTypes.PTypes.TSHIRT;
            Assert.Equals(expResult, result);

            result = ProductTypes.getPType("MOK");
            expResult = ProductTypes.PTypes.MOK;
            Assert.Equals(expResult, result);

            result = ProductTypes.getPType("CANVAS");
            expResult = ProductTypes.PTypes.CANVAS;
            Assert.Equals(expResult, result);

            result = ProductTypes.getPType("DIBOND");
            expResult = ProductTypes.PTypes.DIBOND;
            Assert.Equals(expResult, result);
        }
    }
}