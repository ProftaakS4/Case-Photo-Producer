using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;

namespace PhotoshopWebsite
{
    public partial class PhotoshopMaster : System.Web.UI.MasterPage
    {
        private String clientName;
        private String pageName;
        private int size = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["shoppingCart"] != null)
            {
                Dictionary<Product, int> dict  = Session["shoppingCart"] as Dictionary<Product, int>;
                foreach(Product product in dict.Keys)
                {
                    size += dict[product];
                }
                Labelquantity.Text = size.ToString();
            }
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
            if(pageName.Contains("account"))
            {
                LabelTitle.Text = "<h1>My Account</h1>";
            }
            else if(pageName.Contains("mainstore"))
            {
                LabelTitle.Text = "<h1>My Pictures</h1>";
            }
            else
            {
                LabelTitle.Text = "<h1>My Shoppingcart</h1>";
            }
        }
    }
}