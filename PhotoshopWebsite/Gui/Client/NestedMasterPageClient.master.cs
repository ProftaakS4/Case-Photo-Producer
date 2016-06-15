using PhotoshopWebsite.Controller;
using PhotoshopWebsite.DatabaseTier;
using PhotoshopWebsite.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoshopWebsite.Gui.Client
{
    [ExcludeFromCodeCoverage]
    public partial class NestedMasterPageClient : System.Web.UI.MasterPage
    {
        private QueryDatabase database = new QueryDatabase();
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

                //pageName = this.ContentPlaceHolder1.Page.GetType().FullName;
                pageName = "";
                Label LabelTitle = (Label)Master.FindControl("LabelTitle");
                if (pageName.Contains("account"))
                {

                    LabelTitle.Text = "<h1>"+Resources.LocalizedText.my_account+"</h1>";
                }
                else if (pageName.Contains("mainstore"))
                {
                    LabelTitle.Text = "<h1>"+Resources.LocalizedText.pictures+"</h1>";
                }
                else if (pageName.Contains("group"))
                {
                    LabelTitle.Text = "<h1>"+Resources.LocalizedText.group_pictures+"</h1>";
                }
                else if (pageName.Contains("google"))
                {
                    LabelTitle.Text = "<h1>"+Resources.LocalizedText.google_checkout_payment+"</h1>";
                }
                else if (pageName.Contains("ideal"))
                {
                    LabelTitle.Text = "<h1>"+Resources.LocalizedText.ideal_payment+"</h1>";
                }
                else if (pageName.Contains("ogone"))
                {
                    LabelTitle.Text = "<h1>"+Resources.LocalizedText.ogone_payment+"</h1>";
                }
                else if (pageName.Contains("transfer"))
                {
                    LabelTitle.Text = "<h1>"+Resources.LocalizedText.bank_transfer+"</h1>";
                }
                else if (pageName.Contains("checkpayment"))
                {
                    LabelTitle.Text = "<h1>"+Resources.LocalizedText.payment_check+"</h1>";
                }
                else if (pageName.Contains("shoppingcart"))
                {
                    LabelTitle.Text = "<h1>"+Resources.LocalizedText.shopping_cart+"</h1>";
                }
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
                    Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
                    parameters.Add("p_searchedText", new string[] { "string", tbSearch.Text });
                    DataTable dt = database.CallProcedure("searchGroupPhoto", parameters);
                    PhotoController photo = new PhotoController();
                    foreach (DataRow row in dt.Rows)
                    {
                        Domain.Photo newphoto = photo.getPhoto(int.Parse(row[0].ToString()));
                        searchedPhotos.Add(newphoto);
                    }
                    Session["SearchedPhotos"] = searchedPhotos;
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    Response.Write("<script>alert('"+Resources.LocalizedText.error_enter_search_term+"')</script>");
                }
            }
            else
            {
                searchedPhotos = new List<Domain.Photo>();
                if (tbSearch.Text != "")
                {
                    Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
                    parameters.Add("p_user_ID", new string[] { "int", curUser.ID.ToString() });
                    parameters.Add("p_searchedText", new string[] { "string", tbSearch.Text });
                    DataTable dt = database.CallProcedure("searchPhoto", parameters);
                    PhotoController photo = new PhotoController();
                    foreach (DataRow row in dt.Rows)
                    {
                        Domain.Photo newphoto = photo.getPhoto(int.Parse(row[0].ToString()));
                        searchedPhotos.Add(newphoto);
                    }
                    Session["SearchedPhotos"] = searchedPhotos;
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    Response.Write("<script>alert('"+Resources.LocalizedText.error_enter_search_term+"')</script>");
                }
            }
        }
    }
}