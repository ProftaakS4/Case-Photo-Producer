using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.DatabaseTier
{
    public class QueryDatabase
    {
        //Get the MySQL connection Singleton Instance
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;

        public QueryDatabase()
        {
            // get the mysqlconnection singleton
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        public DataTable CallProcedure(string procedure, string[] parameters)
        {
            try
            {
                myCommand = new MySqlCommand(procedure, mysqlConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                foreach (string parameter in parameters)
                {
                    myCommand.Parameters.Add("@p_" + parameter, MySqlDbType.Int32).Value = parameter;
                    myCommand.Parameters["@p_" + parameter].Direction = ParameterDirection.Input;
                }

                //execute query
                mysqlConnection.Open();
                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                //return datatable
                return dt;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
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