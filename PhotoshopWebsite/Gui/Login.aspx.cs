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
        private DatabaseTier.Login login = new DatabaseTier.Login();


        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            loginName = tbInputEmail.Text;
            passWord = tbInputPassword.Text;
            bool result = login.loginUser(loginName, passWord);

            if (result)
            {
                Session["logindata"] = loginName;
                Controller.User newUser = new Controller.User(loginName);
                newUser = newUser.getUserData(loginName);
                Session["UserData"] = newUser;
                Response.Write("<script>alert('" + newUser.ID.ToString() + " " + newUser.Type + " " + newUser.Firstname + " " + newUser.Lastname + "')</script>");
                redirectToUserTypePage(newUser.Type);
            }
            else
            {
                Response.Write("<script>alert('Wrong emailaddress or password')</script>");
            }

        }

        private void redirectToUserTypePage(string type)
        {
            if (type == "School photographer")
            {
                Response.Redirect("~/Gui/Photographer/Account.aspx?ReturnPath=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            else if (type == "Portrait photographer")
            {
                Response.Redirect("~/Gui/Photographer/Account.aspx?ReturnPath=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            else if (type == "School- Portraitphotographe")
            {
                Response.Redirect("~/Gui/Photographer/Account.aspx?ReturnPath=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            else if (type == "Customer")
            {
                Response.Redirect("~/Gui/Client/Mainstore.aspx?ReturnPath=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            else if (type == "Financial Administration")
            {
                Response.Redirect("~/Gui/Finance/Finance.aspx?ReturnPath=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            else if (type == "Admin")
            {
                Response.Redirect("~/Gui/Admin/Mainadmin.aspx?ReturnPath=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            else
            {
                Response.Write("No type found");
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