using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using System.Diagnostics.CodeAnalysis;
using System.Data;

namespace PhotoshopWebsite.Gui.Client
{
    [ExcludeFromCodeCoverage]
    public partial class CreateAccount : System.Web.UI.Page
    {
        private String loginCode;

        AccountCreationController acc = new AccountCreationController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loginCode"] != null)
            {
                loginCode = Session["loginCode"] as String;
                tbloginCode.Text = loginCode;
                tbloginCode.ReadOnly = true;
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            User newUser = new User(
                tbFirstname.Text,
                tbLastname.Text,
                tbStreetname.Text,
                tbHousenumber.Text,
                tbZipcode.Text,
                tbCity.Text,
                tbPhoneNumber.Text,
                tbIBAN.Text,
                tbEMail.Text,
                tbPassword.Text);
            if (tbEMail.Text.Contains("@"))
            {
                DataTable dt = acc.createAccountandgetinformation("Customer", newUser.Firstname, newUser.Lastname, newUser.Streetname, newUser.Housenumber, newUser.Zipcode, newUser.City, newUser.Phonenumber, newUser.IBAN, newUser.Emailaddress, newUser.Password);
                int id = acc.getAccountId(newUser.Emailaddress);
                acc.insertAccountLoginCode(id, Convert.ToInt32(loginCode));
                Response.Redirect("../Login.aspx");
            }
            else
            {
                Response.Write("<script>alert('Please enter a valid Email adress')</script>");
            }
            
        }
    }
}