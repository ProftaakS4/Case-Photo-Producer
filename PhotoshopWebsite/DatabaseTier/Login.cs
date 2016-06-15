using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace PhotoshopWebsite.DatabaseTier
{
    public class Login
    {
        //Get the MySQL connection Singleton Instance
        private ConnectionSingleton connectionSingleton;

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;

        public const int NO_USER_FOUND = -1;

        /// <summary>
        /// constructor of the class
        /// </summary>
        public Login()
        {
            // get the mysqlconnection singleton
            connectionSingleton = ConnectionSingleton.GetSingleton();
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        /// <summary>
        /// This method returns a true false boolean when a user trys to log in
        /// </summary>
        /// <param name="emailadres"></param> emailadres of the user
        /// <param name="password"></param> password of the user
        /// <returns></returns>
        public bool loginUser(string emailadres, string password)
        {
            try
            {
                myCommand = new MySqlCommand(null, mysqlConnection);
                myCommand.CommandText = "getUserPassword";
                myCommand.CommandType = CommandType.StoredProcedure;
                // input
                myCommand.Parameters.Add("@p_account_Email", MySqlDbType.VarChar).Value = emailadres;
                myCommand.Parameters.Add("@p_password", MySqlDbType.VarChar).Value = password;
                // output
                myCommand.Parameters.Add("@p_passed", MySqlDbType.VarChar);
                myCommand.Parameters["@p_passed"].Direction = ParameterDirection.Output;
                //execute query
                mysqlConnection.Open();
                myCommand.ExecuteNonQuery();
                //return true when output is validated
                if (myCommand.Parameters["@p_passed"].Value.ToString() == "true")
                {
                    return true;
                }
                return false;
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
            return false;
        }
    }
}