using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class Purchase
    {
        public int ID { get; set; }
        public int accountID { get; set; }
        public DateTime Date { get; set; }
        public String PaidStatus { get; set; }
        public String ShippingStatus { get; set; }

        public Purchase(int id, int accountID, DateTime date, string paidStatus, string shippingStatus)
        {
            this.ID = id;
            this.accountID = accountID;
            this.Date = date;
            this.PaidStatus = paidStatus;
            this.ShippingStatus = shippingStatus;
        }
    }
}