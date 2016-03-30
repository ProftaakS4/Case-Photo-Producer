using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace PhotoshopWebsite.DatabaseTier
{
    public class Photo
    {
        //Get the MySQL connection Singleton Instance
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;
        private string result = "";

        /// <summary>
        /// constructor of the class
        /// </summary>
        public Photo()
        {
            // get the mysqlconnection singleton
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        public string getPhotosUser(string userID)
        {
            try
            {
                myCommand = new MySqlCommand("getPhotosUser", mysqlConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                // input
                myCommand.Parameters.Add("@p_id", MySqlDbType.Int32).Value = Convert.ToInt32(userID);
                // output
                myCommand.Parameters.Add("@p_photos", MySqlDbType.VarChar);
                myCommand.Parameters["@p_photos"].Direction = ParameterDirection.Output;
                //execute query
                mysqlConnection.Open();
                myCommand.ExecuteNonQuery();
                //return the result of the query
                return myCommand.Parameters["@p_photos"].Value.ToString();
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
            return "";
        }


        public List<string> getPhoto(string photoID)
        {
            List<string> photoElementen;
            try
            {
                myCommand = new MySqlCommand("getPhoto", mysqlConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                // input
                myCommand.Parameters.Add("@p_photo_ID", MySqlDbType.VarChar).Value = photoID;
                // output
                myCommand.Parameters.Add("@p_account_id", MySqlDbType.Int32);
                myCommand.Parameters["@p_account_id"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_map_id", MySqlDbType.Int32);
                myCommand.Parameters["@p_map_id"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_logincode", MySqlDbType.Int32);
                myCommand.Parameters["@p_logincode"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_type", MySqlDbType.VarChar);
                myCommand.Parameters["@p_type"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_path", MySqlDbType.VarChar);
                myCommand.Parameters["@p_path"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_resolution", MySqlDbType.VarChar);
                myCommand.Parameters["@p_resolution"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_description", MySqlDbType.VarChar);
                myCommand.Parameters["@p_description"].Direction = ParameterDirection.Output;

                //execute query
                mysqlConnection.Open();
                myCommand.ExecuteNonQuery();
                //return the result of the query
                photoElementen = new List<string>();
                photoElementen.Add(photoID);
                photoElementen.Add(myCommand.Parameters["@p_account_id"].Value.ToString());
                photoElementen.Add(myCommand.Parameters["@p_map_id"].Value.ToString());
                photoElementen.Add(myCommand.Parameters["@p_logincode"].Value.ToString());
                photoElementen.Add(myCommand.Parameters["@p_type"].Value.ToString());
                photoElementen.Add(myCommand.Parameters["@p_path"].Value.ToString());
                photoElementen.Add(myCommand.Parameters["@p_resolution"].Value.ToString());
                photoElementen.Add(myCommand.Parameters["@p_description"].Value.ToString());
                return photoElementen;

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