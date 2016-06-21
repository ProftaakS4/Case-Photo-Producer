using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    /// <summary>
    /// OrderInfo class is created for the product info of the orders
    /// </summary>
    public class OrderInfo
    {        
        public int ID { get; set; }
        public string Description { get; set; }
        public string Filter { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }

        /// <summary>
        /// Create an orderInfo with all possible variables
        /// </summary>
        /// <param name="id">the id of the  photo</param>
        /// <param name="description">the description of the  photo</param>
        /// <param name="filter">the filter of the foto</param>
        /// <param name="type">the type of the product</param>
        /// <param name="amount">the amount of the photo</param>
        public OrderInfo(int id, string description, string filter, string type, int amount)
        {
            this.ID = id;
            this.Description = description;
            this.Filter = filter;
            this.Type = type;
            this.Amount = amount;
        }

        public OrderInfo()
        {

        }
    }
}