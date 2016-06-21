using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class PurchaseController
    {
        private DatabaseTier.QueryDatabase database = new DatabaseTier.QueryDatabase();

        public int ID { get; set; }
        public List<Purchase> purchases { get; set; }
        
        public PurchaseController()
        {
            this.purchases = this.getAllPurchases();
        }
        
        public PurchaseController(int accountID)
        {
            this.ID = accountID;
            //this.purchases = this.getPurchases(ID);
        }

        /// <summary>
        /// get loginCodedata of the photographer corresponding to photographerID
        /// </summary>
        /// <param name="photographerID"></param>
        /// <returns></returns>
        public List<Purchase> getAllPurchases()
        {
            List<Purchase> temp = new List<Purchase>();
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            DataTable dt = database.CallProcedure("getAllPurchases", parameters);
            // when data is found and returned
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    temp.Add(new Purchase(int.Parse(data[0].ToString()), int.Parse(data[1].ToString()), DateTime.Parse(data[2].ToString()), data[3].ToString(), data[4].ToString()));
                }
            }
            return temp;
        }

        public void updatePurchaseStatus(int purchaseID, string paidStatus, string shippingStatus)
        {
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_purchase_ID", new string[] { "int", purchaseID.ToString() });
            parameters.Add("p_newPaidStatus", new string[] { "string", paidStatus.ToString() });
            parameters.Add("p_newShippingStatus", new string[] { "string", shippingStatus.ToString() });
            database.CallProcedure("changeOrderStatus", parameters);
        }

        public int getPrice(int product_ID, int photo_ID)
        {
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_Product_ID", new string[] { "int", product_ID.ToString() });
            parameters.Add("p_Photo_ID", new string[] { "string", photo_ID.ToString() });
            DataTable dt = database.CallProcedure("getPrice", parameters);
            if (dt.Rows.Count != 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            return -1;
        }
    }
}