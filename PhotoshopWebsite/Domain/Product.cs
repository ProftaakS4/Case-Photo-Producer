using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    /// <summary>
    /// Product class is created so that products can be used in the application
    /// </summary>
    [Serializable]
    public class Product
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Material { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }

        /// <summary>
        /// create a Product with all possible variables
        /// </summary>
        /// <param name="ID">id of the product</param>
        /// <param name="Type">the product type</param>
        /// <param name="Material">product material it's made</param>
        /// <param name="Description">a description of the product</param>
        /// <param name="Image">the imagepath of the product</param>
        /// <param name="Stock">the amount of items are left in stock for this product</param>
        public Product(int ID, string Type, string Material, string Description, string Image, int Stock)
        {
            this.ID = ID;
            this.Type = Type;
            this.Material = Material;
            this.Description = Description;
            this.Image = Image;
            this.Stock = Stock;
        }
        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Product p = obj as Product;
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this.ID == p.ID;
        }
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}