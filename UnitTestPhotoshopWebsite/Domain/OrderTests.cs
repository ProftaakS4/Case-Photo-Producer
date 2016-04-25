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
    public class OrderTests
    {
        Order order;
        [TestMethod()]
        public void getIDTest()
        {
            Dictionary<Product, int> test = new Dictionary<Product, int>();
            DateTime datum = new DateTime();
            PaymentType pt = new PaymentType();
            order = new Order(1,test,datum,pt,"NL16RABO1234567890",9.99);
            int id = order.getID();
            Assert.AreEqual(1, id);
            Assert.IsNotNull(order.getDate());
            Assert.IsNotNull(order.getType());
            Assert.AreEqual("NL16RABO1234567890", order.getIBAN());
            Assert.AreEqual(9.99, order.getPrice());
        }
    }
}