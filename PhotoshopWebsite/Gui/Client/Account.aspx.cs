using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using System.Web.UI.HtmlControls;
using System.Data;

namespace PhotoshopWebsite.Gui.Client
{
    public partial class Account : System.Web.UI.Page
    {
        User testuser = new User(1, "client", "Loek", "Delahaye", "Voogdijstraat", "5", "6041EX", "Roermond", "1235325", "NLRAB012309814", "Loekdelaaye@gmail.com");
        
        private Dictionary<Product, int> testproducts;
        private List<Order> orders;
        private List<Order> Reorders;

        Product testproduct1 = new Product(1, "PHOTO1x2", "PAPIER", "Foto van formaat 1x2", "../Images/Visitekaart-Delahaye-IT.png", -1);
        Product testproduct2 = new Product(2, "PHOTO1x2", "Hout", "Foto van formaat 200X200", "../Images/Visitekaart-Delahaye-IT.png", -1);
        Product testproduct3 = new Product(3, "PHOTO1x2", "Steen", "Foto van formaat 300x300", "../Images/Visitekaart-Delahaye-IT.png", -1);
        Product testproduct4 = new Product(4, "PHOTO1x2", "Rubber", "Foto van formaat 500x1500", "../Images/Visitekaart-Delahaye-IT.png", -1);
        Product testproduct5 = new Product(5, "PHOTO1x2", "Rubber", "Foto van formaat 500x1500", "../Images/Visitekaart-Delahaye-IT.png", -1);
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
            testproducts = new Dictionary<Product, int>();
            testproducts.Add(testproduct1, 1);
            testproducts.Add(testproduct2, 2);
            testproducts.Add(testproduct3, 3);
            testproducts.Add(testproduct4, 4);
            testproducts.Add(testproduct5, 5);
            Reorders = new List<Order>();
            orders = new List<Order>();
            order1 = new Order(1, testproducts, new DateTime(2016 - 3 - 15), PaymentType.iDeal, "NLRABO1239871238761", 22.60);
            order2 = new Order(2, testproducts, new DateTime(2016 - 3 - 15), PaymentType.iDeal, "NLRABO1239871238761", 22.60);
            orders.Add(order1);
            orders.Add(order2);

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
                foreach (Product product in order.getProducts().Keys)
                {
                    Products.Items.Add(product.Description);
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