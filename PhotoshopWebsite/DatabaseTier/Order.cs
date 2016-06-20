using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.DatabaseTier
{
    public class Order
    {
        //Get the MySQL connection Singleton Instance
        private ConnectionSingleton connectionSingleton;

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;

        public Order()
        {
            // get the mysqlconnection singleton
            connectionSingleton = ConnectionSingleton.GetSingleton();
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        /// <summary>
        /// this method returns a dictionary containing all the purchase data. when user not found, method returns null
        /// </summary>
        /// <param name="accountID"></param> id of the logged on account
        /// <returns></returns>
        public DataTable getAllPurchases()
        {
            try
            {
                myCommand = new MySqlCommand("getAllPurchases", mysqlConnection);
                // input - There is no input needed.
                // output
                myCommand.Parameters.Add("@ID", MySqlDbType.Int32);
                myCommand.Parameters["@ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@ACCOUNT_ID", MySqlDbType.Int32);
                myCommand.Parameters["@ACCOUNT_ID"].Direction = ParameterDirection.Output;
                //myCommand.Parameters.Add("@Date", MySqlDbType.DateTime);
                //myCommand.Parameters["@Date"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Status", MySqlDbType.VarChar);
                myCommand.Parameters["@Status"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Shipping", MySqlDbType.VarChar);
                myCommand.Parameters["@Shipping"].Direction = ParameterDirection.Output;
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
        /// this method updates the purchases' status to Paid if the status is 'Not Paid'.
        /// </summary>
        /// <param name="accountID"></param> id of the logged on account
        /// <returns></returns>
        public void updatePurchaseStatus(int purchaseID, string paidStatus, string shippingStatus)
        {
            try
            {
                myCommand = new MySqlCommand("changeOrderStatus", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_purchase_ID", MySqlDbType.Int32).Value = purchaseID;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_newPaidStatus", MySqlDbType.VarChar).Value = paidStatus;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_newShippingStatus", MySqlDbType.VarChar).Value = shippingStatus;
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


        public int getPrice(int product_ID, int photo_ID)
        {
            try
            {
                myCommand = new MySqlCommand("getPrice", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_Product_ID", MySqlDbType.Int32).Value = product_ID;

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_Photo_ID", MySqlDbType.Int32).Value = photo_ID;

                myCommand.Parameters.Add("@Price", MySqlDbType.Int32);
                myCommand.Parameters["@Price"].Direction = ParameterDirection.Output;
                // execute query
                // execute query
                mysqlConnection.Open();

                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                //return price int
                return int.Parse(dt.Rows[0][0].ToString());
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
            return 0;
        }
    }
}