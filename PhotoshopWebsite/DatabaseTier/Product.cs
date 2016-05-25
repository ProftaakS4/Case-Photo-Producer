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

        /// <summary>
        /// this method returns a dictionary containing all the purchase data. when user not found, method returns null
        /// </summary>
        /// <param name="accountID"></param> id of the logged on account
        /// <returns></returns>
        public DataTable getAllProducts() // TODO
        {
            try
            {
                myCommand = new MySqlCommand("getAllProducts", mysqlConnection);
                // input - There is no input needed.
                // output
                myCommand.Parameters.Add("@ID", MySqlDbType.Int32);
                myCommand.Parameters["@ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Type", MySqlDbType.VarChar);
                myCommand.Parameters["@Type"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Material", MySqlDbType.VarChar);
                myCommand.Parameters["@Material"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Description", MySqlDbType.VarChar);
                myCommand.Parameters["@Description"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Image", MySqlDbType.VarChar);
                myCommand.Parameters["@Image"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Stock", MySqlDbType.Int32);
                myCommand.Parameters["@Stock"].Direction = ParameterDirection.Output;
                // execute query
                mysqlConnection.Open();
                //myCommand.ExecuteNonQuery();

                //MySqlDataAdapter da = new MySqlDataAdapter(myCommand);
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
        /// this method updates the product's stock with the amount which is given in the parameter.
        /// </summary>
        /// <param name="productID"></param> id of the product
        /// /// <param name="addedStock"></param> amount added to the stock.
        /// <returns></returns>
        public void updateProductStock(int productID, int addedStock)
        {
            try
            {
                myCommand = new MySqlCommand("addStock", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_product_ID", MySqlDbType.Int32).Value = productID;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_amount", MySqlDbType.VarChar).Value = addedStock;
                // output - There is no output.
                // execute query
                mysqlConnection.Open();
                //myCommand.ExecuteNonQuery();

                //MySqlDataAdapter da = new MySqlDataAdapter(myCommand);
                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
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
        }

        public void updateProductsPerPhotographer(int productID, int addedStock)
        {
            try
            {
                myCommand = new MySqlCommand("addStock", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_product_ID", MySqlDbType.Int32).Value = productID;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_amount", MySqlDbType.VarChar).Value = addedStock;
                // output - There is no output.
                // execute query
                mysqlConnection.Open();
                //myCommand.ExecuteNonQuery();

                //MySqlDataAdapter da = new MySqlDataAdapter(myCommand);
                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
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
        }
    }
}