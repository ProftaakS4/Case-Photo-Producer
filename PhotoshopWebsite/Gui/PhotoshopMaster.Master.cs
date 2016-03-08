using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoshopWebsite
{
    public partial class PhotoshopMaster : System.Web.UI.MasterPage
    {
        String klantnaam;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logindata"] != null)
            {
                klantnaam = Session["logindata"] as String;
            }
            Labelklantnaam.Text = "Welcome! " + " " + klantnaam ;
        }
    }
}