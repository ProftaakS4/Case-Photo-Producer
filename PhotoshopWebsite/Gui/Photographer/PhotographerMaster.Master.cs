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
        private string clientName;
        private string pageName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logindata"] != null)
            {
                this.clientName = Session["logindata"] as string;
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
            Labelklantnaam.Text = this.clientName;
            pageName = this.ContentPlaceHolder1.Page.GetType().FullName;
            if (pageName.Contains("account"))
            {
                LabelTitle.Text = "<h1>" + Resources.LocalizedText.my_account + "</h1>";
            }
            else if (pageName.Contains("codes"))
            {
                LabelTitle.Text = "<h1>" + Resources.LocalizedText.my_codes + "</h1>";
            }
            else if (pageName.Contains("products"))
            {
                LabelTitle.Text = "<h1>" + Resources.LocalizedText.my_products + "</h1>";
            }
            else
            {
                LabelTitle.Text = "<h1>" + Resources.LocalizedText.pictures + "</h1>";
            }
        }
        protected void Btnlogout_Click(object sender, EventArgs e)
        {
            Session["logindata"] = null;
            Response.Redirect(Request.RawUrl);
        }
    }
}