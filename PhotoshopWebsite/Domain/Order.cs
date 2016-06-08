using PhotoshopWebsite.Domain;
using PhotoshopWebsite.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class Order
    {
        private DatabaseTier.Order DB_Login = new DatabaseTier.Order();

        private int ID { get; set; }
        private Dictionary<Product, int> Products { get; set; }
        private DateTime Date { get; set; }
        private PaymentType Type { get; set; }
        private String IBAN { get; set; }
        private Double Price { get; set; }       

        public Order(int id, Dictionary<Product, int> products,DateTime date, PaymentType type, string iban, Double price)
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

        public int getID()
        {
            return this.ID;
        }
        public Dictionary<Product, int> getProducts()
        {
            return this.Products;
        }
        public DateTime getDate()
        {
            return this.Date;
        }
        public PaymentType getType()
        {
            return this.Type;
        }
        public String getIBAN()
        {
            return this.IBAN;
        }
        public Double getPrice()
        {
            return this.Price;
        }

        public int insertPrintOrder(int accountID, DateTime date, string status, int productID, int photoID, string filterType, string paymentType, string productType, string iban, double price, int quantity)
        {
            return DB_Login.inserOrder(accountID, date, status, productID, photoID, filterType, paymentType, productType, iban, price, quantity);
        }
    }
}