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
        private double totalAmount = 0;
        private Image btnTransfer;
        private Image btnPayPal;
        private Image btniDeal;
        private Image btnGoogle;
        private Image btnOgone;
        private Label lblPaymentMethod;
        private RadioButtonList rblPayment;

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
            TableHeaderCell DescriptionHeader = new TableHeaderCell();
            DescriptionHeader.Text = "Photo Description";
            TableHeaderCell QuantityHeader = new TableHeaderCell();
            QuantityHeader.Text = "Quantity";
            TableHeaderCell PriceHeader = new TableHeaderCell();
            PriceHeader.Text = "Price";
            TableHeaderCell RemoveHeader = new TableHeaderCell();
            RemoveHeader.Text = "Remove";
            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(FilterHeader);
            MainHeaderRow.Cells.Add(TypeHeader);
            MainHeaderRow.Cells.Add(DescriptionHeader);
            MainHeaderRow.Cells.Add(QuantityHeader);
            MainHeaderRow.Cells.Add(PriceHeader);
            MainHeaderRow.Cells.Add(RemoveHeader);
            MainTable.Rows.Add(MainHeaderRow);

            foreach (Domain.ShoppingbasketItem item in shoppingCart)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 60;

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

                TableCell PriceCell = new TableCell();
                PriceCell.Text = "€" + item.Price.ToString() + ",00";
                totalAmount = (totalAmount + (item.Price * item.quantity));

                TableCell ButtonCell = new TableCell();
                CheckBox cbRemove = new CheckBox();
                cbRemove.ID = item.GetHashCode().ToString();
                cbRemove.CssClass = "checkbox";
                cbRemove.CheckedChanged += Check_Clicked;
                cbRemove.Height = 30;
                cbRemove.AutoPostBack = true;
                cbRemove.Checked = false;
                ButtonCell.Controls.Add(cbRemove);

                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(Filter);
                MainRow.Cells.Add(Type);
                MainRow.Cells.Add(Description);
                MainRow.Cells.Add(Quantity);
                MainRow.Cells.Add(PriceCell);
                MainRow.Cells.Add(ButtonCell);

                MainTable.Rows.Add(MainRow);
            }

            // Fixed row
            TableRow FixedRow = new TableRow();
            FixedRow.Height = 60;

            TableCell TotalTextCell = new TableCell();
            TotalTextCell.Text = "<B>Total:</B>";
            TableCell TotalAmountCell = new TableCell();
            TotalAmountCell.Text = "€" + totalAmount.ToString() + ",00";
            Session["totalAmount"] = totalAmount;

            for (int i = 0; i < 4; i++)
            {
                FixedRow.Cells.Add(new TableCell());
            }
            FixedRow.Cells.Add(TotalTextCell);
            FixedRow.Cells.Add(TotalAmountCell);
            FixedRow.Cells.Add(new TableCell());

            MainTable.Rows.Add(FixedRow);

            pnlProduct.Controls.Add(firstcontrol);
            pnlProduct.Controls.Add(MainTable);
            pnlProduct.Controls.Add(closingcontrol);
            pnlProduct.Controls.Add(new LiteralControl(" <br />"));
            pnlProduct.Controls.Add(new LiteralControl(" <br />"));
        }

        void setupEventhandlersPaymentMethod()
        {
            btnPayPal = new Image();
        }
        void createPaymentPanel()
        {
            //HtmlGenericControl container = new HtmlGenericControl();
            //HtmlGenericControl wrapper = new HtmlGenericControl();
            //HtmlGenericControl columnbtnTransfer = new HtmlGenericControl();
            //HtmlGenericControl closerbtnTransfer = new HtmlGenericControl();
            //HtmlGenericControl columnbtnPayPal = new HtmlGenericControl();
            //HtmlGenericControl closerbtnPayPal = new HtmlGenericControl();
            //HtmlGenericControl columnbtniDeal = new HtmlGenericControl();
            //HtmlGenericControl closerbtniDeal = new HtmlGenericControl();
            //HtmlGenericControl columnbtnGoogle = new HtmlGenericControl();
            //HtmlGenericControl closerbtnGoogle = new HtmlGenericControl();
            //HtmlGenericControl columnbtnOgone = new HtmlGenericControl();
            //HtmlGenericControl closerbtnOgone = new HtmlGenericControl();
            //HtmlGenericControl orderButtonRow = new HtmlGenericControl();
            //HtmlGenericControl wrapperCloser = new HtmlGenericControl();

            //container.InnerHtml = "<div class='container'>";
            //wrapper.InnerHtml = "<div class='row' background-color:red>";
            //columnbtnTransfer.InnerHtml = "<div class='col-md-6'>";
            //closerbtnTransfer.InnerHtml = "</div>";
            //columnbtnPayPal.InnerHtml = "<div class='col-md-6'> ";
            //closerbtnPayPal.InnerHtml = "</div>";
            //columnbtniDeal.InnerHtml = "<div class='col-md-6'> ";
            //closerbtniDeal.InnerHtml = "</div>";
            //columnbtnGoogle.InnerHtml = "<div class='col-md-6'> ";
            //closerbtnGoogle.InnerHtml = "</div>";
            //columnbtnOgone.InnerHtml = "<div class='col-md-6'> ";
            //closerbtnOgone.InnerHtml = "</div>";
            //wrapperCloser.InnerHtml = "</div>"; 

            //btnTransfer.ID = "btnTransfer";
            //btnTransfer.Height = 30;
            //btnTransfer.Width = 90;
            //btnTransfer.AlternateText = "Pay by Money Transfer";
            //btnTransfer.ImageUrl = "http://www.glerups.nl/media/wysiwyg/infortis/ultimo/custom/overboeking.jpg";
            //btnTransfer.CssClass = "btnPayment";

            //btnPayPal.ID = "btnPaypal";
            //btnPayPal.Height = 30;
            //btnPayPal.Width = 90;
            //btnPayPal.AlternateText = "Pay with PayPal";
            //btnPayPal.ImageUrl = "http://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif";
            //btnPayPal.CssClass = "btnPayment";

            //btniDeal.ID = "btniDeal";
            //btniDeal.Height = 30;
            //btniDeal.Width = 90;
            //btniDeal.AlternateText = "Pay with iDeal";
            //btniDeal.ImageUrl = "http://www.dcpfilm.nl/Uitgeverij/images/images_button-ideal.jpg";
            //btniDeal.CssClass = "btnPayment";

            //btnGoogle.ID = "btnGoogle";
            //btnGoogle.Height = 30;
            //btnGoogle.Width = 90;
            //btnGoogle.AlternateText = "Pay with Google-Checkout";
            //btnGoogle.ImageUrl = "https://lh5.googleusercontent.com/-eES4aTLteqY/TWmvwSg2tQI/AAAAAAAAAjo/RXrOWfCy6m4/s1600/google_checkout_button.gif";
            //btnGoogle.CssClass = "btnPayment";

            //btnOgone.ID = "btnOgone";
            //btnOgone.Height = 30;
            //btnOgone.Width = 90;
            //btnOgone.AlternateText = "Pay with Ogone";
            //btnOgone.ImageUrl = "https://tctechcrunch2011.files.wordpress.com/2012/07/87407v3-max-250x250.jpg";
            //btnOgone.CssClass = "btnPayment";

            //Button btnOrder = new Button();
            //btnOrder.CssClass = "btn btn-default";
            //btnOrder.Click += BtnOrder_Click;
            //btnOrder.Text = "Bestelling Plaatsen";

            //RadioButton rbPaymentTransfer = new RadioButton();
            //RadioButton rbPaymentPaypal = new RadioButton();
            //RadioButton rbPaymentiDeal = new RadioButton();
            //RadioButton rbPaymentGoogle = new RadioButton();
            //RadioButton rbPaymentOgone = new RadioButton();

            //rbPaymentTransfer.GroupName = "Payment";
            //rbPaymentPaypal.GroupName = "Payment";
            //rbPaymentiDeal.GroupName = "Payment";
            //rbPaymentGoogle.GroupName = "Payment";
            //rbPaymentOgone.GroupName = "Payment";

            //lblPaymentMethod = new Label();
            //lblPaymentMethod.Text = "<b> Kies een betaal methode: </b>";
            //lblPaymentMethod.Style.Add("margin-bot", "30px;");
            //String innerhtml = "<div class='col-md-12 paymentrow' style='background-color:white; border-radius:7px; height:100px; padding:10px; margin-bottom:20px;'>";
            //columnbtnTransfer.InnerHtml = innerhtml;
            //closerbtnTransfer.InnerHtml = "</div>";
            //columnbtnPayPal.InnerHtml = innerhtml;
            //closerbtnPayPal.InnerHtml = "</div>";
            //columnbtniDeal.InnerHtml = innerhtml;
            //closerbtniDeal.InnerHtml = "</div>";
            //columnbtnGoogle.InnerHtml = innerhtml;
            //closerbtnGoogle.InnerHtml = "</div>";
            //columnbtnOgone.InnerHtml = innerhtml;
            //closerbtnOgone.InnerHtml = "</div>";

            //paymentPanel.Controls.Add(lblPaymentMethod);
            //paymentPanel.Controls.Add(wrapper);
            //paymentPanel.Controls.Add(columnbtnTransfer);
            //paymentPanel.Controls.Add(rbPaymentTransfer);
            //paymentPanel.Controls.Add(btnTransfer);
            //paymentPanel.Controls.Add(closerbtnTransfer);
            //paymentPanel.Controls.Add(columnbtnPayPal);
            //paymentPanel.Controls.Add(rbPaymentPaypal);
            //paymentPanel.Controls.Add(btnPayPal);
            //paymentPanel.Controls.Add(closerbtnPayPal);
            //paymentPanel.Controls.Add(columnbtniDeal);
            //paymentPanel.Controls.Add(rbPaymentiDeal);
            //paymentPanel.Controls.Add(btniDeal);
            //paymentPanel.Controls.Add(closerbtniDeal);
            //paymentPanel.Controls.Add(columnbtnGoogle);
            //paymentPanel.Controls.Add(rbPaymentGoogle);
            //paymentPanel.Controls.Add(btnGoogle);
            //paymentPanel.Controls.Add(closerbtnGoogle);
            //paymentPanel.Controls.Add(columnbtnOgone);
            //paymentPanel.Controls.Add(rbPaymentOgone);
            //paymentPanel.Controls.Add(btnOgone);
            //paymentPanel.Controls.Add(closerbtnOgone);

            //innerhtml = "<div class='row'> <div class='col-md-12 paymentrow' style='background-color:blue; border-radius:7px; height:100px; padding:10px; margin-bottom:20px;'>";
            //orderButtonRow.InnerHtml = innerhtml;
            //paymentPanel.Controls.Add(wrapperCloser);

            //paymentPanel.Controls.Add(orderButtonRow);
            //paymentPanel.Controls.Add(btnOrder);
            //ListItem raboBank = new ListItem("Rabobank", "rabobank", true); raboBank.Selected = true;
            //ListItem ing = new ListItem("Ing", "ing", true); 
            //ListItem snsBank = new ListItem("SnsBank", "snsBank", true);
            //paymentMenu.Items.Add(raboBank);
            //paymentMenu.Items.Add(ing);
            //paymentMenu.Items.Add(snsBank);

        }

        private void BtnOrder_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnOgone_Click(object sender, ImageClickEventArgs e)
            {
            Response.Redirect("Payment/Ogone.aspx");
            }

        private void BtnTransfer_Click(object sender, ImageClickEventArgs e)
            {
            Response.Redirect("Payment/Transfer.aspx");
        }
                
        private void BtnGoogle_Click(object sender, ImageClickEventArgs e)
                    {
            Response.Redirect("Payment/Google.aspx");
                }

        private void BtniDeal_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Payment/iDeal.aspx");
            }

        private void btnPayPal_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://www.paypal.com/us/cgi-bin/webscr?cmd=_xclick&business=stanniez%40live%2enl&item_name=" + orderName + "&currency_code=EUR&amount=" + totalAmount);
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

        protected void btnPaypal_Click1(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("https://www.paypal.com/us/cgi-bin/webscr?cmd=_xclick&business=stanniez%40live%2enl&item_name=" + orderName + "&currency_code=EUR&amount=" + totalAmount);
        }
    }
}
