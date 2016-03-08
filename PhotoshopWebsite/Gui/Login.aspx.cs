﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PhotoshopWebsite
{
    public partial class Login : System.Web.UI.Page
    {
        private String loginName;
        private String passWord;
        private Boolean Rememberme = false;
        private Boolean LoginSuccess = true; 
       

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            loginName = tbInputEmail.Text;
            passWord = tbInputPassword.Text;
            if (LoginSuccess)
            {
                Session["logindata"] = loginName;
                Response.Redirect("Mainstore.aspx");
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Rememberme = true;
        }
    }
}