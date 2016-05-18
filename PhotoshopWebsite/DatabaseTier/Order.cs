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
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;
        private string result = "";
        
        public Order()
        {
            // get the mysqlconnection singleton
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        /// <summary>
        /// this method returns a dictionary containing all the purchase data. when user not found, method returns null
        /// </summary>
        /// <param name="accountID"></param> id of the logged on account
        /// <returns></returns>
        public DataTable getOrders(int accountID)
        {
            try
            {
                myCommand = new MySqlCommand("getPurchases", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_account_ID", MySqlDbType.Int32).Value = accountID;
                // output
                myCommand.Parameters.Add("@ID", MySqlDbType.Int32);
                myCommand.Parameters["@ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@ACCOUNT_ID", MySqlDbType.Int32);
                myCommand.Parameters["@ACCOUNT_ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Date", MySqlDbType.Date);
                myCommand.Parameters["@Date"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Status", MySqlDbType.VarChar);
                myCommand.Parameters["@Status"].Direction = ParameterDirection.Output;
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
    }
}