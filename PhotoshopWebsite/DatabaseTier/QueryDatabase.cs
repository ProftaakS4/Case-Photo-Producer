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
        // Get the MySQL connection Singleton Instance
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;

        public QueryDatabase()
        {
            // get the mysqlconnection singleton
            this.mysqlConnection = this.connectionSingleton.getSqlConnection();
        }

        public DataTable CallProcedure(string procedure, Dictionary<string, string[]> parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                this.myCommand = new MySqlCommand(procedure, this.mysqlConnection);
                this.myCommand.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, string[]> parameter in parameters)
                {
                    switch (parameter.Value[0])
                    {
                        case "int":
                            this.myCommand.Parameters.Add("@" + parameter.Key, MySqlDbType.Int32).Value = int.Parse(parameter.Value[1]);
                            break;
                        case "string":
                            this.myCommand.Parameters.Add("@" + parameter.Key, MySqlDbType.VarChar).Value = parameter.Value[1];
                            break;
                        case "date":
                            this.myCommand.Parameters.Add("@" + parameter.Key, MySqlDbType.Date).Value = parameter.Value[1];
                            break;
                        case "double":
                            this.myCommand.Parameters.Add("@" + parameter.Key, MySqlDbType.Decimal).Value = parameter.Value[1];
                            break;
                        default:
                            break;
                    }
                    this.myCommand.Parameters["@" + parameter.Key].Direction = ParameterDirection.Input;
                }

                // execute query
                this.mysqlConnection.Open();
                MySqlDataReader dr = this.myCommand.ExecuteReader(CommandBehavior.CloseConnection);

                dt.Load(dr);
                // return datatable
                return dt;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
            finally
            {
                this.myCommand.Connection.Close();
                this.mysqlConnection.Close();
            }
            return dt;
        }
    }
}