using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoshopWebsite.Controller.Tests
{
    [TestClass()]
    public class ProductTests
    {
        Product product;
        [TestMethod()]
        public void EqualsTest()
        {

            product = new Product(1, "mok", "steen", "mok", "ergens", 3);
            bool result = product.Equals(null);
            Assert.IsFalse(result);

            Product product2 = new Product(1, "mok", "steen", "mok", "ergens", 3);
            result = product.Equals(product2);
            Assert.IsTrue(result);
        }
    }
}