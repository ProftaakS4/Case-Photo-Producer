using System;
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
        private List<Product> shoppingCart;


        protected void Page_Load(object sender, EventArgs e)
        {            
            Session["shoppingCart"] = shoppingCart;
            testproducts = new List<Product>();
            testproducts.Add(testproduct1);
            testproducts.Add(testproduct2);
            testproducts.Add(testproduct3);
            testproducts.Add(testproduct4);
            foreach (Product x in testproducts)
            {
                //create button
                Button btnAddToCart = new Button();
                btnAddToCart.ID = "btnAddtoCart" + x.ID;
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
                Image Picture = new Image();
                Picture.ID = "Picture" + x.ID;
                Picture.AlternateText = "Couldn't find picture";
                Picture.ImageUrl = x.Image;
                //

                HtmlGenericControl firstControl = new HtmlGenericControl("div");
                HtmlGenericControl lastControl = new HtmlGenericControl("div");
                firstControl.InnerHtml = "<div id='thumbnailcontroll'> <div class='col-md-6' ;> <div class='thumbnail'>";
                lastControl.InnerHtml = "</div> </div> </div>";
                firstControl.Controls.Add(Picture);
                //add buttons
                firstControl.Controls.Add(btnAddToCart);
                firstControl.Controls.Add(btnSepia);
                firstControl.Controls.Add(btnBlackWhite);
                firstControl.Controls.Add(btnColor);
                pnlProduct.Controls.Add(firstControl);
                pnlProduct.Controls.Add(lastControl);

            }
        }

        void btnAddToCart_Click(object sender, EventArgs e)
        {
            
            Response.Write("<script LANGUAGE='JavaScript' >alert('test')</script>");

            //foreach(Control control in pnlProduct.Controls)
            //{
            //    if (control.GetType() == typeof(Button))
            //    {

            //    }
            //}
        }

        void btnSepia_Click(object sender, EventArgs e)
        {
        }
        void btnBlackWhite_Click(object sender, EventArgs e)
        {
        }
        void btnColor_Click(object sender, EventArgs e)
        {
        }
    }
}

   
