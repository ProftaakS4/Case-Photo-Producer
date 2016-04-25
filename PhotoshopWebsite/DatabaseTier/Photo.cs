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

        /// <summary>
        /// This method gets all the photoIDS from the specified userID. When there are no photo found the method returns null
        /// </summary>
        /// <param name="userID"></param> the user id of the account
        /// <returns></returns>
        public List<string> getPhotosUser(string userID)
        {
            List<string> photoIDS = null;
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
                //capture the out parameter form the databas into a variable
                result = (string)myCommand.Parameters["@p_photos"].Value + " ";
                // check if the result has some numeric values into it, meaning that there are indeed photoIDS returned
                if (result.Any(char.IsDigit))
                {
                    string photoID = "";
                    // create new string list and iterate over the result and break down the photoIDS into idividual photoIDS
                    photoIDS = new List<string>();
                    foreach (char c in result)
                    {
                        if (c != ' ')
                        {
                            photoID = photoID + c;
                        }
                        else
                        {
                            if (photoID != "")
                            {
                                // replace potential whitespace
                                photoIDS.Add(photoID.Replace(" ", ""));
                                photoID = "";
                            }
                        }
                    }
                    return photoIDS;
                }
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
        public List<ProductTypes.ETypes> getTypes(string photoID)
        {
            List<ProductTypes.ETypes> types = new List<ProductTypes.ETypes>();
            string output;
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
                // output
                myCommand.Parameters.Add("@p_types", MySqlDbType.VarChar);
                myCommand.Parameters["@p_types"].Direction = ParameterDirection.Output;
                //execute query
                myCommand.ExecuteNonQuery();
                output = myCommand.Parameters["@p_types"].Value.ToString();
                string[] id = output.Split(null);
                // loop over output string and get types for the picture
                for(int i = 0; i < id.Length; i++)
                {
                    switch (id[i])
                    {
                        case "1":
                            types.Add(ProductTypes.ETypes.PHOTO1x2);     
                            break;
                        case "2":
                            types.Add(ProductTypes.ETypes.PHOTO2x4);
                            break;
                        case "3":
                            types.Add(ProductTypes.ETypes.PHOTO5x8);
                            break;
                        case "4":
                            types.Add(ProductTypes.ETypes.MUISMAT);
                            break;
                        case "5":
                            types.Add(ProductTypes.ETypes.TASSEN);
                            break;
                        case "6":
                            types.Add(ProductTypes.ETypes.TSHIRT);
                            break;
                        case "7":
                            types.Add(ProductTypes.ETypes.MOK);
                            break;
                        case "8":
                            types.Add(ProductTypes.ETypes.CANVAS);
                            break;
                        case "9":
                            types.Add(ProductTypes.ETypes.DIBOND);
                            break;
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
        public List<string> getPhoto(string photoID)
        {
            List<string> photoElements;
            try
            {
                myCommand = new MySqlCommand("getPhoto", mysqlConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                // input
                myCommand.Parameters.Add("@p_photo_ID", MySqlDbType.VarChar).Value = photoID;
                // output
                myCommand.Parameters.Add("@p_photographer_id", MySqlDbType.Int32);
                myCommand.Parameters["@p_photographer_id"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_map_id", MySqlDbType.Int32);
                myCommand.Parameters["@p_map_id"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_image", MySqlDbType.VarChar);
                myCommand.Parameters["@p_image"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_resolution", MySqlDbType.VarChar);
                myCommand.Parameters["@p_resolution"].Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("@p_description", MySqlDbType.VarChar);
                myCommand.Parameters["@p_description"].Direction = ParameterDirection.Output;

                //execute query
                mysqlConnection.Open();
                myCommand.ExecuteNonQuery();

                // add all the outputs to the list
                photoElements = new List<string>();
                photoElements.Add(photoID);
                photoElements.Add(myCommand.Parameters["@p_photographer_id"].Value.ToString());
                photoElements.Add(myCommand.Parameters["@p_map_id"].Value.ToString());
                photoElements.Add(myCommand.Parameters["@p_image"].Value.ToString());
                photoElements.Add(myCommand.Parameters["@p_resolution"].Value.ToString());
                photoElements.Add(myCommand.Parameters["@p_description"].Value.ToString());

                //return the result of the query
                //System.Windows.Forms.MessageBox.Show("Succes");
                return photoElements;

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