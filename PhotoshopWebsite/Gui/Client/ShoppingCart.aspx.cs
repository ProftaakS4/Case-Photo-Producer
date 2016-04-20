using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using System.Web.UI.HtmlControls;

namespace PhotoshopWebsite.Gui
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        private Dictionary<Domain.Photo, int> shoppingCart = null;
        private List<Domain.ShoppingbasketItem> shoppingbasketItems;
        private string orderName = "Photo Shop";
        private string orderPrice;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["shoppingCart"] != null)
            {
                shoppingCart = Session["shoppingCart"] as Dictionary<Domain.Photo, int>;
                Fillpage(shoppingCart);
            }

            if (Session["shoppingbasketItems"] != null)
            {
                shoppingbasketItems = (List<Domain.ShoppingbasketItem>)Session["shoppingbasketItems"];
            }

        }
        private void Fillpage(Dictionary<Domain.Photo, int> productlist)
        {
            HtmlGenericControl firstcontrol = new HtmlGenericControl();
            firstcontrol.InnerHtml = "<div class='table-responsive'>";
            HtmlGenericControl closingcontrol = new HtmlGenericControl();
            closingcontrol.InnerHtml = "</div>";
            Table MainTable = new Table();
            MainTable.CssClass = "table table-striped table-hover table-bordered";
            MainTable.Width = 600;
            TableHeaderRow MainHeaderRow = new TableHeaderRow();
            TableHeaderCell IDHeader = new TableHeaderCell();
            IDHeader.Text = "Photo ID";
            TableHeaderCell FilterHeader = new TableHeaderCell();
            FilterHeader.Text = "Filter";
            TableHeaderCell TypeHeader = new TableHeaderCell();
            TypeHeader.Text = "Product Type";
            TableHeaderCell Descriptionheader = new TableHeaderCell();
            Descriptionheader.Text = "Product Description";
            TableHeaderCell Quantityheader = new TableHeaderCell();
            Quantityheader.Text = "Quantity";
            TableHeaderCell Removeheader = new TableHeaderCell();
            Removeheader.Text = "Remove";
            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(FilterHeader);
            MainHeaderRow.Cells.Add(TypeHeader);            
            MainHeaderRow.Cells.Add(Descriptionheader);
            MainHeaderRow.Cells.Add(Quantityheader);
            MainHeaderRow.Cells.Add(Removeheader);
            MainTable.Rows.Add(MainHeaderRow);

            foreach (Domain.Photo product in productlist.Keys)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 80;
                TableCell ID = new TableCell();
                ID.Text = product.ID.ToString();
                TableCell Filter = new TableCell();
                Filter.Text = product.ID.ToString();
                TableCell Type = new TableCell();
                Type.Text = product.ID.ToString();
                TableCell Description = new TableCell();
                Description.Text = product.Description;
                TableCell Quantity = new TableCell();
                TextBox tbQuantity = new TextBox();
                tbQuantity.ID = "TextBoxRow_" + product.ID;
                tbQuantity.Text = productlist[product].ToString();
                tbQuantity.TextChanged += new EventHandler(this.Quantity_Change);
                tbQuantity.AutoPostBack = true;
                tbQuantity.MaxLength = 3;
                Quantity.Controls.Add(tbQuantity);

                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(Filter);
                MainRow.Cells.Add(Type);
                MainRow.Cells.Add(Description);
                MainRow.Cells.Add(Quantity);


                TableCell ButtonCell = new TableCell();
                CheckBox cbRemove = new CheckBox();
                cbRemove.ID = product.ID.ToString();
                cbRemove.CssClass = "checkbox";
                cbRemove.CheckedChanged += new EventHandler(this.Check_Clicked);
                cbRemove.Height = 30;
                cbRemove.AutoPostBack = true;
                cbRemove.Checked = false;

                ButtonCell.Controls.Add(cbRemove);
                MainRow.Cells.Add(ButtonCell);
                MainTable.Rows.Add(MainRow);
            }
            Button btnORder = new Button();
            btnORder.ID = "btnOrder";
            btnORder.CssClass = "btn btn-default";
            btnORder.Click += btnORder_Click;
            btnORder.Height = 30;
            btnORder.Text = "Order";

            ImageButton btnPayPal = new ImageButton();
            btnPayPal.ID = "btnPaypal";
            btnPayPal.Click += btnPayPal_Click;
            btnORder.CssClass = "btn btn-default";
            btnPayPal.Height = 30;
            btnPayPal.AlternateText = "Buy Now!";
            btnPayPal.OnClientClick = "target='blank'";
            btnPayPal.ImageUrl = "http://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif";


            pnlProduct.Controls.Add(firstcontrol);
            pnlProduct.Controls.Add(MainTable);
            pnlProduct.Controls.Add(closingcontrol);
            pnlProduct.Controls.Add(btnORder);
            pnlProduct.Controls.Add(new LiteralControl(" <br />"));
            pnlProduct.Controls.Add(new LiteralControl(" <br />"));
            pnlProduct.Controls.Add(btnPayPal);
        }

        void btnPayPal_Click(object sender, EventArgs e)
        {
            orderPrice = "0.01";
            Response.Redirect("https://www.paypal.com/us/cgi-bin/webscr?cmd=_xclick&business=stanniez%40live%2enl&item_name=" + orderName + "&currency_code=EUR&amount=" + orderPrice);
        }

        void btnORder_Click(object sender, EventArgs e)
        {
            if (shoppingCart.Count == 0)
            {
                Response.Write("<script>alert('ShoppingCart is empty, please fill your cart first')</script>");
            }
            else
            {
                PhotoshopWebsite.WebSocket.WebSocketSingleton socket = PhotoshopWebsite.WebSocket.WebSocketSingleton.GetSingleton();

                int quantity = 1;
                string photoIDQualtityType = "";
                
                if(shoppingCart != null)
                {
                    foreach(Domain.Photo photo in shoppingCart.Keys.ToList())
                    {
                        if (shoppingCart.ContainsKey(photo))
                        {
                            quantity = shoppingCart[photo];
                        }
                        photoIDQualtityType = photo.ID + ";" + Convert.ToString(quantity) + "#" + getPhotoType(photo.ID.ToString());
                        socket.sendData(photoIDQualtityType);
                    }                    
                }
                //Order NUMMERS doorsturen
            }
            //not yet implemented 
        }

        private string getPhotoType(string photoID)
        {
            foreach(Domain.ShoppingbasketItem item in shoppingbasketItems)
            {
                if (item.photoID == photoID)
                {
                    return item.type;
                }               
            }
            return "Color";
        }

        private void Check_Clicked(object sender, EventArgs e)
        {
            CheckBox cbremove = sender as CheckBox;
            foreach (Domain.Photo product in shoppingCart.Keys.ToList())
            {
                if (product.ID.ToString() == cbremove.ID)
                {
                    shoppingCart.Remove(product);
                    Session["shoppingCart"] = shoppingCart;
                    Response.Redirect(Request.RawUrl);
                }
            }
        }
        private void Quantity_Change(object sender, EventArgs e)
        {
            TextBox tbQuantity = sender as TextBox;
            foreach (Domain.Photo product in shoppingCart.Keys.ToList())
            {
                if ("TextBoxRow_" + product.ID.ToString() == tbQuantity.ID.ToString())
                {
                    if (int.Parse(tbQuantity.Text) > 0)
                    {
                        shoppingCart[product] = int.Parse(tbQuantity.Text);
                    }
                    else
                    {
                        shoppingCart.Remove(product);
                    }
                    Session["shoppingCart"] = shoppingCart;
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        

    }


}
