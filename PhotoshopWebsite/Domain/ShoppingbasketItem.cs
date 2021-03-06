﻿using PhotoshopWebsite.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    /// <summary>
    /// ShoppingbasketItem class is used the keep track of items that want to get purchased
    /// </summary>
    public class ShoppingbasketItem
    {
        public int PhotoID { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public FilterTypes.FTypes Filter { get; set; }
        public ProductTypes.PTypes Product { get; set; }
        public double Price { get; set; }
        public string Boundaries { get; set; }
        public string cropValues { get; set; }

        /// <summary>
        /// create a ShoppingbasketItem with all possible variables
        /// </summary>
        /// <param name="photoID">photo id of the photo</param>
        /// <param name="description">description of the product</param>
        /// <param name="filter">filter type of the filter</param>
        /// <param name="product">product type of the product</param>
        /// <param name="price">price of the product from the photographer of the photo</param>
        public ShoppingbasketItem(int photoID, string description, FilterTypes.FTypes filter, ProductTypes.PTypes product, double price)
        {
            this.PhotoID = photoID;
            this.Description = description;
            this.Quantity = 1;
            this.Filter = filter;
            this.Product = product;
            this.Price = price;
            this.Boundaries = string.Empty;
            this.cropValues = string.Empty;
        }

        /// <summary>
        /// sets the boundaries of the ShoppingbasketItem
        /// </summary>
        /// <param name="boundaries">boundaries for the shoppingbasketitem</param>
        public void setBoundaries(string boundaries)
        {
            this.Boundaries = boundaries;
        }

        /// <summary>
        /// set the crop values of the foto
        /// </summary>
        /// <param name="cropValues">the new cropvalues</param>
        public void setCropValues(string cropValues)
        {
            this.cropValues = cropValues;
        }
            
        /// <summary>
        /// get the cropvalues
        /// </summary>
        /// <returns>the cropvalues</returns>
        public string GetCropValues()
        {
            return this.cropValues;
        }

        public override string ToString()
        {
            return "PhotoID: " + this.PhotoID + ", " + this.Description + ", quantity: " + this.Quantity + ", filter: " + this.Filter.ToString() + ", product: " + this.Product.ToString() + ", price per item: " + this.Price;
        }
    }
}