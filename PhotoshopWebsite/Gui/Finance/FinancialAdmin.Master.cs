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
            Labelklantnaam.Text = "Welcome! " + " " + clientName;
            pageName = this.ContentPlaceHolder1.Page.GetType().FullName;
            if (pageName.Contains("account"))
            {
                LabelTitle.Text = "<h1>My Account</h1>";
            }
            else if (pageName.Contains("orders"))
            {
                LabelTitle.Text = "<h1>Orders</h1>";
            }
            else if (pageName.Contains("stock"))
            {
                LabelTitle.Text = "<h1>Stock</h1>";
            }
            else if (pageName.Contains("finance"))
            {
                LabelTitle.Text = "<h1>Finance</h1>";
            }
        }
        
        }
    }
