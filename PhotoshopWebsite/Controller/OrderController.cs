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
        private DatabaseTier.QueryDatabase database = new DatabaseTier.QueryDatabase();

        public int ID { get; set; }
        public List<Order> orders { get; set; }
        public List<OrderInfo> orderInfos { get; set; }

        public OrderController(int accountID)
        {
            this.orders = this.GetAllOrders(accountID);
        }

        public OrderController(int accountID, int orderID)
        {
            this.orderInfos = this.GetAllOrderInfos(accountID, orderID);
        }

        public List<Order> GetAllOrders(int accountID)
        {
            List<Order> temp = new List<Order>();
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_account_ID", new string[] { "int", accountID.ToString() });
            DataTable dt = this.database.CallProcedure("getAllPaymentsByAccount", parameters);
            // when data is found and returned
            foreach (DataRow data in dt.Rows)
            {
                temp.Add(new Order(int.Parse(data[0].ToString()), DateTime.Parse(data[1].ToString()), (PaymentType)Enum.Parse(typeof(PaymentType), data[2].ToString(), true), data[3].ToString(), double.Parse(data[4].ToString())));
            }
            return temp;
        }

        public List<OrderInfo> GetAllOrderInfos(int accountID, int orderID)
        {
            List<OrderInfo> temp = new List<OrderInfo>();
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_account_ID", new string[] { "int", accountID.ToString() });
            parameters.Add("p_order_ID", new string[] { "int", orderID.ToString() });
            DataTable dt = this.database.CallProcedure("getAllOrderInfos", parameters);
            // when data is found and returned
            foreach (DataRow data in dt.Rows)
            {
                temp.Add(new OrderInfo(int.Parse(data[0].ToString()), data[1].ToString(), data[2].ToString(), data[3].ToString(), int.Parse(data[4].ToString())));
            }
            return temp;
        }
    }
}