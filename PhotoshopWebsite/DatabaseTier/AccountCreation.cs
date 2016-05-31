using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.DatabaseTier
{
    public class AccountCreation
    {

        //Get the MySQL connection Singleton Instance
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;

        public AccountCreation()
        {
            // get the mysqlconnection singleton
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        public DataTable createAccount(String type,String firstName, String lastName, String streetName, String Housenumber, String Zipcode, String City, String Phonenumber, String IBAN, String Emailaddress, String Password)
        {
            try
            {
                myCommand = new MySqlCommand("insertUser", mysqlConnection);
                // input - There is no input needed.
                // output

                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("@p_type", MySqlDbType.VarChar).Value = type;
                myCommand.Parameters["@p_type"].Direction = ParameterDirection.Input;
                
                myCommand.Parameters.Add("@p_firstname", MySqlDbType.VarChar).Value = firstName;
                myCommand.Parameters["@p_firstname"].Direction = ParameterDirection.Input;

                myCommand.Parameters.Add("@p_Lastname", MySqlDbType.VarChar).Value = lastName;
                myCommand.Parameters["@p_Lastname"].Direction = ParameterDirection.Input;

                myCommand.Parameters.Add("@p_streetname", MySqlDbType.VarChar).Value = streetName; 
                myCommand.Parameters["@p_streetname"].Direction = ParameterDirection.Input;

                myCommand.Parameters.Add("@p_housenumber", MySqlDbType.VarChar).Value = Housenumber;
                myCommand.Parameters["@p_housenumber"].Direction = ParameterDirection.Input;

                myCommand.Parameters.Add("@p_zipcode", MySqlDbType.VarChar).Value = Zipcode;
                myCommand.Parameters["@p_zipcode"].Direction = ParameterDirection.Input;

                myCommand.Parameters.Add("@p_city", MySqlDbType.VarChar).Value = City;
                myCommand.Parameters["@p_city"].Direction = ParameterDirection.Input;

                myCommand.Parameters.Add("@p_phonenumber", MySqlDbType.VarChar).Value = Phonenumber; 
                myCommand.Parameters["@p_phonenumber"].Direction = ParameterDirection.Input;

                myCommand.Parameters.Add("@p_iban", MySqlDbType.VarChar).Value = IBAN; 
                myCommand.Parameters["@p_iban"].Direction = ParameterDirection.Input;

                myCommand.Parameters.Add("@p_emailaddress", MySqlDbType.VarChar).Value = Emailaddress;
                myCommand.Parameters["@p_emailaddress"].Direction = ParameterDirection.Input;

                myCommand.Parameters.Add("@p_password", MySqlDbType.VarChar).Value = Password; 
                myCommand.Parameters["@p_password"].Direction = ParameterDirection.Input;

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
