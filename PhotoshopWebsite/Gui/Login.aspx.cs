using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace PhotoshopWebsite
{
    [ExcludeFromCodeCoverage]
    public partial class Login : System.Web.UI.Page
    {
        private String loginName;
        private String passWord;
        private Boolean rememberMe = false;
        private Boolean LoginSuccess = true;
        private String loginCode;
        HttpCookie _userInfoCookies;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _userInfoCookies = Request.Cookies["Userinfo"];
                if (_userInfoCookies != null)
                {
                    loginName = _userInfoCookies["loginName"];
                    passWord = _userInfoCookies["passWord"];
                    tbInputEmail.Text = loginName;
                    tbInputPassword.Text = passWord;
                }
            }
        }       
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string loginname = tbInputEmail.Text;
            string password = tbInputPassword.Text;
            navigateThroughAuthentication(loginname, password);
        }

        private void navigateThroughAuthentication(string loginname, string password)
        {
            Controller.User userWithNoData = new Controller.User(loginname);
            Controller.User userWithData = userWithNoData.loginUser(loginname, password);
            if (userWithData != null)
            {
                Session["logindata"] = loginName;
                Session["UserData"] = userWithData;
                if (rememberMe && _userInfoCookies == null)
                {
                    createPersistentCookie(loginName, passWord);
                }
                redirectToUserTypePage(userWithData.Type);
            }
            else
            {
                Response.Write("<script>alert('Wrong emailaddress or password')</script>");
            }

        }

        private void createPersistentCookie(string loginName, string passWord)
        {
            HttpCookie _userInfoCookies = new HttpCookie("Userinfo");

            _userInfoCookies["loginName"] = loginName;
            _userInfoCookies["passWord"] = passWord;
            _userInfoCookies["Expire"] = "5 Days";

            _userInfoCookies.Expires = DateTime.Now.AddDays(5);
            Response.Cookies.Add(_userInfoCookies);
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
            rememberMe = !rememberMe;

        }

        protected void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            loginCode = tbInputCode.Text;

            if (loginCode == String.Empty)
            {
                Response.Write("<script>alert('Pease enter a login code')</script>");
            }
            else
            {
                Session["loginCode"] = loginCode;
                Response.Redirect("~/Gui/Client/CreateAccount.aspx?ReturnPath=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            
        }
    }
}