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

        /// <summary>
        /// constructor of the class
        /// </summary>
        public LoginCode()
        {
            // get the mysqlconnection singleton
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        public String LoginCodeValidation(int loginCode)
        {
            try
            {
                myCommand = new MySqlCommand("checkLoginCode", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_logincode", MySqlDbType.Int32).Value = loginCode;
              
                myCommand.Parameters.Add("@p_passed", MySqlDbType.VarChar);
                myCommand.Parameters["@p_passed"].Direction = ParameterDirection.Output;
                // execute query
                mysqlConnection.Open();
                myCommand.ExecuteNonQuery();
                String value = myCommand.Parameters["@p_passed"].Value.ToString();
                //MySqlDataAdapter da = new MySqlDataAdapter(myCommand);
                //MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                //DataTable dt = new DataTable();
                //dt.Load(dr);

                return value;
                //return datatable
                //return dt;
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
    /// this method returns a dictionary containing all the logincode data. when user not found, method returns null
    /// </summary>
    /// <param name="photographerID"></param> id of the photographer
    /// <returns></returns>
    public DataTable getLoginCodeData(int photographerID)
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