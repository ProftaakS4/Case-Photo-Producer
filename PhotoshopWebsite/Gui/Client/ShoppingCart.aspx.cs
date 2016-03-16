using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;

namespace PhotoshopWebsite.Gui
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        private List<Product> shoppingCart;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["shoppingCart"] != null){
                shoppingCart = Session["shoppingCart"] as List<Product>;
            }
            else
            {
                Label1.Text = "Shoppingcart is empty";

            }
        }
    }
}