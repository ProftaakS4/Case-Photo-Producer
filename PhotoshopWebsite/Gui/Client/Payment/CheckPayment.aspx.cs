using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoshopWebsite.Gui.Client.Payment
{
    public partial class CheckPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", "3;URL=../Mainstore.aspx");
            Label wait = new Label();
            wait.Font.Bold = true;
            wait.Text = "<u>Please wait 3 seconds while the payment is being verified...</u>";
            pnlWait.Controls.Add(wait);
            Session["shoppingCart"] = new List<Domain.ShoppingbasketItem>();
        }
    }
}