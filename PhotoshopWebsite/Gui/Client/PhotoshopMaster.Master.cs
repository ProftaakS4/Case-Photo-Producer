﻿using System;
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
            if(Session["UserData"] != null)
            {
                curUser = Session["UserData"] as User;
            }
            if (HttpContext.Current.Session["shoppingCart"] != null)
            {
                Dictionary<Product, int> dict = Session["shoppingCart"] as Dictionary<Product, int>;
                foreach (Product product in dict.Keys)
                {
                    size += dict[product];
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
            String output = s.searchPhoto(tbSearch.Text, curUser.ID);
            PhotoshopWebsite.DatabaseTier.Photo photo = new PhotoshopWebsite.DatabaseTier.Photo();
            List<String> data = photo.getPhoto(output);
        }
    }
}