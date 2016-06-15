using PhotoshopWebsite.Domain;
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
        private DatabaseTier.QueryDatabase database = new DatabaseTier.QueryDatabase();

        public int ID { get; set; }
        public List<Product> products { get; set; }

        public ProductController()
        {
            this.products = this.getAllProducts();
        }

        /// <summary>
        /// Get all the products in the database.
        /// </summary>
        /// <returns>List with products</returns>
        public List<Product> getAllProducts()
        {
            List<Product> temp = new List<Product>();
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            DataTable dt = database.CallProcedure("getAllProducts", parameters);
            // when data is found and returned
            foreach (DataRow data in dt.Rows)
            {
                temp.Add(new Product(int.Parse(data[0].ToString()), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString(), int.Parse(data[5].ToString())));
            }
            return temp;
        }

        public void updateProductStock(int productID, int stock)
        {
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_product_ID", new string[] { "int", productID.ToString() });
            parameters.Add("p_amount", new string[] { "int", stock.ToString() });
            database.CallProcedure("addStock", parameters);
        }

        /// <summary>
        /// get Product of the photographer corresponding to photographerID
        /// </summary>
        /// <param name="photographerID"></param>
        /// <returns></returns>
        public List<ProductPerPhotographer> getProductDataPerPhotographer(int photographerID)
        {
            List<ProductPerPhotographer> temp = new List<ProductPerPhotographer>();
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_user_ID", new string[] { "int", photographerID.ToString() });
            DataTable dt = database.CallProcedure("getProductAvailability", parameters);
            // when userdata is found and returned
            foreach (DataRow data in dt.Rows)
            {
                temp.Add(new ProductPerPhotographer(int.Parse(data[0].ToString()), int.Parse(data[1].ToString()), int.Parse(data[2].ToString()), int.Parse(data[3].ToString())));
            }
            return temp;
        }

        public void updateProductsPerPhotographer(List<ProductPerPhotographer> productPerPhotographer)
        {
            foreach (ProductPerPhotographer p in productPerPhotographer)
            {
                int available = 0;
                if (p.Available)
                {
                    available = 1;
                }
                Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
                parameters.Add("p_Photographer_ID", new string[] { "int", p.Photographer_ID.ToString() });
                parameters.Add("p_Product_ID", new string[] { "int", p.Product_ID.ToString() });
                parameters.Add("p_Price", new string[] { "int", p.Price.ToString() });
                parameters.Add("p_available", new string[] { "int", available.ToString() });
                database.CallProcedure("updateProductsPerPhotographer", parameters);
            }
        }
    }
}