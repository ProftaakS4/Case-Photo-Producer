using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    public class ShoppingbasketItem
    {
        public string photoID { get; set; }
        public string quantity { get; set; }
        public string type { get; set; }
        public string product { get; set; }

        public ShoppingbasketItem(string photoID, string type, string product)
        {
            this.photoID = photoID;
            //this.quantity = quantity;
            this.type = type;
            this.product = product;
        }
    }
}