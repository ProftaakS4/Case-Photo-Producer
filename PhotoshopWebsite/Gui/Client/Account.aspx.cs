using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace PhotoshopWebsite.Gui.Client
{
    [ExcludeFromCodeCoverage]
    public partial class Account : System.Web.UI.Page
    {
        private List<Order> orders;
        private List<Order> Reorders;
        private User currentUser;
        Order order1;
        Order order2;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserData"] != null)
            {
                currentUser = Session["UserData"] as User;
                FillAccountData(currentUser);
            }
            Reorders = new List<Order>();
            orders = new List<Order>();

            FillPage(orders);

        }

        private void FillAccountData(User currentUser)
        {
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
        private void FillPage(List<Order> Orders)
        {
            TableHeaderRow MainHeaderRow = new TableHeaderRow();
            TableHeaderCell IDHeader = new TableHeaderCell();
            IDHeader.Text = "Order ID";
            TableHeaderCell TypeHeader = new TableHeaderCell();
            TypeHeader.Text = "Products";
            TableHeaderCell Dateheader = new TableHeaderCell();
            Dateheader.Text = "Order date";
            TableHeaderCell Typeheader = new TableHeaderCell();
            Typeheader.Text = "Payment Type";
            TableHeaderCell Ibanheader = new TableHeaderCell();
            Ibanheader.Text = "IBAN";
            TableHeaderCell Priceheader = new TableHeaderCell();
            Priceheader.Text = "Price";
            TableHeaderCell ReOrderheader = new TableHeaderCell();
            ReOrderheader.Text = "Reorder";
            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(TypeHeader);
            MainHeaderRow.Cells.Add(Dateheader);
            MainHeaderRow.Cells.Add(Typeheader);
            MainHeaderRow.Cells.Add(Ibanheader);
            MainHeaderRow.Cells.Add(Priceheader);
            MainHeaderRow.Cells.Add(ReOrderheader);
            MainTable.Rows.Add(MainHeaderRow);

            foreach (Order order in Orders)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 80;
                TableCell ID = new TableCell();
                ID.Text = order.getID().ToString();
                TableCell products = new TableCell();
                ListBox Products = new ListBox();
                if (order != null)
                {
                    foreach (Product product in order.getProducts().Keys)
                    {
                        Products.Items.Add(product.Description);
                    }
                }
                products.Controls.Add(Products);
                TableCell Date = new TableCell();
                Date.Text = order.getDate().ToString();
                TableCell Type = new TableCell();
                Type.Text = order.getType().ToString();
                TableCell iban = new TableCell();
                iban.Text = order.getIBAN().ToString();
                TableCell price = new TableCell();
                price.Text = order.getPrice().ToString();
                TableCell BtnOrdercell = new TableCell();
                Button btnOrder = new Button();
                btnOrder.ID = order.getID().ToString();
                btnOrder.CssClass = "btn btn-default";
                btnOrder.Click += btnOrder_Click;
                btnOrder.Height = 30;
                btnOrder.Text = "Reorder";
                BtnOrdercell.Controls.Add(btnOrder);

                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(products);
                MainRow.Cells.Add(Date);
                MainRow.Cells.Add(Type);
                MainRow.Cells.Add(iban);
                MainRow.Cells.Add(price);
                MainRow.Cells.Add(BtnOrdercell);
                MainTable.Rows.Add(MainRow);
            }
        }

        void btnOrder_Click(object sender, EventArgs e)
        {
            Button x = sender as Button;
            string id = x.ID;
            foreach (Order order in orders)
            {
                if (order.getID().ToString() == id)
                {
                    Reorders.Add(order);
                }
            }
        }
    }
}