using PhotoshopWebsite.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoshopWebsite.Gui
{
    [ExcludeFromCodeCoverage]
    public partial class Website : System.Web.UI.MasterPage
    {
        private string clientName;
        private User curUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserData"] != null)
            {
                curUser = Session["UserData"] as User;
            }
            if (Session["logindata"] != null)
            {
                this.clientName = Session["logindata"] as string;
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
            Labelklantnaam.Text = this.clientName;
        }
    }
}