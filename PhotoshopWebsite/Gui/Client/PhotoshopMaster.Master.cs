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

namespace PhotoshopWebsite
{
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
                Dictionary<Domain.Photo, int> dict = Session["shoppingCart"] as Dictionary<Domain.Photo, int>;
                foreach (Domain.Photo photo in dict.Keys)
                {
                    size += dict[photo];
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
            Labelklantnaam.Text = "Welcome! " + " " + clientName;
            pageName = this.ContentPlaceHolder1.Page.GetType().FullName;
            if (pageName.Contains("account"))
            {
                LabelTitle.Text = "<h1>My Account</h1>";
            }
            else if (pageName.Contains("mainstore"))
            {
                LabelTitle.Text = "<h1>My Pictures</h1>";
            }
            else
            {
                LabelTitle.Text = "<h1>My Shoppingcart</h1>";
            }
        }

        protected void Btnsearch_Click(object sender, EventArgs e)
        {
            searchedPhotos = new List<Domain.Photo>();
            if(tbSearch.Text != "")
            {
                string output = s.searchPhoto(tbSearch.Text, curUser.ID);
                char[] charoutput = output.ToCharArray();
                PhotoshopWebsite.DatabaseTier.Photo photo = new PhotoshopWebsite.DatabaseTier.Photo();
                for (int i = 0; i < charoutput.Count(); i++)
                {
                    List<String> data = photo.getPhoto(charoutput[i].ToString());
                    Domain.Photo newphoto = new Domain.Photo(Convert.ToInt32(data.ElementAt(0)), Convert.ToInt32(data.ElementAt(1)), Convert.ToInt32(data.ElementAt(2)), data.ElementAt(3), data.ElementAt(4), data.ElementAt(5), data.ElementAt(6), data.ElementAt(7));
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