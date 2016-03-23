using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using System.Drawing;

namespace PhotoshopWebsite
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Product testproduct1 = new Product(1, "PHOTO1x2", "PAPIER", "Foto van formaat 1x2", "../Images/Visitekaart-Delahaye-IT.png", -1);
        Product testproduct2 = new Product(2, "PHOTO1x2", "Hout", "Foto van formaat 200X200", "../Images/Visitekaart-Delahaye-IT.png", -1);
        Product testproduct3 = new Product(3, "PHOTO1x2", "Steen", "Foto van formaat 300x300", "../Images/Visitekaart-Delahaye-IT.png", -1);
        Product testproduct4 = new Product(4, "PHOTO1x2", "Rubber", "Foto van formaat 500x1500", "../Images/Visitekaart-Delahaye-IT.png", -1);
        Product testproduct5 = new Product(5, "PHOTO1x2", "Rubber", "Foto van formaat 500x1500", "../Images/Visitekaart-Delahaye-IT.png", -1);


        private int number;
        private List<Product> testproducts;
        public Dictionary<Product, int> shoppingCart
        {
            get
            {
                if (!(Session["shoppingCart"] is Dictionary<Product, int>))
                {
                    Session["shoppingCart"] = new Dictionary<Product, int>();
                }

                return (Dictionary<Product, int>)Session["shoppingCart"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            testproducts = new List<Product>();
            testproducts.Add(testproduct1);
            testproducts.Add(testproduct2);
            testproducts.Add(testproduct3);
            testproducts.Add(testproduct4);
            testproducts.Add(testproduct5);
            number = testproducts.Count();
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

            HtmlGenericControl firstControl = new HtmlGenericControl("div");
            HtmlGenericControl secondControl = new HtmlGenericControl("div");
            HtmlGenericControl lastControl = new HtmlGenericControl("div");
            //adding other div elements containing discriptions
            string div;
            if (number > 4)
            {
                div = "<div class='col-sm-4'>";
            }
            else
            {
                div= "<div class='col-sm-6'>";
            }
           
            firstControl.InnerHtml = div + "<div class='thumbnail' style='max-width:330px max-height:150px;'> <img src=" + x.Image + " " + "alt=" + x.Description + ">  <div class='caption'>";


            //add buttons
            secondControl.InnerHtml = "<p>" + x.Description + "</p>";
            firstControl.Controls.Add(secondControl);
            firstControl.Controls.Add(btnAddToCart);
            firstControl.Controls.Add(btnSepia);
            firstControl.Controls.Add(btnBlackWhite);
            firstControl.Controls.Add(btnColor);
            pnlProduct.Controls.Add(firstControl);
            lastControl.InnerHtml = "</div> </div>  </div>";
            pnlProduct.Controls.Add(lastControl);


        }

        void btnAddToCart_Click(object sender, EventArgs e)
        {
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
            Response.Redirect(Request.RawUrl);
        }


        void btnSepia_Click(object sender, EventArgs e)
        {
        }

        private System.Drawing.Image grayscale(Product image)
        {
            System.Drawing.Image returnimage;
            Bitmap btm = new Bitmap(image.Image);
            for (int i = 0; i < btm.Width; i++)
            {
                for (int j = 0; j < btm.Height; j++)
                {
                    int ser = (btm.GetPixel(i, j).R + btm.GetPixel(i, j).G + btm.GetPixel(i, j).B) / 3;
                    btm.SetPixel(i, j, Color.FromArgb(ser, ser, ser));
                }
            }
            returnimage = (System.Drawing.Image)btm;
            return returnimage;
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



