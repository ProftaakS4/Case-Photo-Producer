﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoshopWebsite.DatabaseTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhotoshopWebsite.DatabaseTier.Tests
{
    [TestClass()]
    public class ConnectionSingletonTests
    {
        [TestMethod()]
        public void GetSingletonTest()
        {
            ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();
            Assert.IsNotNull(connectionSingleton);

            //MySqlConnection mysqlConnection = connectionSingleton.getSqlConnection();
            //Assert.IsNotNull(mysqlConnection);
        }
    }
}