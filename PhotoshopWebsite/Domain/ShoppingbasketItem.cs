using PhotoshopWebsite.DatabaseTier;
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
        public string filterType { get; set; }
        public ProductTypes.ETypes product { get; set; }

        public ShoppingbasketItem(int photoID, string description, string type, ProductTypes.ETypes product)
        {
            this.photoID = photoID;
            this.description = description;
            this.quantity = 1;
            this.filterType = type;
            this.product = product;
        }
    }
}