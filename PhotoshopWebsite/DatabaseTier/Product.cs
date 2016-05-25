using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace PhotoshopWebsite.DatabaseTier
{
    public class Product
    {
        //Get the MySQL connection Singleton Instance
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;
        private string result = "";
        
        public Product()
        {
            // get the mysqlconnection singleton
            mysqlConnection = connectionSingleton.getSqlConnection();
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
        public void updateProductStock(int productID, int addedStock) // TODO
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