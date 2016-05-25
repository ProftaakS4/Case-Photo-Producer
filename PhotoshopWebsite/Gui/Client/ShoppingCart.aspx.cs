using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;

namespace PhotoshopWebsite.Gui
{
    [ExcludeFromCodeCoverage]
    public partial class ShoppingCart : System.Web.UI.Page
    {
        private string orderName = "Photo Shop";
        private string orderPrice;

        public List<Domain.ShoppingbasketItem> shoppingCart
        {
            get
            {
                if (!(Session["shoppingCart"] is List<Domain.ShoppingbasketItem>))
                {
                    Session["shoppingCart"] = new List<Domain.ShoppingbasketItem>();
                }

                return Session["shoppingCart"] as List<Domain.ShoppingbasketItem>;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Fillpage();
        }
        private void Fillpage()
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
            Descriptionheader.Text = "Photo Description";
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

            foreach (Domain.ShoppingbasketItem item in shoppingCart)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 80;
                TableCell ID = new TableCell();
                ID.Text = item.photoID.ToString();
                TableCell Filter = new TableCell();
                Filter.Text = item.filterType.ToString();
                TableCell Type = new TableCell();
                Type.Text = item.product.ToString();
                TableCell Description = new TableCell();
                Description.Text = item.description;
                TableCell Quantity = new TableCell();
                TextBox tbQuantity = new TextBox();
                tbQuantity.ID = item.filterType.ToString() + item.photoID.ToString();
                tbQuantity.Text = item.quantity.ToString();
                tbQuantity.TextChanged += Quantity_Change;
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
                cbRemove.ID = item.GetHashCode().ToString();
                cbRemove.CssClass = "checkbox";
                cbRemove.CheckedChanged += Check_Clicked;
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

            pnlProduct.Controls.Add(firstcontrol);
            pnlProduct.Controls.Add(MainTable);
            pnlProduct.Controls.Add(closingcontrol);
            pnlProduct.Controls.Add(btnORder);
            pnlProduct.Controls.Add(new LiteralControl(" <br />"));
            pnlProduct.Controls.Add(new LiteralControl(" <br />"));

            pnlProduct.Controls.Add(new LiteralControl(" <br />"));
            pnlProduct.Controls.Add(new LiteralControl(" <br />"));
        }

        void createPaymentPanel()
        {
            ImageButton btnTransfer = new ImageButton();
            btnTransfer.ID = "btnTransfer";
            btnTransfer.Click += btnTransfer_Click;
            btnTransfer.Height = 30;
            btnTransfer.Width = 90;
            btnTransfer.AlternateText = "Pay by Money Transfer";
            btnTransfer.ImageUrl = "http://www.glerups.nl/media/wysiwyg/infortis/ultimo/custom/overboeking.jpg";

            ImageButton btnPayPal = new ImageButton();
            btnPayPal.ID = "btnPaypal";
            btnPayPal.Click += btnPayPal_Click;
            btnPayPal.Height = 30;
            btnPayPal.Width = 90;
            btnPayPal.AlternateText = "Pay with PayPal";
            btnPayPal.ImageUrl = "http://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif";

            ImageButton btniDeal = new ImageButton();
            btniDeal.ID = "btniDeal";
            btniDeal.Click += btniDeal_Click;
            btniDeal.Height = 30;
            btniDeal.Width = 90;
            btniDeal.AlternateText = "Pay with iDeal";
            btniDeal.ImageUrl = "http://www.dcpfilm.nl/Uitgeverij/images/images_button-ideal.jpg";

            ImageButton btnGoogle = new ImageButton();
            btnGoogle.ID = "btnGoogle";
            btnGoogle.Click += btnGoogle_Click;
            btnGoogle.Height = 30;
            btnGoogle.Width = 90;
            btnGoogle.AlternateText = "Pay with Google-Checkout";
            btnGoogle.ImageUrl = "https://lh5.googleusercontent.com/-eES4aTLteqY/TWmvwSg2tQI/AAAAAAAAAjo/RXrOWfCy6m4/s1600/google_checkout_button.gif";

            ImageButton btnOgone = new ImageButton();
            btnOgone.ID = "btnOgone";
            btnOgone.Click += btnOgone_Click;
            btnOgone.Height = 30;
            btnOgone.Width = 90;
            btnOgone.AlternateText = "Pay with Ogone";
            btnOgone.ImageUrl = "https://tctechcrunch2011.files.wordpress.com/2012/07/87407v3-max-250x250.jpg";
            

            pnlPayment.Controls.Add(btnTransfer);
            pnlPayment.Controls.Add(new LiteralControl(" <br />"));
            pnlPayment.Controls.Add(btnPayPal);
            pnlPayment.Controls.Add(new LiteralControl(" <br />"));
            pnlPayment.Controls.Add(btniDeal);
            pnlPayment.Controls.Add(new LiteralControl(" <br />"));
            pnlPayment.Controls.Add(btnGoogle);
            pnlPayment.Controls.Add(new LiteralControl(" <br />"));
            pnlPayment.Controls.Add(btnOgone);
            pnlPayment.Controls.Add(new LiteralControl(" <br />"));
        }

