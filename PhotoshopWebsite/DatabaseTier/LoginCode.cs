﻿using System;
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
                myCommand.Parameters["@p_photographer_ID"].Direction = ParameterDirection.Input;
                // output
                myCommand.Parameters.Add("@p_id", MySqlDbType.Int32);
                myCommand.Parameters["@p_id"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_map_id", MySqlDbType.Int32);
                myCommand.Parameters["@p_map_id"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_photographerout_ID", MySqlDbType.Int32);
                myCommand.Parameters["@p_photographerout_ID"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_used", MySqlDbType.Int32);
                myCommand.Parameters["@p_used"].Direction = ParameterDirection.Output;
                // execute query
                mysqlConnection.Open();                
                //MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(myCommand);
                da.Fill(dt);
                
                //dt.Load(dr);
                
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