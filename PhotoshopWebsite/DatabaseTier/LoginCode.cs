using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace PhotoshopWebsite.DatabaseTier
{
    public class LoginCode
    {
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;
        private string result = "";

        /// <summary>
        /// constructor of the class
        /// </summary>
        public LoginCode()
        {
            // get the mysqlconnection singleton
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        /// <summary>
        /// this method returns a dictionary containing all the logincode data. when user not found, method returns null
        /// </summary>
        /// <param name="photographerID"></param> id of the photographer
        /// <returns></returns>
        public DataTable getLoginCodeData(int photographerID)
        {
           
            try
            {
                myCommand = new MySqlCommand("getLoginCodesFromPhotographer", mysqlConnection);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_photographer_ID", MySqlDbType.Int32).Value = photographerID;
                // output
                myCommand.Parameters.Add("@p_id", MySqlDbType.Int32);
                myCommand.Parameters["@p_id"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_map_id", MySqlDbType.Int32);
                myCommand.Parameters["@p_map_id"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_superuser_ID", MySqlDbType.Int32);
                myCommand.Parameters["@p_superuser_ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_user_id", MySqlDbType.Int32);
                myCommand.Parameters["@p_user_id"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_used", MySqlDbType.Int32);
                myCommand.Parameters["@p_used"].Direction = ParameterDirection.Output;
                //myCommand.Parameters.Add("@p_validUntil", MySqlDbType.Int32);
                //myCommand.Parameters["@p_validUntil"].Direction = ParameterDirection.Output;
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