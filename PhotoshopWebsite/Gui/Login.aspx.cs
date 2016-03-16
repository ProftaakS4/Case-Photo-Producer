using System;
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
        private String loginCode; 
       

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
                Response.Redirect("~/Gui/Client/Mainstore.aspx?ReturnPath=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Rememberme = true;
        }

        protected void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            loginCode = tbInputCode.Text;
            Session["loginCode"] = loginCode;
            Response.Redirect("~/Gui/Client/CreateAccount.aspx?ReturnPath=" + Server.UrlEncode(Request.Url.AbsoluteUri));
        }
    }
}