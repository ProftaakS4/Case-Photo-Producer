using PhotoshopWebsite.Controller;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PhotoshopWebsite.Gui.Client.Payment
{
    public partial class Transfer : System.Web.UI.Page
    {
        private User currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            fillPaymenInfo();
        }

        void fillPaymenInfo()
        {
            if (Session["UserData"] != null)
            {
                currentUser = Session["UserData"] as User;

                Label info = new Label();
                Label accountInfo = null;
                info.Text = "Please transfer the total amount to <u>NL16RABO0123456789</u>";
                if (currentUser.ID != null)
                {
                    accountInfo = new Label();
                    accountInfo.Text = "Please refer your account ID " + currentUser.ID + " within the transaciont";
                }
                pnlTransfer.Controls.Add(info);
                pnlTransfer.Controls.Add(new LiteralControl(" <br />"));
                if (accountInfo != null)
                {
                    pnlTransfer.Controls.Add(accountInfo);
                }
            }

            
        }
    }
}