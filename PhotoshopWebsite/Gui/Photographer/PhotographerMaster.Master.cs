using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoshopWebsite.Gui.Photographer
{
    [ExcludeFromCodeCoverage]
    public partial class PhotographerMaster : System.Web.UI.MasterPage
    {
        private String clientName;
        private String pageName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logindata"] != null)
            {
                clientName = Session["logindata"] as String;
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
            Labelklantnaam.Text = clientName;
            pageName = this.ContentPlaceHolder1.Page.GetType().FullName;
            if (pageName.Contains("account"))
            {
                LabelTitle.Text = "<h1>My Account</h1>";
            }
            else if (pageName.Contains("codes"))
            {
                LabelTitle.Text = "<h1>My Codes</h1>";
            }
            else if (pageName.Contains("products"))
            {
                LabelTitle.Text = "<h1>My products</h1>";
            }
            else
            {
                LabelTitle.Text = "<h1>My Pictures</h1>";
            }
        }
        protected void Btnlogout_Click(object sender, EventArgs e)
        {
            Session["logindata"] = null;
            Response.Redirect(Request.RawUrl);
        }
    }
}