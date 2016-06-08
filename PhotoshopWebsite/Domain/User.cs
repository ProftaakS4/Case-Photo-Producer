using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    /// <summary>
    /// User class is the person that has signed in on the website.
    /// </summary>
    public class User
    {
        private DatabaseTier.Login DB_Login = new DatabaseTier.Login();

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
            if (DB_Login.loginUser(emailaddress, password))
            {
                returnUser = getUserData(emailaddress);
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
            int userID = DB_Login.getUserID(emailaddress);
            // when user id is validated
            if (userID != DatabaseTier.Login.NO_USER_FOUND)
            {
                this.ID = userID;
                Dictionary<string, string> userData = DB_Login.getUserData(userID);
                // when userdata is found and returned
                if (userData != null)
                {
                    // set the data for the current user
                    this.Type = userData["type"];
                    this.Firstname = userData["firstname"];
                    this.Lastname = userData["lastname"];
                    this.Streetname = userData["streetname"];
                    this.Housenumber = userData["housenumber"];
                    this.Zipcode = userData["zipcode"];
                    this.City = userData["city"];
                    this.Phonenumber = userData["phonenumber"];
                    this.IBAN = userData["iban"];
                    // return the user with its data
                    return this;
                }
            }
            return null;
        }
    }
}