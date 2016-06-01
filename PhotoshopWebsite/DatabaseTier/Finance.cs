using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.DatabaseTier
{
    public class Finance
    {
        //Get the MySQL connection Singleton Instance
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;
       
        public Finance()
        {
            // get the mysqlconnection singleton
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        /// <summary>
        /// this method returns all the photographers with their first and last name and the money the company owes them.
        /// </summary>
        /// <returns></returns>
        public DataTable getAllFinances()
        {
            try
            {
                myCommand = new MySqlCommand("getPricePerPhotographer", mysqlConnection);
                // input - There is no input needed.
                // output
                myCommand.Parameters.Add("@ID", MySqlDbType.Int32);
                myCommand.Parameters["@ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@FirstName", MySqlDbType.VarChar);
                myCommand.Parameters["@FirstName"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@LastName", MySqlDbType.VarChar);
                myCommand.Parameters["@LastName"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@Owed_Money", MySqlDbType.Int32);
                myCommand.Parameters["@Owed_Money"].Direction = ParameterDirection.Output;
                
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