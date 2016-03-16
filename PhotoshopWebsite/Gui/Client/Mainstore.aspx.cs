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
                Button btnAddToCart = new Button();
                btnAddToCart.ID = "btAddtoCart" + x.ID;
                btnAddToCart.CssClass = "btn btn-default;";
                btnAddToCart.Click += btnAddToCart_Click;
                HtmlGenericControl newControl = new HtmlGenericControl("div");
                newControl.ID = "NewControl";
                newControl.InnerHtml = " <div class='col-md-6' ;> <div class='thumbnail'> <img src='" + x.Image + "'alt='Unable to load image, please contact admin'> <div class='caption'> <h3>" + x.ID + "</h3> <p>" + x.Description + "</p>   <asp:LinkButton ID='btnOrder'" + x.ID + "'  OnClick='btnAddToCart_Click' class='btn btn-default' runat='server'>Add to Card</asp:LinkButton><asp:LinkButton ID='Color'" + x.ID + "' class='btn btn-default' runat='server'>Show Color</asp:LinkButton> <asp:LinkButton ID='Sepia'" + x.ID + "' class='btn btn-default' runat='server'>Show Sepia</asp:LinkButton> <asp:LinkButton ID='Black&White'" + x.ID + "' class='btn btn-default' runat='server'>Show Black and white</asp:LinkButton> </div> </div>";
                pnlProduct.Controls.Add(newControl);
            }   
        }

        void btnAddToCart_Click(object sender, EventArgs e)
        {
        }
    }
}

   
