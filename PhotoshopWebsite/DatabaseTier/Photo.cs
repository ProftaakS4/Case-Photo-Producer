using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using PhotoshopWebsite.Enumeration;

namespace PhotoshopWebsite.DatabaseTier
{
    public class Photo
    {
        //Get the MySQL connection Singleton Instance
        private ConnectionSingleton connectionSingleton = ConnectionSingleton.GetSingleton();

        // Create a new MySQL connections
        private MySqlConnection mysqlConnection;
        private MySqlCommand myCommand = null;

        /// <summary>
        /// constructor of the class
        /// </summary>
        public Photo()
        {
            // get the mysqlconnection singleton
            mysqlConnection = connectionSingleton.getSqlConnection();
        }

        /// <summary>
        /// This method gets all the photoIDS from the specified userID. When there are no photo found the method returns null
        /// </summary>
        /// <param name="userID"></param> the user id of the account
        /// <returns></returns>
        public DataTable getPhotosUser(int userID)
        {
            try
            {
                myCommand = new MySqlCommand("getPhotosUser", mysqlConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                // input
                myCommand.Parameters.Add("@p_id", MySqlDbType.Int32).Value = Convert.ToInt32(userID);
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
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
            return null;
        }
        /// <summary>
        /// This method gets all the GroupphotoIDS from the specified userID. When there are no photo found the method returns null
        /// </summary>
        /// <param name="userID"></param> the user id of the account
        /// <returns></returns>
        public DataTable getGroupPhotos()
        {
            try
            {
                myCommand = new MySqlCommand("getGroupPhotos", mysqlConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                // output
                myCommand.Parameters.Add("@p_photos", MySqlDbType.VarChar);
                myCommand.Parameters["@p_photos"].Direction = ParameterDirection.Output;
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
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
            return null;
        }
        public List<ProductTypes.PTypes> getTypes(int photoID)
        {
            List<ProductTypes.PTypes> types = new List<ProductTypes.PTypes>();
            try
            {
                myCommand = new MySqlCommand("getAccountIdperPhoto", mysqlConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                // input
                myCommand.Parameters.Add("@p_id", MySqlDbType.VarChar).Value = photoID;
                // output
                myCommand.Parameters.Add("@a_id", MySqlDbType.VarChar);
                myCommand.Parameters["@a_id"].Direction = ParameterDirection.Output;
                //execute query
                mysqlConnection.Open();
                myCommand.ExecuteNonQuery();
                int accountid = Convert.ToInt32(myCommand.Parameters["@a_id"].Value);
                // get the account id belonging to the picture             
                myCommand = new MySqlCommand("getProductTypesByAccountId", mysqlConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                // input
                myCommand.Parameters.Add("@a_id", MySqlDbType.VarChar).Value = accountid;
                //execute query
                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        switch (data[0].ToString())
                        {
                            case "1":
                                types.Add(ProductTypes.PTypes.PHOTO1x2);
                                break;
                            case "2":
                                types.Add(ProductTypes.PTypes.PHOTO2x4);
                                break;
                            case "3":
                                types.Add(ProductTypes.PTypes.PHOTO5x8);
                                break;
                            case "4":
                                types.Add(ProductTypes.PTypes.MUISMAT);
                                break;
                            case "5":
                                types.Add(ProductTypes.PTypes.TASSEN);
                                break;
                            case "6":
                                types.Add(ProductTypes.PTypes.TSHIRT);
                                break;
                            case "7":
                                types.Add(ProductTypes.PTypes.MOK);
                                break;
                            case "8":
                                types.Add(ProductTypes.PTypes.CANVAS);
                                break;
                            case "9":
                                types.Add(ProductTypes.PTypes.DIBOND);
                                break;
                        }
                    }
                }
                return types;
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
        /// <summary>
        /// this method will get all the photo information thats stored in the database from the specified photoid
        /// </summary>
        /// <param name="photoID"></param> the photoid of the photo
        /// <returns></returns>
        public DataTable getPhoto(int photoID)
        {
            try
            {
                myCommand = new MySqlCommand("getPhoto", mysqlConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                // input
                myCommand.Parameters.Add("@p_photo_ID", MySqlDbType.VarChar).Value = photoID.ToString();

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
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                myCommand.Connection.Close();
                mysqlConnection.Close();
            }
            return null;
        }

        public DataTable getPhotosPhotographer(string userID)
        {
            List<string> photoIDS = new List<string>(); ;
            try
            {
                myCommand = new MySqlCommand("getPhotosPhotographer", mysqlConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                // input
                myCommand.Parameters.Add("@p_id", MySqlDbType.Int32).Value = Convert.ToInt32(userID);
                //execute query
                mysqlConnection.Open();
                MySqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);

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