        void btnTransfer_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Gui/Client/Payment/Transfer.aspx?ReturnPath=" + Server.UrlEncode(Request.Url.AbsoluteUri));
        }

        void btnPayPal_Click(object sender, EventArgs e)
        {
            orderPrice = "0.01";
            Response.Redirect("https://www.paypal.com/us/cgi-bin/webscr?cmd=_xclick&business=stanniez%40live%2enl&item_name=" + orderName + "&currency_code=EUR&amount=" + orderPrice);
        }

        void btniDeal_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Gui/Client/Payment/iDeal.aspx");            
        }

        void btnGoogle_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Gui/Client/Payment/Google.aspx?ReturnPath=" + Server.UrlEncode(Request.Url.AbsoluteUri));
        }

        void btnOgone_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Gui/Client/Payment/Ogone.aspx?ReturnPath=" + Server.UrlEncode(Request.Url.AbsoluteUri));
        }

        void btnORder_Click(object sender, EventArgs e)
        {
            if (shoppingCart.Count == 0)
            {
                //Response.Write("<script>alert('ShoppingCart is empty, please fill your cart first')</script>");
            }

            createPaymentPanel();
            //else
            //{
            //    PhotoshopWebsite.WebSocket.WebSocketSingleton socket = PhotoshopWebsite.WebSocket.WebSocketSingleton.GetSingleton();
                
            //    if (shoppingCart != null)
            //    {
            //        foreach (Domain.ShoppingbasketItem item in shoppingCart)
            //        {
            //            string photoIDQualtityType = item.photoID.ToString() + ";" + item.quantity.ToString() + "#" + item.filterType;
            //            socket.sendData(photoIDQualtityType);
            //        }
            //    }
            //    //Order NUMMERS doorsturen
            //}
            //not yet implemented 
        }


        private void Check_Clicked(object sender, EventArgs e)
        {
            CheckBox cbremove = sender as CheckBox;
            foreach (Domain.ShoppingbasketItem item in shoppingCart)
            {
                if (item.GetHashCode().ToString() == cbremove.ID)
                {
                    shoppingCart.Remove(item);
                    Response.Redirect(Request.RawUrl);
                    break;
                }
            }
        }
        private void Quantity_Change(object sender, EventArgs e)
        {
            TextBox tbQuantity = sender as TextBox;
            Regex regex = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
            Match match = regex.Match(tbQuantity.ID);

            string name = match.Groups["Alpha"].Value;
            int num = Int32.Parse(match.Groups["Numeric"].Value);

            foreach (Domain.ShoppingbasketItem item in shoppingCart)
            {
                if (item.photoID == num && item.filterType.ToString() == name)
                {
                    if (int.Parse(tbQuantity.Text) > 0)
                    {
                        item.quantity = int.Parse(tbQuantity.Text);
                    }
                    else
                    {
                        shoppingCart.Remove(item);
                    }
                    Response.Redirect(Request.RawUrl);
                    break;
                }
            }
        }

    }


}
