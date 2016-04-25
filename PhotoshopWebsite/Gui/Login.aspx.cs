using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace PhotoshopWebsite
{
    [ExcludeFromCodeCoverage]
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
            // check is emailaddress and password are legit
            Controller.User userWithNoData = new Controller.User(loginName);
            Controller.User userWithData = userWithNoData.loginUser(loginName, passWord);

            if (userWithData != null)
            {
                // save user's login name into session
                Session["logindata"] = loginName;
                Session["UserData"] = userWithData;
                redirectToUserTypePage(userWithData.Type);
                //Response.Write("<script>alert('" + newUser.ID.ToString() + " " + newUser.Type + " " + newUser.Firstname + " " + newUser.Lastname + "')</script>");
            }
            else
            {
                Response.Write("<script>alert('Wrong emailaddress or password')</script>");
            }

        }

        /// <summary>
        /// this method redirects the user by type to it's allowed page. When not type found the browser will give feedback
        /// </summary>
        /// <param name="type"></param> the type of the user
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
            else if (type == "School- Portraitphotographer")
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
                Response.Write("<script>alert('Unknown user type')</script>");
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