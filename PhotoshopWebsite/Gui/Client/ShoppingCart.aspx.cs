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
        private ImageButton btnTransfer;
        private ImageButton btnPayPal;
        private ImageButton btniDeal;
        private ImageButton btnGoogle;
        private ImageButton btnOgone;

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
            setupEventhandlersPaymentMethod();
            createPaymentPanel();
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
            pnlProduct.Controls.Add(firstcontrol);
            pnlProduct.Controls.Add(MainTable);
            pnlProduct.Controls.Add(closingcontrol);
            pnlProduct.Controls.Add(new LiteralControl(" <br />"));
            pnlProduct.Controls.Add(new LiteralControl(" <br />"));
            pnlProduct.Controls.Add(new LiteralControl(" <br />"));
            pnlProduct.Controls.Add(new LiteralControl(" <br />"));


        }

        void setupEventhandlersPaymentMethod()
        {
            btnTransfer = new ImageButton();
            btnTransfer.Click += BtnTransfer_Click;
            btnPayPal = new ImageButton();
            btnPayPal.Click += btnPayPal_Click;
            btniDeal = new ImageButton();
            btniDeal.Click += BtniDeal_Click;
            btnGoogle = new ImageButton();
            btnGoogle.Click += BtnGoogle_Click;
            btnOgone = new ImageButton();
            btnOgone.Click += BtnOgone_Click;
        }
        void createPaymentPanel()
        {
            Label payment = new Label();
            payment.Text = "Choose your payment method";
            payment.Font.Bold = true;

            btnTransfer.ID = "btnTransfer";
            btnTransfer.Height = 30;
            btnTransfer.Width = 90;
            btnTransfer.AlternateText = "Pay by Money Transfer";
            btnTransfer.ImageUrl = "http://www.glerups.nl/media/wysiwyg/infortis/ultimo/custom/overboeking.jpg";
            btnTransfer.CssClass = "btnPayment";

            btnPayPal.ID = "btnPaypal";
            btnPayPal.Height = 30;
            btnPayPal.Width = 90;
            btnPayPal.AlternateText = "Pay with PayPal";
            btnPayPal.ImageUrl = "http://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif";
            btnPayPal.CssClass = "btnPayment";

            btniDeal.ID = "btniDeal";
            btniDeal.Height = 30;
            btniDeal.Width = 90;
            btniDeal.AlternateText = "Pay with iDeal";
            btniDeal.ImageUrl = "http://www.dcpfilm.nl/Uitgeverij/images/images_button-ideal.jpg";
            btniDeal.CssClass = "btnPayment";

            btnGoogle.ID = "btnGoogle";
            btnGoogle.Height = 30;
            btnGoogle.Width = 90;
            btnGoogle.AlternateText = "Pay with Google-Checkout";
            btnGoogle.ImageUrl = "https://lh5.googleusercontent.com/-eES4aTLteqY/TWmvwSg2tQI/AAAAAAAAAjo/RXrOWfCy6m4/s1600/google_checkout_button.gif";
            btnGoogle.CssClass = "btnPayment";

            btnOgone.ID = "btnOgone";
            btnOgone.Height = 30;
            btnOgone.Width = 90;
            btnOgone.AlternateText = "Pay with Ogone";
            btnOgone.ImageUrl = "https://tctechcrunch2011.files.wordpress.com/2012/07/87407v3-max-250x250.jpg";
            btnOgone.CssClass = "btnPayment";

            pnlPayment.Controls.Add(payment);
            pnlPayment.Controls.Add(new LiteralControl(" <br />"));
            pnlPayment.Controls.Add(new LiteralControl(" <br />"));
            pnlPayment.Controls.Add(btnTransfer);
            //pnlPayment.Controls.Add(new LiteralControl(" <br />"));
            pnlPayment.Controls.Add(btnPayPal);
            //pnlPayment.Controls.Add(new LiteralControl(" <br />"));
            pnlPayment.Controls.Add(btniDeal);
            //pnlPayment.Controls.Add(new LiteralControl(" <br />"));
            pnlPayment.Controls.Add(btnGoogle);
            //pnlPayment.Controls.Add(new LiteralControl(" <br />"));
            pnlPayment.Controls.Add(btnOgone);
            //pnlPayment.Controls.Add(new LiteralControl(" <br />"));
        }
        private void BtnOgone_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Payment/Ogone.aspx");
        }

        private void BtnTransfer_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Payment/iDeal.aspx");
        }

        private void BtnGoogle_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Payment/Google.aspx");
        }

        private void BtniDeal_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Payment/iDeal.aspx");
        }

        void btnPayPal_Click(object sender, EventArgs e)
        {
            orderPrice = "0.01";
            Response.Redirect("https://www.paypal.com/us/cgi-bin/webscr?cmd=_xclick&business=stanniez%40live%2enl&item_name=" + orderName + "&currency_code=EUR&amount=" + orderPrice);
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
