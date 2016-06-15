using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoshopWebsite.Domain;
using System.Data;
using PhotoshopWebsite.Enumeration;

namespace PhotoshopWebsite.Controller
{
    public class OrderController
    {
        private DatabaseTier.Order DB = new DatabaseTier.Order();

        public int ID { get; set; }
        public List<Order> orders { get; set; }
        
        public OrderController(int accountID)
        {
            this.orders = this.getAllOrders(accountID);
        }

        public List<Order>  getAllOrders(int accountID) {
            
            List<Order> temp = new List<Order>();
            DataTable dt = DB.getAllOrders(accountID);
            // when data is found and returned
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    temp.Add(new Order(int.Parse(data[0].ToString()), DateTime.Parse(data[1].ToString()), (PaymentType)Enum.Parse(typeof(PaymentType), data[2].ToString(), true), data[3].ToString(), double.Parse(data[4].ToString())));
                }
            }
            return temp;
        }
    }
}