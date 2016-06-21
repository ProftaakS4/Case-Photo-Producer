using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace PhotoshopWebsite.DatabaseTier
{
    public class ConnectionSingleton
    {
        // create instance of its owne class
        private static ConnectionSingleton _singleton = null;

        // create connection bases on the connectionstring in the web.config 
        private static MySqlConnection connection = null;

        /// <summary>
        /// Private constructor of the Singleton class
        /// </summary>
        private ConnectionSingleton()
        {
        }

        /// <summary>
        /// Get the one instance of the Connection singleton
        /// </summary>
        /// <returns></returns>
        public static ConnectionSingleton GetSingleton()
        {
            if (_singleton == null)
            {
                _singleton = new ConnectionSingleton();
                return _singleton;
            }
            return _singleton;
        }

        /// <summary>
        /// Get the singleton of the MySQL connections
        /// </summary>
        /// <returns></returns>
        public MySqlConnection getSqlConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                return connection;
            }
            return connection;
        }
    }
}