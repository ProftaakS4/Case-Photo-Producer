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
        public List<Product> products { get; set; }
        
        public ProductController()
        {
            this.products = this.getAllProducts();
        }
        
        public ProductController(int accountID)
        {
            this.ID = accountID;
        }

        /// <summary>
        /// get loginCodedata of the photographer corresponding to photographerID
        /// </summary>
        /// <param name="photographerID"></param>
        /// <returns></returns>
        public List<Product> getAllProducts()
        {
            List<Product> temp = new List<Product>();
            DataTable dt = DB.getAllProducts();
            // when data is found and returned
            if (dt.Rows.Count != 0)
            {
                DataRow[] datarowcategorie = dt.Select("ID=ID");
                foreach (DataRow data in datarowcategorie)
                {
                    temp.Add(new Product(int.Parse(data[0].ToString()), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString(), int.Parse(data[5].ToString())));
                }
            }
            return temp;
        }

        public void updateProductStock(int productID, int stock)
        {
            DB.updateProductStock(productID, stock);
        }
    }
}