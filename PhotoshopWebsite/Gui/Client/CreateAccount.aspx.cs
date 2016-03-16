using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;

namespace PhotoshopWebsite.Gui.Client
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        private String loginCode;
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
        }
    }
}