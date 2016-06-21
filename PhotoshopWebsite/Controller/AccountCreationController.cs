using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class AccountCreationController
    {
        private DatabaseTier.QueryDatabase database = new DatabaseTier.QueryDatabase();

        public AccountCreationController()
        {
            // empty constructor
        }
        public int GetAccountId(string email)
        {
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_email", new string[] { "string", email });
            DataTable dt = this.database.CallProcedure("getAccountWithEmail", parameters);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public bool insertAccountLoginCode(int accountId, int loginCode)
        {
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_account_ID", new string[] { "int", accountId.ToString() });
            parameters.Add("p_logincode", new string[] { "int", loginCode.ToString() });
            DataTable dt = this.database.CallProcedure("insertAccountLoginCode", parameters);
            return dt.Rows.Count != 0;
        }

        public DataTable CreateAccountandgetinformation(string customer, string firstname, string lastname, string streetname, string housenumber, string zipcode, string city, string phonenumber, string iban, string emailadress, string password)
        {
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_type", new string[] { "string", customer });
            parameters.Add("p_firstname", new string[] { "string", firstname });
            parameters.Add("p_Lastname", new string[] { "string", lastname });
            parameters.Add("p_streetname", new string[] { "string", streetname });
            parameters.Add("p_housenumber", new string[] { "string", housenumber });
            parameters.Add("p_zipcode", new string[] { "string", zipcode });
            parameters.Add("p_city", new string[] { "string", city });
            parameters.Add("p_phonenumber", new string[] { "string", phonenumber });
            parameters.Add("p_iban", new string[] { "string", iban });
            parameters.Add("p_emailaddress", new string[] { "string", emailadress });
            parameters.Add("p_password", new string[] { "string", password });
            DataTable dt = this.database.CallProcedure("insertUser", parameters);
            return dt;
        }
    }
}