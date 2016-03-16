using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class User
    {
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

        public User(int ID,String Type, String Firstname,String Lastname,String Streetname,String Housenumber, String Zipcode, String City, String Phonenumber,String IBAN,String Emailaddress, String Password)
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
            this.Password = Password;
        }
        public User(String Firstname,String Lastname,String Streetname,String Housenumber, String Zipcode, String City, String Phonenumber,String IBAN,String Emailaddress, String Password)
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
            this.Password = Password;
        }
    }
}