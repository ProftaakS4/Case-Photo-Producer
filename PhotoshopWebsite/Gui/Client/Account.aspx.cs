using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using System.Web.UI.HtmlControls;

namespace PhotoshopWebsite.Gui.Client
{
    public partial class Account : System.Web.UI.Page
    {
        User testuser = new User(1,"client","Loek","Delahaye","Voogdijstraat","5","6041EX","Roermond","1235325","NLRAB012309814","Loekdelaaye@gmail.com","testpw");
        protected void Page_Load(object sender, EventArgs e)
        {
            tbFirstname.Text = testuser.Firstname;
            tbLastname.Text = testuser.Lastname;
            tbStreetname.Text = testuser.Streetname;
            tbHousenumber.Text = testuser.Housenumber;
            tbZipcode.Text = testuser.Zipcode;
            tbCity.Text = testuser.City;
            tbPhoneNumber.Text = testuser.Phonenumber;
            tbIBAN.Text = testuser.IBAN;
            tbEMail.Text = testuser.Emailaddress;
            tbPassword.Text = testuser.Password;
        }
    }
}