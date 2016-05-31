using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class AccountCreationController
    {
        private DatabaseTier.AccountCreation DB = new DatabaseTier.AccountCreation();

        public AccountCreationController()
        {

        }


        public DataTable createAccountandgetinformation(string customer, string firstname, string lastname, string streetname, string housenumber, string zipcode, string city, string phonenumber, string iban, string emailadress, string password)
        {
            DataTable dt = DB.createAccount(customer,firstname,lastname,streetname,housenumber,zipcode,city,phonenumber,iban,emailadress,password);
            return dt;
        }
    }
}