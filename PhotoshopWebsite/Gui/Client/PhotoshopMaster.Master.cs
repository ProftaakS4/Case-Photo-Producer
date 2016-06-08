using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using PhotoshopWebsite.DatabaseTier;
using PhotoshopWebsite.Domain;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace PhotoshopWebsite
{
    [ExcludeFromCodeCoverage]
    public partial class PhotoshopMaster : System.Web.UI.MasterPage
    {
        private Search s = new Search();
        private String clientName;
        private String pageName;
        private User curUser;
        private List<PhotoshopWebsite.Domain.Photo> searchedPhotos;
        private int size = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SearchedPhotos"] = null;
            }
            if (Session["UserData"] != null)
            {
                curUser = Session["UserData"] as User;
            }
            if (HttpContext.Current.Session["shoppingCart"] != null)
            {
                List<Domain.ShoppingbasketItem> list = Session["shoppingCart"] as List<Domain.ShoppingbasketItem>;
                foreach (ShoppingbasketItem item in list)
                {
                    size += item.Quantity;
                }
                Labelquantity.Text = size.ToString();
            }
            if (Session["logindata"] != null)
            {
                clientName = Session["logindata"] as String;
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
            Labelklantnaam.Text = clientName;
            pageName = this.ContentPlaceHolder1.Page.GetType().FullName;

            if (pageName.Contains("account"))
            {
                LabelTitle.Text = "<h1>My Account</h1>";
            }
            else if (pageName.Contains("mainstore"))
            {
                LabelTitle.Text = "<h1>My Pictures</h1>";
            }
            else if (pageName.Contains("group"))
            {
                LabelTitle.Text = "<h1>Group Pictures</h1>";
            }
            else if (pageName.Contains("google"))
            {
                LabelTitle.Text = "<h1>Google-Checkout Payment</h1>";
            }
            else if (pageName.Contains("ideal"))
            {
                LabelTitle.Text = "<h1>iDeal Payment</h1>";
            }
            else if (pageName.Contains("ogone"))
            {
                LabelTitle.Text = "<h1>Ogone Payment</h1>";
            }
            else if (pageName.Contains("transfer"))
            {
                LabelTitle.Text = "<h1>Money Tranfer Payment</h1>";
            }
            else if (pageName.Contains("checkpayment"))
            {
                LabelTitle.Text = "<h1>Payment Check</h1>";
            }
            else if (pageName.Contains("shoppingcart"))
            {
                LabelTitle.Text = "<h1>Shopping Cart</h1>";
            }
        }
        protected void Btnlogout_Click(object sender, EventArgs e)
        {
            Session["logindata"] = null;
            Response.Redirect(Request.RawUrl);
        }


        protected void Btnsearch_Click(object sender, EventArgs e)
        {
            if (pageName.Contains("group"))
            {
                searchedPhotos = new List<Domain.Photo>();
                if (tbSearch.Text != "")
                {
                    DataTable output = s.searchGroupPhoto(tbSearch.Text);
                    PhotoController photo = new PhotoController();
                    foreach (DataRow row in output.Rows)
                    {
                        Domain.Photo newphoto = photo.getPhoto(int.Parse(row[0].ToString()));
                        searchedPhotos.Add(newphoto);
                    }
                    Session["SearchedPhotos"] = searchedPhotos;
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    Response.Write("<script>alert('Voer zoekcriteria in')</script>");
                }
            }
            else
            {
                searchedPhotos = new List<Domain.Photo>();
                if (tbSearch.Text != "")
                {
                    DataTable output = s.searchPhoto(tbSearch.Text, curUser.ID);
                    PhotoController photo = new PhotoController();
                    foreach (DataRow row in output.Rows)
                    {
                        Domain.Photo newphoto = photo.getPhoto(int.Parse(row[0].ToString()));
                        searchedPhotos.Add(newphoto);
                    }
                    Session["SearchedPhotos"] = searchedPhotos;
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    Response.Write("<script>alert('Voer zoekcriteria in')</script>");
                }
            }
        }
    }
}