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
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;
        private string result = "";

        public Login()
        {
            // get the mysqlconnection singleton
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
                myCommand.Parameters.Add("@p_account_Email", MySqlDbType.VarChar).Value = emailadres;
                myCommand.Parameters.Add("@p_password", MySqlDbType.VarChar).Value = password;
                myCommand.Parameters.Add("@p_passed", MySqlDbType.VarChar);
                myCommand.Parameters["@p_passed"].Direction = ParameterDirection.Output;
                mysqlConnection.Open();
                myCommand.ExecuteNonQuery();

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


        public int getUserID(string emailaddress)
        {
            int id = -1;
            try
            {
                myCommand = new MySqlCommand(null, mysqlConnection);
                myCommand.CommandText = "getUserID";
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_emailaddress", MySqlDbType.VarChar).Value = emailaddress;
                myCommand.Parameters.Add("@p_ID", MySqlDbType.Int32);
                myCommand.Parameters["@p_ID"].Direction = ParameterDirection.Output;
                mysqlConnection.Open();
                myCommand.ExecuteNonQuery();
                id = (int)myCommand.Parameters["@p_ID"].Value;
                return id;
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
            return -1;
        }

        public Dictionary<string,string> getUserData(int id)
        {
            Dictionary<string,string> userData = new Dictionary<string, string>();
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