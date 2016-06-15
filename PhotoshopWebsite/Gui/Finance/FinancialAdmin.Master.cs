using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoshopWebsite.Gui
{
    [ExcludeFromCodeCoverage]
    public partial class FinancialAdmin : System.Web.UI.MasterPage
    {
        private string clientName;
        private string pageName;
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
                LabelTitle.Text = "<h1>"+Resources.LocalizedText.my_account+"</h1>";
            }
            else if (pageName.Contains("orders"))
            {
                LabelTitle.Text = "<h1>"+Resources.LocalizedText.orders+"</h1>";
            }
            else if (pageName.Contains("stock"))
            {
                LabelTitle.Text = "<h1>"+Resources.LocalizedText.stock_overview+"</h1>";
            }
            else if (pageName.Contains("finance"))
            {
                LabelTitle.Text = "<h1>"+Resources.LocalizedText.finance+"</h1>";
            }
        }
        protected void Btnlogout_Click(object sender, EventArgs e)
        {
            Session["logindata"] = null;
            Response.Redirect(Request.RawUrl);
        }
    }
}
