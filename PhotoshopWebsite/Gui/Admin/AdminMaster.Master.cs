﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoshopWebsite.Gui.Admin
{
    [ExcludeFromCodeCoverage]
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        private String pageName;
        private String clientName;
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
            else if (pageName.Contains("clients"))
            {
                LabelTitle.Text = "<h1>My Clients</h1>";
            }
            else
            {
                LabelTitle.Text = "<h1>My Stock</h1>";
            }
        }

        protected void Btnsearch_Click(object sender, EventArgs e)
        {

        }
    }
}