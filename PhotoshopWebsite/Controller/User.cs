using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class User
    {
        private DatabaseTier.Login login = new DatabaseTier.Login();

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

        public User(int ID, String Type, String Firstname, String Lastname, String Streetname, String Housenumber, String Zipcode, String City, String Phonenumber, String IBAN, String Emailaddress)
        {
            this.ID = ID;
            this.Type = Type;
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Streetname = Streetname;
            this.Housenumber = Housenumber;
            this.Zipcode = Zipcode;
            this.City = City;
            this.Phonenumber = Phonenumber;
            this.IBAN = IBAN;
            this.Emailaddress = Emailaddress;
        }
        public User(String Firstname, String Lastname, String Streetname, String Housenumber, String Zipcode, String City, String Phonenumber, String IBAN, String Emailaddress, String password)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Streetname = Streetname;
            this.Housenumber = Housenumber;
            this.Zipcode = Zipcode;
            this.City = City;
            this.Phonenumber = Phonenumber;
            this.IBAN = IBAN;
            this.Emailaddress = Emailaddress;
            this.Password = password;
        }

        public User(string emailaddress)
        {
            this.Emailaddress = emailaddress;
        }

        public User loginUser(string emailaddress, string password)
        {
           if(login.loginUser(emailaddress, password))
            {
                if(getUserData(emailaddress) != null)
                {
                    return this;
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// get userdata of the user corresponding to email address
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <returns></returns>
        public User getUserData(string emailaddress)
        {
            int userID = login.getUserID(emailaddress);
            // when user id is validated
            if (userID != -1)
            {
                this.ID = userID;
                Dictionary<string, string> userData = login.getUserData(userID);
                // when userdata is found and returned
                if (userData != null)
                {
                    // set the data for the current user
                    if (userData.ContainsKey("type")) { this.Type = userData["type"]; }
                    else if (userData.ContainsKey("firstname")) { this.Firstname = userData["firstname"]; }
                    else if (userData.ContainsKey("lastname")) { this.Lastname = userData["lastname"]; }
                    else if (userData.ContainsKey("streetname")) { this.Streetname = userData["streetname"]; }
                    else if (userData.ContainsKey("housenumber")) { this.Housenumber = userData["housenumber"]; }
                    else if (userData.ContainsKey("zipcode")) { this.Zipcode = userData["zipcode"]; }
                    else if (userData.ContainsKey("city")) { this.City = userData["city"]; }
                    else if (userData.ContainsKey("phonenumber")) { this.Phonenumber = userData["phonenumber"]; }
                    else if (userData.ContainsKey("iban")) { this.IBAN = userData["iban"]; }
                    // return the user with its data
                    return this;
                }
                //System.Windows.Forms.MessageBox.Show(Firstname + Lastname + Streetname + Housenumber + Zipcode + City + Phonenumber + IBAN);
            }
            return null;
        }
    }
}