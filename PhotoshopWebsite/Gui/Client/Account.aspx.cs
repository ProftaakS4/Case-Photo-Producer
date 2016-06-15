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
using PhotoshopWebsite.Domain;

namespace PhotoshopWebsite.Gui.Client
{
    [ExcludeFromCodeCoverage]
    public partial class Account : System.Web.UI.Page
    {
        private List<Order> orders;
        private List<Order> Reorders;
        private User currentUser;
        private OrderController oc;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            Reorders = new List<Order>();
            if (Session["UserData"] != null)
            {
                currentUser = Session["UserData"] as User;
                FillAccountData(currentUser);
                oc = new OrderController(currentUser.ID);
                orders = oc.orders;
            }
            

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
            IDHeader.Text = Resources.LocalizedText.order_id;
            TableHeaderCell TypeHeader = new TableHeaderCell();
            TypeHeader.Text = Resources.LocalizedText.products;
            TableHeaderCell Dateheader = new TableHeaderCell();
            Dateheader.Text = Resources.LocalizedText.date;
            TableHeaderCell Typeheader = new TableHeaderCell();
            Typeheader.Text = Resources.LocalizedText.payment_type;
            TableHeaderCell Ibanheader = new TableHeaderCell();
            Ibanheader.Text = Resources.LocalizedText.IBAN;
            TableHeaderCell Priceheader = new TableHeaderCell();
            Priceheader.Text = Resources.LocalizedText.price;
            TableHeaderCell ReOrderheader = new TableHeaderCell();
            ReOrderheader.Text = Resources.LocalizedText.reorder;
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
                ID.Text = order.ID.ToString();
                TableCell products = new TableCell();
                //ListBox Products = new ListBox();
                //if (order != null)
                //{
                //    foreach (Product product in order.Products.Keys)
                //    {
                //        Products.Items.Add(product.Description);
                //    }
                //}
                //products.Controls.Add(Products);
                products.Text = Resources.LocalizedText.view_products;
                TableCell Date = new TableCell();
                Date.Text = order.Date.ToString();
                TableCell Type = new TableCell();
                Type.Text = order.Type.ToString();
                TableCell iban = new TableCell();
                iban.Text = order.IBAN.ToString();
                TableCell price = new TableCell();
                price.Text = order.Price.ToString();
                TableCell BtnOrdercell = new TableCell();
                Button btnOrder = new Button();
                btnOrder.ID = order.ID.ToString();
                btnOrder.CssClass = "btn btn-default";
                btnOrder.Click += btnOrder_Click;
                btnOrder.Height = 30;
                btnOrder.Text = Resources.LocalizedText.reorder;
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
                if (order.ID.ToString() == id)
                {
                    Reorders.Add(order);
                }
            }
        }
    }
}