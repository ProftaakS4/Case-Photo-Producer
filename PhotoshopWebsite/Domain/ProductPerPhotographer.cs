using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    [Serializable]
    public class ProductPerPhotographer
    {
        public int Photographer_ID { get; set; }
        public int Product_ID { get; set; }
        public int Price { get; set; }
        public int Available { get; set; }


        public ProductPerPhotographer(int photographer_ID, int product_ID, int price, int available)
        {
            this.Photographer_ID = photographer_ID;
            this.Product_ID = product_ID;
            this.Price = price;
            this.Available = available;
        }
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            ProductPerPhotographer p = obj as ProductPerPhotographer;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.Product_ID == p.Product_ID);
        }
        public override int GetHashCode()
        {
            return this.Product_ID.GetHashCode();
        }
    }
}