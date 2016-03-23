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

        private Dictionary<Product,int> testproducts;
        private List<Order> orders;

        Product testproduct1 = new Product(1, "PHOTO1x2", "PAPIER", "Foto van formaat 1x2", "../Images/Visitekaart-Delahaye-IT.png", -1);
        Product testproduct2 = new Product(2, "PHOTO1x2", "Hout", "Foto van formaat 200X200", "../Images/Visitekaart-Delahaye-IT.png", -1);
        Product testproduct3 = new Product(3, "PHOTO1x2", "Steen", "Foto van formaat 300x300", "../Images/Visitekaart-Delahaye-IT.png", -1);
        Product testproduct4 = new Product(4, "PHOTO1x2", "Rubber", "Foto van formaat 500x1500", "../Images/Visitekaart-Delahaye-IT.png", -1);
        Product testproduct5 = new Product(5, "PHOTO1x2", "Rubber", "Foto van formaat 500x1500", "../Images/Visitekaart-Delahaye-IT.png", -1);

        Order order1;
        Order order2;
        protected void Page_Load(object sender, EventArgs e)
        {
            testproducts = new Dictionary<Product,int>();
            testproducts.Add(testproduct1,1);
            testproducts.Add(testproduct2,2);
            testproducts.Add(testproduct3,3);
            testproducts.Add(testproduct4,4);
            testproducts.Add(testproduct5,5);

            orders = new List<Order>();
            order1 = new Order(1,testproducts,new DateTime(2016-3-15),PaymentType.iDeal,"NLRABO1239871238761",22.60);
            order2 = new Order(2, testproducts, new DateTime(2016 - 3 - 15), PaymentType.iDeal, "NLRABO1239871238761", 22.60);
            orders.Add(order1);
            orders.Add(order2);
            tbFirstname.Text = testuser.Firstname;
            tbLastname.Text = testuser.Lastname;
            tbStreetname.Text = testuser.Streetname;
            tbHousenumber.Text = testuser.Housenumber;
            tbZipcode.Text = testuser.Zipcode;
            tbCity.Text = testuser.City;
            tbPhoneNumber.Text = testuser.Phonenumber;
            tbIBAN.Text = testuser.IBAN;
            tbEMail.Text = testuser.Emailaddress;
            FillPage(orders);
            
        }
        private void FillPage(List<Order> Orders)
        {
            TableHeaderRow MainHeaderRow = new TableHeaderRow();
            TableHeaderCell IDHeader = new TableHeaderCell();
            IDHeader.Text = "Product ID";
            TableHeaderCell TypeHeader = new TableHeaderCell();
            TypeHeader.Text = "Product Type";
            TableHeaderCell Dateheader = new TableHeaderCell();
            Dateheader.Text = "Order date";
            TableHeaderCell Typeheader = new TableHeaderCell();
            Typeheader.Text = "Payment Type";
            TableHeaderCell Ibanheader = new TableHeaderCell();
            Ibanheader.Text = "IBAN";
            TableHeaderCell Priceheader = new TableHeaderCell();
            Priceheader.Text = "Price";
            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(TypeHeader);
            MainHeaderRow.Cells.Add(Dateheader);
            MainHeaderRow.Cells.Add(Typeheader);
            MainHeaderRow.Cells.Add(Ibanheader);
            MainHeaderRow.Cells.Add(Priceheader);
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

                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(products);
                MainRow.Cells.Add(Date);
                MainRow.Cells.Add(Type);
                MainRow.Cells.Add(iban);
                MainRow.Cells.Add(price);
                MainTable.Rows.Add(MainRow);
            }
        }
        }
    }
