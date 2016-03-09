using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoshopWebsite.Gui.Photographer
{
    public partial class PhotographerMaster : System.Web.UI.MasterPage
    {
        private String clientName;
        private String pageName;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["logindata"] != null)
            //{
            //    clientName = Session["logindata"] as String;
            //}
            //else
            //{
            //    Response.Redirect("../Login.aspx");
            //}
            Labelklantnaam.Text = "Welcome! " + " " + clientName;
            pageName = this.ContentPlaceHolder1.Page.GetType().FullName;
            if (pageName.Contains("account"))
            {
                LabelTitle.Text = "<h1>My Account</h1>";
            }
            else if (pageName.Contains("clients"))
            {
                LabelTitle.Text = "<h1>My Clients</h1>";
            }
            else if (pageName.Contains("selection"))
            {
                LabelTitle.Text = "<h1>My selection</h1>";
            }
            else
            {
                LabelTitle.Text = "<h1>My Pictures</h1>";
            }
        }
    }
}