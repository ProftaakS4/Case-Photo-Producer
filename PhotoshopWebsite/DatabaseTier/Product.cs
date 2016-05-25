using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace PhotoshopWebsite.DatabaseTier
{
    public class Product
    {
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;

        /// <summary>
        /// constructor of the class
        /// </summary>
        public Product()
        {
            // get the mysqlconnection singleton
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        /// <summary>
        /// this method returns a datatable containing all the product data. when user not found, method returns null
        /// </summary>
        /// <param name="photographerID"></param> id of the photographer
        /// <returns></returns>
        public DataTable getProductData(int photographerID)
        {
            try
            {
                myCommand = new MySqlCommand("getLoginCodesFromPhotographer", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_photographer_ID", MySqlDbType.Int32).Value = photographerID;
                // output
                myCommand.Parameters.Add("@ID", MySqlDbType.Int32);
                myCommand.Parameters["@ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@MAP_ID", MySqlDbType.Int32);
                myCommand.Parameters["@MAP_ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@PHOTOGRAPHER_ID", MySqlDbType.Int32);
                myCommand.Parameters["@PHOTOGRAPHER_ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@used", MySqlDbType.Int32);
                myCommand.Parameters["@used"].Direction = ParameterDirection.Output;
                // execute query
                mysqlConnection.Open();

                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                //return datatable
                return dt;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
            return null;
        }

        /// <summary>
        /// this method returns a datatable containing all the product-photographer data. when user not found, method returns null
        /// </summary>
        /// <param name="photographerID"></param> id of the photographer
        /// <returns></returns>
        public DataTable getProductPhotographerData(int photographerID)
        {
            try
            {
                myCommand = new MySqlCommand("getProductAvailability", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_user_ID", MySqlDbType.Int32).Value = photographerID;
                // output
                myCommand.Parameters.Add("@PHOTOGRAPHER_ID", MySqlDbType.Int32);
                myCommand.Parameters["@PHOTOGRAPHER_ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@PRODUCT_ID", MySqlDbType.Int32);
                myCommand.Parameters["@PRODUCT_ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Price", MySqlDbType.Int32);
                myCommand.Parameters["@Price"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Available", MySqlDbType.Int32);
                myCommand.Parameters["@Available"].Direction = ParameterDirection.Output;
                // execute query
                mysqlConnection.Open();

                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                //return datatable
                return dt;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
            return null;
        }
    }
}