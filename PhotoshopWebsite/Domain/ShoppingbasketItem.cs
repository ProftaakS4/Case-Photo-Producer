using PhotoshopWebsite.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    public class ShoppingbasketItem
    {
        public int photoID { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public FilterTypes.FTypes filterType { get; set; }
        public ProductTypes.PTypes product { get; set; }

        public ShoppingbasketItem(int photoID, string description, FilterTypes.FTypes filterType, ProductTypes.PTypes product)
        {
            this.photoID = photoID;
            this.description = description;
            this.quantity = 1;
            this.filterType = filterType;
            this.product = product;
        }
    }
}