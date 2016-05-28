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
                PriceCell.Text = item.Price.ToString();
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
            TotalAmountCell.Text = totalAmount.ToString();

            for (int i = 0; i < 4; i++)
			{
                FixedRow.Cells.Add(new TableCell());			 
			}
            FixedRow.Cells.Add(TotalTextCell);
            FixedRow.Cells.Add(TotalAmountCell);

            MainTable.Rows.Add(FixedRow);


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

            pnlProduct.Controls.Add(new LiteralControl(" <br />"));
            pnlProduct.Controls.Add(new LiteralControl(" <br />"));
            pnlProduct.Controls.Add(btnPayPal);
        }

        void btnPayPal_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://www.paypal.com/us/cgi-bin/webscr?cmd=_xclick&business=stanniez%40live%2enl&item_name=" + orderName + "&currency_code=EUR&amount=" + totalAmount);
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
                
                if (shoppingCart != null)
                {
                    foreach (Domain.ShoppingbasketItem item in shoppingCart)
                    {
                        string photoIDQualtityType = item.photoID.ToString() + ";" + item.quantity.ToString() + "#" + item.filterType;
                        socket.sendData(photoIDQualtityType);
                    }
                }
                //Order NUMMERS doorsturen
            }
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
