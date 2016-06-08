using PhotoshopWebsite.Domain;
using PhotoshopWebsite.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    /// <summary>
    /// Order class is created to place orders
    /// </summary>
    public class Order
    {
        private DatabaseTier.Order DB_Order;

        public int ID { get; set; }
        public Dictionary<Product, int> Products { get; set; }
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
        public Order(int id, Dictionary<Product, int> products, DateTime date, PaymentType type, string iban, double price)
        {
            this.ID = id;
            this.Products = products;
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
            DB_Order = new DatabaseTier.Order();
            return DB_Order.inserOrder(accountID, date.ToString("yyyy-MM-dd"), status, productID, photoID, filterType, paymentType, productType, iban, price, quantity);
        }
    }
}