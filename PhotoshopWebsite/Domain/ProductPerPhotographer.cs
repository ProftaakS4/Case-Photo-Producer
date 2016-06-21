using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    /// <summary>
    /// ProductPerPhotographer class is create to keep track of which products a photographer sells.
    /// </summary>
    [Serializable]
    public class ProductPerPhotographer
    {
        public int Photographer_ID { get; set; }
        public int Product_ID { get; set; }
        public int Price { get; set; }
        public bool Available { get; set; }

        /// <summary>
        /// create a ProductPerPhotographer with all possible variables
        /// </summary>
        /// <param name="photographer_ID">id of the photographer</param>
        /// <param name="product_ID">id of the product</param>
        /// <param name="price">the price of the product set by the photographer</param>
        /// <param name="available">availabilitie of the product</param>
        public ProductPerPhotographer(int photographer_ID, int product_ID, int price, int available)
        {
            this.Photographer_ID = photographer_ID;
            this.Product_ID = product_ID;
            this.Price = price;
            this.Available = available == 1;
        }
        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            ProductPerPhotographer p = obj as ProductPerPhotographer;
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this.Product_ID == p.Product_ID;
        }
        public override int GetHashCode()
        {
            return this.Product_ID.GetHashCode();
        }
    }
}