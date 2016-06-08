using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    /// <summary>
    /// Purchase class created when a payment is placed on a order on the website.
    /// </summary>
    public class Purchase
    {
        public int ID { get; set; }
        public int accountID { get; set; }
        public DateTime Date { get; set; }
        public string PaidStatus { get; set; }
        public string ShippingStatus { get; set; }

        /// <summary>
        /// create a purchase with all possible variables
        /// </summary>
        /// <param name="id">id of the purchase</param>
        /// <param name="accountID">id of the user that purchased</param>
        /// <param name="date">time and day of purchase</param>
        /// <param name="paidStatus">true ones payed</param>
        /// <param name="shippingStatus">false</param>
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