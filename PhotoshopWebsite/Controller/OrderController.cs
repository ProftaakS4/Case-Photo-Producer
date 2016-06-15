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
        public List<OrderInfo> orderInfos { get; set; }
        
        public OrderController(int accountID)
        {
            this.orders = this.getAllOrders(accountID);
        }

        public OrderController(int accountID, int orderID)
        {
            this.orderInfos = this.getAllOrderInfos(accountID, orderID);
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

        public List<OrderInfo> getAllOrderInfos(int accountID, int orderID)
        {

            List<OrderInfo> temp = new List<OrderInfo>();
            DataTable dt = DB.getAllOrderInfos(accountID, orderID);
            // when data is found and returned
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    temp.Add(new OrderInfo(int.Parse(data[0].ToString()), data[1].ToString(), data[2].ToString(), data[3].ToString(), int.Parse(data[4].ToString())));
                }
            }
            return temp;
        }
    }
}