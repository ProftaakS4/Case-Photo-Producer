using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    /// <summary>
    /// User class is the person that has signed in on the website.
    /// </summary>
    public class User
    {
        private DatabaseTier.QueryDatabase database = new DatabaseTier.QueryDatabase();

        public int ID { get; set; }
        public string Type { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Streetname { get; set; }
        public string Housenumber { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Phonenumber { get; set; }
        public string IBAN { get; set; }
        public string Emailaddress { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// create an user without the password
        /// </summary>
        /// <param name="id">id from the user</param>
        /// <param name="type">type of the user</param>
        /// <param name="firstname">firstname of the user</param>
        /// <param name="lastname">lastname of the user</param>
        /// <param name="streetname">streetname of the user</param>
        /// <param name="housenumber">housenumber of the user</param>
        /// <param name="zipcode">zipcode of the user</param>
        /// <param name="city">city of the user</param>
        /// <param name="phonenumber">phonenumber of the user</param>
        /// <param name="iban">iban form the user</param>
        /// <param name="emailaddress">emailaddress of the user</param>
        public User(int id, string type, string firstname, string lastname, string streetname, string housenumber, string zipcode, string city, string phonenumber, string iban, string emailaddress)
        {
            this.ID = id;
            this.Type = type;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Streetname = streetname;
            this.Housenumber = housenumber;
            this.Zipcode = zipcode;
            this.City = city;
            this.Phonenumber = phonenumber;
            this.IBAN = iban;
            this.Emailaddress = emailaddress;
        }

        /// <summary>
        /// create an user with all possible variables
        /// </summary>
        /// <param name="id">id from the user</param>
        /// <param name="type">type of the user</param>
        /// <param name="firstname">firstname of the user</param>
        /// <param name="lastname">lastname of the user</param>
        /// <param name="streetname">streetname of the user</param>
        /// <param name="housenumber">housenumber of the user</param>
        /// <param name="zipcode">zipcode of the user</param>
        /// <param name="city">city of the user</param>
        /// <param name="phonenumber">phonenumber of the user</param>
        /// <param name="iban">iban form the user</param>
        /// <param name="emailaddress">emailaddress of the user</param>
        /// <param name="password">password of the user</param>
        public User(string firstname, string lastname, string streetname, string housenumber, string zipcode, string city, string phonenumber, string iban, string emailaddress, string password)
        {
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Streetname = streetname;
            this.Housenumber = housenumber;
            this.Zipcode = zipcode;
            this.City = city;
            this.Phonenumber = phonenumber;
            this.IBAN = iban;
            this.Emailaddress = emailaddress;
            this.Password = password;
        }

        /// <summary>
        /// create a user using the emailaddress to search for the user's data
        /// </summary>
        /// <param name="emailaddress">emailaddress of the user</param>
        public User(string emailaddress)
        {
            this.Emailaddress = emailaddress;
        }


        /// <summary>
        /// login the user, when succesfull retrieve its data from the database
        /// </summary>
        /// <param name="emailaddress">emailaddress of the user</param>
        /// <param name="password">password of the user</param>
        /// <returns>the logged in user</returns>
        public User loginUser(string emailaddress, string password)
        {
            User returnUser = null;
            // when login credentials are verified
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_account_Email", new string[] { "string", emailaddress });
            parameters.Add("p_password", new string[] { "string", password });
            DataTable dt = this.database.CallProcedure("getUserPassword", parameters);

            if (dt.Rows.Count != 0)
            {
                returnUser = this.getUserData(emailaddress);
            }
            return returnUser;
        }

        /// <summary>
        /// get userdata of the user corresponding to email address
        /// </summary>
        /// <param name="emailaddress">emailaddress from the user</param>
        /// <returns>user from the corresponding email address</returns>
        public User getUserData(string emailaddress)
        {
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_emailaddress", new string[] { "string", emailaddress });
            DataTable dt = this.database.CallProcedure("getUserID", parameters);
            int userID = int.Parse(dt.Rows[0][0].ToString());
            // when user id is validated
            if (userID != -1)
            {
                this.ID = userID;
                List<ProductPerPhotographer> temp = new List<ProductPerPhotographer>();
                parameters = new Dictionary<string, string[]>();
                parameters.Add("p_id", new string[] { "int", userID.ToString() });
                dt = this.database.CallProcedure("getUserInformation", parameters);
                // when userdata is found and returned
                if (dt.Rows.Count != 0)
                {
                    // set the data for the current user
                    this.Type = dt.Rows[0][0].ToString();
                    this.Firstname = dt.Rows[0][1].ToString();
                    this.Lastname = dt.Rows[0][2].ToString();
                    this.Streetname = dt.Rows[0][3].ToString();
                    this.Housenumber = dt.Rows[0][4].ToString();
                    this.Zipcode = dt.Rows[0][5].ToString();
                    this.City = dt.Rows[0][6].ToString();
                    this.Phonenumber = dt.Rows[0][7].ToString();
                    this.IBAN = dt.Rows[0][8].ToString();
                    // return the user with its data
                    return this;
                }
            }
            return null;
        }
    }
}