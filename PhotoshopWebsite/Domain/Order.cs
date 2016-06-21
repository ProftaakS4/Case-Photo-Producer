using PhotoshopWebsite.Domain;
using PhotoshopWebsite.Enumeration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    /// <summary>
    /// Order class is created to place orders
    /// </summary>
    public class Order
    {
        private DatabaseTier.QueryDatabase database = new DatabaseTier.QueryDatabase();

        public int ID { get; set; }
        // public Dictionary<Product, int> Products { get; set; }
        public DateTime Date { get; set; }
        public PaymentType Type { get; set; }
        public string IBAN { get; set; }
        public double Price { get; set; }

        /// <summary>
        /// create a Order with all possible variables
        /// </summary>
        /// <param name="id">id of the order</param>
        /// <param name="products">a dictionary of products and how many times they are added</param>
        /// <param name="date">the time and date of the order</param>
        /// <param name="type">the paymenttype that is used to place the order</param>
        /// <param name="iban">the iban of the user that places the order</param>
        /// <param name="price">the total price of the order</param>
        public Order(int id, DateTime date, PaymentType type, string iban, double price)
        {
            this.ID = id;
            // this.Products = products;
            this.Date = date;
            this.Type = type;
            this.IBAN = iban;
            this.Price = price;
        }

        public Order()
        {

        }

        public int insertPrintOrder(int accountID, DateTime date, string status, int productID, int photoID, string filterType, string paymentType, string productType, string iban, double price, int quantity)
        {
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_account_ID", new string[] { "int", accountID.ToString() });
            parameters.Add("p_date", new string[] { "date", date.ToString("yyyy-MM-dd") });
            parameters.Add("p_status", new string[] { "string", status.ToString() });
            DataTable dt = this.database.CallProcedure("insertPurchase", parameters);
            int purchaseID = int.Parse(dt.Rows[0][0].ToString());
            if (purchaseID != 0)
            {

                parameters = new Dictionary<string, string[]>();
                parameters.Add("p_purchase_ID", new string[] { "int", purchaseID.ToString() });
                parameters.Add("p_product_ID", new string[] { "int", productID.ToString("yyyy-MM-dd") });
                parameters.Add("p_photo_ID", new string[] { "int", photoID.ToString() });
                parameters.Add("p_filterType", new string[] { "string", filterType.ToString() });
                parameters.Add("p_quantity", new string[] { "int", quantity.ToString() });
                this.database.CallProcedure("insertPurchaseProduct", parameters);

                parameters = new Dictionary<string, string[]>();
                parameters.Add("p_purchase_ID", new string[] { "int", purchaseID.ToString() });
                parameters.Add("p_date", new string[] { "date", date.ToString("yyyy-MM-dd") });
                parameters.Add("p_type", new string[] { "string", paymentType.ToString() });
                parameters.Add("p_iban", new string[] { "string", iban.ToString() });
                parameters.Add("p_price", new string[] { "double", price.ToString() });
                dt = this.database.CallProcedure("insertPayment", parameters);
                int paymentID = int.Parse(dt.Rows[0][0].ToString());

                parameters = new Dictionary<string, string[]>();
                parameters.Add("p_payment_ID", new string[] { "int", paymentID.ToString() });
                parameters.Add("p_date", new string[] { "date", date.ToString("yyyy-MM-dd") });
                dt = this.database.CallProcedure("insertPrintOrder", parameters);
                if (dt.Rows.Count != 0)
                {
                    int printOrderID = int.Parse(dt.Rows[0][0].ToString());

                    parameters = new Dictionary<string, string[]>();
                    parameters.Add("p_type", new string[] { "string", productType.ToString() });
                    parameters.Add("p_quantity", new string[] { "int", quantity.ToString() });
                    this.database.CallProcedure("updateStock", parameters);
                    return printOrderID;
                }
            }
            return 0;
        }
    }
}