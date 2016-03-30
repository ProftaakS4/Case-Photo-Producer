using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PhotoshopWebsite.Controller
{
    public class Transaction
    {
        public int ID { get; set; }
        public int Order_ID { get; set; }

        public DateTime Date { get; set; }
        public String Type { get; set; }
        public String IBAN { get; set; }
        public double Price { get; set; }

        public Transaction(int ID, int ORDER_ID,DateTime Date, string Type, string IBAN, double Price)
        {
            this.ID = ID;
            this.Order_ID = ORDER_ID;
            this.Date = Date;
            this.Type = Type;
            this.IBAN = IBAN;
            this.Price = Price;
        }
    }
}