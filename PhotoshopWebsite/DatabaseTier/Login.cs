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

        /// <summary>
        /// this method get the user id of the given emailaddress. when not found, method returns -1
        /// </summary>
        /// <param name="emailaddress"></param> of the user
        /// <returns></returns>
        public int getUserID(string emailaddress)
        {
            int id = NO_USER_FOUND;
            try
            {
                myCommand = new MySqlCommand(null, mysqlConnection);
                myCommand.CommandText = "getUserID";
                myCommand.CommandType = CommandType.StoredProcedure;
                // input
                myCommand.Parameters.Add("@p_emailaddress", MySqlDbType.VarChar).Value = emailaddress;
                //output
                myCommand.Parameters.Add("@p_ID", MySqlDbType.Int32);
                myCommand.Parameters["@p_ID"].Direction = ParameterDirection.Output;
                // execute query
                mysqlConnection.Open();
                myCommand.ExecuteNonQuery();
                //add output to integer
                if (myCommand.Parameters["@p_ID"].Value.ToString() != "")
                {
                    id = Int32.Parse(myCommand.Parameters["@p_ID"].Value.ToString());
                }
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
            return id;
        }

        /// <summary>
        /// this method returns a dictionary containing all the user data. when user not found, method returns null
        /// </summary>
        /// <param name="id"></param> id of the user
        /// <returns></returns>
        public Dictionary<string, string> getUserData(int id)
        {
            Dictionary<string, string> userData = null;
            try
            {
                myCommand = new MySqlCommand(null, mysqlConnection);
                myCommand.CommandText = "getUserInformation";
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_id", MySqlDbType.Int32).Value = id;
                // output
                myCommand.Parameters.Add("@p_type", MySqlDbType.VarChar);
                myCommand.Parameters["@p_type"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_firstname", MySqlDbType.VarChar);
                myCommand.Parameters["@p_firstname"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_lastname", MySqlDbType.VarChar);
                myCommand.Parameters["@p_lastname"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_streetname", MySqlDbType.VarChar);
                myCommand.Parameters["@p_streetname"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_housenumber", MySqlDbType.VarChar);
                myCommand.Parameters["@p_housenumber"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_zipcode", MySqlDbType.VarChar);
                myCommand.Parameters["@p_zipcode"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_city", MySqlDbType.VarChar);
                myCommand.Parameters["@p_city"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_phonenumber", MySqlDbType.VarChar);
                myCommand.Parameters["@p_phonenumber"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_iban", MySqlDbType.VarChar);
                myCommand.Parameters["@p_iban"].Direction = ParameterDirection.Output;
                // execute query
                mysqlConnection.Open();
                myCommand.ExecuteNonQuery();
                // add OUTPUT to DICTIONARY
                userData = new Dictionary<string, string>();
                userData.Add("type", (string)myCommand.Parameters["@p_type"].Value);
                userData.Add("firstname", (string)myCommand.Parameters["@p_firstname"].Value);
                userData.Add("lastname", (string)myCommand.Parameters["@p_lastname"].Value);
                userData.Add("streetname", (string)myCommand.Parameters["@p_streetname"].Value);
                userData.Add("housenumber", (string)myCommand.Parameters["@p_housenumber"].Value);
                userData.Add("zipcode", (string)myCommand.Parameters["@p_zipcode"].Value);
                userData.Add("city", (string)myCommand.Parameters["@p_city"].Value);
                userData.Add("phonenumber", (string)myCommand.Parameters["@p_phonenumber"].Value);
                userData.Add("iban", (string)myCommand.Parameters["@p_iban"].Value);
                // return dictionary
                return userData;
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