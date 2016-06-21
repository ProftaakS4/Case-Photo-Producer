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
using PhotoshopWebsite.Domain;
using PhotoshopWebsite.Enumeration;

namespace PhotoshopWebsite.Gui
{
    [ExcludeFromCodeCoverage]
    public partial class ShoppingCart : System.Web.UI.Page
    {
        private string orderName = "Photo Shop";
        private double totalAmount = 0;
        private User currentUser;
       

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
            if (Session["UserData"] != null)
            {
                currentUser = Session["UserData"] as User;
            }
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
            IDHeader.Text = Resources.LocalizedText.photo_id;
            TableHeaderCell FilterHeader = new TableHeaderCell();
            FilterHeader.Text = Resources.LocalizedText.filter;
            TableHeaderCell TypeHeader = new TableHeaderCell();
            TypeHeader.Text = Resources.LocalizedText.product_type;
            TableHeaderCell DescriptionHeader = new TableHeaderCell();
            DescriptionHeader.Text = Resources.LocalizedText.product_description;
            TableHeaderCell QuantityHeader = new TableHeaderCell();
            QuantityHeader.Text = Resources.LocalizedText.quantity;
            TableHeaderCell PriceHeader = new TableHeaderCell();
            PriceHeader.Text = Resources.LocalizedText.price;
            TableHeaderCell RemoveHeader = new TableHeaderCell();
            RemoveHeader.Text = Resources.LocalizedText.remove;
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
                ID.Text = item.PhotoID.ToString();
                TableCell Filter = new TableCell();
                Filter.Text = item.Filter.ToString();
                TableCell Type = new TableCell();
                Type.Text = item.Product.ToString();
                TableCell Description = new TableCell();
                Description.Text = item.Description;
                TableCell Quantity = new TableCell();
                TextBox tbQuantity = new TextBox();
                tbQuantity.ID = item.Filter.ToString() + item.PhotoID.ToString();
                tbQuantity.Text = item.Quantity.ToString();
                tbQuantity.TextChanged += Quantity_Change;
                tbQuantity.AutoPostBack = true;
                tbQuantity.MaxLength = 3;
                Quantity.Controls.Add(tbQuantity);

                TableCell PriceCell = new TableCell();
                PriceCell.Text = "€" + item.Price.ToString() + ",00";
                totalAmount = (totalAmount + (item.Price * item.Quantity));


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
            TotalTextCell.Text = "<B>"+Resources.LocalizedText.total+":</B>";
            TableCell TotalAmountCell = new TableCell();
            TotalAmountCell.Text = "€" + totalAmount.ToString() + ",00";
            TotalAmountCell.ID = "tdTotalAmount";
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
                if (item.PhotoID == num && item.Filter.ToString() == name)
                {
                    if (int.Parse(tbQuantity.Text) > 0)
                    {
                        item.Quantity = int.Parse(tbQuantity.Text);
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (shoppingCart.Count == 0)
            {
                Response.Write("<script>alert('ShoppingCart is empty, please fill your cart first')</script>");
            }

            else
            {
                if (shoppingCart != null && currentUser != null)
                {
                    PhotoshopWebsite.WebSocket.WebSocketSingleton socket = PhotoshopWebsite.WebSocket.WebSocketSingleton.GetSingleton();
                    Domain.Order newOrder = new Domain.Order();
                    socket.sendData("BEGIN");
                    string ids = "INDEX ";
                    foreach (Domain.ShoppingbasketItem item in shoppingCart)
                    {
                        // create socket string
                        ids = ids + " " + item.PhotoID.ToString();

                        string photoIDQualtityType = item.PhotoID.ToString() + ";" + item.Quantity.ToString() + "#" + item.Filter + " " + item.getCropValues();                        

                        // send socket string to fileserver and check if string is correctly send
                        if (socket.sendData(photoIDQualtityType))
                        {
                            // insert order into database
                            //newOrder.insertPrintOrder(currentUser.ID, DateTime.Now, "Paid", ProductTypes.getInt(item.Product.ToString()), item.PhotoID, item.Filter.ToString(), "iDeal", item.Product.ToString(), currentUser.IBAN, item.Price, item.Quantity);
                        }
                    }
                    socket.sendData(ids + "?" + currentUser.ID.ToString());
                    socket.sendData("END");
                    // Email senden
                }
                else
                {
                    Response.Write("<script>alert('Unknown User, order not placed.')</script>");
                }
            }
        }
    }
}
