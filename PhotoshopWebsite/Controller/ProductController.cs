﻿using PhotoshopWebsite.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class ProductController
    {
        private DatabaseTier.Product DB = new DatabaseTier.Product();

        public int ID { get; set; }
        public List<Product> Products { get; set; }
        public ProductController(int photographerID)
        {
            this.ID = photographerID;
            this.Products = this.getProductData(ID);
        }

        /// <summary>
        /// get Product of the photographer corresponding to photographerID
        /// </summary>
        /// <param name="photographerID"></param>
        /// <returns></returns>
        public List<Product> getProductData(int photographerID)
        {
            List<Product> temp = new List<Product>();
            DataTable dt = DB.getProductData(photographerID);
            // when userdata is found and returned
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    temp.Add(new Product(int.Parse(data[0].ToString()), data[1].ToString(), data[2].ToString(),data[3].ToString(),data[4].ToString(),int.Parse(data[5].ToString())));
                }
            }
            return temp;
        }

        /// <summary>
        /// get Product of the photographer corresponding to photographerID
        /// </summary>
        /// <param name="photographerID"></param>
        /// <returns></returns>
        public List<ProductPerPhotographer> getProductDataPerPhotographer(int photographerID)
        {
            List<ProductPerPhotographer> temp = new List<ProductPerPhotographer>();
            DataTable dt = DB.getProductData(photographerID);
            // when userdata is found and returned
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    temp.Add(new ProductPerPhotographer(int.Parse(data[0].ToString()),int.Parse(data[1].ToString()),int.Parse(data[2].ToString()),int.Parse(data[3].ToString())));
                }
            }
            return temp;
        }
    }
}