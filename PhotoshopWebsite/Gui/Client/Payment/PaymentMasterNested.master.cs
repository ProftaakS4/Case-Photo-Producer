 using PhotoshopWebsite.Domain;
using PhotoshopWebsite.Enumeration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PhotoshopWebsite.Gui.Client.Payment
{
    public partial class PaymentMasterNested : System.Web.UI.MasterPage
    {

        private User currentUser;
        private RadioButton rabobank = new RadioButton();
        private RadioButton abn = new RadioButton();
        private RadioButton ingb = new RadioButton();
        private RadioButton sns = new RadioButton();


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
            fillUserInfo();
            fillOrderInfo();
            paymentInfo();
        }

        void fillOrderInfo()
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
            TableHeaderCell PriceHeader = new TableHeaderCell();
            PriceHeader.Text = "Price";
            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(FilterHeader);
            MainHeaderRow.Cells.Add(TypeHeader);
            MainHeaderRow.Cells.Add(Descriptionheader);
            MainHeaderRow.Cells.Add(Quantityheader);
            MainHeaderRow.Cells.Add(PriceHeader);
            MainTable.Rows.Add(MainHeaderRow);

            Label totalAmount = new Label();
            totalAmount.ForeColor = Color.Green;
            totalAmount.Font.Bold = true;
          //  double total = (double)Session["totalAmount"];
       //     totalAmount.Text = "<u>Total Amount: €" + total + ",00</u>";
            totalAmount.Font.Size = 18;

            foreach (Domain.ShoppingbasketItem item in shoppingCart)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 40;
                TableCell ID = new TableCell();
                ID.Text = item.PhotoID.ToString();
                TableCell Filter = new TableCell();
                Filter.Text = item.Filter.ToString();
                TableCell Type = new TableCell();
                Type.Text = item.Product.ToString();
                TableCell Description = new TableCell();
                Description.Text = item.Description;
                TableCell Quantity = new TableCell();
                Quantity.Text = item.Quantity.ToString();
                TableCell PriceCell = new TableCell();
                PriceCell.Text = "€" + item.Price.ToString() + ",00";

                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(Filter);
                MainRow.Cells.Add(Type);
                MainRow.Cells.Add(Description);
                MainRow.Cells.Add(Quantity);
                MainRow.Cells.Add(PriceCell);

                MainTable.Rows.Add(MainRow);
            }

            pnlOrderInfo.Controls.Add(firstcontrol);
            pnlOrderInfo.Controls.Add(MainTable);
            pnlOrderInfo.Controls.Add(closingcontrol);
            pnlOrderInfo.Controls.Add(new LiteralControl(" <br />"));
            pnlOrderInfo.Controls.Add(totalAmount);
        }

        void fillUserInfo()
        {
            if (Session["UserData"] != null)
            {
                currentUser = Session["UserData"] as User;
                tbFirstname.Text = currentUser.Firstname;
                tbLastname.Text = currentUser.Lastname;
                tbStreetname.Text = currentUser.Streetname;
                tbHousenumber.Text = currentUser.Housenumber;
                tbZipcode.Text = currentUser.Zipcode;
                tbCity.Text = currentUser.City;
                tbPhoneNumber.Text = currentUser.Phonenumber;
                tbIBAN.Text = currentUser.IBAN;
                tbEMail.Text = currentUser.Emailaddress;
            }
        }

        void paymentInfo()
        {
            rabobank.Text = "Rabobank  ";
            rabobank.GroupName = "bank";
            abn.Text = "ABN Amro  ";
            abn.GroupName = "bank";
            ingb.Text = "ING Bank  ";
            ingb.GroupName = "bank";
            sns.Text = "SNS Bank  ";
            sns.GroupName = "bank";

            Button confirmButton = new Button();
            confirmButton.Text = "Confirm";
            confirmButton.Click += ConfirmButton_Click;

            pnlPaymentInfo.Controls.Add(rabobank);
            pnlPaymentInfo.Controls.Add(abn);
            pnlPaymentInfo.Controls.Add(ingb);
            pnlPaymentInfo.Controls.Add(sns);
            pnlOrderInfo.Controls.Add(new LiteralControl(" <br />"));
            pnlPaymentInfo.Controls.Add(confirmButton);
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (rabobank.Checked || abn.Checked || ingb.Checked || sns.Checked)
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

                        foreach (Domain.ShoppingbasketItem item in shoppingCart)
                        {
                            // create socket string
                            string photoIDQualtityType = item.PhotoID.ToString() + ";" + item.Quantity.ToString() + "#" + item.Filter + "& 55 73 187 187";
                            // send socket string to fileserver and check if string is correctly send
                            if (socket.sendData(photoIDQualtityType))
                            {
                                // insert order into database
                                newOrder.insertPrintOrder(currentUser.ID, DateTime.Now, "Paid", ProductTypes.getInt(item.Product.ToString()), item.PhotoID, item.Filter.ToString(), "iDeal", item.Product.ToString(), currentUser.IBAN, item.Price, item.Quantity);
                            }
                        }
                        Response.Redirect("CheckPayment.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Unknown User, order not placed.')</script>");
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Select your bank')</script>");
            }
        }
    }
}

