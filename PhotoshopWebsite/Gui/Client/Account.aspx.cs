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
        private List<OrderInfo> orderInfos;
        private List<Order> Reorders;
        private User currentUser;
        private OrderController oc;
        private OrderController ocInfo;

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
                ID.Text = order.ID.ToString();
                TableCell products = new TableCell();
                Button btnShowProducts = new Button();
                btnShowProducts.ID = "btn" + order.ID.ToString();
                btnShowProducts.CssClass = "btn btn-default";
                btnShowProducts.Click += btnShowProducts_Click;
                btnShowProducts.Height = 30;
                btnShowProducts.Text = "View";
                //btnShowProducts.Attributes.Add("onClientClick", "return false;");
                //btnShowProducts.Attributes.Add("onClick", "btnShowProducts_Click;");
                btnShowProducts.CausesValidation = false;
                products.Controls.Add(btnShowProducts);

                products.ID = "ID: " + order.ID.ToString();
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
                if (order.ID.ToString() == id)
                {
                    Reorders.Add(order);
                }
            }
        }

        void btnShowProducts_Click(object sender, EventArgs e)
        {
            Button x = sender as Button;
            string id = x.ID;
            int orderID = int.Parse(id.Substring(3));
            ocInfo = new OrderController(currentUser.ID, orderID);
            orderInfos = ocInfo.orderInfos;
            FillProducts(orderInfos);
            
        }

        private void FillProducts(List<OrderInfo> orderInfos)
        {
            TableHeaderRow MainHeaderRow = new TableHeaderRow();
            TableHeaderCell IDHeader = new TableHeaderCell();
            IDHeader.Text = "Foto ID";
            IDHeader.ID = "fotoIDheader";
            TableHeaderCell FilterHeader = new TableHeaderCell();
            FilterHeader.Text = "Filter";
            FilterHeader.Text = "filterHeader";
            TableHeaderCell ProductTypeHeader = new TableHeaderCell();
            ProductTypeHeader.Text = "Product Type";
            ProductTypeHeader.ID = "productTypeHeader";
            TableHeaderCell AmountHeader = new TableHeaderCell();
            AmountHeader.Text = "Amount";
            AmountHeader.ID = "AmountHeader";
            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(FilterHeader);
            MainHeaderRow.Cells.Add(ProductTypeHeader);
            MainHeaderRow.Cells.Add(AmountHeader);
            MainTable.Rows.Add(MainHeaderRow);

            foreach (OrderInfo orderInfo in orderInfos)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 80;
                TableCell ID = new TableCell();
                ID.Text = orderInfo.ID.ToString();
                TableCell Filter = new TableCell();
                Filter.Text = orderInfo.Filter.ToString();
                TableCell Type = new TableCell();
                Type.Text = orderInfo.Type.ToString();
                TableCell Amount = new TableCell();
                Amount.Text = orderInfo.Amount.ToString();

                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(Filter);
                MainRow.Cells.Add(Type);
                MainRow.Cells.Add(Amount);
                MainTable.Rows.Add(MainRow);
            }
        }
    }
}