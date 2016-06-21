using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using PhotoshopWebsite.Domain;
using System.Diagnostics.CodeAnalysis;
using System.Data;

namespace PhotoshopWebsite.Gui.Client
{
    [ExcludeFromCodeCoverage]
    public partial class CreateAccount : System.Web.UI.Page
    {
        private string loginCode;

        AccountCreationController acc = new AccountCreationController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loginCode"] != null)
            {
                this.loginCode = Session["loginCode"] as string;
                tbloginCode.Text = this.loginCode;
                tbloginCode.ReadOnly = true;
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
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
                string customer = "Customer";
                DataTable dt = this.acc.CreateAccountandgetinformation(customer, newUser.Firstname, newUser.Lastname, newUser.Streetname, newUser.Housenumber, newUser.Zipcode, newUser.City, newUser.Phonenumber, newUser.IBAN, newUser.Emailaddress, newUser.Password);
                int id = this.acc.GetAccountId(newUser.Emailaddress);
                this.acc.insertAccountLoginCode(id, Convert.ToInt32(this.loginCode));
                Response.Redirect("../Login.aspx");
            }
            else
            {
                Response.Write("<script>alert('" + Resources.LocalizedText.error_wrong_email + "')</script>");
            }

        }
    }
}