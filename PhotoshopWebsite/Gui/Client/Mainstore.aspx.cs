﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;

namespace PhotoshopWebsite
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Product testproduct1 = new Product(1, "PHOTO1x2", "PAPIER", "Foto van formaat 1x2", "../Images/Shoppingcart.png", -1);
        Product testproduct2 = new Product(2, "PHOTO1x2", "Hout", "Foto van formaat Dien mam", "../Images/Shoppingcart.png", -1);
        Product testproduct3 = new Product(3, "PHOTO1x2", "Steen", "Foto van formaat Stan zien mam", "../Images/Shoppingcart.png", -1);
        Product testproduct4 = new Product(4, "PHOTO1x2", "Rubber", "Foto van formaat loek zien lul(500x1500)", "../Images/Shoppingcart.png", -1);



        private List<Product> testproducts;
        public Dictionary<Product, int> shoppingCart
        {
            get
            {
                if (!(ViewState["shoppingCart"] is Dictionary<Product, int>))
                {
                    ViewState["shoppingCart"] = new Dictionary<Product, int>();
                }

                return (Dictionary<Product, int>)ViewState["shoppingCart"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            testproducts = new List<Product>();
            testproducts.Add(testproduct1);
            testproducts.Add(testproduct2);
            testproducts.Add(testproduct3);
            testproducts.Add(testproduct4);
            foreach (Product x in testproducts)
            {
                Fillpage(x);
            }
        }


        private void Fillpage(Product x)
        {
            //create buttons
            Button btnAddToCart = new Button();
            btnAddToCart.ID = x.ID.ToString();
            btnAddToCart.CssClass = "btn btn-default";
            btnAddToCart.Click += btnAddToCart_Click;
            btnAddToCart.Height = 30;
            btnAddToCart.Text = "Order";


            Button btnSepia = new Button();
            btnSepia.ID = "btnSepia" + x.ID;
            btnSepia.CssClass = "btn btn-default";
            btnSepia.Click += btnSepia_Click;
            btnSepia.Height = 30;
            btnSepia.Text = "Sepia";

            Button btnBlackWhite = new Button();
            btnBlackWhite.ID = "btnBlackWhite" + x.ID;
            btnBlackWhite.CssClass = "btn btn-default";
            btnBlackWhite.Click += btnBlackWhite_Click;
            btnBlackWhite.Height = 30;
            btnBlackWhite.Text = "Black & White";

            Button btnColor = new Button();
            btnColor.ID = "btnColor" + x.ID;
            btnColor.CssClass = "btn btn-default";
            btnColor.Click += btnColor_Click;
            btnColor.Height = 30;
            btnColor.Text = "Color";

            //create image
            //Image Picture = new Image();
            //Picture.ID = "Picture" + x.ID;
            //Picture.AlternateText = "Couldn't find picture";
            //Picture.ImageUrl = x.Image;
            //creating 2 controlls to echo div elements into the page
            HtmlGenericControl firstControl = new HtmlGenericControl("div");
            HtmlGenericControl secondControl = new HtmlGenericControl("div");
            HtmlGenericControl lastControl = new HtmlGenericControl("div");
            //adding other div elements containing discriptions
            //firstControl.InnerHtml = "<div id='thumbnailcontroll'> <div class='col-md-6';> <div class='caption'> <h3>" + x.ID + "</h3> <p>" + x.Description + "</p> <div class='thumbnail'>";
            firstControl.InnerHtml = "<div class='col-sm-6'> <div class='thumbnail'> <img src=" + x.Image + " " + "alt=" + x.Description + ">  <div class='caption'>";
            //add image
            //firstControl.Controls.Add(Picture);

            //add buttons

            secondControl.InnerHtml = "<p>" + x.Description + "</p>";
            firstControl.Controls.Add(secondControl);
            firstControl.Controls.Add(btnAddToCart);
            firstControl.Controls.Add(btnSepia);
            firstControl.Controls.Add(btnBlackWhite);
            firstControl.Controls.Add(btnColor);
            pnlProduct.Controls.Add(firstControl);
            pnlProduct.Controls.Add(lastControl);

            lastControl.InnerHtml = "</div> </div>  </div>";
        }

        void btnAddToCart_Click(object sender, EventArgs e)
        {
            Boolean contained = false;
            Button x = sender as Button;
            string id = x.ID;
            foreach (Product product in testproducts)
            {
                if (product.ID.ToString() == id)
                {
                    if (shoppingCart.Count == 0)
                    {
                        shoppingCart.Add(product, 1);
                    }
                    else
                    {
                        if (shoppingCart.ContainsKey(product))
                        {
                            shoppingCart[product]++;
                        }
                        else
                        {
                            shoppingCart.Add(product, 1);
                        }
                    }
                }
            }
            HttpContext.Current.Session["shoppingCart"] = shoppingCart;
        }


        void btnSepia_Click(object sender, EventArgs e)
        {
            //Image image1 = new Image();
            //for (x = 0; x < image1.Width; x++)
            //{
            //    for (y = 0; y < image1.Height; y++)
            //    {
            //        Color pixelColor = image1.GetPixel(x, y);
            //        int grayScale = (int)((originalColor.R * .3) +
            //            (originalColor.G * .59) + (originalColor.B * .11));

            //        //create the color object
            //        Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);
            //        image1.SetPixel(x, y, newColor);
            //    }
            //}
        }
        void btnBlackWhite_Click(object sender, EventArgs e)
        {
            //Not yet implemented
        }
        void btnColor_Click(object sender, EventArgs e)
        {
            //Not yet implemented
        }
    }
}



