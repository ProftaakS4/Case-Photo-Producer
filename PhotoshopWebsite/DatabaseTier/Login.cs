using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

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
        /// <param name="emailadres"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool loginUser(string emailadres, string password)
        {

            try
            {
                //System.Windows.Forms.MessageBox.Show("Opening SQL connection");

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
                //System.Windows.Forms.MessageBox.Show("Closed SQL connection");
            }

            return false;

        }
    }
}