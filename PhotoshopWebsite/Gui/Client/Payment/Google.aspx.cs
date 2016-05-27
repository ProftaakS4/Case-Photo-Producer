using PhotoshopWebsite.Controller;
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
    public partial class Google : System.Web.UI.Page
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
            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(FilterHeader);
            MainHeaderRow.Cells.Add(TypeHeader);
            MainHeaderRow.Cells.Add(Descriptionheader);
            MainHeaderRow.Cells.Add(Quantityheader);
            MainTable.Rows.Add(MainHeaderRow);

            Label totalAmount = new Label();
            totalAmount.ForeColor = Color.Green;
            totalAmount.Font.Bold = true;
            totalAmount.Text = "<u>Total Amount: € 9,99</u>";
            totalAmount.Font.Size = 18;

            // allign text to the righ

            foreach (Domain.ShoppingbasketItem item in shoppingCart)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 40;
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
                tbQuantity.AutoPostBack = true;
                tbQuantity.MaxLength = 3;
                tbQuantity.ReadOnly = true;
                Quantity.Controls.Add(tbQuantity);

                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(Filter);
                MainRow.Cells.Add(Type);
                MainRow.Cells.Add(Description);
                MainRow.Cells.Add(Quantity);

                TableCell ButtonCell = new TableCell();
                MainRow.Cells.Add(ButtonCell);
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
                Response.Redirect("CheckPayment.aspx");
            }
            else
            {
                Response.Write("<script>alert('Select your bank')</script>");
            }
        }

    }
}