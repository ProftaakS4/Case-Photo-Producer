using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.DatabaseTier
{
    public class Photo
    {
        //Get the MySQL connection Singleton Instance
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;

        public Photo()
        {
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        public String getPhotos(int userid)
        {
            String Photos = null;
            try
            {
                myCommand = new MySqlCommand(null, mysqlConnection);
                myCommand.CommandText = "getPhotosUser";
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_id", MySqlDbType.Int32).Value = userid;
                // output
                myCommand.Parameters.Add("@p_photos", MySqlDbType.VarChar);
                myCommand.Parameters["@p_photos"].Direction = ParameterDirection.Output;

                // execute query
                mysqlConnection.Open();
                myCommand.ExecuteNonQuery();
                // add OUTPUT to string

                Photos = (string)myCommand.Parameters["@p_photos"].Value;

                // return dictionary
                return Photos;
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
