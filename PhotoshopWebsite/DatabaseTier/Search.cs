using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.DatabaseTier
{
    public class Search
    {
        //Get the MySQL connection Singleton Instance
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;

        /// <summary>
        /// constructor of the class
        /// </summary>
        public Search()
        {
            // get the mysqlconnection singleton
            mysqlConnection = connectionSingleton.getSqlConnection();
        }
        public DataTable searchPhoto(string photoName, int userID)
        {
            try
            {
                myCommand = new MySqlCommand("searchPhoto", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_user_ID", MySqlDbType.Int32).Value = userID;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_searchedText", MySqlDbType.VarChar).Value = photoName;
                // output
                myCommand.Parameters.Add("@ID", MySqlDbType.Int32);
                myCommand.Parameters["@ID"].Direction = ParameterDirection.Output;
                // execute query
                mysqlConnection.Open();
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

        public DataTable searchGroupPhoto(string photoName)
        {
            try
            {
                myCommand = new MySqlCommand("searchGroupPhoto", mysqlConnection);
                // input
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@p_searchedText", MySqlDbType.VarChar).Value = photoName;
                myCommand.Parameters["@p_searchedText"].Direction = ParameterDirection.Input;
                // output
                myCommand.Parameters.Add("@ID", MySqlDbType.Int32);
                myCommand.Parameters["@ID"].Direction = ParameterDirection.Output;
                // execute query
                mysqlConnection.Open();
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



        //    // set the data for the current photo
        //    try
        //    {
        //        myCommand = new MySqlCommand("searchPhoto", mysqlConnection);
        //        myCommand.CommandType = CommandType.StoredProcedure;
        //        // input
        //        myCommand.Parameters.Add("@p_user_ID", MySqlDbType.Int32).Value = userID;
        //        myCommand.Parameters.Add("@p_searchedtext", MySqlDbType.VarChar).Value = photoName;
        //        // output
        //        myCommand.Parameters.Add("@p_photos", MySqlDbType.Int32);
        //        myCommand.Parameters["@p_photos"].Direction = ParameterDirection.Output;


        //        //execute query
        //        mysqlConnection.Open();
        //        myCommand.ExecuteNonQuery();
        //        //return the result of the query
        //        return myCommand.Parameters["@p_photos"].Value.ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        myCommand.Connection.Close();
        //        mysqlConnection.Close();
        //    }
        //    return null;
        //}
    }
}
