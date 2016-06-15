using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.Controller;
using PhotoshopWebsite.Domain;
using PhotoshopWebsite.Enumeration;
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
            DateTime datum = new DateTime();
            PaymentType pt = new PaymentType();
            order = new Order(1,datum,pt,"NL16RABO1234567890",9.99);
            Assert.AreEqual(1, order.ID);
            Assert.IsNotNull(order.Date);
            Assert.IsNotNull(order.Type);
            Assert.AreEqual("NL16RABO1234567890", order.IBAN);
            Assert.AreEqual(9.99, order.Price);
        }
    }
}