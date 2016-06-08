using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PhotoshopWebsite.Controller
{
    /// <summary>
    /// Transaction class is used to create a view of a transaction(payment)
    /// </summary>
    public class Transaction
    {
        public int ID { get; set; }
        public int Order_ID { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string IBAN { get; set; }
        public double Price { get; set; }

        /// <summary>
        /// create a transaction with all possible variables
        /// </summary>
        /// <param name="id">id of the transaction</param>
        /// <param name="order_ID">order id of the transaction</param>
        /// <param name="date">date of the transaction</param>
        /// <param name="type">type of the transaction</param>
        /// <param name="iban">iban of the user</param>
        /// <param name="price">price of the order</param>
        public Transaction(int id, int order_ID, DateTime date, string type, string iban, double price)
        {
            this.ID = id;
            this.Order_ID = order_ID;
            this.Date = date;
            this.Type = type;
            this.IBAN = iban;
            this.Price = price;
        }
    }
}